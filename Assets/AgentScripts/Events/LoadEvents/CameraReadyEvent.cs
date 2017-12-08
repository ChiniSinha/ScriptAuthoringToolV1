#region

using UnityEngine;

#endregion

public class CameraReadyEvent : Event
{
    public CameraReadyEvent(GameObject cameraRig)
    {
        CameraRig = cameraRig;
    }

    public GameObject CameraRig { get; private set; }
}