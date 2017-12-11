#region

using System;
using System.Xml;

#endregion

/// <summary>
///     Deserializes messages from the server or local command authority
///     and converts them into Actions
/// </summary>
public class DefaultMessageParser
{
    protected IMessageDeserializer _deserializer;

    public DefaultMessageParser()
    {
        _deserializer = new DefaultRAG2Deserializer();
    }

    public void DeserializeMessage(XmlNode doc)
    {
        XmlNode node;
        for (int i = 0; i < doc.ChildNodes.Count; i++)
        {
            node = doc.ChildNodes[i];
            if (node.Name.Equals("input", StringComparison.CurrentCultureIgnoreCase))
            {
                ParseInputBlock(node);
            }
            else if (node.Name.Equals("perform", StringComparison.CurrentCultureIgnoreCase))
            {
                ParsePerformBlock(node);
            }
            else
            {
                BaseCommand command = _deserializer.DeserializeActionXml(node);
                if (command != null)
                {
                    Globals.CommandQueue.Enqueue(command);
                }
            }
        }
    }

    private void ParseInputBlock(XmlNode input)
    {
        CommandQueue actionQueueRef = Globals.CommandQueue;
        XmlNode node;
        for (int i = 0; i < input.ChildNodes.Count; i++)
        {
            node = input.ChildNodes[i];

            BaseCommand command = _deserializer.DeserializeActionXml(node);
            if (command != null)
            {
                actionQueueRef.Enqueue(command);
            }
        }
    }

    protected void ParsePerformBlock(XmlNode performBlock)
    {
        XmlNode node;
        CommandQueue actionQueueRef = Globals.CommandQueue;
        for (int i = 0; i < performBlock.ChildNodes.Count; i++)
        {
            node = performBlock.ChildNodes[i];
            if (node.Name.Equals("unsync", StringComparison.CurrentCultureIgnoreCase))
            {
                ParseUnsyncBlock(node);
            }
            else
            {
                BaseCommand command = _deserializer.DeserializeActionXml(node);
                if (command != null)
                {
                    actionQueueRef.Enqueue(command);
                }
            }
        }
        actionQueueRef.Enqueue(new PollForCommandsCommand());
    }

    protected void ParseUnsyncBlock(XmlNode unsyncBlock)
    {
        XmlNode node;
        for (int i = 0; i < unsyncBlock.ChildNodes.Count; i++)
        {
            node = unsyncBlock.ChildNodes[i];

            BaseCommand command = _deserializer.DeserializeActionXml(node);
            if (command != null)
            {
                Globals.CommandQueue.Enqueue(command);
            }
        }
    }
}