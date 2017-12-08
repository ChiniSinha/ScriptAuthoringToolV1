using UnityEngine;
using UnityEngine.UI;

public class PauseButton : UIElement
{
    protected IGuiElementMediator _mediator;

    [SerializeField]
    protected Button _button;

    private void Awake()
    {
        _mediator = new PauseButtonMediator(this);
        _mediator.OnRegister();
    }

    public override void SetInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }

    public void TogglePause()
    {
        Globals.EventBus.Dispatch(new PauseSelectedEvent());
        SetInteractable(!_button.interactable);
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}