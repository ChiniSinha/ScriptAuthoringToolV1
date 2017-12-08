#region

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#endregion
using System.Collections.Generic;

public class WhiteboardView : ImageDisplay
{
    private IWhiteboardDisplayMediator _mediator;
    public bool BoldTopRow;
    public TableDisplayRow DisplayRow;
    public List<TableDisplayRow> Rows = new List<TableDisplayRow>();
    public RectTransform TableSection;

    private void Awake()
    {
        _mediator = new SimpleWhiteboardDisplayMediator(this);
        _mediator.OnRegister();
    }

    public void SetupTable(List<List<string>> contents)
    {
        ClearTable();

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

    public void ClearTable()
    {
        for (int i = 0; i < Rows.Count; i++)
        {
            Destroy(Rows[i].gameObject);
        }

        Rows.Clear();
    }

    private static readonly float scaleUnit = 0.2f;
    private float _defaultScale = 1.0f;

    private float _scaleFactor = 1.0f;
    public RawImage Image;

    public ScrollRect ScrollRect;

    protected override IEnumerator DoLoad(string url)
    {
        url = url.Replace('\\', '/');
        WWW www = Globals.Get<ResourceLoader>().GetImageLoader(url);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("Error loading ScrollableImagePanel: ");
        }

        SetImage(www.texture);
    }

    public override void SetImage(Texture2D tex)
    {
        Image.texture = tex;
        Vector2 size = ScrollRect.viewport.rect.size;
        _defaultScale = Mathf.Min(Mathf.Abs(size.x/tex.width), Mathf.Abs(size.y/tex.height));
        Image.SetNativeSize();
        Image.enabled = true;
        
    }

    public void Hide()
    {
        Image.enabled = false;
        ClearTable();
    }
    
    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}