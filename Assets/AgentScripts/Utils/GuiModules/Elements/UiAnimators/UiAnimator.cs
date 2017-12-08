#region

using UnityEngine;

#endregion

public abstract class UiAnimator : MonoBehaviour
{
    public delegate void AnimationCallback();

    public bool Showing { get; set; }
    public abstract void Show(AnimationCallback completeCb = null);
    public abstract void Hide(AnimationCallback completeCb = null);
}