using System;

public class GestureCommand : BaseCommand
{
    private string _gestureString;
    private ArmAnimation.Type _gesture;
    private Side _hand;
    private float _duration;
    private Agent _agent;

    public GestureCommand(string hand, string gesture, float duration = 0f)
    {
        _hand = hand.Equals("L", StringComparison.CurrentCultureIgnoreCase) ? Side.LEFT : Side.RIGHT;
        SaveStringAsGesture(gesture);
        _gestureString = gesture;
        _duration = duration;
        _agent = Globals.Get<Agent>();
    }

    public GestureCommand(Side hand, string gesture, float duration = 0f)
    {
        _hand = hand;
        SaveStringAsGesture(gesture);
        _gestureString = gesture;
        _duration = duration;
        _agent = Globals.Get<Agent>();
    }

    public GestureCommand(Side hand, ArmAnimation.Type gesture, float duration = 0f)
    {
        _hand = hand;
        _gesture = gesture;
        _duration = duration;
        _agent = Globals.Get<Agent>();
    }

    public override void Execute()
    {
 /*       if (_gestureString != null && _gestureString.Equals("BEAT", StringComparison.CurrentCultureIgnoreCase))
        {
            //Globals.EventBus.Dispatch(new AgentBeatEvent(_hand));
            Globals.CommandQueue.Enqueue(new BeatCommand(_hand));
            return;
        }

        //Globals.EventBus.Dispatch(new AgentPerformGestureEvent(_hand, _gesture));
        if (!_agent.IsHoldingObject())
        {
            _agent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(_gesture, _duration), _hand);
        }*/
    }

    private void SaveStringAsGesture(string gesture)
    {
        switch (gesture.ToUpper())
        {
            case "CONTRAST":
                _gesture = ArmAnimation.Type.CONTRAST;
                break;
            case "READY":
                _gesture = ArmAnimation.Type.READY;
                break;
            case "POINT_DOWN":
                _gesture = ArmAnimation.Type.POINT_DOWN;
                break;
            case "THUMBS_UP":
                _gesture = ArmAnimation.Type.THUMBS_UP;
                break;
            case "YOU":
                _gesture = ArmAnimation.Type.POINT_USER;
                break;
            case "ME":
                _gesture = ArmAnimation.Type.POINT_SELF;
                break;
            case "RELAX":
                _gesture = ArmAnimation.Type.NEUTRAL;
                break;
            case "WAVE":
                _gesture = ArmAnimation.Type.WAVE;
                break;
        }
    }
}