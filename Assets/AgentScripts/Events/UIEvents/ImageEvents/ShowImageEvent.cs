public class ShowImageEvent : Event
{
    public ShowImageEvent(string url, string zoom = "", string elementName = "")
    {
        Url = url;
        Zoom = zoom;
        ElementName = elementName;
    }

    public string Url { get; private set; }
    public string Zoom { get; private set; }
    public string ElementName { get; private set; }
}