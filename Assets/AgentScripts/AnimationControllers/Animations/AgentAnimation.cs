public abstract class AgentAnimation
{
    public float Duration;

    public abstract void Play(AgentAnimationController animController, Side side = Side.CENTER);
}