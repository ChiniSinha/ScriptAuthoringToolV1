public class AgentSetPostureEvent : RagEvent
{
    public AgentSetPostureEvent(string direction)
    {
        Direction = direction;
    }

    public string Direction { get; private set; }
}