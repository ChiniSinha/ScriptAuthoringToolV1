#region



#endregion

public class ShowMenuEvent : Event
{
    public ShowMenuEvent(string[] options, string prompt = "", string elementName = "", bool interactable = true)
    {
        Options = options;
        Prompt = prompt;
        ElementName = elementName;
        Interactable = interactable;
    }

    public string[] Options { get; private set; }
    public string Prompt { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
}