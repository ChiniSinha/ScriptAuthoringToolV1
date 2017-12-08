#region

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class ZoomableImagePanel : ImageDisplay
{
    private static readonly float scaleUnit = 0.2f;
    private float _defaultScale = 1.0f;
    private IImageDisplayMediator _mediator;

    private float _scaleFactor = 1.0f;
    public RawImage Image;

    public ScrollRect ScrollRect;

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

        ResetZoom();
    }

    /// <summary>
    ///     Zooms in the picture
    /// </summary>
    public void ZoomIn()
    {
        _scaleFactor += scaleUnit;
        ScalePage();
    }

    /// <summary>
    ///     Zooms out the picture
    /// </summary>
    public void ZoomOut()
    {
        _scaleFactor -= scaleUnit;
        ScalePage();
    }

    /// <summary>
    ///     Zooms in the picture
    /// </summary>
    public void ResetZoom()
    {
        _scaleFactor = _defaultScale;
        ScalePage();
    }


    private void ScalePage()
    {
        Image.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
    }

    // TA: WTF
    public void RightButtonClicked()
    {
        ScrollRect.horizontalScrollbar.value = Mathf.Clamp01(ScrollRect.horizontalScrollbar.value + 0.2f);
    }

    public void LeftButtonClicked()
    {
        ScrollRect.horizontalScrollbar.value = Mathf.Clamp01(ScrollRect.horizontalScrollbar.value - 0.2f);
    }

    public void UpButtonClicked()
    {
        ScrollRect.verticalScrollbar.value = Mathf.Clamp01(ScrollRect.verticalScrollbar.value + 0.2f);
    }

    public void DownButtonClicked()
    {
        ScrollRect.verticalScrollbar.value = Mathf.Clamp01(ScrollRect.verticalScrollbar.value - 0.2f);
    }

    public void SetZoom(string zoomType)
    {
        switch (zoomType.ToLower())
        {
            case "in":
                ZoomIn();
                break;
            case "out":
                ZoomOut();
                break;
            case "default":
                _scaleFactor = 1.0f;
                ScalePage();
                break;
            default:
                _scaleFactor = 1.0f;
                ScalePage();
                break;
        }
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}