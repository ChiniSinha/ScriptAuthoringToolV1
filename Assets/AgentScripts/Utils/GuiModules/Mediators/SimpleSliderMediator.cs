public class SimpleSliderMediator : ISliderMediator
{
    private readonly SliderPanel _slider;

    public SimpleSliderMediator(SliderPanel slider)
    {
        _slider = slider;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowSliderEvent>(OnShowSlider);
        Globals.EventBus.Register<HideSliderEvent>(OnHideSlider);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _slider.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowSliderEvent>(OnShowSlider);
        Globals.EventBus.Unregister<HideSliderEvent>(OnHideSlider);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _slider.SetInteractable(e.Interactable);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHideSlider(HideSliderEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _slider.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowSlider(ShowSliderEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _slider.name)
        {
            return;
        }
        if (_slider.PrimaryAnimator.Showing)
        {
            _slider.PrimaryAnimator.Hide(delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowSliderEvent e)
    {
        _slider.PanelLabel.text = e.Label;
        _slider.MinValueText.text = e.MinValueText;
        _slider.MaxValueText.text = e.MaxValueText;

        _slider.ConfigureSlider(e.MinSliderValue, e.MaxSliderValue, e.SliderResolution);

        _slider.SetupMenu(e.ButtonTexts);
        _slider.PrimaryAnimator.Show();
        _slider.SetInteractable(e.Interactable);
    }

    private void Hide()
    {
        if (_slider.PrimaryAnimator.Showing)
        {
            _slider.PrimaryAnimator.Hide();
        }
    }
}