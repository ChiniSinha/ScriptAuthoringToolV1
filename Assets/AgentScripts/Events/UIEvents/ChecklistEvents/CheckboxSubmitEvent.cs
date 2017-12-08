public class CheckboxSubmitEvent : UIEvent
{
    public CheckboxSubmitEvent(int[] selectedIdxs, int submitButtonPressed = -1)
    {
        SelectedIdxs = selectedIdxs;
        ButtonPressed = submitButtonPressed;
    }

    public int[] SelectedIdxs { get; protected set; }
    public int ButtonPressed { get; protected set; }
}