public class AgentHeadNodCommand : BaseCommand
{
    public override void Execute()
    {
        Globals.EventBus.Dispatch(new AgentHeadNodEvent());
    }
}