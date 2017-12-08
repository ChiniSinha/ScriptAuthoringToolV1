public class AgentUpdateExpressionEvent : Event
{
    public AgentUpdateExpressionEvent(FaceAnimation.Type expression, float duration = 0f)
    {
        Expression = expression;
        Duration = duration;
    }

    public FaceAnimation.Type Expression { get; private set; }
    public float Duration { get; private set; }
}