public class HideNumberKeypadEvent : Event
{
    public HideNumberKeypadEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}