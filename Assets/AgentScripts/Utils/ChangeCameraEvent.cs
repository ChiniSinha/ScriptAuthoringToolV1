public class ChangeCameraEvent : Event
{
    public ChangeCameraEvent(string cameraName)
    {
        CameraName = cameraName;
    }

    public string CameraName { get; private set; }
}