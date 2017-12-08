#region



#endregion

public class ProcessUILogicEvent : Event
{
    public ProcessUILogicEvent(UI ui)
    {
        Ui = ui;
    }

    public UI Ui { get; private set; }
}