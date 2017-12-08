#region

using System.Collections.Generic;

#endregion

public class ShowBasicChecklistEvent : Event
{
    public ShowBasicChecklistEvent(List<string> choices, List<string> controlButtons, string prompt = "", int limit = 0,
        string elementName = "", bool interactable = true)
    {
        Choices = choices;
        ControlButtons = controlButtons;
        Prompt = prompt;
        Limit = limit;
        ElementName = elementName;
        Interactable = interactable;
    }

    public List<string> Choices { get; private set; }
    public List<string> ControlButtons { get; private set; }
    public string Prompt { get; private set; }
    public int Limit { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
}