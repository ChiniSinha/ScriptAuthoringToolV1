public class SpeechCompleteEvent : Event
{
    public SpeechCompleteEvent(Agent agent)
    {
        Agent = agent;
    }

    public Agent Agent { get; private set; }
}