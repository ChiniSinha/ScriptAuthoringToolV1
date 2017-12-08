#region

using System.Collections.Generic;

#endregion

public class ShowTableViewEvent : Event
{
    public ShowTableViewEvent(List<List<string>> contents, string[] menuOptions, bool boldTopRow = false,
        string elementName = "", bool interactable = true)
    {
        Contents = contents;
        MenuOptions = menuOptions;
        ElementName = elementName;
        Interactable = interactable;
        BoldTopRow = boldTopRow;
    }

    public List<List<string>> Contents { get; private set; }
    public string[] MenuOptions { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
    public bool BoldTopRow { get; private set; }
}