#region



#endregion

public class MenuPanel : UIElementWithMenu
{
    private IMenuMediator _mediator;

    protected virtual void Awake()
    {
        _mediator = new SimpleMenuMediator(this);
        _mediator.OnRegister();
    }

    // create user menu buttons
    public void CreateMenuButtons(string[] menuChoices)
    {
        SetupMenu(menuChoices);
    }

    protected override void DoButtonPressed(int pressedIdx)
    {
        DisableButtons();
        Globals.EventBus.Dispatch(new MenuSelectedEvent(pressedIdx));
    }

    protected void OnDestroy()
    {
        _mediator.OnRemove();
    }
}