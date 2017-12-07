#region

using System.Xml;

#endregion

public class SpeakRequestEvent : Event
{
    public SpeakRequestEvent(XmlNode node)
    {
        Node = node;
    }

    public SpeakRequestEvent(string utterance)
    {
        Utterance = utterance;
    }

    public string Utterance { get; private set; }
    public XmlNode Node { get; private set; }
}