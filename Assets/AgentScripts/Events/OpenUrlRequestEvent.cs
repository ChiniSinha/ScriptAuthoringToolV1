public class OpenUrlRequestEvent : Event
{
    public OpenUrlRequestEvent(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }
}