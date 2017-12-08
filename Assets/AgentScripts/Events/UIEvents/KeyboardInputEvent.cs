public class KeyboardInputEvent : UIEvent
{
    public KeyboardInputEvent(string inputtedString, int buttonPressed)
    {
        UserString = inputtedString;
        ButtonPressed = buttonPressed;
    }

    public string UserString { get; protected set; }
    public int ButtonPressed { get; protected set; }
}