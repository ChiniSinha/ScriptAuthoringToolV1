public class HideImageEvent : Event
{
    public HideImageEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}