public class SliderInputSubmitEvent : UIEvent
{
    public SliderInputSubmitEvent(float input, int buttonPressed = 0)
    {
        SliderValue = input;
        ButtonPressed = buttonPressed;
    }

    public float SliderValue { get; private set; }
    public int ButtonPressed { get; private set; }
}