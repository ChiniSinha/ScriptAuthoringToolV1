public class CameraDistanceUpdateEvent : Event
{
    public CameraDistanceUpdateEvent(float distance)
    {
        Distance = distance;
    }

    public float Distance { get; set; }
}