#region

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

#endregion

public static class AnimationUtils
{
    public static TweenerCore<Vector2, Vector2, VectorOptions> AnimatePivot(RectTransform rect, float time,
        Vector2 goalPivot, Ease easeFunction)
    {
        return DOTween.To(delegate { return rect.pivot; }, delegate(Vector2 v) { rect.pivot = v; }, goalPivot,
            time).SetEase(easeFunction);
    }
}