public class AgentPlayVisemeEvent : Event
{
    public AgentPlayVisemeEvent(int visemeId, int durationMs)
    {
        VisemeId = visemeId;
        DurationMs = durationMs;
    }

    public int VisemeId { get; private set; }
    public int DurationMs { get; private set; }
}