public class SetReportEvent : Event
{
    public SetReportEvent(string bugType, string bugMessage)
    {
        BugType = bugType;
        BugMessage = bugMessage;
    }

    public string BugType { get; protected set; }
    public string BugMessage { get; protected set; }
}