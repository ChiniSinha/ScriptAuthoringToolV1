using System.Collections.Generic;

public class ShowChecklistCommand : BaseCommand
{
    private readonly string[] _choices;
    private readonly string[] _controlButtons;
    private readonly int _limit;
    private readonly string _prompt;

    public ShowChecklistCommand(string prompt, string[] choices, string[] controlButtons, int limit = 0)
    {
        _prompt = prompt;
        _choices = choices;
        _controlButtons = controlButtons;
        _limit = limit;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowBasicChecklistEvent(new List<string>(_choices),
            new List<string>(_controlButtons), _prompt, _limit));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}