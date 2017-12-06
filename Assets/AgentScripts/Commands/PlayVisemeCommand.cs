public class PlayVisemeCommand : BaseCommand
{
    private int _visemeId;
    private int _durationMs;

    public PlayVisemeCommand(int visemeId, int durationMs)
    {
        _visemeId = visemeId;
        _durationMs = durationMs;
    }
    public override void Execute()
    {
        if(Globals.HasRegistered<Agent>())
            Globals.Get<Agent>().AgentAnimationController.Face.PlayViseme(_visemeId, _durationMs);
    }
}
