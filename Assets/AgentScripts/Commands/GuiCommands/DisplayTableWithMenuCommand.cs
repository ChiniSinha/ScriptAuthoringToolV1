#region

using System.Collections.Generic;

#endregion

public class DisplayTableWithMenuCommand : BaseCommand
{
    public DisplayTableWithMenuCommand(List<List<string>> tableContents, List<string> menuOptions, bool boldTopRow = false)
    {
        TableContents = tableContents;
        MenuOptions = menuOptions;
        BoldTopRow = boldTopRow;
    }

    public List<List<string>> TableContents { get; private set; }
    public List<string> MenuOptions { get; private set; }
    public bool BoldTopRow { get; private set; }

    public override void Execute()
    {
        InProgress = true;
        Globals.EventBus.Register<GuiAnimationEvent>(OnGuiEvent);
        Globals.EventBus.Dispatch(new ClearUiEvent());
        Globals.EventBus.Dispatch(new ShowTableViewEvent(TableContents, MenuOptions.ToArray(), BoldTopRow));
    }

    private void OnGuiEvent(GuiAnimationEvent e)
    {
        InProgress = false;
        Globals.EventBus.Unregister<GuiAnimationEvent>(OnGuiEvent);
    }
}