public class PlayAudioRequestEvent : Event
{
    public PlayAudioRequestEvent(string audioUrl, string playerName)
    {
        AudioUrl = audioUrl;
        PlayerName = playerName;
    }

    public string AudioUrl { get; private set; }
    public string PlayerName { get; private set; }
}