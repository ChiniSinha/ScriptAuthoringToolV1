public class ShowSliderCommand : BaseCommand
{
    public ShowSliderCommand(string label, string[] menuOptions, string minValueLabel, string maxValueLabel,
        float minValue, float maxValue, float resolution = 0)
    {
        Label = label;
        MenuOptions = menuOptions;
        MinValueLabel = minValueLabel;
        MaxValueLabel = maxValueLabel;
        MinValue = minValue;
        MaxValue = maxValue;
        Resolution = resolution;
    }

    public string Label { get; private set; }
    public string[] MenuOptions { get; private set; }
    public string MinValueLabel { get; private set; }
    public string MaxValueLabel { get; private set; }
    public float MinValue { get; private set; }
    public float MaxValue { get; private set; }
    public float Resolution { get; private set; }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowSliderEvent(Label, MenuOptions, MinValueLabel, MaxValueLabel, MinValue,
            MaxValue, Resolution));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}