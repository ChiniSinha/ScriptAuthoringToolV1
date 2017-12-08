public class HideKeyboardEvent : UIEvent
{
    public HideKeyboardEvent(string elementName = "")
    {
        ElementName = elementName;
    }

    public string ElementName { get; private set; }
}