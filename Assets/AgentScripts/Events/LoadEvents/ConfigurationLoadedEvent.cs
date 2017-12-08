public class ConfigurationLoadedEvent : Event
{
    public ConfigurationLoadedEvent(Config config)
    {
        Configuration = config;
    }

    public Config Configuration { get; protected set; }
}