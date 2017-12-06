public class BeatCommand : BaseCommand
{
    private Side _hand;

    public BeatCommand(Side hand)
    {
        _hand = hand;
    }

    public override void Execute()
    {
        if (Globals.HasRegistered<Agent>())
            Globals.Get<Agent>().AgentAnimationController.Body.PlayBeat(_hand);
    }
}
