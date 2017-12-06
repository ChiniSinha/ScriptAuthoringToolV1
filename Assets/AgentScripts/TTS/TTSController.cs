#region

using System.Xml;
using UnityEngine;

#endregion

public abstract class TTSController : MonoBehaviour
{
    protected int _lastPhoneme = -1;
    public Agent Agent;
    public bool IsSpeaking { get; protected set; }

    public virtual void InitTts()
	{
		Debug.Log("Starting up empty TTS");
    }

    public virtual void SpeakBlock(XmlNode block)
	{
    }

    public virtual void SpeakText(string text)
	{
    }

    public void OnSpeakComplete()
    {
        IsSpeaking = false;
        Globals.EventBus.Dispatch(new SpeechCompleteEvent(Agent));
    }
}