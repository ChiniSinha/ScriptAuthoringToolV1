using UnityEngine;

public class SimpleTableViewMediator : ITableViewMediator
{
    private readonly TableView _table;

    public SimpleTableViewMediator(TableView table)
    {
        _table = table;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowTableViewEvent>(OnShowTable);
        Globals.EventBus.Register<HideTableViewEvent>(OnHideTable);
        Globals.EventBus.Register<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Register<InteractableChangeEvent>(OnInteractableChange);
        _table.PrimaryAnimator.Hide();
        _table.MenuAnimator.Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowTableViewEvent>(OnShowTable);
        Globals.EventBus.Unregister<HideTableViewEvent>(OnHideTable);
        Globals.EventBus.Unregister<ClearUiEvent>(OnClearUi);
        Globals.EventBus.Unregister<InteractableChangeEvent>(OnInteractableChange);
    }

    private void OnClearUi(ClearUiEvent e)
    {
        Hide();
    }

    private void OnHideTable(HideTableViewEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _table.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowTable(ShowTableViewEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _table.name)
        {
            return;
        }
        string logString = "";
        for (int i = 0; i < e.Contents.Count; i++) {
            if (logString != "")
                logString += ",";
            logString += "[" + string.Join(",",e.Contents[i].ToArray()) + "]";
        }
        Debug.Log("Displaying text prompt: [" + logString + "]");
        _table.BoldTopRow = e.BoldTopRow;
        _table.SetupTable(e.Contents);
        _table.PrimaryAnimator.Show();
        if (_table.MenuAnimator.Showing)
        {
            _table.MenuAnimator.Hide(delegate { DoSetup(e); });
        }
        else
        {
            DoSetup(e);
        }
    }

    private void DoSetup(ShowTableViewEvent e)
    {
        _table.SetupMenu(e.MenuOptions);
        _table.MenuAnimator.Show();
        _table.SetInteractable(e.Interactable);
    }

    private void OnInteractableChange(InteractableChangeEvent e)
    {
        _table.SetInteractable(e.Interactable);
    }

    private void Hide()
    {
        if (_table.PrimaryAnimator.Showing)
        {
            _table.PrimaryAnimator.Hide();
        }
        if (_table.MenuAnimator.Showing)
        {
            _table.MenuAnimator.Hide();
        }
    }
}