#region

using System.Xml;

#endregion

public class SpeakCommand : BaseCommand
{
    protected XmlNode _speechBlockNode;
    protected string _speechString;

    public SpeakCommand(XmlNode speechBlock)
    {
        if (speechBlock is XmlDocument)
        {
            _speechBlockNode = speechBlock.FirstChild;
        }
        else
        {
            _speechBlockNode = speechBlock;
        }
    }

    public SpeakCommand(string speechString)
    {
        _speechString = speechString;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<SpeechCompleteEvent>(OnSpeechComplete);
        if (_speechBlockNode != null)
        {
            Globals.EventBus.Dispatch(new SpeakRequestEvent(_speechBlockNode));
        }
        else
        {
            Globals.EventBus.Dispatch(new SpeakRequestEvent(_speechString));
        }
    }

    private void OnSpeechComplete(SpeechCompleteEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<SpeechCompleteEvent>(OnSpeechComplete);
    }
}