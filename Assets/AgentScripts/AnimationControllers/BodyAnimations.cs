#region

using System;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

[Serializable]
public class BodyAnimations
{
    private const int IdleAnimationCount = 6;
    private const int BeatVariationCount = 2;

    protected Animator _animator;

    public BodyAnimations(Animator animator)
    {
        _animator = animator;
    }

    public Side CurrentPosture
    {
        get { return (Side) _animator.GetInteger("Posture"); }
    }

    public void PlayCustomFullbodyAnimation(int num)
    {
        _animator.SetInteger("IdleAnimation", num + IdleAnimationCount);
    }

    public void PlayIdleAnimation(int num)
    {
        _animator.SetInteger("IdleAnimation", num);
    }

    public void SetPosture(Side direction)
    {
        _animator.SetInteger("Posture", (int) direction);
    }

    public void SetOrientation(Side direction)
    {
        _animator.SetInteger("Orientation", (int) direction);
    }

    public void PlayNeutral(Side side)
    {
        PlayArmAnimation(side, 0);
    }

    public void PlayContrast(Side side)
    {
        PlayArmAnimation(side, 1);
    }

    public void PlayPointDown(Side side)
    {
        PlayArmAnimation(side, 2);
    }

    public void PlayPointSelf(Side side)
    {
        PlayArmAnimation(side, 3);
    }

    public void PlayPointOut(Side side)
    {
        PlayArmAnimation(side, 4);
    }

    public void PlayPointUser(Side side)
    {
        PlayArmAnimation(side, 5);
    }

    public void PlayThumbsUp(Side side)
    {
        PlayArmAnimation(side, 6);
    }

    public void PlayHold(Side side)
    {
        PlayArmAnimation(side, 7);
    }

    public void PlayBoardGesture(Side side)
    {
        PlayArmAnimation(side, 8);
    }

    public void PlayCountTo(Side side, int value)
    {
        PlayArmAnimation(side, 8 + value);
    }

    public void PlayPointDocument(Side side)
    {
        PlayArmAnimation(side, 14);
    }

    public void PlayWave(Side side)
    {
        PlayArmAnimation(side, 15);
    }

    public void PlayReady(Side side)
    {
        PlayArmAnimation(side, 16);
    }

    private void PlayArmAnimation(Side side, int animationId)
    {
        if (side == Side.LEFT)
        {
            _animator.SetInteger("Left_Anim", animationId);
        }
        else if (side == Side.RIGHT)
        {
            _animator.SetInteger("Right_Anim", animationId);
        }
    }

    public void PlayBeat(Side side)
    {
        if (side == Side.LEFT)
        {
            _animator.SetInteger("LeftBeatVariant", Random.Range(0, BeatVariationCount));
            _animator.SetTrigger("LeftBeat");
        }
        else if (side == Side.RIGHT)
        {
            _animator.SetInteger("RightBeatVariant", Random.Range(0, BeatVariationCount));
            _animator.SetTrigger("RightBeat");
        }
    }

    public void PlayHeadNod()
    {
        _animator.SetInteger("Head_Anim", 2);
    }

    public void PlayHeadNeutral()
    {
        _animator.SetInteger("Head_Anim", 0);
    }

    public void PlayEyeFlick()
    {
        _animator.SetInteger("Head_Anim", 1);
    }

    public void HandPoint(Side hand)
    {
        if (hand == Side.LEFT)
        {
            _animator.SetInteger("LeftHandPose", 2);
        }
        else if (hand == Side.RIGHT)
        {
            _animator.SetInteger("RightHandPose", 2);
        }
    }

    public void HandClose(Side hand)
    {
        if (hand == Side.LEFT)
        {
            _animator.SetInteger("LeftHandPose", 1);
        }
        else if (hand == Side.RIGHT)
        {
            _animator.SetInteger("RightHandPose", 1);
        }
    }

    public void HandOpen(Side hand)
    {
        if (hand == Side.LEFT)
        {
            _animator.SetInteger("LeftHandPose", 0);
        }
        else if (hand == Side.RIGHT)
        {
            _animator.SetInteger("RightHandPose", 0);
        }
    }
}