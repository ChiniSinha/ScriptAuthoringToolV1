public class HideTextInputEvent : Event
{
    public HideTextInputEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}