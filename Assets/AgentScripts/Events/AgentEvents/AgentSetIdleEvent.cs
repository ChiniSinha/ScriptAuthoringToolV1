public class AgentSetIdleEvent : Event
{
    public AgentSetIdleEvent(bool shouldIdle)
    {
        ShouldIdle = shouldIdle;
    }

    public bool ShouldIdle { get; private set; }
}