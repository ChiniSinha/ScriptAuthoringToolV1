public class ExternalProcessEvent : Event
{
    public enum ProcessStatus
    {
        STARTED,
        COMPLETED,
        FAILED
    }

    public ExternalProcessEvent(ProcessStatus status)
    {
        Status = status;
    }

    public ProcessStatus Status { get; protected set; }
}