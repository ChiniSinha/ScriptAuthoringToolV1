#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using AOT;
using System.IO;
using UnityEngine;

#endregion

[RequireComponent(typeof(AudioSource))]
public class CereprocTts : TTSController, ICereprocTTS
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void TtsEventCallback(string caller, string eventString);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void TtsErrorCallback(string caller, string error);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void FileReadyCallback(string caller, string filename);

    //protected readonly List<RagEvent> Bookmarks = new List<RagEvent>();
    protected readonly List<BaseCommand> Bookmarks = new List<BaseCommand>();

    //protected readonly SortedList<float, RagEvent> EventQueue = new SortedList<float, RagEvent>();
    protected readonly SortedList<float, BaseCommand> CommandQueue = new SortedList<float, BaseCommand>();
    protected WWW _audioLoader;

    private static readonly Dictionary<string, CereprocTts> AllCereprocSpeakers = new Dictionary<string, CereprocTts>();

    [SerializeField] protected AudioSource _audioSource;

    private string[] _eventChunks = new string[4];

    private float _audioStartTime;

    private int _lastViseme;
    private float _lastVisemeStart;
    private bool _loading;

    private string _audioFilename;
    private bool _isAudioFileReady;

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_WEBGL)
    [DllImport("__Internal")]
#else
    [DllImport("cereproc_plugin")]
#endif
    internal static extern void SetListenerObject(string listenerName);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_WEBGL)
    [DllImport("__Internal")]
#else
    [DllImport("cereproc_plugin")]
#endif
    internal static extern void SetCallbacks(TtsEventCallback eventCb, FileReadyCallback audioCb,
        TtsErrorCallback errorCb);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_WEBGL)
    [DllImport("__Internal")]
#else
    [DllImport("cereproc_plugin")]
#endif
    internal static extern void LoadTTS(string resourcePath, string cachePath, string voice);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_WEBGL)
    [DllImport("__Internal")]
#else
    [DllImport("cereproc_plugin")]
#endif
    internal static extern void SpeakSSMLBlock(string ssmlText);

#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_WEBGL)
    [DllImport("__Internal")]
#else
    [DllImport("cereproc_plugin")]
#endif
    internal static extern void CleanupTTS();


    [MonoPInvokeCallback(typeof(TtsEventCallback))]
    private static void CereprocEvent(string caller, string dataString)
    {
        AllCereprocSpeakers[caller].OnTtsEvent(dataString);
    }

    [MonoPInvokeCallback(typeof(TtsErrorCallback))]
    private static void CereprocError(string caller, string error)
    {
        AllCereprocSpeakers[caller].OnTtsError(error);
    }

    [MonoPInvokeCallback(typeof(FileReadyCallback))]
    private static void CereprocFileReady(string caller, string filename)
    {
        AllCereprocSpeakers[caller].OnAudioGenerationComplete(filename);
    }

    protected void Update()
    {
        if (_isAudioFileReady)
        {
            _isAudioFileReady = false;
            StartCoroutine(LoadAndPlayAudio(_audioFilename));
            return;
        }

        if (!IsSpeaking || _loading)
        {
            return;
        }

        int msSinceAudioStart = Mathf.FloorToInt((Time.time - _audioStartTime)*1000);

        while (CommandQueue.Count > 0 && CommandQueue.Keys[0] < msSinceAudioStart)
        {
            //RagEvent e = EventQueue[EventQueue.Keys[0]];
            BaseCommand command = CommandQueue[CommandQueue.Keys[0]];
            CommandQueue.RemoveAt(0);

            //Globals.EventBus.Dispatch(e, e.GetType());
            Globals.CommandQueue.EnqueueThreadSafe(command);
        }

        if (CommandQueue.Count == 0)
        {
            //Globals.EventBus.Dispatch(new AgentPlayVisemeEvent(0, 0));
            Globals.CommandQueue.EnqueueThreadSafe(new PlayVisemeCommand(0, 0));
            OnSpeakComplete();
        }
    }

    private void OnDestroy()
    {
        CleanupTTS();
    }

    public void OnTtsEvent(string dataString)
    {
        _eventChunks = dataString.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
        float startTime = float.Parse(_eventChunks[2])*1000;
        float endTime = float.Parse(_eventChunks[3])*1000;

        switch (_eventChunks[0])
        {
            case "viseme":
                int viseme = int.Parse(_eventChunks[1]);
                if (_lastViseme != viseme)
                {
                    //EventQueue.Add(_lastVisemeStart,
                    //    new AgentPlayVisemeEvent(viseme, Mathf.RoundToInt(endTime - _lastVisemeStart)));
                    CommandQueue.Add(_lastVisemeStart, new PlayVisemeCommand(viseme, Mathf.RoundToInt(endTime - _lastVisemeStart)));
                    _lastVisemeStart = endTime;
                }
                _lastViseme = viseme;
                break;

            case "marker":
                int eventIdx;
                bool isNumericMarker = int.TryParse(_eventChunks[1], out eventIdx);
                if (isNumericMarker)
                {
                    CommandQueue.Add(startTime, Bookmarks[eventIdx]);
                }
                break;
        }
    }

    public void OnTtsError(string error)
    {
        Debug.LogError(error);
    }

    public void OnAudioGenerationComplete(string filename)
    {
        _isAudioFileReady = true;
        _audioFilename = filename;
    }


    protected WWW GetFileLoader(string uri)
    {
        if (uri.Contains("://"))
        {
            return new WWW(uri);
        }
        if (Path.IsPathRooted(uri))
        {
            return new WWW("file://" + uri);
        }
        return new WWW("file://" + Path.Combine(Application.streamingAssetsPath, uri));
    }

    protected IEnumerator LoadAndPlayAudio(string sourceUrl)
    {
        _audioLoader = GetFileLoader(sourceUrl);
        yield return _audioLoader;

        if (_audioLoader.GetAudioClip() == null)
        {
            throw new Exception("Audio file not found: " + sourceUrl);
        }
        _audioSource.clip = _audioLoader.GetAudioClip(false, false);
        while (_audioSource.clip.loadState != AudioDataLoadState.Loaded)
        {
            if (_audioSource.clip.loadState == AudioDataLoadState.Failed)
            {
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        _audioSource.Play();
        _audioStartTime = Time.time;
        _lastVisemeStart = 0;
        _lastViseme = 0;
        _loading = false;
    }


    public override void InitTts()
    {
        Debug.Log("Starting up Cereproc TTS");
        /*string voiceFile = Globals.Config.Agent.VoiceFile;
        if (string.IsNullOrEmpty(voiceFile))
        {
            voiceFile = "heather";
        }*/
        string voiceFile = "heather";
        string voicePath;
        if (Application.isEditor)
        {
            voicePath = Application.dataPath + "/" + "CerevoiceFiles" + "/";
        }
        else
        {
            voicePath = Application.streamingAssetsPath + "/";
        }

        AllCereprocSpeakers[gameObject.name] = this;
        _audioSource = Agent.AudioSource;

        SetListenerObject(gameObject.name);
        SetCallbacks(CereprocEvent, CereprocFileReady, CereprocError);

        LoadTTS(voicePath, Application.temporaryCachePath + "/", voiceFile);
    }

    public override void SpeakText(string speech)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<speech>" + speech + "</speech>");
        SpeakBlock(doc.FirstChild);
    }

    public override void SpeakBlock(XmlNode inputNode)
    {
        CommandQueue.Clear();
        Bookmarks.Clear();
        string input = inputNode.InnerXml;
        StringBuilder builder = new StringBuilder("<speak>", input.Length);
        for (int i = 0; i < inputNode.ChildNodes.Count; i++)
        {
            XmlNode node = inputNode.ChildNodes[i];

            if (node.NodeType == XmlNodeType.Text) // Speakable text
            {
                builder.Append(node.InnerText);
            }
            else if (node.Name.Contains("INT_") || node.Name.Equals("phoneme") || node.Name.Equals("sub") ||
                     node.Name.Equals("voice") ||
                     node.Name.Equals("emphasis") || node.Name.Equals("break") || node.Name.Equals("prosody"))
                // SSML tags
            {
                builder.Append(node.OuterXml);
            }
            else // bookmark commands
            {
                builder.Append("<mark mark=\"" + Bookmarks.Count + "\"/>");
                if (node.Name.Equals("gaze"))
                {
                    //Bookmarks.Add(new AgentChangeGazeEvent(node.AttributeCaseInsensitive("dir")));
                    Bookmarks.Add(new GazeCommand(node.AttributeCaseInsensitive("dir")));
                }
                else if (node.Name.Equals("gesture"))
                {
                    //Side hand = node.AttributeCaseInsensitive("hand")
                    //    .Equals("L", StringComparison.CurrentCultureIgnoreCase)
                    //    ? Side.LEFT
                    //    : Side.RIGHT;
                    //Bookmarks.Add(new AgentPerformGestureEvent(hand, node.AttributeCaseInsensitive("cmd")));
                    Bookmarks.Add(new GestureCommand(node.AttributeCaseInsensitive("hand"), node.AttributeCaseInsensitive("cmd")));
                }
                else if (node.Name.Equals("brows"))
                {
                    FaceAnimation.Type browType = node.SafeGetAttribute("value") == "POINTUP"
                        ? FaceAnimation.Type.BROWS_UP
                        : FaceAnimation.Type.BROWS_DOWN;
                    //Bookmarks.Add(new AgentUpdateExpressionEvent(browType));
                    Bookmarks.Add(new SetExpressionCommand(browType));
                }
            }
        }

        IsSpeaking = true;
        _loading = true;
        builder.Append("</speak>");
        string ssmlText = builder.ToString();
        Debug.Log("Cereproc about to speak: " + ssmlText);
        SpeakSSMLBlock(ssmlText);
    }
}