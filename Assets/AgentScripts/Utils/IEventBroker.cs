/// <summary>
///     Brokers are one of two methods of application control (the other being Commands).
///     Brokers are collections of functions invoked in response to events. Brokers
///     typically map to a single mediator, and perform operations on that mediator when
///     Events get raised.
///     Whereas Mediators restrict the functionality of a service, Brokers decide what
///     functions are appropriate to use when certain events occur.
/// </summary>
public interface IEventBroker
{
    void Activate();
    void Deactivate();
}