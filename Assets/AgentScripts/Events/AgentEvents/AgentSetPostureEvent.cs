public class AgentSetPostureEvent : Event
{
    public AgentSetPostureEvent(string direction)
    {
        Direction = direction;
    }

    public string Direction { get; private set; }
}