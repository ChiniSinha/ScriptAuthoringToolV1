#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class ChecklistItem : UIElement
{
    public Text Label;
    public Selectable SelectButton;
    public Toggle Toggle;

    public override void SetInteractable(bool interactable)
    {
        Toggle.interactable = interactable;
        Color textColor = Label.color;
        textColor.a = interactable ? 255f : 128f;
        Label.color = textColor;
        SelectButton.interactable = interactable;
    }

    public virtual void OnClick()
    {
        Toggle.isOn = !Toggle.isOn;
    }
}