public class SetDebugTextEvent : Event
{
    public SetDebugTextEvent(string debugText)
    {
        DebugText = debugText;
    }

    public string DebugText { get; private set; }
}