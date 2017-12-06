#region



#endregion

public class ProcessUILogicCommand : BaseCommand
{
    protected UI _ui;

    public ProcessUILogicCommand(UI ui)
    {
        _ui = ui;
    }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new ProcessUILogicEvent(_ui));
    }
}