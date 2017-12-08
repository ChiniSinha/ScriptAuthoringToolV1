public class SimpleKeyboardMediator : IGuiElementMediator
{
    private readonly SoftKeyboard _keyboard;

    public SimpleKeyboardMediator(SoftKeyboard keyboard)
    {
        _keyboard = keyboard;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowKeyboardEvent>(OnShowKeyboard);
        Globals.EventBus.Register<HideKeyboardEvent>(OnHideKeyboard);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _keyboard.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowKeyboardEvent>(OnShowKeyboard);
        Globals.EventBus.Unregister<HideKeyboardEvent>(OnHideKeyboard);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void Hide()
    {
        if (_keyboard.PrimaryAnimator.Showing)
        {
            _keyboard.PrimaryAnimator.Hide();
        }
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHideKeyboard(HideKeyboardEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _keyboard.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowKeyboard(ShowKeyboardEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _keyboard.name)
        {
            return;
        }
        if (_keyboard.PrimaryAnimator.Showing)
        {
            _keyboard.PrimaryAnimator.Hide(delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowKeyboardEvent e)
    {
        _keyboard.Prompt.text = e.Prompt;
        _keyboard.SetupMenu(e.ControlButtonChoices);
        _keyboard.PrimaryAnimator.Show();
        _keyboard.SetInteractable(e.Interactable);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _keyboard.SetInteractable(e.Interactable);
    }
}