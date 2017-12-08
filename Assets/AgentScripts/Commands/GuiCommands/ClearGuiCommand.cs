public class ClearGuiCommand : BaseCommand
{
    public override void Execute()
    {
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiAnimationEvent);
        InProgress = true;
        Globals.EventBus.Dispatch(new ClearUiEvent());
    }

    private void OnGuiAnimationEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiAnimationEvent);
    }
}