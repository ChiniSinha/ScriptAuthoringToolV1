public class HidePauseButtonEvent : Event
{
    public HidePauseButtonEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}