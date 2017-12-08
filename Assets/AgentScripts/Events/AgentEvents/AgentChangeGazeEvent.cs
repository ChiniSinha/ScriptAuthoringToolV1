public class AgentChangeGazeEvent : Event
{
    public AgentChangeGazeEvent(string target)
    {
        Target = target;
    }

    public string Target { get; private set; }
}