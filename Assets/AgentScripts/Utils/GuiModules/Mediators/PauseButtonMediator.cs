public class PauseButtonMediator : IGuiElementMediator
{
    protected PauseButton _button;

    public PauseButtonMediator(PauseButton button)
    {
        _button = button;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowPauseButtonEvent>(OnShowPause);
        Globals.EventBus.Register<HidePauseButtonEvent>(OnHidePause);
        _button.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowPauseButtonEvent>(OnShowPause);
        Globals.EventBus.Unregister<HidePauseButtonEvent>(OnHidePause);
    }

    private void OnShowPause(ShowPauseButtonEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _button.name)
        {
            return;
        }

        if (!_button.PrimaryAnimator.Showing)
        {
            _button.PrimaryAnimator.Show();
            _button.SetInteractable(true);
        }
    }

    private void OnHidePause(HidePauseButtonEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _button.name)
        {
            return;
        }

        if (_button.PrimaryAnimator.Showing)
        {
            _button.PrimaryAnimator.Hide();
        }
    }
}