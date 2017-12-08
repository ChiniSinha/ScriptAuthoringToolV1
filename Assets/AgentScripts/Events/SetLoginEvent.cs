public class SetLoginEvent : Event
{
    public SetLoginEvent(string user, string pass)
    {
        UserName = user;
        Password = pass;
    }

    public string UserName { get; protected set; }
    public string Password { get; protected set; }
}