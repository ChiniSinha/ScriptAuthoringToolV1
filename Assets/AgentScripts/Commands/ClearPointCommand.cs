public class ClearPointCommand : BaseCommand
{
    public override void Execute()
    {
        Globals.EventBus.Dispatch(new AgentClearPointEvent());
    }
}