#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class TieredChecklist : UIElementWithMenu
{
    private List<ChecklistItem> _checkedItems = new List<ChecklistItem>();
    private ITieredChecklistMediator _mediator;
    public int CheckLimit;
    public List<RectTransform> ColumnList = new List<RectTransform>();
    public Text[] HeaderPrefabs;

    public Transform ItemArea;
    public ChecklistItem ItemPrefab;
    public List<ChecklistItem> Items = new List<ChecklistItem>();
    public Text Label;
    public UiAnimator MenuAnimator;

    public RectTransform SectionPrefab;

    private void Awake()
    {
        _mediator = new SimpleTieredChecklistMediator(this);
        _mediator.OnRegister();
    }

    public void LoadData(NestedString data)
    {
        ClearChecklist();

        for (int i = 0; i < data.Children.Count; i++)
        {
            NestedString s = data.Children[i];
            RectTransform section = Instantiate(SectionPrefab);
            ColumnList.Add(section);
            section.SetParent(ItemArea);
            section.localScale = Vector3.one;
            CreateItems(s, section, 0);
        }
    }

    protected void CreateItems(NestedString s, Transform parent, int depth)
    {
        if (s.IsLeaf)
        {
            ChecklistItem item = Instantiate(ItemPrefab);
            item.Label.text = s.Value;
            item.transform.SetParent(parent);
            item.transform.localScale = Vector3.one;
            item.Toggle.isOn = false;
            if (CheckLimit > 0)
            {
                item.Toggle.onValueChanged.AddListener(delegate(bool val) { OnItemValueChange(item, val); });
            }
            Items.Add(item);
        }
        else
        {
            if (!string.IsNullOrEmpty(s.Value))
            {
                Text header = Instantiate(HeaderPrefabs[depth]);
                header.transform.SetParent(parent);
                header.transform.localScale = Vector3.one;
                header.text = s.Value;
            }

            for (int i = 0; i < s.Children.Count; i++)
            {
                NestedString child = s.Children[i];
                CreateItems(child, parent, depth + 1);
            }
        }
    }

    private void OnItemValueChange(ChecklistItem item, bool updatedValue)
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

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Toggle.isOn)
            {
                selectedIdxs.Add(i);
            }
        }

        Globals.EventBus.Dispatch(new CheckboxSubmitEvent(selectedIdxs.ToArray(), pressedIndex));
    }

    public void ClearChecklist()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Destroy(Items[i].gameObject);
        }
        Items.Clear();
        _checkedItems = new List<ChecklistItem>();

        for (int i = 0; i < ColumnList.Count; i++)
        {
            Destroy(ColumnList[i].gameObject);
        }
        ColumnList.Clear();
    }

    public override void SetInteractable(bool interactable)
    {
        base.SetInteractable(interactable);

        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].SetInteractable(Interactable);
        }
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}