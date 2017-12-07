#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

#endregion

[RequireComponent(typeof(AudioSource))]
public class WebTTS : TTSController
{
    //protected readonly SortedList<float, RagEvent> EventQueue = new SortedList<float, RagEvent>();
    protected readonly SortedList<float, BaseCommand> CommandQueue = new SortedList<float, BaseCommand>();
    protected WWW _audioLoader;

    [SerializeField] protected AudioSource _audioSource;

    //TA: Fix this when Unity 5.5 comes out
    protected float _audioStartTime;

    protected bool _loading;
    private int _playbackDuration;

    public override void InitTts()
	{
		Debug.Log("Starting up Web TTS");
        _audioSource = Agent.AudioSource;
    }

    public override void SpeakText(string text)
    {
        if (Globals.HasRegistered<ExternalTTSServer>())
        {
            Globals.Get<ExternalTTSServer>().RequestUtterance(text);
        }
        else
        {
            Debug.Log("WebTTS was asked to speak string without an utterance generator");
        }
    }

    public override void SpeakBlock(XmlNode syncBlock)
    {
        _audioSource.Stop();
        _audioSource.clip = null;

        _playbackDuration = int.Parse(syncBlock.SafeGetAttribute("duration"));
        CommandQueue.Capacity = syncBlock.ChildNodes.Count;

        XmlNodeList visemes = syncBlock.SelectNodes("viseme");
        if (visemes != null)
        {
            XmlNode thisViseme;
            int visemePtr = 0;
            while (visemePtr < visemes.Count)
            {
                thisViseme = visemes[visemePtr];
                int visemeId = int.Parse(thisViseme.SafeGetAttribute("value"));
                int thisTimestamp = int.Parse(thisViseme.SafeGetAttribute("timestamp"));

                int nextTimestamp = thisTimestamp;
                int nextViseme = visemeId;
                while (visemeId == nextViseme && visemes.Count > visemePtr + 1)
                {
                    visemePtr++;
                    nextViseme = int.Parse(visemes[visemePtr].SafeGetAttribute("value"));
                    nextTimestamp = int.Parse(visemes[visemePtr].SafeGetAttribute("timestamp"));
                }
                if (visemePtr >= visemes.Count - 1)
                {
                    visemePtr++;
                    nextTimestamp = _playbackDuration;
                }

                float timestamp = thisTimestamp;
                while (CommandQueue.ContainsKey(timestamp))
                {
                    timestamp += 0.000001f;
                }

                
                switch (Globals.Config.Tts.VisemeMapping)
                {
                    case Config.VisemeMapping.SAPI:
                        visemeId = SapiVisemeMap.MapViseme(visemeId);
                        break;
                    case Config.VisemeMapping.CRAPI:
                        visemeId = CrapiVisemeMap.MapViseme(visemeId);
                        break;
                }
                

                //EventQueue.Add(timestamp, new AgentPlayVisemeEvent(visemeId, nextTimestamp - thisTimestamp));
                CommandQueue.Add(timestamp, new PlayVisemeCommand(visemeId, nextTimestamp - thisTimestamp));
            }
        }

        XmlNodeList gestures = syncBlock.SelectNodes("gesture");
        if (gestures != null)
        {
            for (int i = 0; i < gestures.Count; i++)
            {
                string gesture = gestures[i].SafeGetAttribute("cmd");
                Side hand = gestures[i].SafeGetAttribute("hand") == "L" ? Side.LEFT : Side.RIGHT;
                int duration;
                if (!int.TryParse(gestures[i].SafeGetAttribute("duration"), out duration))
                {
                    duration = 0;
                }

                float timestamp = float.Parse(gestures[i].SafeGetAttribute("timestamp"));
                while (CommandQueue.ContainsKey(timestamp))
                {
                    timestamp = timestamp - 0.001f;
                }

                if (gesture.Equals("beat", StringComparison.CurrentCultureIgnoreCase))
                {
                    CommandQueue.Add(timestamp, new BeatCommand(hand));
                }
                else
                {
                    CommandQueue.Add(timestamp, new GestureCommand(hand, gesture, duration));
                }
            }
        }

        XmlNodeList gazes = syncBlock.SelectNodes("gaze");
        if (gazes != null)
        {
            for (int i = 0; i < gazes.Count; i++)
            {
                float timestamp = float.Parse(gazes[i].SafeGetAttribute("timestamp"));
                while (CommandQueue.ContainsKey(timestamp))
                {
                    timestamp = timestamp - 0.001f;
                }

                CommandQueue.Add(timestamp, new GazeCommand(gazes[i].SafeGetAttribute("dir")));
            }
        }

        XmlNodeList brows = syncBlock.SelectNodes("brows");
        if (brows != null)
        {
            for (int i = 0; i < brows.Count; i++)
            {
                float timestamp = float.Parse(brows[i].SafeGetAttribute("timestamp"));
                while (CommandQueue.ContainsKey(timestamp))
                {
                    timestamp = timestamp - 0.001f;
                }

                FaceAnimation.Type browType = brows[i].SafeGetAttribute("value") == "POINTUP"
                    ? FaceAnimation.Type.BROWS_UP
                    : FaceAnimation.Type.BROWS_DOWN;
                CommandQueue.Add(timestamp, new SetExpressionCommand(browType));
            }
        }

        IsSpeaking = true;
        string audioUrl = syncBlock.SafeGetAttribute("audioURL");
        StartCoroutine(LoadAndPlayAudio(audioUrl));
    }

    protected IEnumerator LoadAndPlayAudio(string sourceUrl)
    {
        _loading = true;
#if UNITY_EDITOR || UNITY_STANDALONE_OSX
        // TA: Hacks
        sourceUrl = sourceUrl.Replace("mp3", "wav");
#endif

        _audioLoader = Globals.Get<ResourceLoader>().GetAudioLoader(sourceUrl);
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
        _loading = false;
    }

    public void Update()
    {
        if (!IsSpeaking || _loading)
        {
            return;
        }

        int msSinceAudioStart = Mathf.FloorToInt((Time.time - _audioStartTime)*1000);

        while (CommandQueue.Count > 0 && CommandQueue.Keys[0] < msSinceAudioStart)
        {
            BaseCommand command = CommandQueue[CommandQueue.Keys[0]];
            CommandQueue.RemoveAt(0);

            //Globals.EventBus.Dispatch(e, e.GetType());
            Globals.CommandQueue.EnqueueThreadSafe(command);
            //command.Execute();
        }

        if (CommandQueue.Count == 0)
        {
            //Globals.EventBus.Dispatch(new AgentPlayVisemeEvent(0, 0));
            Globals.CommandQueue.EnqueueThreadSafe(new PlayVisemeCommand(0, 0));
            //BaseCommand command = new PlayVisemeCommand(0, 0);
            //command.Execute();
            OnSpeakComplete();
        }
    }
}