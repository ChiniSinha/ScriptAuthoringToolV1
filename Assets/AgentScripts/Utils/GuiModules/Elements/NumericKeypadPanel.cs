#region

using System.Collections.Generic;
using UnityEngine.UI;

#endregion

public class NumericKeypadPanel : UIElementWithMenu
{
    private IKeypadMediator _mediator;
    public List<Button> InputButtons;
    public Text PromptText;
    public InputField ValueBox;

    protected virtual void Awake()
    {
        _mediator = new SimpleKeypadMediator(this);
        _mediator.OnRegister();
    }

    public void OnNumberPress(int num)
    {
        ValueBox.text += num.ToString();
    }

    public void OnBackspacePress()
    {
        string currentText = ValueBox.text;
        ValueBox.text = currentText.Substring(0, currentText.Length - 1);
    }

    public void OnClearPress()
    {
        ValueBox.text = "";
    }

    public override void SetInteractable(bool interactable)
    {
        base.SetInteractable(interactable);
        ValueBox.interactable = Interactable;
        for (int i = 0; i < InputButtons.Count; i++)
        {
            InputButtons[i].interactable = Interactable;
        }
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();
        string currentValue = ValueBox.text;
        int userValue = currentValue.ParseInt();

        Globals.EventBus.Dispatch(new NumberKeypadSubmitEvent(userValue, pressedIndex));
        SetInteractable(false);
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}