public class ShowPauseButtonEvent : Event
{
    public ShowPauseButtonEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}