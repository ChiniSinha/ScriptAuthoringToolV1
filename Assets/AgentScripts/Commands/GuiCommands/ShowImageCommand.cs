using System;

public class ShowImageCommand : BaseCommand
{
    protected string _imageUrl;
    protected string _zoom;

    public ShowImageCommand(string imageUrl, string cameraZoom)
    {
        _imageUrl = imageUrl;
        _zoom = cameraZoom;
    }

    public override void Execute()
    {
        if (string.IsNullOrEmpty(_imageUrl) || _imageUrl.Equals("NONE", StringComparison.CurrentCultureIgnoreCase))
        {
            Globals.EventBus.Dispatch(new HideImageEvent());
        }
        else
        {
            Globals.EventBus.Dispatch(new ShowImageEvent(_imageUrl, _zoom));
        }
    }
}