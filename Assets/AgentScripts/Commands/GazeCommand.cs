//using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GazeCommand : BaseCommand
{
    protected string _target;
    //private Tweener _gazeTween;
    private Agent _agent;

    public GazeCommand(string target)
    {
        _target = target;
        _agent = Globals.Get<Agent>();
    }

    public override void Execute()
    {
        _agent.OnGazeChange(_target);

       

            if (_target.Equals("away", StringComparison.CurrentCultureIgnoreCase))
            {
                _agent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.EYE_FLICK));
            }
            else
            {
                _agent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.GAZE));
            }
        
    }
}
