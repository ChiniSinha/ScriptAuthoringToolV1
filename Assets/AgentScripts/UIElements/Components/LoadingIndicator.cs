using UnityEngine.UI;

public class LoadingIndicator : UIElement
{
    protected int _dotCount;
    public string BaseString;
    public Text TextObj;

    // Update is called once per frame
    private void Update()
    {
        _dotCount = (_dotCount + 1)%4;

        TextObj.text = BaseString;
        for (int i = 0; i < _dotCount; i++)
        {
            TextObj.text += ".";
        }
    }
}