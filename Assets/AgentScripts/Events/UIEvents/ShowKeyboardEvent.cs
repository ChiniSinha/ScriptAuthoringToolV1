public class ShowKeyboardEvent : Event
{
    public ShowKeyboardEvent(string[] controlButtonChoices, string prompt = "", string elementName = "",
        bool interactable = true)
    {
        Prompt = prompt;
        ControlButtonChoices = controlButtonChoices;
        ElementName = elementName;
        Interactable = interactable;
    }

    public string Prompt { get; private set; }
    public string[] ControlButtonChoices { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
}