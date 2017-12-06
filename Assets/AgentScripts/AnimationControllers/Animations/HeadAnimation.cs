using UnityEngine;

public class HeadAnimation : AgentAnimation
{
    public enum Type
    {
        PAUSED = -1,
        GAZE = 0,
        EYE_FLICK = 1,
        NOD = 2
    }

    public readonly Type Animation;

    public HeadAnimation(Type animation, float duration = 0)
    {
        Animation = animation;
        Duration = duration;
    }

    public override void Play(AgentAnimationController animController, Side side = Side.CENTER)
    {
        float transitionTime = 0;
        switch (Animation)
        {
            case Type.GAZE:
                animController.Body.PlayHeadNeutral();
                transitionTime = Random.Range(0.5f, 1.5f);
                break;
            case Type.EYE_FLICK:
                animController.Body.PlayEyeFlick();
                break;
            case Type.NOD:
                animController.Body.PlayHeadNod();
                transitionTime = Random.Range(0.5f, 1.5f);
                break;
        }
//        animController.UpdateIKParameters(Animation, transitionTime);
    }
}