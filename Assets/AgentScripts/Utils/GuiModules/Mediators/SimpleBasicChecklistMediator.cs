using UnityEngine;

public class SimpleBasicChecklistMediator : IChecklistMediator
{
    private readonly BasicChecklist _checklist;

    public SimpleBasicChecklistMediator(BasicChecklist checklist)
    {
        _checklist = checklist;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowBasicChecklistEvent>(OnShow);
        Globals.EventBus.Register<HideBasicChecklistEvent>(OnHide);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _checklist.PrimaryAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowBasicChecklistEvent>(OnShow);
        Globals.EventBus.Unregister<HideBasicChecklistEvent>(OnHide);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void Hide()
    {
        if (_checklist.PrimaryAnimator.Showing)
        {
            _checklist.PrimaryAnimator.Hide();
        }
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _checklist.SetInteractable(e.Interactable);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnShow(ShowBasicChecklistEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _checklist.name)
        {
            return;
        }

        if (_checklist.PrimaryAnimator.Showing)
        {
            _checklist.PrimaryAnimator.Hide(delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowBasicChecklistEvent e)
    {
        Debug.Log("Displaying checkbox options: [" + string.Join(",",e.Choices.ToArray()) + "]");

        _checklist.ClearChecklist();

        _checklist.CheckLimit = e.Limit;
        _checklist.Label.text = e.Prompt;
        _checklist.CreateChecklist(e.Choices.ToArray());

        _checklist.SetupMenu(e.ControlButtons.ToArray());
        _checklist.PrimaryAnimator.Show();
        _checklist.SetInteractable(e.Interactable);
    }

    private void OnHide(HideBasicChecklistEvent e)
    {
        if (string.IsNullOrEmpty(e.ElementName) || e.ElementName == _checklist.name)
        {
            Hide();
        }
    }
}