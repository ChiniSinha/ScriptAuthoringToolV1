#region

using System.Collections.Generic;

#endregion
using System;

public class DisplayWhiteboardCommand : BaseCommand
{
    public DisplayWhiteboardCommand(List<List<string>> tableContents,string url = "", bool boldTopRow = false)
    {
        TableContents = tableContents;
        BoldTopRow = boldTopRow;
        Url = url;
    }

    public List<List<string>> TableContents { get; private set; }
    public bool BoldTopRow { get; private set; }
    public string Url { get; private set; }

    public override void Execute()
    {
        if (Url.Equals("HIDE"))
        {
            Globals.EventBus.Dispatch(new HideWhiteboardEvent());
        }
        else
        {
            Globals.EventBus.Dispatch(new DisplayWhiteboardEvent(TableContents, Url, BoldTopRow));
        }
    }

}