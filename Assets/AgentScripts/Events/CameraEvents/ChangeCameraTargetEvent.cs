public class ChangeCameraTargetEvent : Event
{
    public ChangeCameraTargetEvent(string newTarget)
    {
        NewTarget = newTarget;
    }

    public string NewTarget { get; private set; }
}