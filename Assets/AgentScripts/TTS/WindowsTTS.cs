using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEngine;
using AOT;

public class WindowsTTS : TTSController
{ 
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void bookmarkCallback(string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public delegate void phonemeCallback(string message);

    //TTS Threading Variables
    private static readonly EventWaitHandle SpeechWaitHandler = new AutoResetEvent(false);
    private static bool _speechThreadActive = true;
    private static string _speechString = "";
    protected static bool _endSpeechFlag;

    //public static readonly List<RagEvent> Events = new List<RagEvent>();
    protected static readonly List<BaseCommand> Bookmarks = new List<BaseCommand>();

    private Thread _speechThread;

    [DllImport("UnityTTS")]
    private static extern int init(phonemeCallback pCallback, bookmarkCallback bCallback);

    // SEE: http://www.mono-project.com/docs/advanced/pinvoke/#marshaling
    [DllImport("UnityTTS")]
    private static extern int speakText(byte[] input);

    [DllImport("UnityTTS")]
    private static extern int closeTTS();

    [DllImport("UnityTTS")]
    private static extern int endSpeak();

    public override void InitTts()
    {
        _speechThread = new Thread(ttsHandler);
        _speechThread.Start();
    }
    
    public override void SpeakText(string input)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml("<speech>" + input + "</speech>");
        SpeakBlock(doc.FirstChild);
    }

    public override void SpeakBlock(XmlNode inputNode)
    {
        _endSpeechFlag = false;
        Bookmarks.Clear();
        string input = inputNode.InnerXml;
        if (input.Contains("INT_"))
        {
            TranslateSpeechBlockWithIntTags(inputNode);
            input = inputNode.InnerXml;
        }
        StringBuilder builder = new StringBuilder("<speak>", input.Length);
        for (int i = 0; i < inputNode.ChildNodes.Count; i++)
        {
            XmlNode node = inputNode.ChildNodes[i];

            if (node.NodeType == XmlNodeType.Text) // Speakable text
            {
                builder.Append(node.InnerText);
            }
            else if (node.Name.Equals("phoneme") || node.Name.Equals("sub") || node.Name.Equals("voice") ||
                node.Name.Equals("emphasis") || node.Name.Equals("break") || node.Name.Equals("prosody")) // SSML tags
            {
                builder.Append(node.OuterXml);
            }
            else // bookmark commands
            {
                builder.Append("<bookmark mark=\"" + Bookmarks.Count + "\"/>");
                if (node.Name.Equals("gaze"))
                {
                    //Events.Add(new AgentChangeGazeEvent(node.AttributeCaseInsensitive("dir")));
                    Bookmarks.Add(new GazeCommand(node.AttributeCaseInsensitive("dir")));
                }
                else if (node.Name.Equals("delay") || node.Name.Equals("animationdelay"))
                {
                    //Events.Add(new AgentPauseAnimationEvent(node.AttributeCaseInsensitive("ms").ParseInt()));
                    Bookmarks.Add(new AnimationDelayCommand(node.AttributeCaseInsensitive("ms").ParseInt()));
                }
                else if (node.Name.Equals("gesture"))
                {
                    Side hand = node.AttributeCaseInsensitive("hand")
                        .Equals("L", StringComparison.CurrentCultureIgnoreCase)
                        ? Side.LEFT
                        : Side.RIGHT;
                    string command = node.AttributeCaseInsensitive("cmd");
                    if (command.Equals("beat", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Bookmarks.Add(new BeatCommand(hand));
                    }
                    else
                    {
                        Bookmarks.Add(new GestureCommand(hand, command));
                    }
                }
                else if (node.Name.Equals("brows", StringComparison.CurrentCultureIgnoreCase) ||
                         node.Name.Equals("eyebrows", StringComparison.CurrentCultureIgnoreCase))
                {
                    FaceAnimation.Type browType = node.SafeGetAttribute("value") == "POINTUP" ||
                                                  node.SafeGetAttribute("dir") == "UP"
                        ? FaceAnimation.Type.BROWS_UP
                        : FaceAnimation.Type.BROWS_DOWN;
                    Bookmarks.Add(new SetExpressionCommand(browType));
                }
            }
        }

        builder.Append("</speak>");
        _speechString = builder.ToString();
        Debug.Log("About to speak:" + _speechString);
        _endSpeechFlag = false;
        IsSpeaking = true;
        SpeechWaitHandler.Set();
    }

    // TA: This is pretty hacky, but makes me wonder if we should do all speech XML processing like this
    // It could potentially reduce allocations (since we don't deal with strings)
    private void TranslateSpeechBlockWithIntTags(XmlNode inputNode)
    {
        XmlNodeList intNodes = inputNode.SelectNodes("//*[substring(name(), 0, 5) = 'INT_']");

        XmlNode node;
        XmlElement replacement = null;
        for (int i = 0; i < intNodes.Count; i++)
        {
            node = intNodes[i];
            switch (node.Name)
            {
                case "INT_SPEED":
                    replacement = inputNode.OwnerDocument.CreateElement("prosody");
                    replacement.SetAttribute("rate", node.AttributeCaseInsensitive("speed"));
                    break;
                case "INT_PITCH":
                    replacement = inputNode.OwnerDocument.CreateElement("prosody");
                    replacement.SetAttribute("pitch", node.AttributeCaseInsensitive("pitch"));
                    replacement.SetAttribute("middle", "0");
                    break;
                case "INT_VOLUME":
                    replacement = inputNode.OwnerDocument.CreateElement("prosody");
                    replacement.SetAttribute("volume", node.AttributeCaseInsensitive("vol"));
                    replacement.SetAttribute("level", "100");
                    break;
                case "INT_EMPHASIS":
                    replacement = inputNode.OwnerDocument.CreateElement("emphasis");
                    replacement.SetAttribute("level", "strong");
                    break;
                case "INT_PAUSE":
                    replacement = inputNode.OwnerDocument.CreateElement("break");
                    replacement.SetAttribute("time", node.AttributeCaseInsensitive("dur"));
                    break;
            }
            if (replacement != null)
            {
                replacement.InnerXml = node.InnerXml;
                node.ParentNode.ReplaceChild(replacement, node);
            }
            replacement = null;
        }
    }

    private void ttsHandler()
    {
        init(PrintPhoneme, PrintBookmark);
        while (_speechThreadActive)
        {
            SpeechWaitHandler.WaitOne();
            endSpeak();
            if (_speechThreadActive)
            {
                speakText(Encoding.GetEncoding("iso-8859-1").GetBytes(_speechString));
            }
        }
        closeTTS();
    }

    [MonoPInvokeCallback(typeof(phonemeCallback))]
    private static void PrintPhoneme(string phoneme)
    {
        if (phoneme != "endspeak")
        {
            string[] splitString = phoneme.Split(' ');
            string duration = splitString[0].Trim(',');
            _endSpeechFlag = false;
            //Globals.EventBus.DispatchThreadSafe(new AgentPlayVisemeEvent(int.Parse(splitString[1]), int.Parse(duration)));
            Globals.CommandQueue.EnqueueThreadSafe(new PlayVisemeCommand(int.Parse(splitString[1]), int.Parse(duration)));
        }
        else
        {
            _endSpeechFlag = true;
        }
    }


    [MonoPInvokeCallback(typeof(bookmarkCallback))]
    private static void PrintBookmark(string bookmark)
    {
        if (bookmark != "")
        {
            //Globals.EventBus.DispatchThreadSafe(Events[int.Parse(bookmark)]);
            Globals.CommandQueue.EnqueueThreadSafe(Bookmarks[int.Parse(bookmark)]);
        }
    }

    private void Update()
    {
        if (_endSpeechFlag && IsSpeaking)
        {
            OnSpeakComplete();
        }
    }

    private void OnApplicationQuit()
    {
        _speechThreadActive = false;
        SpeechWaitHandler.Set();
    }
}
