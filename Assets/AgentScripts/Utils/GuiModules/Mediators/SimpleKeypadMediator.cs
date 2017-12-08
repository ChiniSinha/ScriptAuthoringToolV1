public class SimpleKeypadMediator : IKeypadMediator
{
    private readonly NumericKeypadPanel _panel;

    public SimpleKeypadMediator(NumericKeypadPanel panel)
    {
        _panel = panel;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowNumberKeypadEvent>(OnShowMenu);
        Globals.EventBus.Register<HideNumberKeypadEvent>(OnHideMenu);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _panel.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowNumberKeypadEvent>(OnShowMenu);
        Globals.EventBus.Unregister<HideNumberKeypadEvent>(OnHideMenu);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void Hide()
    {
        if (_panel.PrimaryAnimator.Showing)
        {
            _panel.PrimaryAnimator.Hide();
        }
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHideMenu(HideNumberKeypadEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _panel.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowMenu(ShowNumberKeypadEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _panel.name)
        {
            return;
        }
        if (_panel.PrimaryAnimator.Showing)
        {
            _panel.PrimaryAnimator.Hide(delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowNumberKeypadEvent e)
    {
        _panel.PromptText.text = e.Prompt;
        _panel.ValueBox.text = "";
        _panel.SetupMenu(e.ControlButtonChoices);
        _panel.PrimaryAnimator.Show();
        _panel.SetInteractable(e.Interactable);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _panel.SetInteractable(e.Interactable);
    }
}