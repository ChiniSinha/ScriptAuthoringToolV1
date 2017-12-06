#region



#endregion

public class ProcessExecuteEvent : RagEvent
{
    public ProcessExecuteEvent(string command)
    {
        Command = command;
    }

    public string Command { get; private set; }
}