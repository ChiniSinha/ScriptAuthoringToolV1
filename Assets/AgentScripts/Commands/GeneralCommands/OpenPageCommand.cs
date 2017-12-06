public class OpenPageCommand : BaseCommand
{
    public OpenPageCommand(string url)
    {
        Url = url;
    }

    public string Url { get; protected set; }

    public override void Execute()
    {
        Globals.EventBus.Dispatch(new OpenUrlRequestEvent(Url));
    }
}