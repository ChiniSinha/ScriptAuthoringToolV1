#region



#endregion

public class ProcessUILogicEvent : RagEvent
{
    public ProcessUILogicEvent(UI ui)
    {
        Ui = ui;
    }

    public UI Ui { get; private set; }
}