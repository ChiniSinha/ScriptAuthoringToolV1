public class AgentPerformGestureEvent : Event
{
    public AgentPerformGestureEvent(Side hand, ArmAnimation.Type gesture, float duration = 0f)
    {
        Hand = hand;
        Gesture = gesture;
        Duration = duration;
    }

    public AgentPerformGestureEvent(Side hand, string gestureString, float duration = 0f)
    {
        Hand = hand;
        Duration = duration;

        switch (gestureString.ToUpper())
        {
            case "CONTRAST":
                Gesture = ArmAnimation.Type.CONTRAST;
                break;
            case "POINT_DOWN":
                Gesture = ArmAnimation.Type.POINT_DOWN;
                break;
            case "THUMBS_UP":
                Gesture = ArmAnimation.Type.THUMBS_UP;
                break;
            case "YOU":
                Gesture = ArmAnimation.Type.POINT_USER;
                break;
            case "ME":
                Gesture = ArmAnimation.Type.POINT_SELF;
                break;
            case "RELAX":
                Gesture = ArmAnimation.Type.NEUTRAL;
                break;
            case "WAVE":
                Gesture = ArmAnimation.Type.WAVE;
                break;
            case "POINT":
                Gesture = ArmAnimation.Type.POINT_OUT;
                break;
        }
    }

    public Side Hand { get; private set; }
    public ArmAnimation.Type Gesture { get; private set; }
    public float Duration { get; private set; }
}