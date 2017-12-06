#region



#endregion

public class PlayAudioCommand : BaseCommand
{
    private readonly string _playerName;
    protected string _url;

    public PlayAudioCommand(string url, string playerName = "")
    {
        _url = url;
        _playerName = playerName;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<AudioEvent>(OnAudioEvent);
        Globals.EventBus.Dispatch(new PlayAudioRequestEvent(_url, _playerName));
    }

    private void OnAudioEvent(AudioEvent e)
    {
        if (e.EventType == AudioEvent.Type.PLAYBACK_COMPLETE && (e.PlayerName == _playerName || _playerName == ""))
        {
            InProgress = false;
        }
    }
}