public class HideSliderEvent : Event
{
    public HideSliderEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}