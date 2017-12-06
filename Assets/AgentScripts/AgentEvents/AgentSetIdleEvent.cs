public class AgentSetIdleEvent : RagEvent
{
    public AgentSetIdleEvent(bool shouldIdle)
    {
        ShouldIdle = shouldIdle;
    }

    public bool ShouldIdle { get; private set; }
}