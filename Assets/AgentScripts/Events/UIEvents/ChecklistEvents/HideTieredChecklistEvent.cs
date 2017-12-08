public class HideTieredChecklistEvent : Event
{
    public HideTieredChecklistEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}