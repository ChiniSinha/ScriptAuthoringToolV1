#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class ButtonScrollbar : Scrollbar
{
    public float ButtonAmount;

    public void ScrollPositive()
    {
        value = Mathf.Clamp01(value + ButtonAmount);
    }

    public void ScrollNegative()
    {
        value = Mathf.Clamp01(value - ButtonAmount);
    }
}