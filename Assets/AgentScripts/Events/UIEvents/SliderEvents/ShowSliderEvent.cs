public class ShowSliderEvent : Event
{
    public ShowSliderEvent(string label, string[] buttonTexts, string minValueText, string maxValueText,
        float minSliderValue,
        float maxSliderValue, float sliderResolution = 0, string elementName = "", bool interactable = true)
    {
        Label = label;
        ButtonTexts = buttonTexts;
        MinValueText = minValueText;
        MaxValueText = maxValueText;
        MinSliderValue = minSliderValue;
        MaxSliderValue = maxSliderValue;
        SliderResolution = sliderResolution;
        ElementName = elementName;
        Interactable = interactable;
    }

    public string Label { get; private set; }
    public string[] ButtonTexts { get; private set; }
    public string MinValueText { get; private set; }
    public string MaxValueText { get; private set; }
    public float MinSliderValue { get; private set; }
    public float MaxSliderValue { get; private set; }
    public float SliderResolution { get; private set; }
    public string ElementName { get; private set; }
    public bool Interactable { get; private set; }
}