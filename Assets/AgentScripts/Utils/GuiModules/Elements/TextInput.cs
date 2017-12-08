#region

using UnityEngine.UI;
using UnityEngine;

#endregion

public class TextInput : UIElementWithMenu
{
    private ITextInputMediator _mediator;
    public InputField InputText;
    public Text PromptText;

    protected void Awake()
    {
        _mediator = new SimpleTextInputMediator(this);
        _mediator.OnRegister();
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();
        InputText.interactable = false;
        string textInput = InputText.text;
        Debug.Log("Recieved text input: " + textInput);

        Globals.EventBus.Dispatch(new TextInputSubmitEvent(textInput, pressedIndex));
    }

    public override void SetInteractable(bool interactable)
    {
        base.SetInteractable(interactable);
        InputText.interactable = Interactable;
        if (Interactable)
        {
            InputText.Select();
            InputText.ActivateInputField();
        }
    }

    protected void OnDestroy()
    {
        _mediator.OnRemove();
    }
}