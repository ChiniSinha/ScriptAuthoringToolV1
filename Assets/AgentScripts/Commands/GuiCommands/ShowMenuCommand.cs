#region

#endregion

public class ShowMenuCommand : BaseCommand
{
    protected string[] _options;
    protected string _elementName;

    public ShowMenuCommand(string[] options, string elementName="Menu")
    {
        _options = options;
        _elementName = elementName;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowMenuEvent(_options, elementName: _elementName));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}