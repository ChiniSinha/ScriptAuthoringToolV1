public class SimpleTieredChecklistMediator : ITieredChecklistMediator
{
    private readonly TieredChecklist _checklist;

    public SimpleTieredChecklistMediator(TieredChecklist checklist)
    {
        _checklist = checklist;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowTieredChecklistEvent>(OnShow);
        Globals.EventBus.Register<HideTieredChecklistEvent>(OnHide);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _checklist.PrimaryAnimator.Hide();
        _checklist.MenuAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowTieredChecklistEvent>(OnShow);
        Globals.EventBus.Unregister<HideTieredChecklistEvent>(OnHide);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _checklist.SetInteractable(e.Interactable);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHide(HideTieredChecklistEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _checklist.name)
        {
            return;
        }
        Hide();
    }

    private void OnShow(ShowTieredChecklistEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _checklist.name)
        {
            return;
        }

        _checklist.Label.text = e.Label;
        _checklist.CheckLimit = e.Limit;
        _checklist.LoadData(e.Data);
        _checklist.SetupMenu(e.ButtonTexts);

        _checklist.PrimaryAnimator.Show();
        _checklist.MenuAnimator.Show();
        _checklist.SetInteractable(e.Interactable);
    }

    private void Hide()
    {
        if (_checklist.PrimaryAnimator.Showing)
        {
            _checklist.PrimaryAnimator.Hide();
        }
        if (_checklist.MenuAnimator.Showing)
        {
            _checklist.MenuAnimator.Hide();
        }
    }
}