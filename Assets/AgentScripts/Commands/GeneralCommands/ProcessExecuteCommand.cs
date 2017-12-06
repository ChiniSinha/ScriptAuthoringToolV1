#region



#endregion

public class ProcessExecuteCommand : BaseCommand
{
    protected string _command;

    public ProcessExecuteCommand(string command)
    {
        _command = command;
    }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new ProcessExecuteEvent(_command));
    }
}