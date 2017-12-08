public class StartEnvironmentTransitionEvent : Event
{
    public StartEnvironmentTransitionEvent(string environmentName)
    {
        EnvironmentName = environmentName;
    }

    public string EnvironmentName { get; protected set; }
}