using System.Xml;

public class XmlMessageReceived : Event
{
    public XmlMessageReceived(XmlDocument fullMessage)
    {
        Message = fullMessage;
    }

    public XmlDocument Message { get; protected set; }
}