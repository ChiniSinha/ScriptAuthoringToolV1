public class CameraFadeOutTransitionEvent : Event
{
    public CameraFadeOutTransitionEvent(Event midPointEvent, Event endEvent, float fadeTime)
    {
        MidPointEvent = midPointEvent;
        EndEvent = endEvent;
        FadeTime = fadeTime;
    }

    public Event MidPointEvent { get; protected set; }
    public Event EndEvent { get; protected set; }

    public float FadeTime { get; protected set; }
}