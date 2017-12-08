public class AgentBeatEvent : Event
{
    public Side Hand { get; set; }

    public AgentBeatEvent(Side hand)
    {
        Hand = hand;
    }
}