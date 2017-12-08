public class NumberKeypadSubmitEvent : UIEvent
{
    public NumberKeypadSubmitEvent(int input, int buttonPressed)
    {
        UserInput = input;
        ButtonPressed = buttonPressed;
    }

    public int UserInput { get; protected set; }
    public int ButtonPressed { get; protected set; }
}