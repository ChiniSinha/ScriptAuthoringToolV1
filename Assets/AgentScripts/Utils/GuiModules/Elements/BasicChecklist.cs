#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class BasicChecklist : UIElementWithMenu
{
    protected List<ChecklistItem> _checkedItems = new List<ChecklistItem>();
    private IChecklistMediator _mediator;
    public int CheckLimit;

    public Transform CheckListArea;
    public ChecklistItem ChecklistItemPrefab;
    public List<ChecklistItem> ChecklistItems = new List<ChecklistItem>();

    public Text Label;

    private void Awake()
    {
        _mediator = new SimpleBasicChecklistMediator(this);
        _mediator.OnRegister();
    }

    public void CreateChecklist(string[] choices)
    {
        
        for (int i = 0; i < choices.Length; i++)
        {
            ChecklistItem item = Instantiate(ChecklistItemPrefab);
            item.Label.text = choices[i].ToString();
            item.Toggle.isOn = false;
            if (CheckLimit > 0)
            {
                item.Toggle.onValueChanged.AddListener(delegate(bool val) { OnItemChecked(item, val); });
            }

            item.SetInteractable(Interactable);

            ChecklistItems.Add(item);
            item.transform.SetParent(CheckListArea);
            item.transform.localScale = Vector3.one;
        }
    }

    private void OnItemChecked(ChecklistItem item, bool updatedValue)
    {
        if (updatedValue)
        {
            _checkedItems.Add(item);
        }
        else
        {
            _checkedItems.Remove(item);
        }
        if (CheckLimit > 0)
        {
            while (_checkedItems.Count > CheckLimit)
            {
                _checkedItems[0].Toggle.isOn = false;
            }
        }
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();
        List<int> selectedIdxs = new List<int>();

        string logString = "";
        for (int i = 0; i < ChecklistItems.Count; i++)
        {
            if (ChecklistItems[i].Toggle.isOn)
            {
                selectedIdxs.Add(i);
                if (logString != "")
                    logString += ",";
                logString += ChecklistItems[i].Label.text;
            }
        }
        Debug.Log("Selected checkbox options: [" + logString + "]");

        Globals.EventBus.Dispatch(new CheckboxSubmitEvent(selectedIdxs.ToArray(), pressedIndex));
    }

    public void ClearChecklist()
    {
        for (int i = 0; i < ChecklistItems.Count; i++)
        {
            Destroy(ChecklistItems[i].gameObject);
        }
        ChecklistItems.Clear();
        _checkedItems = new List<ChecklistItem>();
    }

    public override void SetInteractable(bool interactable)
    {
        base.SetInteractable(interactable);
        for (int i = 0; i < ChecklistItems.Count; i++)
        {
            ChecklistItems[i].SetInteractable(Interactable);
        }
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}