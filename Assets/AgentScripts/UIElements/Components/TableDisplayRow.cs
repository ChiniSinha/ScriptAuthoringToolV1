#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class TableDisplayRow : MonoBehaviour
{
    public GameObject cellReference;
    public List<TextReference> Cells;

    public void SetCellCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject row = Instantiate(cellReference);
            row.transform.SetParent(transform);
            TextReference textRef = row.GetComponent<TextReference>();
            Cells.Add(textRef);
        }
    }
}