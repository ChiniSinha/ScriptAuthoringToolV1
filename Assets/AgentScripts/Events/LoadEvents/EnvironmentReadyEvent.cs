public class EnvironmentReadyEvent : Event
{
    public EnvironmentReadyEvent(Environment environment)
    {
        Environment = environment;
    }

    public Environment Environment { get; private set; }
}