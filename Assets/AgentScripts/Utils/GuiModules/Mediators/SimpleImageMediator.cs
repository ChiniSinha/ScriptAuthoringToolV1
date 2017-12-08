#region

#endregion

public class SimpleImageMediator : IImageDisplayMediator
{
    private readonly ImageDisplay _image;

    public SimpleImageMediator(ImageDisplay image)
    {
        _image = image;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<ShowImageEvent>(OnShowImage);
        Globals.EventBus.Register<HideImageEvent>(OnHideImage);
        Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<ShowImageEvent>(OnShowImage);
        Globals.EventBus.Unregister<HideImageEvent>(OnHideImage);
    }

    private void Show()
    {
        _image.PrimaryAnimator.Show();
    }

    private void Hide()
    {
        _image.PrimaryAnimator.Hide();
    }

    private void OnHideImage(HideImageEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _image.name)
        {
            return;
        }
        Hide();
    }

    private void OnShowImage(ShowImageEvent e)
    {
        if (!string.IsNullOrEmpty(e.ElementName) && e.ElementName != _image.name)
        {
            return;
        }
        string zoom = e.Zoom;
        _image.LoadImage(e.Url);
        Show();
    }
}