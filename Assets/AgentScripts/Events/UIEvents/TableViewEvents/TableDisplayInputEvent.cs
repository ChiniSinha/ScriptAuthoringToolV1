public class TableDisplayInputEvent : UIEvent
{
    public TableDisplayInputEvent(int buttonPressed)
    {
        ButtonPressed = buttonPressed;
    }

    public int ButtonPressed { get; private set; }
}