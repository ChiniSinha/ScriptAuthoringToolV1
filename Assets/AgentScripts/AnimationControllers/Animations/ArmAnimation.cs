//using DG.Tweening;
using UnityEngine;

public class ArmAnimation : AgentAnimation
{
    public enum Type
    {
        PAUSED = -1,
        NEUTRAL = 0,
        CONTRAST = 1,
        POINT_DOWN = 2,
        POINT_SELF = 3,
        POINT_OUT = 4,
        POINT_USER = 5,
        THUMBS_UP = 6,
        HOLD_DOCUMENT = 7,
        GESTURE_BOARD = 8,
        COUNT_ONE = 9,
        COUNT_TWO = 10,
        COUNT_THREE = 11,
        COUNT_FOUR = 12,
        COUNT_FIVE = 13,
        WAVE = 14,
        POINT_DOCUMENT = 15,
        READY = 16
    }

    public readonly Type Animation;

    public ArmAnimation(Type animation, float duration = 0)
    {
        Animation = animation;
        Duration = duration;
    }

    public override void Play(AgentAnimationController animController, Side side = Side.CENTER)
    {
        float transitionTime = 0f;
        switch (Animation)
        {
            case Type.NEUTRAL:
                animController.Body.PlayNeutral(side);
                animController.Body.HandOpen(side);
                transitionTime = Random.Range(0.5f, 1.5f);
                break;
            case Type.CONTRAST:
                animController.Body.PlayContrast(side);
                break;
            case Type.POINT_DOWN:
                animController.Body.PlayPointDown(side);
                break;
            case Type.POINT_SELF:
                animController.Body.PlayPointSelf(side);
                break;
            case Type.POINT_OUT:
                animController.Body.PlayPointOut(side);
                break;
            case Type.POINT_USER:
                animController.Body.PlayPointUser(side);

/*                if (side == Side.LEFT)
                {
                    animController.LeftArmAffector.SetParent(Camera.main.transform);
                    animController.LeftArmAffector.localPosition = Vector3.zero;
                }
                else
                {
                    animController.LeftArmAffector.SetParent(Camera.main.transform);
                    animController.LeftArmAffector.localPosition = Vector3.zero;
                }
*/
                break;
            case Type.THUMBS_UP:
                animController.Body.PlayThumbsUp(side);
                break;
            case Type.HOLD_DOCUMENT:
                animController.Body.PlayHold(side);
                //animController.Body.PlayIdleAnimation(side == Side.LEFT ? 2 : 3);
                break;
            case Type.GESTURE_BOARD:
                animController.Body.PlayBoardGesture(side);
                break;
            case Type.COUNT_ONE:
                animController.Body.PlayCountTo(side, 1);
                break;
            case Type.COUNT_TWO:
                animController.Body.PlayCountTo(side, 2);
                break;
            case Type.COUNT_THREE:
                animController.Body.PlayCountTo(side, 3);
                break;
            case Type.COUNT_FOUR:
                animController.Body.PlayCountTo(side, 4);
                break;
            case Type.COUNT_FIVE:
                animController.Body.PlayCountTo(side, 5);
                break;
            case Type.POINT_DOCUMENT:
                animController.Body.PlayPointDocument(side);
                animController.Body.HandPoint(side);
                transitionTime = Random.Range(0.5f, 1.5f);
                break;
            case Type.WAVE:
                animController.Body.PlayWave(side);
                break;
            case Type.READY:
                animController.Body.PlayReady(side);
                break;
        }

//        animController.UpdateIKParameters(Animation, side, transitionTime);
    }
}