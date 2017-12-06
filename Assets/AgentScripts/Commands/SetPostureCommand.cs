public class SetPostureCommand : BaseCommand
{
    protected string _direction;

    public SetPostureCommand(string direction)
    {
        _direction = direction;
    }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new AgentSetPostureEvent(_direction));
    }
}