public class UpdateCameraCommand : BaseCommand
{
    public UpdateCameraCommand(string cameraName, float position, string easing, string targetName)
    {
        CameraName = cameraName;
        Position = position;
        Easing = easing;
        TargetName = targetName;
    }

    public string CameraName { get; private set; }
    public float Position { get; private set; }
    public string Easing { get; private set; }
    public string TargetName { get; private set; }

    public override void Execute()
    {
        if (!string.IsNullOrEmpty(CameraName))
        {
            Globals.EventBus.Dispatch(new ChangeCameraEvent(CameraName));
        }

        if (Position >= 0)
        {
            Globals.EventBus.Dispatch(new CameraMoveEvent(Position, Easing, CameraName));
        }

        if (!string.IsNullOrEmpty(TargetName))
        {
            Globals.EventBus.Dispatch(new ChangeCameraTargetEvent(TargetName));
        }
    }
}