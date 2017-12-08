#region



#endregion

public class ShowTieredChecklistCommand : BaseCommand
{
    private readonly string[] _buttonTexts;
    private readonly NestedString _data;
    private readonly string _label;
    private readonly int _limit;

    public ShowTieredChecklistCommand(string label, NestedString data, string[] buttonTexts, int limit)
    {
        _label = label;
        _data = data;
        _buttonTexts = buttonTexts;
        _limit = limit;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowTieredChecklistEvent(_data, _buttonTexts, _label, _limit));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}