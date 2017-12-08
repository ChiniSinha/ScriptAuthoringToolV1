public class HideTableViewEvent : Event
{
    public HideTableViewEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}