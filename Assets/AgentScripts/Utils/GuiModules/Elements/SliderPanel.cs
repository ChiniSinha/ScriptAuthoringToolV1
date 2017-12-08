#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class SliderPanel : UIElementWithMenu
{
    protected float _increment;
    protected float _maxValue;
    private ISliderMediator _mediator;

    protected float _minValue;

    protected bool _snapToValue;

    public Text MaxValueText;
    public Text MinValueText;
    public Text PanelLabel;
    public Slider Slider;

    protected float Range
    {
        get { return Mathf.Abs(_maxValue - _minValue); }
    }

    public float Value
    {
        get
        {
            if (!_snapToValue)
            {
                return Slider.value;
            }

            return Slider.value*_increment + _minValue;
        }
    }

    protected virtual void Awake()
    {
        _mediator = new SimpleSliderMediator(this);
        _mediator.OnRegister();
    }

    public void ConfigureSlider(float minSliderValue, float maxSliderValue, float sliderResolution = 0)
    {
        _increment = sliderResolution;
        _minValue = minSliderValue;
        _maxValue = maxSliderValue;

        _snapToValue = _increment > 0;

        if (_snapToValue)
        {
            Slider.minValue = 0;
            Slider.maxValue = Range/_increment;
        }
        else
        {
            Slider.minValue = _minValue;
            Slider.maxValue = _maxValue;
        }
        Slider.wholeNumbers = _snapToValue;
        Slider.value = Slider.minValue;
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();
        Globals.EventBus.Dispatch(new SliderInputSubmitEvent(Value));
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}