public class ShowTieredChecklistEvent : Event
{
    public ShowTieredChecklistEvent(NestedString data, string[] buttonTexts, string label = "", int limit = 0,
        string elementName = "", bool interactable = true)
    {
        Data = data;
        ButtonTexts = buttonTexts;
        Label = label;
        Limit = limit;
        ElementName = elementName;
        Interactable = interactable;
    }

    public NestedString Data { get; private set; }
    public string[] ButtonTexts { get; private set; }
    public string Label { get; private set; }
    public int Limit { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
}