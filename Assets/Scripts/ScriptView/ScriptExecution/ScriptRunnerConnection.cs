using System;

public class ScriptRunnerConnection : ExternalConnection
{
    private ScriptRunner _scriptRunner;

    public override bool Authenticated
    {
        get { return true; }
    }

    private void Awake()
    {
        _scriptRunner = new ScriptRunner();
        ScriptCommandProtocol protocol = new ScriptCommandProtocol(_scriptRunner);
        Globals.Register<ICommandProtocol>(protocol);
        Globals.EventBus.Register<LoadCompleteEvent>(OnLoadComplete);
        Globals.StartMediator(new ScriptCommandMediator(protocol));
    }

  

    public void OnLoadComplete(LoadCompleteEvent e)
    {
        _scriptRunner.Start();
    }

    public void Update()
    {
        _scriptRunner.OnUpdate();
    }
}