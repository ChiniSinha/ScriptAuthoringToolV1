public class CameraMoveEvent : Event
{
    public CameraMoveEvent(float zoomLevel, string easing, string cameraName = Consts.ActiveCameraLabel)
    {
        ZoomLevel = zoomLevel;
        Easing = easing;
        CameraName = cameraName;
    }

    public string Easing { get; private set; }
    public string CameraName { get; private set; }
    public float ZoomLevel { get; private set; }
}