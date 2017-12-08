public class AppearUiAnimator : UiAnimator
{
    public override void Show(AnimationCallback completeCb = null)
    {
        gameObject.SetActive(true);
        Showing = true;
        Globals.EventBus.Dispatch(new GuiAnimationEvent());
        if (completeCb != null)
        {
            completeCb();
        }
    }

    public override void Hide(AnimationCallback completeCb = null)
    {
        gameObject.SetActive(false);
        Showing = false;
        Globals.EventBus.Dispatch(new GuiAnimationEvent());
        if (completeCb != null)
        {
            completeCb();
        }
    }
}