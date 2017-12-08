#region

using DG.Tweening;
using UnityEngine;

#endregion

public class ScalingUiAnimator : UiAnimator
{
    [SerializeField] protected Ease _easeFunction;

    [SerializeField] protected float _scaleTime;

    public override void Show(AnimationCallback completeCb = null)
    {
        RectTransform rect = transform as RectTransform;
        if (rect)
        {
            DOTween.To(delegate { return rect.localScale; }, delegate(Vector3 v) { rect.localScale = v; }, Vector3.one,
                _scaleTime)
                .SetEase(_easeFunction)
                .OnComplete(delegate
                {
                    Showing = true;
                    Globals.EventBus.Dispatch(new GuiAnimationEvent());
                    if (completeCb != null)
                    {
                        completeCb();
                    }
                });
        }
    }

    public override void Hide(AnimationCallback completeCb = null)
    {
        RectTransform rect = transform as RectTransform;
        if (rect)
        {
            DOTween.To(delegate { return rect.localScale; }, delegate(Vector3 v) { rect.localScale = v; }, Vector3.zero,
                _scaleTime)
                .SetEase(_easeFunction)
                .OnComplete(delegate
                {
                    Showing = false;
                    Globals.EventBus.Dispatch(new GuiAnimationEvent());
                    if (completeCb != null)
                    {
                        completeCb();
                    }
                });
        }
    }
}