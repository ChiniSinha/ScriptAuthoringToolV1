public class BaseCommandMediator : IMediator
{
    private ICommandProtocol _commandProtocol;

    public BaseCommandMediator(ICommandProtocol commandProtocol)
    {
        _commandProtocol = commandProtocol;
    }

    public virtual void Setup()
    {
        //Globals.EventBus.Register<ScriptingInterfaceLoadedEvent>(OnScriptingInterfaceLoaded);
        //Globals.EventBus.Register<NetworkStatusEvent>(OnNetworkStatus);
    }

    public virtual void Cleanup()
    {
        Globals.EventBus.Unregister<ScriptingInterfaceLoadedEvent>(OnScriptingInterfaceLoaded);
        Globals.EventBus.Unregister<NetworkStatusEvent>(OnNetworkStatus);
    }

    private void OnNetworkStatus(NetworkStatusEvent e)
    {
        if (e.Status == NetworkStatusEvent.AuthStatus.AUTHENTICATED ||
            e.Status == NetworkStatusEvent.AuthStatus.CONNECTION_ESTABLISHED)
        {
            _commandProtocol.Startup();
        }
    }

    private void OnScriptingInterfaceLoaded(ScriptingInterfaceLoadedEvent e)
    {
        _commandProtocol.TryConnect();
    }
}