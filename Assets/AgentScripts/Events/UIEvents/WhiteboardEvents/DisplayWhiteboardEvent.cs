#region

using System.Collections.Generic;

#endregion

public class DisplayWhiteboardEvent : Event
{
    public DisplayWhiteboardEvent(List<List<string>> contents, string url, bool boldTopRow = false)
    {
        Contents = contents;
        Url = url;
        BoldTopRow = boldTopRow;
    }

    public List<List<string>> Contents { get; private set; }
    public bool BoldTopRow { get; private set; }
    public string Url { get; private set; }
}