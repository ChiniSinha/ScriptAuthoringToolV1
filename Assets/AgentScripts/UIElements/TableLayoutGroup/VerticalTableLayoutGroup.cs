using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VerticalTableLayoutGroup : LayoutGroup
{
    private readonly List<float> _columnMinimums = new List<float>();
    private readonly List<float> _columnWidths = new List<float>();
    private readonly List<float> _rowHeights = new List<float>();

    public bool LastColumnFill;

    public bool Dirty { get; protected set; }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        float tableWidth = rectTransform.rect.width - padding.horizontal;
        RectTransform row;
        RectTransform cell;

        _columnWidths.Clear();
        _columnMinimums.Clear();
        float totalCellPreferred = 0;
        float columnWidth;
        float prefWidth;
        float minCellWidth;
        float totalMinCellWidth = 0;
        float combinedPadding = padding.horizontal;

        float totalMin = 0;
        float totalFlex = 0;
        float totalPref = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            row = rectChildren[i];
            float currentMin;
            for (int j = 0; j < row.childCount; j++)
            {
                cell = (RectTransform) row.GetChild(j);
                if (_columnWidths.Count <= j)
                {
                    _columnWidths.Add(0);
                }
                if (_columnMinimums.Count <= j)
                {
                    _columnMinimums.Add(0);
                }

                minCellWidth = LayoutUtility.GetMinWidth(cell);
                currentMin = _columnMinimums[j];
                if (currentMin < minCellWidth)
                {
                    totalMinCellWidth -= currentMin;
                    totalMinCellWidth += minCellWidth;
                    _columnMinimums[j] = minCellWidth;
                }
                totalMin = Mathf.Max(minCellWidth + combinedPadding, totalMin);

                // If this is an interior column and it fills to the end, don't worry about its preferred width
                prefWidth = LayoutUtility.GetPreferredWidth(cell);
                if (!(LastColumnFill && i == row.childCount - 1 && row.childCount != _columnWidths.Count))
                {
                    columnWidth = _columnWidths[j];
                    if (columnWidth < prefWidth)
                    {
                        totalCellPreferred -= columnWidth;
                        totalCellPreferred += prefWidth;
                        _columnWidths[j] = prefWidth;
                    }
                }
                totalPref = Mathf.Max(prefWidth + combinedPadding, totalPref);

                totalFlex = Mathf.Max(LayoutUtility.GetFlexibleWidth(cell) + combinedPadding, totalFlex);
            }
        }

        // Nobody has a preference, so even them all out
        if (totalCellPreferred == 0 && totalMinCellWidth == 0)
        {
            totalCellPreferred = tableWidth;
            float average = totalCellPreferred/_columnWidths.Count;
            for (int i = 0; i < _columnWidths.Count; i++)
            {
                _columnWidths[i] = average;
                _columnMinimums[i] = average;
            }
        }

        float reservedWidth = 0;
        float totalMassaged = 0;
        // Normalize prefered widths and pay attention to minimums
        float calcW, min;
        for (int i = 0; i < _columnWidths.Count; i++)
        {
            min = _columnMinimums[i];
            calcW = _columnWidths[i]*tableWidth/totalCellPreferred;
            if (calcW < min)
            {
                calcW = min;
                reservedWidth += calcW;
            }
            else
            {
                totalMassaged += calcW;
            }
            _columnWidths[i] = calcW;
        }

        float freeSpace = tableWidth - reservedWidth;
        for (int i = 0; i < _columnWidths.Count; i++)
        {
            _columnWidths[i] = _columnWidths[i]*freeSpace/totalMassaged;
        }

        SetLayoutInputForAxis(totalMin, totalPref, totalFlex, 0);
    }


    public override void CalculateLayoutInputVertical()
    {
        float totalPreferred = padding.vertical;
        float totalMin = padding.vertical;
        float totalFlexible = padding.vertical;
        RectTransform row;
        RectTransform cell;

        _rowHeights.Clear();
        float totalRowHeight = 0;
        float maxPreferredHeight;
        float maxMinHeight;
        float maxFlexibleHeight;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            row = rectChildren[i];
            maxPreferredHeight = 0;
            maxMinHeight = 0;
            maxFlexibleHeight = 0;
            for (int j = 0; j < row.childCount; j++)
            {
                cell = (RectTransform) row.GetChild(j);

                float pHeight = LayoutUtility.GetPreferredHeight(cell);
                if (maxPreferredHeight < pHeight)
                {
                    maxPreferredHeight = pHeight;
                }
                float mHeight = LayoutUtility.GetMinHeight(cell);
                if (maxMinHeight < mHeight)
                {
                    maxMinHeight = mHeight;
                }
                float fHeight = LayoutUtility.GetFlexibleHeight(cell);
                if (maxFlexibleHeight < fHeight)
                {
                    maxFlexibleHeight = fHeight;
                }
            }
            totalPreferred += maxPreferredHeight;
            totalMin += maxMinHeight;
            totalFlexible += maxFlexibleHeight;
            _rowHeights.Add(maxPreferredHeight);
            totalRowHeight += maxPreferredHeight;
        }
        SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, 1);

        for (int i = 0; i < _rowHeights.Count; i++)
        {
            _rowHeights[i] /= totalRowHeight;
        }
    }


    public override void SetLayoutHorizontal()
    {
        float width = rectTransform.rect.width - padding.horizontal;
        RectTransform row;
        RectTransform cell;
        float xPos;
        float cellWidth;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            row = rectChildren[i];
            SetChildAlongAxis(row, 0, padding.left, width);
            xPos = 0;
            for (int j = 0; j < row.childCount; j++)
            {
                cell = (RectTransform) row.GetChild(j);
                cell.pivot = Vector2.up;

                if (LastColumnFill && j == row.childCount - 1)
                {
                    cellWidth = width - xPos;
                }
                else
                {
                    cellWidth = _columnWidths[j];
                }
                SetChildAlongAxis(cell, 0, xPos, cellWidth);
                xPos += cellWidth;
            }
        }
    }

    public override void SetLayoutVertical()
    {
        float totalHeight = rectTransform.rect.height - padding.vertical;
        RectTransform row;
        RectTransform cell;
        float yPos = padding.top;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            row = rectChildren[i];
            float height = 0;
            if (i < _rowHeights.Count)
            {
                height = _rowHeights[i]*totalHeight;
            }
            SetChildAlongAxis(row, 1, yPos, height);
            for (int j = 0; j < row.childCount; j++)
            {
                cell = (RectTransform) row.GetChild(j);
                cell.pivot = Vector2.up;
                SetChildAlongAxis(cell, 1, 0, height);
            }
            yPos += height;
        }
    }
}