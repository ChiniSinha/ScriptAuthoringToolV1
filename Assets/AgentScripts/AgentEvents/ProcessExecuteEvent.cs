#region



#endregion

public class ProcessExecuteEvent : Event
{
    public ProcessExecuteEvent(string command)
    {
        Command = command;
    }

    public string Command { get; private set; }
}