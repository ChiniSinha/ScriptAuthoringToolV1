public class PauseButtonCommand : BaseCommand
{
    protected bool _visible;

    public PauseButtonCommand(bool visible)
    {
        _visible = visible;
    }

    public override void Execute()
    {
        if (_visible)
        {
            Globals.EventBus.Dispatch(new ShowPauseButtonEvent());
        }
        else
        {
            Globals.EventBus.Dispatch(new HidePauseButtonEvent());
        }
    }
}