public class AgentPauseAnimationEvent : Event
{
    public float DelayMs { get; protected set; }

    public AgentPauseAnimationEvent(float delayMs)
    {
        DelayMs = delayMs;
    }
}