using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PinchZoomImagePanel : ImageDisplay
{
    private Vector2 _lastMousePos;
    private IImageDisplayMediator _mediator;

    [SerializeField]
    private Vector2 _pin;

    private bool _pinned;
    private bool _touchedLastFrame;
    public Image Image;

    public bool Debug;
    private Vector2 _imageResetPosition;

    private void Awake()
    {
        _mediator = new SimpleImageMediator(this);
        _mediator.OnRegister();
        _imageResetPosition = Image.rectTransform.anchoredPosition;
    }

    protected override IEnumerator DoLoad(string url)
    {
        url = url.Replace('\\', '/');
        WWW www = Globals.Get<ResourceLoader>().GetImageLoader(url);
        yield return www;
        if (www.error != null)
        {
            //Debug.Log("Error loading AspectPreservingImagePanel ");
        }

        SetImage(www.texture);
        Image.gameObject.SetActive(true);
    }

    public override void SetImage(Texture2D tex)
    {
        Image.transform.localScale = Vector3.one;
        _imageResetPosition = Vector2.zero;
        Image.rectTransform.anchoredPosition = _imageResetPosition;

        Image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        Image.type = Image.Type.Simple;
        Image.preserveAspect = true;
        Image.SetNativeSize();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _pinned = true;
                _pin = Input.mousePosition;
            }
            else if (Input.GetMouseButtonDown(2))
            {
                _pinned = false;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (_pinned)
                    {
                        if (_lastMousePos != Vector2.zero)
                        {
                            UpdateZoom(_pin, Input.mousePosition, Vector2.zero,
                                (Vector2) Input.mousePosition - _lastMousePos);
                        }
                    }
                    else
                    {
                        if (_lastMousePos != Vector2.zero)
                        {
                            Image.rectTransform.anchoredPosition += (Vector2)Input.mousePosition - _lastMousePos;
                        }
                    }

                    _lastMousePos = Input.mousePosition;
                }
                else
                {
                    _lastMousePos = Vector2.zero;
                }
            }
            return;
        }

        if (Input.touchCount == 2)
        {
            if (_touchedLastFrame)
            {
                UpdateZoom(Input.GetTouch(0).position, Input.GetTouch(1).position, Input.GetTouch(0).deltaPosition, Input.GetTouch(1).deltaPosition);
            }

            _touchedLastFrame = true;
        }
        else
        {
            _touchedLastFrame = false;
            if (Input.touchCount == 1)
            {
                Image.rectTransform.anchoredPosition += Input.GetTouch(0).deltaPosition;
            }
        }
    }

    void OnGUI()
    {
        if (!Application.isEditor || !Debug || !_pinned)
        {
            return;
        }

        Rect drawPosition = new Rect(_pin.x - 1, Screen.height - _pin.y - 1, 2, 2);
        GUI.DrawTexture(drawPosition, new Texture2D(2, 2));
    }

    private void UpdateZoom(Vector2 touchPos1, Vector2 touchPos2, Vector2 touchDelta1, Vector2 touchDelta2)
    {
        Vector2 center = (touchPos1 + touchPos2)/2;

        float zoom = Vector2.Dot((touchPos1 - center).normalized, touchDelta1) +
                     Vector2.Dot((touchPos2 - center).normalized, touchDelta2);
        
        Image.transform.localScale *= Mathf.Pow(1.005f, zoom);
    }

    private void OnDestroy()
    {
        _mediator.OnRemove();
    }
}