public class SetInteractivityCommand : BaseCommand
{
    private readonly bool _interactable;

    public SetInteractivityCommand(bool interactable)
    {
        _interactable = interactable;
    }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new InteractableChangeEvent(_interactable));
    }
}