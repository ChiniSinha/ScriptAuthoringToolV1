public class AudioEvent : Event
{
    public enum Type
    {
        PLAYBACK_START,
        PLAYBACK_COMPLETE
    }

    public AudioEvent(Type eventType, string playerName = "")
    {
        PlayerName = playerName;
        EventType = eventType;
    }

    public string PlayerName { get; private set; }

    public Type EventType { get; private set; }
}