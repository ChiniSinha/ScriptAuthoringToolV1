//using DG.Tweening;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SingleAgentMediator : MonoBehaviour
{
    public Agent _primaryAgent;
    //private Tweener _gazeTween;
    private float _armMoveSpeed = 2f;

    public SingleAgentMediator(Agent agent)
    {
        _primaryAgent = agent;
    }

    public void Setup()
    {
        Globals.EventBus.Register<StopSpeakingRequestEvent>(OnStopSpeaking);
        Globals.EventBus.Register<SpeakRequestEvent>(OnSpeakRequest);
        //Globals.EventBus.Register<AgentBeatEvent>(OnBeat);
        //Globals.EventBus.Register<AgentPauseAnimationEvent>(OnPauseAnimations);
        //Globals.EventBus.Register<AgentPlayVisemeEvent>(OnViseme);
        Globals.EventBus.Register<AgentClearPointEvent>(OnResetDoc);
        //Globals.EventBus.Register<AgentChangeGazeEvent>(OnGazeChange);
        Globals.EventBus.Register<AgentSetIdleEvent>(OnSetIdle);
        Globals.EventBus.Register<AgentDisplayDocumentEvent>(OnDisplayDocument);
        Globals.EventBus.Register<AgentChangePointEvent>(OnPointChange);
        Globals.EventBus.Register<AgentSetPostureEvent>(OnPostureChange);
        //Globals.EventBus.Register<AgentPerformGestureEvent>(OnPerformGesture);
        //Globals.EventBus.Register<AgentUpdateExpressionEvent>(OnUpdateExpression);
        Globals.EventBus.Register<AgentHeadNodEvent>(OnHeadNod);

        //ENABLE THIS TO TEST SPEECH
//        Globals.EventBus.Dispatch(new SpeakRequestEvent("Hello World"));
        // ?? (Ask Lazlo)-- Globals.EventBus.Register<EnvironmentReadyEvent>(OnEnvironmentReady);
    }

    public void Cleanup()
    {
        Globals.EventBus.Unregister<StopSpeakingRequestEvent>(OnStopSpeaking);
        Globals.EventBus.Unregister<SpeakRequestEvent>(OnSpeakRequest);
        //Globals.EventBus.Unregister<AgentBeatEvent>(OnBeat);
        //Globals.EventBus.Unregister<AgentPauseAnimationEvent>(OnPauseAnimations);
        //Globals.EventBus.Unregister<AgentPlayVisemeEvent>(OnViseme);
        // - Globals.EventBus.Unregister<AgentClearPointEvent>(OnResetDoc);
        //Globals.EventBus.Unregister<AgentChangeGazeEvent>(OnGazeChange);
        Globals.EventBus.Unregister<AgentSetIdleEvent>(OnSetIdle);
        Globals.EventBus.Unregister<AgentDisplayDocumentEvent>(OnDisplayDocument);
        Globals.EventBus.Unregister<AgentChangePointEvent>(OnPointChange);
        Globals.EventBus.Unregister<AgentSetPostureEvent>(OnPostureChange);
        //Globals.EventBus.Unregister<AgentPerformGestureEvent>(OnPerformGesture);
        //Globals.EventBus.Unregister<AgentUpdateExpressionEvent>(OnUpdateExpression);
        Globals.EventBus.Unregister<AgentHeadNodEvent>(OnHeadNod);
        // -- Ask Lazlo -- Globals.EventBus.Unregister<EnvironmentReadyEvent>(OnEnvironmentReady);
    }

    //private void OnPauseAnimations(AgentPauseAnimationEvent e)
    //{
    //    _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.PAUSED, e.DelayMs/1000), Side.LEFT);
    //    _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.PAUSED, e.DelayMs/1000), Side.RIGHT);
    //    _primaryAgent.AgentAnimationController.EnqueueAnimation(new BodyAnimation(BodyAnimation.Type.PAUSED, e.DelayMs/1000));
    //    _primaryAgent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.PAUSED, e.DelayMs / 1000));
    //}

    //private void OnViseme(AgentPlayVisemeEvent e)
    //{
    //    _primaryAgent.AgentAnimationController.Face.PlayViseme(e.VisemeId, e.DurationMs);
    //}

    //private void OnBeat(AgentBeatEvent e)
    //{
    //    _primaryAgent.AgentAnimationController.Body.PlayBeat(e.Hand);
    //}
    
    private void OnHeadNod(AgentHeadNodEvent e)
    {
        _primaryAgent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.NOD, 0.2f));
    }

    //private void OnUpdateExpression(AgentUpdateExpressionEvent e)
    //{
    //    _primaryAgent.AgentAnimationController.EnqueueAnimation(new FaceAnimation(e.Expression, e.Duration));
    //}

    //private void OnPerformGesture(AgentPerformGestureEvent e)
    //{
    //    if (!_primaryAgent.IsHoldingObject())
    //    {
    //        _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(e.Gesture, e.Duration), e.Hand);
    //    }
    //}

    private void OnPostureChange(AgentSetPostureEvent e)
    {
        _primaryAgent.AgentAnimationController.EnqueueAnimation(new BodyAnimation(BodyAnimation.Type.POSTURE_SHIFT_CHANGE));
    }

    private void OnPointChange(AgentChangePointEvent e)
    {
        bool holdWithLeft = e.Hand == "L";

        _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.POINT_DOCUMENT),
            holdWithLeft ? Side.RIGHT : Side.LEFT);
        
        Bounds bounds = _primaryAgent.AgentAnimationController.paperRenderer.bounds;
        /*
        GrabbableObject grabbableObject = _primaryAgent.Paper.Grabbable;
        Vector3 goal = grabbableObject.LocalBoundingBox.min;
        goal.x += (1 - e.X/100f)*grabbableObject.LocalBoundingBox.size.x;
        goal.y += (1 - e.Y/100f)*grabbableObject.LocalBoundingBox.size.y;
        goal.z += 0.05f;
        */
//        float xPos = e.X/100f;
//        float yPos = e.Y/100f;
        float xPos = 0f;
        float yPos = 1 - e.X/100f;
        float zPos = 1 - e.Y/100f;
        float x = bounds.max.x - ((bounds.size.x) * xPos);
        float y = bounds.max.y - ((bounds.size.y) * yPos);
        float z = bounds.min.z + ((bounds.size.z) * zPos);
        float xOffset = 0;
        float yOffset = 0;
        float zOffset = 0;
        _primaryAgent.AgentAnimationController.pointerLocation.transform.position = new Vector3(x  + xOffset,y + yOffset, z + zOffset);
        /*
        Vector3 offset;
        //float angle = Mathf.PI*(3/4f) + (Mathf.PI / 2f);
        float handSize = _primaryAgent.HandSize;
        float angle = Random.Range(0, Mathf.PI/4) + (Mathf.PI / 2f);
        if (!holdWithLeft)
        {
            angle += Mathf.PI*(3/4f);
        }
        offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;
        handSize /= grabbableObject.transform.lossyScale.magnitude;
        offset *= handSize;
        Debug.Log(offset);

        Transform affector;
        Transform hint;
        Vector3 hintPosition;
        if (holdWithLeft)
        {
            affector = _primaryAgent.AgentAnimationController.RightArmAffector;
            hint = _primaryAgent.AgentAnimationController.RightElbowHint;
            hintPosition = new Vector3(1, 1, 0.7f);
        }
        else
        {
            affector = _primaryAgent.AgentAnimationController.LeftArmAffector;
            hint = _primaryAgent.AgentAnimationController.LeftElbowHint;
            hintPosition = new Vector3(-1, 1, 0.7f);
        }

        float time = affector.localPosition.magnitude / _armMoveSpeed;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, -offset);
        if (holdWithLeft)
        {
            // Flip the orientation because the right arm is 180 degrees rotated
            rotation *= Quaternion.FromToRotation(Vector3.right, Vector3.left);
        }

        affector.parent = _primaryAgent.Paper.transform;
        affector.DOLocalRotateQuaternion(rotation, time);
        affector.DOLocalMove(goal + offset, time);
        hint.SetParent(_primaryAgent.transform);
        hint.DOLocalMove(hintPosition, time/2f);
        */
        StartCoroutine(StartPoint());
    }

    private IEnumerator StartPoint(){
        yield return new WaitForSeconds(2f);
        _primaryAgent.AgentAnimationController.ikActive = true;
    }

    private void OnDisplayDocument(AgentDisplayDocumentEvent e)
    {
        /*
        if (string.IsNullOrEmpty(e.DocUrl))
        {
            _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.NEUTRAL), Side.LEFT);
            _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.NEUTRAL), Side.RIGHT);
            _primaryAgent.AgentAnimationController.EnqueueAnimation(new BodyAnimation(BodyAnimation.Type.POSTURE_SHIFT_MIDDLE));
            _primaryAgent.AgentAnimationController.Body.PlayIdleAnimation(0);
            _primaryAgent.ReleaseObject(_primaryAgent.Paper.Grabbable);
        }
        else
        {
            bool holdWithLeft = e.Hand == "L";
            if (holdWithLeft && _primaryAgent.LeftHandHeldObject == _primaryAgent.Paper.gameObject)
            {
                return;
            }

            if (!holdWithLeft && _primaryAgent.RightHandHeldObject == _primaryAgent.Paper.gameObject)
            {
                return;
            }

            _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.HOLD_DOCUMENT, 1.5f),
                holdWithLeft ? Side.LEFT : Side.RIGHT);
            _primaryAgent.AgentAnimationController.EnqueueAnimation(
                new BodyAnimation(holdWithLeft
                    ? BodyAnimation.Type.POSTURE_SHIFT_LEFT
                    : BodyAnimation.Type.POSTURE_SHIFT_RIGHT));
            _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.NEUTRAL, 1.5f),
                holdWithLeft ? Side.RIGHT : Side.LEFT);

            if (_primaryAgent.Paper.Cloth)
            {
                _primaryAgent.Paper.Cloth.enabled = false;
            }
            GrabbableObject grabbableObject = _primaryAgent.Paper.Grabbable;
            _primaryAgent.HoldObject(grabbableObject, holdWithLeft ? Side.LEFT : Side.RIGHT);

            if (_primaryAgent.Paper.Cloth)
            {
                _primaryAgent.Paper.Cloth.enabled = true;
            }
        }
        */
    }

    private void OnSetIdle(AgentSetIdleEvent e)
    {
        if (e.ShouldIdle)
        {
            _primaryAgent.AgentAnimationController.Body.PlayIdleAnimation(1);
        }
    }

    //private void OnGazeChange(AgentChangeGazeEvent e)
    //{
    //    if (_primaryAgent.AgentAnimationController.GazeTarget)
    //    {
    //        string targetName = e.Target;
    //        if (e.Target.Equals("towards", StringComparison.CurrentCultureIgnoreCase))
    //        {
    //            targetName = "camera";
    //        }

    //        if (PointOfInterest.PoIs.ContainsKey(targetName))
    //        {
    //            PointOfInterest poi = PointOfInterest.PoIs[targetName];
    //            if (_gazeTween != null)
    //            {
    //                _gazeTween.Kill();
    //            }
    //            _primaryAgent.AgentAnimationController.GazeTarget.SetParent(poi.transform, true);
    //            _gazeTween = _primaryAgent.AgentAnimationController.GazeTarget.DOLocalMove(Vector3.zero, Random.Range(0.5f, 1.5f));
    //        }

    //        if (targetName.Equals("away", StringComparison.CurrentCultureIgnoreCase))
    //        {
    //            _primaryAgent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.EYE_FLICK));
    //        }
    //        else
    //        {
    //            _primaryAgent.AgentAnimationController.EnqueueAnimation(new HeadAnimation(HeadAnimation.Type.GAZE));
    //        }
    //    }
    //}

    private void OnResetDoc(AgentClearPointEvent e)
    {
        _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.NEUTRAL), Side.LEFT);
        _primaryAgent.AgentAnimationController.EnqueueAnimation(new ArmAnimation(ArmAnimation.Type.NEUTRAL), Side.RIGHT);
    }

    private void OnSpeakRequest(SpeakRequestEvent e)
    {
        if (e.Node != null)
        {
            _primaryAgent.Tts.SpeakBlock(e.Node);
        }
        else
        {
            _primaryAgent.Tts.SpeakText(e.Utterance);
        }
    }

    private void OnStopSpeaking(StopSpeakingRequestEvent e)
    {
        _primaryAgent.Tts.OnSpeakComplete();
    }

/*    private void OnEnvironmentReady(EnvironmentReadyEvent e)
    {
        e.Environment.PositionAgent(_primaryAgent);
    }*/
}