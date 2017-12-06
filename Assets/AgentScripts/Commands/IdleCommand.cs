public class IdleCommand : BaseCommand
{
    protected bool _idleState;

    public IdleCommand(bool idleState)
    {
        _idleState = idleState;
    }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new AgentSetIdleEvent(_idleState));
    }
}