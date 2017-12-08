#region

using System.Collections;
using UnityEngine;

#endregion

public abstract class ImageDisplay : UIElement
{
    public void LoadImage(string url)
    {
        gameObject.SetActive(true);
        StartCoroutine(DoLoad(url));
    }

    protected abstract IEnumerator DoLoad(string url);

    public abstract void SetImage(Texture2D tex);
}