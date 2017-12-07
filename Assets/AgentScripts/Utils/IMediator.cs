/// <summary>
///     Mediators are communication layers between services and views and the rest of the application
///     Typically, the communication is one way. Other elements of the application (Controllers and Commands)
///     call methods on the mediator, which is then responsible for instructing the service or view.
///     When using the mediator pattern, the mediator should be the only part of the application that
///     interfaces with the service or view.
///     Though it may be tempting to integrate the functionality of a service into its mediator, it is
///     recommended that the two remain separate. This will allow future tinkerers to change the internal
///     service as much as they want without damaging the interface into it.
/// </summary>
public interface IMediator
{
    void Setup();
    void Cleanup();
}