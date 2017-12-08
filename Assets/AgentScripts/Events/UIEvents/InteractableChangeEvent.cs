public class InteractableChangeEvent : UIEvent
{
    public InteractableChangeEvent(bool newStatus)
    {
        Interactable = newStatus;
    }

    public bool Interactable { get; protected set; }
}