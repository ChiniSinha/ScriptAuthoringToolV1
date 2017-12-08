public class SetTitleEvent : Event
{
    public SetTitleEvent(string title)
    {
        Title = title;
    }

    public string Title { get; private set; }
}