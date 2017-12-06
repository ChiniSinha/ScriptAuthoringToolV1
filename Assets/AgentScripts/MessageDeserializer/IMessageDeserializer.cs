#region

using System.Xml;

#endregion

public interface IMessageDeserializer
{
    BaseCommand DeserializeActionXml(XmlNode node);
}