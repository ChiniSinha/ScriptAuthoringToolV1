public class HideMenuEvent : Event
{
    public HideMenuEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}