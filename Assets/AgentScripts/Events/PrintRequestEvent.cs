public class PrintRequestEvent : Event
{
    public PrintRequestEvent(string content)
    {
        Content = content;
    }

    public string Content { get; private set; }
}