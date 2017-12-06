public class SetExpressionCommand : BaseCommand
{
    private FaceAnimation.Type _expression;
    private float _duration;
    private Agent _agent;

    public SetExpressionCommand(string expression, float duration = 0f)
    {
        _expression = FaceAnimation.Type.NEUTRAL_EXPRESSION;
        switch (expression)
        {
            case "SMILE":
            case "HAPPY":
                _expression = FaceAnimation.Type.SMILE;
                break;
            case "NEUTRAL":
                _expression = FaceAnimation.Type.NEUTRAL_EXPRESSION;
                break;
            case "FROWN":
            case "CONCERN":
                _expression = FaceAnimation.Type.FROWN;
                break;
            case "EYEBROWS_UP":
                _expression = FaceAnimation.Type.BROWS_UP;
                break;
            case "EYEBROWS_DOWN":
                _expression = FaceAnimation.Type.BROWS_DOWN;
                break;
            case "EYEBROWS_NEUTRAL":
                _expression = FaceAnimation.Type.BROWS_NEUTRAL;
                break;
        }
        _duration = duration;
        _agent = Globals.Get<Agent>();
    }

    public SetExpressionCommand(FaceAnimation.Type expression, float duration = 0f)
    {
        _expression = expression;
        _duration = duration;
        _agent = Globals.Get<Agent>();
    }

    public override void Execute()
    {
        //Globals.EventBus.Dispatch(new AgentUpdateExpressionEvent(expression));
        _agent.AgentAnimationController.EnqueueAnimation(new FaceAnimation(_expression, _duration));
    }
}