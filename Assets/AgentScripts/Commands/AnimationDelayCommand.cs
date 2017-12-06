public class AnimationDelayCommand : BaseCommand
{
    private Agent _agent;
    private float _delay;

    public AnimationDelayCommand(float delayMs)
    {
        _delay = delayMs / 1000;
        _agent = Globals.Get<Agent>();
    }
    
    public override void Execute()
    {
        //Globals.EventBus.Dispatch(new AgentPauseAnimationEvent(DelayMs));
        _agent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.PAUSED, _delay), Side.LEFT);
        _agent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.PAUSED, _delay), Side.RIGHT);
        _agent.AgentAnimationController.EnqueueAnimation(new BodyAnimation(BodyAnimation.Type.PAUSED, _delay));
        _agent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.PAUSED, _delay));
    }
}