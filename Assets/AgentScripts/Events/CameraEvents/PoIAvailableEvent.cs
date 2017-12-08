public class PoIAvailableEvent : Event
{
    public PoIAvailableEvent(string label)
    {
        Label = label;
    }

    public string Label { get; private set; }
}