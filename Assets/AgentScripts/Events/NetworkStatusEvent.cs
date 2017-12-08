public class NetworkStatusEvent : Event
{
    public enum AuthStatus
    {
        CONNECTION_BROKEN,
        CONNECTION_ESTABLISHED,
        AUTHENTICATED,
        AUTH_REQUIRED,
        NO_TTS_CONNECTION,
        NO_DATABASE_CONNECTION
    }

    public NetworkStatusEvent(AuthStatus status)
    {
        Status = status;
    }

    public AuthStatus Status { get; protected set; }
}