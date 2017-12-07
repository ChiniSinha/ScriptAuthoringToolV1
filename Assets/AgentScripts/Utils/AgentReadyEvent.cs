public class AgentReadyEvent : Event
{
    public AgentReadyEvent(Agent agent)
    {
        Agent = agent;
    }

    public Agent Agent { get; private set; }
}