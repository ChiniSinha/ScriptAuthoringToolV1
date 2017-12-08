#region



#endregion

public class ShowTextInputCommand : BaseCommand
{
    protected readonly string[] _menuOptions;
    protected readonly string _prompt;

    public ShowTextInputCommand(string prompt, string[] menuOptions)
    {
        _prompt = prompt;
        _menuOptions = menuOptions;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowTextInputEvent(_prompt, _menuOptions));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}