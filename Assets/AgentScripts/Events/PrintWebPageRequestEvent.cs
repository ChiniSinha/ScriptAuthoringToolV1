public class PrintWebPageRequestEvent : Event
{
    public PrintWebPageRequestEvent(string url)
    {
        Url = url;
    }

    public string Url { get; private set; }
}