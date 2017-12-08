public class PoIRemovedEvent : Event
{
    public PoIRemovedEvent(string label)
    {
        Label = label;
    }

    public string Label { get; private set; }
}