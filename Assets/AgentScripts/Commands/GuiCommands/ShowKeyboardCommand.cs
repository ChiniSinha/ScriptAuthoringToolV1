#region



#endregion

public class ShowKeyboardCommand : BaseCommand
{
    protected string[] _controlButtons;
    protected string _prompt;

    public ShowKeyboardCommand(string prompt, string[] controlButtons)
    {
        _prompt = prompt;
        _controlButtons = controlButtons;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowKeyboardEvent(_controlButtons, _prompt));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}