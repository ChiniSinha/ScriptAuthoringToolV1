#region



#endregion

public class BasicEnvironment : Environment
{
    private IEnvironmentMediator _mediator;

    protected void Awake()
    {
        _mediator = new BasicEnvironmentMediator(this);
        _mediator.Setup();
    }

    protected void OnDestroy()
    {
        _mediator.Cleanup();
    }
}