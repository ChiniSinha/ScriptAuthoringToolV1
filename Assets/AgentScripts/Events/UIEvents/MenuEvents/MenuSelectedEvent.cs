public class MenuSelectedEvent : UIEvent
{
    public MenuSelectedEvent(int selectedIdx)
    {
        SelectedOptionIdx = selectedIdx;
    }

    public int SelectedOptionIdx { get; protected set; }
}