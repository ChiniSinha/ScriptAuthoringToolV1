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

/*        if (_agent.AgentAnimationController.GazeTarget)
        {
            if (_target.Equals("towards", StringComparison.CurrentCultureIgnoreCase))
            {
                _target = "camera";
            }

            if (PointOfInterest.PoIs.ContainsKey(_target))
            {
                PointOfInterest poi = PointOfInterest.PoIs[_target];
                if (_gazeTween != null)
                {
                    _gazeTween.Kill();
                }
                _agent.AgentAnimationController.GazeTarget.SetParent(poi.transform, true);
                _gazeTween = _agent.AgentAnimationController.GazeTarget.DOLocalMove(Vector3.zero, Random.Range(0.5f, 1.5f));
            }

            if (_target.Equals("away", StringComparison.CurrentCultureIgnoreCase))
            {
                _agent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.EYE_FLICK));
            }
            else
            {
                _agent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.GAZE));
            }
        }*/
    }
}
