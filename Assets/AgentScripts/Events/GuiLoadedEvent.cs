public class GuiLoadedEvent : Event
{
    public GuiLoadedEvent(GuiBundle bundle)
    {
        Bundle = bundle;
    }

    public GuiBundle Bundle { get; private set; }
}