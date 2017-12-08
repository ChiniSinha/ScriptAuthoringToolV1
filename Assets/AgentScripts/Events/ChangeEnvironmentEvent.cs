public class ChangeEnvironmentEvent : Event
{
    public ChangeEnvironmentEvent(string environmentName)
    {
        EnvironmentName = environmentName;
    }

    public string EnvironmentName { get; protected set; }
}