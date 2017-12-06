public class FaceAnimation : AgentAnimation
{
    public enum Type
    {
        PAUSED = -1,
        SMILE = 10,
        NEUTRAL_EXPRESSION = 1,
        FROWN = 2,
        BROWS_UP = 3,
        BROWS_NEUTRAL = 4,
        BROWS_DOWN = 5
    }

    public readonly Type Animation;

    public FaceAnimation(Type animation, float duration = 0)
    {
        Animation = animation;
        Duration = duration;
    }

    public override void Play(AgentAnimationController animController, Side side = Side.CENTER)
    {
        switch (Animation)
        {
            case Type.BROWS_DOWN:
                animController.Face.BrowsDown();
                break;
            case Type.BROWS_NEUTRAL:
                animController.Face.BrowsNeutral();
                break;
            case Type.BROWS_UP:
                animController.Face.BrowsUp();
                break;
            case Type.FROWN:
                animController.Face.Frown();
                break;
            case Type.NEUTRAL_EXPRESSION:
                animController.Face.NeutralExpression();
                break;
            case Type.SMILE:
                animController.Face.Smile();
                break;
        }
    }
}