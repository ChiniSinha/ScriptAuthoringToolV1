#region

using UnityEngine.UI;

#endregion

public class MenuButton : Button
{
    public Text ButtonText;

    public bool Displaying
    {
        get { return ButtonText.enabled && targetGraphic.enabled; }
        set
        {
            ButtonText.enabled = value;
            targetGraphic.enabled = value;
        }
    }
}