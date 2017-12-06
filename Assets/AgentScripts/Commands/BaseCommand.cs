public abstract class BaseCommand
{
    public bool InProgress { get; protected set; }

    public abstract void Execute();

    public virtual void OnUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnComplete()
    {
    }
}