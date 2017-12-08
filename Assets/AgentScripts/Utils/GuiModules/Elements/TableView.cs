#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class TableView : UIElementWithMenu
{
    private ITableViewMediator _mediator;
    public bool BoldTopRow;
    public TableDisplayRow DisplayRow;
    public UiAnimator MenuAnimator;
    public List<TableDisplayRow> Rows = new List<TableDisplayRow>();
    public RectTransform TableSection;

    protected void Awake()
    {
        _mediator = new SimpleTableViewMediator(this);
        _mediator.OnRegister();
    }

    public void SetupTable(List<List<string>> contents)
    {
        for (int i = 0; i < Rows.Count; i++)
        {
            Destroy(Rows[i].gameObject);
        }

        Rows.Clear();


        for (int i = 0; i < contents.Count; i++)
        {
            TableDisplayRow row = Instantiate(DisplayRow);
            row.SetCellCount(contents[i].Count);
            for (int j = 0; j < contents[i].Count; j++)
            {
                string cellContent = contents[i][j];
                cellContent = cellContent.Replace("\\n", "\n");
                row.Cells[j].TextDisplay.text = cellContent;
                if (BoldTopRow && i == 0)
                {
                    row.Cells[j].TextDisplay.fontStyle = FontStyle.Bold;
                    row.Cells[j].TextDisplay.fontSize = row.Cells[j].TextDisplay.fontSize + 2;
                }
            }
            row.transform.SetParent(TableSection);
            row.transform.localScale = Vector3.one;
            row.transform.localRotation = Quaternion.identity;

            RectTransform rectTransform = row.transform as RectTransform;
            if (rectTransform != null)
            {
                Vector3 rowPosition = rectTransform.anchoredPosition3D;
                rowPosition.z = 0;
                rectTransform.anchoredPosition3D = rowPosition;
            }

            Rows.Add(row);
        }
    }

    protected override void DoButtonPressed(int pressedIndex)
    {
        DisableButtons();
        //TODO: Figure out how to get button text here
        Debug.Log("Selecting menu option: [" + pressedIndex + "]");
        Globals.EventBus.Dispatch(new TableDisplayInputEvent(pressedIndex));
    }

    protected void OnDestroy()
    {
        _mediator.OnRemove();
    }
}