public class EmotionDataAvailableEvent : Event
{
    public EmotionDataAvailableEvent(float happyPerc, float neutralPerc, float sadPerc)
    {
        HappyPerc = happyPerc;
        NeutralPerc = neutralPerc;
        SadPerc = sadPerc;
    }

    public float HappyPerc { get; private set; }
    public float NeutralPerc { get; private set; }
    public float SadPerc { get; private set; }
}