#region

using System;
using System.Collections.Generic;
//using DG.Tweening;
//using DG.Tweening.Core;
//using DG.Tweening.Plugins.Options;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

[Serializable]
public class FaceAnimations
{
    protected Animator _animator;

    private int _lastVisemeId;

    public FaceAnimations(Animator animator)
    {
        _animator = animator;
    }

    public void Blink(float duration)
    {
        _animator.SetFloat("BlinkSpeed", 1/(duration * 2));
        _animator.SetTrigger("Blink");
    }

    public void Smile()
    {
        _animator.SetFloat("ExpressionVariant", Random.Range(0, 1f));
        _animator.SetInteger("Expression", 1);
    }

    public void NeutralExpression()
    {
        _animator.SetFloat("ExpressionVariant", Random.Range(0, 1f));
        _animator.SetInteger("Expression", 0);
    }

    public void Frown()
    {
        _animator.SetFloat("ExpressionVariant", Random.Range(0, 1f));
        _animator.SetInteger("Expression", 2);
    }

    public void BrowsUp()
    {
        _animator.SetInteger("BrowVariant", Random.Range(0, 1));
        _animator.SetInteger("Brows", 2);
    }

    public void BrowsDown()
    {
        _animator.SetFloat("BrowVariant", Random.Range(0, 1f));
        _animator.SetInteger("Brows", 1);
    }

    public void BrowsNeutral()
    {
        _animator.SetFloat("BrowVariant", Random.Range(0, 1f));
        _animator.SetInteger("Brows", 0);
    }

    public void PlayViseme(int visemeId, int durationTimeMs = -1)
    {
        if (visemeId == _lastVisemeId)
        {
            return;
        }

        if (durationTimeMs < 0)
        {
            //HACK if the tts is not generating timings
            durationTimeMs = Random.Range(40, 100);
        }

        float targetDuration;

        for (int i = 0; i < 22; i++)
        {
            string name = "Viseme_" + i + "_Strength";
            //FIX THIS TO A NORMAL TWEEEN
//            DOTween.To(delegate { return _animator.GetFloat(name); }, delegate(float v) { _animator.SetFloat(name, v); },
//                i == visemeId ? 1f : 0f, durationTimeMs/1000f);
        }

        _lastVisemeId = visemeId;
    }


}