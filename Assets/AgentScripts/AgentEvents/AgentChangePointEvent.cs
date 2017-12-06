public class AgentChangePointEvent : RagEvent
{
    public AgentChangePointEvent(float x, float y, string hand, string shape)
    {
        X = x;
        Y = y;
        Hand = hand;
        Shape = shape;
    }

    public float X { get; private set; }
    public float Y { get; private set; }
    public string Hand { get; private set; }
    public string Shape { get; private set; }
}