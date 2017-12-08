#region

using DG.Tweening;
using UnityEngine;

#endregion

public class SlidingUiAnimator : UiAnimator
{
    public enum Direction
    {
        LEFT,
        UP,
        RIGHT,
        DOWN
    }

    [SerializeField] protected Direction _dir;

    [SerializeField] protected Ease _easeFunction;

    [SerializeField] protected float _slideTime;

    public override void Show(AnimationCallback completeCb = null)
    {
        RectTransform rect = transform as RectTransform;
        if (rect)
        {
            AnimationUtils.AnimatePivot(rect, _slideTime, rect.anchorMax, _easeFunction)
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
            Vector2 goal = rect.anchorMax;
            switch (_dir)
            {
                case Direction.LEFT:
                    goal.x = 1;
                    break;
                case Direction.RIGHT:
                    goal.x = 0;
                    break;
                case Direction.UP:
                    goal.y = 0;
                    break;
                case Direction.DOWN:
                    goal.y = 1;
                    break;
            }

            AnimationUtils.AnimatePivot(rect, _slideTime, goal, _easeFunction)
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