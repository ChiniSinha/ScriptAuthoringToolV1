public class EnvironmentTransitionCompleteEvent : Event
{
    public EnvironmentTransitionCompleteEvent(string newEnvironmentName)
    {
        NewEnvironmentName = newEnvironmentName;
    }

    public string NewEnvironmentName { get; protected set; }
}