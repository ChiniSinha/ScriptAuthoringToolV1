public class HideBasicChecklistEvent : Event
{
    public HideBasicChecklistEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}