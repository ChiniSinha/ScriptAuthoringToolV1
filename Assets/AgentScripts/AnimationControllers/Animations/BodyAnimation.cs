#region

using System;

#endregion

public class BodyAnimation : AgentAnimation
{
    public readonly Type Animation;

    public enum Type
    {
        PAUSED = -1,
        POSTURE_SHIFT_RIGHT = 0,
        POSTURE_SHIFT_MIDDLE = 1,
        POSTURE_SHIFT_LEFT = 2,
        POSTURE_SHIFT_CHANGE = 3,
        ORIENTATION_CHANGE_LEFT = 4,
        ORIENTATION_CHANGE_CENTER = 5,
        ORIENTATION_CHANGE_RIGHT = 6
    }

    public BodyAnimation(Type animation, float duration=0)
    {
        Animation = animation;
        Duration = duration;
    }

    public override void Play(AgentAnimationController animController, Side side = Side.CENTER)
    {
        float transitionTime = 0;
        switch (Animation)
        {
            case Type.POSTURE_SHIFT_CHANGE:
                int mod = UnityEngine.Random.Range(0, 2) + 1;
                Side newPosture = (Side) (((int)animController.Body.CurrentPosture + mod)%3);
                animController.Body.SetPosture(newPosture);
                break;
            case Type.POSTURE_SHIFT_LEFT:
                animController.Body.SetPosture(Side.LEFT);
                break;
            case Type.POSTURE_SHIFT_MIDDLE:
                animController.Body.SetPosture(Side.CENTER);
                break;
            case Type.POSTURE_SHIFT_RIGHT:
                animController.Body.SetPosture(Side.RIGHT);
                break;
            case Type.ORIENTATION_CHANGE_LEFT:
                animController.Body.SetOrientation(Side.LEFT);
                break;
            case Type.ORIENTATION_CHANGE_CENTER:
                animController.Body.SetOrientation(Side.CENTER);
                break;
            case Type.ORIENTATION_CHANGE_RIGHT:
                animController.Body.SetOrientation(Side.RIGHT);
                break;
        }
//        animController.UpdateIKParameters(Animation, transitionTime);
    }
}