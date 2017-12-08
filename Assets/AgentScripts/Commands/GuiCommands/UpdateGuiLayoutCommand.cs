public class UpdateGuiLayoutCommand : BaseCommand
{
    protected string _configName;

    public UpdateGuiLayoutCommand(string configName)
    {
        _configName = configName;
    }

    public override void Execute()
    {
    }
}