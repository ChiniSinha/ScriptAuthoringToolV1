public class PollForCommandsCommand : BaseCommand
{
    public override void Execute()
    {
        Globals.EventBus.Dispatch(new EndOfSequenceEvent());
    }
}