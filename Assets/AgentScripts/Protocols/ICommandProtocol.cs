public interface ICommandProtocol
{
    void TryConnect();
    void Startup();
    void SendDummyInputResponse();
    void Shutdown();
}