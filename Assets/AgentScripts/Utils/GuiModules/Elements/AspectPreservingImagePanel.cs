#region

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class AspectPreservingImagePanel : ImageDisplay
{
    private IImageDisplayMediator _mediator;

    public Image Image;

    private void Awake()
    {
        _mediator = new SimpleImageMediator(this);
        _mediator.OnRegister();
    }

    protected override IEnumerator DoLoad(string url)
    {
        url = url.Replace('\\', '/');
        WWW www = Globals.Get<ResourceLoader>().GetImageLoader(url);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("Error loading AspectPreservingImagePanel: " );
        }

        SetImage(www.texture);
        Image.gameObject.SetActive(true);
    }

    public override void SetImage(Texture2D tex)
    {
        Image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        Image.type = Image.Type.Simple;
        Image.preserveAspect = true;
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}