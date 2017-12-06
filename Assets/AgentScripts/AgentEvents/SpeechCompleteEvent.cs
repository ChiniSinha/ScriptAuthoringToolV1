public class SpeechCompleteEvent : RagEvent
{
    public SpeechCompleteEvent(Agent agent)
    {
        Agent = agent;
    }

    public Agent Agent { get; private set; }
}