public class TextInputSubmitEvent : UIEvent
{
    public TextInputSubmitEvent(string inputtedString, int buttonPressed = -1)
    {
        UserString = inputtedString;
        ButtonPressed = buttonPressed;
    }

    public string UserString { get; protected set; }
    public int ButtonPressed { get; protected set; }
}