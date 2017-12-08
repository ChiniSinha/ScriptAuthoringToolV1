public class PossibleVoiceRecognitionEvent : Event
{
    public PossibleVoiceRecognitionEvent(int optionIdx)
    {
        OptionIdx = optionIdx;
    }

    public int OptionIdx { get; private set; }
}