using UnityEngine;

public class SimpleTextInputMediator : ITextInputMediator
{
    private readonly TextInput _textInput;

    public SimpleTextInputMediator(TextInput textInput)
    {
        _textInput = textInput;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowTextInputEvent>(OnShowInput);
        Globals.EventBus.Register<HideTextInputEvent>(OnHideMenu);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _textInput.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowTextInputEvent>(OnShowInput);
        Globals.EventBus.Unregister<HideTextInputEvent>(OnHideMenu);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _textInput.SetInteractable(e.Interactable);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHideMenu(HideTextInputEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _textInput.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowInput(ShowTextInputEvent e)
    {
        Debug.Log("Displaying text prompt: " + _textInput.PromptText.text);
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _textInput.name)
        {
            return;
        }
        if (_textInput.PrimaryAnimator.Showing)
        {
            _textInput.PrimaryAnimator.Hide(
                delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowTextInputEvent e)
    {
        _textInput.PromptText.text = e.Prompt;
        _textInput.InputText.text = "";
        _textInput.SetupMenu(e.ControlButtonChoices);
        _textInput.PrimaryAnimator.Show();
        _textInput.SetInteractable(e.Interactable);
    }

    private void Hide()
    {
        if (_textInput.PrimaryAnimator.Showing)
        {
            _textInput.PrimaryAnimator.Hide();
        }
    }
}