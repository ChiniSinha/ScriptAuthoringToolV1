#region

using System.Collections.Generic;
//using Cinemachine.Utility;
//using DG.Tweening;
//using RootMotion.FinalIK;
using UnityEngine;

#endregion

public class AgentAnimationController : MonoBehaviour
{
    private const int IdleAnimationCount = 6;
    public readonly Queue<AgentAnimation> BodyQueue = new Queue<AgentAnimation>();
    public readonly Queue<AgentAnimation> FaceQueue = new Queue<AgentAnimation>();
    public readonly Queue<AgentAnimation> HeadQueue = new Queue<AgentAnimation>();
    public readonly Queue<AgentAnimation> LeftArmQueue = new Queue<AgentAnimation>();
    public readonly Queue<AgentAnimation> RightArmQueue = new Queue<AgentAnimation>();
    
   // [HideInInspector]
   // public IkSettings IkSettings;

    [SerializeField] public Animator _animator;

    protected float _blinkTimer;

    public BodyAnimations Body;
    public FaceAnimations Face;
/*
    public Transform LeftArmAffector
    {
        get
        {
            if (_simpleArmIk)
            {
                return _simpleArmIk.LeftArmEffector;
            }
            if (_fullBodyBipedIk)
            {
                return _fullBodyBipedIk.solver.leftHandEffector.target;
            }
            return null;
        }
    }

    public Transform LeftElbowHint
    {
        get
        {
            if (_simpleArmIk)
            {
                return _simpleArmIk.LeftElbowHint;
            }
            if (_fullBodyBipedIk)
            {
                return _fullBodyBipedIk.solver.leftArmChain.bendConstraint.bendGoal;
            }
            return null;
        }
    }

    public Transform RightArmAffector
    {
        get
        {
            if (_simpleArmIk)
            {
                return _simpleArmIk.RightArmEffector;
            }
            if (_fullBodyBipedIk)
            {
                return _fullBodyBipedIk.solver.rightHandEffector.target;
            }
            return null;
        }
    }

    public Transform RightElbowHint
    {
        get
        {
            if (_simpleArmIk)
            {
                return _simpleArmIk.RightElbowHint;
            }
            if (_fullBodyBipedIk)
            {
                return _fullBodyBipedIk.solver.rightArmChain.bendConstraint.bendGoal;
            }
            return null;
        }
    }

    public Transform GazeTarget
    {
        get
        {
            if (_gazeTracking)
            {
                return _gazeTracking.GazeTarget;
            }
            if (_simpleLookIk)
            {
                return _simpleLookIk.GazeTarget;
            }
            if (_lookAtIk)
            {
                return _lookAtIk.solver.target;
            }
            return null;
        }
    }
*/
    public void EnqueueAnimation(ArmAnimation anim, Side side)
    {
        if (side == Side.LEFT)
        {
            LeftArmQueue.Enqueue(anim);
        }
        if (side == Side.RIGHT)
        {
            RightArmQueue.Enqueue(anim);
        }
    }

    public void EnqueueAnimation(HeadAnimation anim)
    {
        HeadQueue.Enqueue(anim);
    }

    public void EnqueueAnimation(FaceAnimation anim)
    {
        FaceQueue.Enqueue(anim);
    }

    public void EnqueueAnimation(BodyAnimation anim)
    {
        BodyQueue.Enqueue(anim);
    }

    private void Start()
    {
        Body = new BodyAnimations(_animator);
        Face = new FaceAnimations(_animator);
    }

    private void Update()
    {
        UpdateQueue(LeftArmQueue, Side.LEFT);
        UpdateQueue(RightArmQueue, Side.RIGHT);
        UpdateQueue(HeadQueue);
        UpdateQueue(FaceQueue);
        UpdateQueue(BodyQueue);
        
//        UpdateBodyIkParameters();
//        UpdateHeadIkParameters();

        if (BodyQueue.Count <= 0)
        {
            // Random movement changes
            if (Random.value < _idleShiftiness/1000f)
            {
                Body.SetOrientation(Side.CENTER);
                Body.PlayIdleAnimation(Random.Range(0, IdleAnimationCount));
            }
            if (Random.value < _postureShiftiness/1000f)
            {
                Body.SetPosture((Side) Random.Range(0, 3));
            }
        }

        if (Random.value < _randomSacadeFrequency/1000f)
        {
            Vector2 direction = Random.insideUnitCircle;
            _animator.SetFloat("EyeLeftRight", direction.x);
            _animator.SetFloat("EyeUpDown", direction.y);
        }

        _blinkTimer -= Time.deltaTime;
        if (_blinkTimer <= 0)
        {
            Face.Blink(Random.Range(_blinkDurationMin, _blinkDurationMax));
            _blinkTimer = Random.Range(_blinkPeriodMin, _blinkPeriodMax);
        }

        // TODO: there's a better way to do this
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 agentToCamera = cameraPos - transform.position;
        Vector3 agentToCameraFlat = Vector3.ProjectOnPlane(agentToCamera, transform.up).normalized;
        float distance = agentToCamera.magnitude;
        float angle = Mathf.Acos(Vector3.Dot(transform.forward, agentToCameraFlat))*Mathf.Rad2Deg;
        if (angle > 180)
        {
            angle -= 360;
        }

        _animator.SetFloat("CameraDistance", distance);
        _animator.SetFloat("CameraAngle", angle);
    }

    private void UpdateQueue(Queue<AgentAnimation> queue, Side side = Side.CENTER)
    {
        if (queue.Count > 0)
        {
            AgentAnimation anim = queue.Peek();
            anim.Play(this, side);
            anim.Duration -= Time.deltaTime;
            if (anim.Duration <= 0)
            {
                queue.Dequeue();
            }
        }
    }

/*    private void UpdateHeadIkParameters()
    {
        if (_gazeTracking)
        {
            _gazeTracking.Strength = LookAtIkStrength;
        }
        else if (_simpleLookIk)
        {
            _simpleLookIk.Strength = LookAtIkStrength;
            _simpleLookIk.UpdateIkParameters(LookAtIkBodyComponent, LookAtIkEyeComponent, LookAtIkHeadComponent);
        }
        else if (_lookAtIk)
        {
            _lookAtIk.solver.IKPositionWeight = LookAtIkStrength;
            _lookAtIk.solver.bodyWeight = LookAtIkBodyComponent;
            _lookAtIk.solver.eyesWeight = LookAtIkEyeComponent;
            _lookAtIk.solver.headWeight = LookAtIkHeadComponent;
        }
    }

    private void UpdateBodyIkParameters()
    {
        if (_simpleArmIk)
        {
            _simpleArmIk.LeftArmIkOrientationIntensity = BodyIkLeftArmOrientationStrength;
            _simpleArmIk.LeftArmIkPositionIntensity = BodyIkLeftArmPositionStrength;
            _simpleArmIk.LeftElbowHintPositionIntensity = BodyIkLeftElbowPositionStrength;

            _simpleArmIk.RightArmIkOrientationIntensity = BodyIkRightArmOrientationStrength;
            _simpleArmIk.RightArmIkPositionIntensity = BodyIkRightArmPositionStrength;
            _simpleArmIk.RightElbowHintPositionIntensity = BodyIkRightElbowPositionStrength;
        }
        else if (_fullBodyBipedIk)
        {
            _fullBodyBipedIk.solver.IKPositionWeight = BodyIkStrength;
            _fullBodyBipedIk.solver.SetEffectorWeights(FullBodyBipedEffector.LeftHand, BodyIkLeftArmPositionStrength,
                BodyIkLeftArmOrientationStrength);
            _fullBodyBipedIk.solver.SetEffectorWeights(FullBodyBipedEffector.RightHand, BodyIkRightArmPositionStrength,
                BodyIkRightArmOrientationStrength);
            _fullBodyBipedIk.solver.leftArmChain.bendConstraint.weight = BodyIkLeftElbowPositionStrength;
            _fullBodyBipedIk.solver.rightArmChain.bendConstraint.weight = BodyIkRightElbowPositionStrength;
        }
    }

    public void UpdateIKParameters(ArmAnimation.Type anim, Side side, float tweenTime = 0)
    {
        ApplyIKSetting(IkSettings.GetSetting(anim), side, tweenTime);
    }

    public void UpdateIKParameters(HeadAnimation.Type anim, float tweenTime = 0)
    {
        ApplyIKSetting(IkSettings.GetSetting(anim), Side.CENTER, tweenTime);
    }

    public void UpdateIKParameters(BodyAnimation.Type anim, float tweenTime = 0)
    {
        ApplyIKSetting(IkSettings.GetSetting(anim), Side.CENTER, tweenTime);
    }

    protected void ApplyIKSetting(IkSettings.IkSetting setting, Side side, float tweenTime = 0)
    {
        if (setting == null)
        {
            return;
        }

        if (setting.BodyStrength >= 0 && setting.BodyStrength != BodyIkStrength)
        {
            DOTween.To(delegate { return BodyIkStrength; }, delegate(float value) { BodyIkStrength = value; },
                setting.BodyStrength, tweenTime);
        }
        if (setting.ElbowPositionStrength >= 0)
        {
            if (side != Side.LEFT && setting.ElbowPositionStrength != BodyIkRightElbowPositionStrength)
            {
                DOTween.To(delegate { return BodyIkRightElbowPositionStrength; },
                    delegate(float value) { BodyIkRightElbowPositionStrength = value; },
                    setting.ElbowPositionStrength, tweenTime);
            }
            if (side != Side.RIGHT && setting.ElbowPositionStrength != BodyIkLeftElbowPositionStrength)
            {
                DOTween.To(delegate { return BodyIkLeftElbowPositionStrength; },
                    delegate(float value) { BodyIkLeftElbowPositionStrength = value; },
                    setting.ElbowPositionStrength, tweenTime);
            }
        }
        if (setting.HandOrientationStrength >= 0 && setting.LookEyeStrength != LookAtIkEyeComponent)
        {
            if (side != Side.LEFT && setting.HandOrientationStrength != BodyIkRightArmOrientationStrength)
            {
                DOTween.To(delegate { return BodyIkRightArmOrientationStrength; },
                    delegate (float value) { BodyIkRightArmOrientationStrength = value; },
                    setting.HandOrientationStrength, tweenTime);
            }
            if (side != Side.RIGHT && setting.HandOrientationStrength != BodyIkLeftArmOrientationStrength)
            {
                DOTween.To(delegate { return BodyIkLeftArmOrientationStrength; },
                    delegate (float value) { BodyIkLeftArmOrientationStrength = value; },
                    setting.HandOrientationStrength, tweenTime);
            }
        }
        if (setting.HandPositionStrength >= 0 && setting.LookEyeStrength != LookAtIkEyeComponent)
        {
            if (side != Side.LEFT && setting.HandPositionStrength != BodyIkRightArmPositionStrength)
            {
                DOTween.To(delegate { return BodyIkRightArmPositionStrength; },
                    delegate (float value) { BodyIkRightArmPositionStrength = value; },
                    setting.HandPositionStrength, tweenTime);
            }
            if (side != Side.RIGHT && setting.HandPositionStrength != BodyIkLeftArmPositionStrength)
            {
                DOTween.To(delegate { return BodyIkLeftArmPositionStrength; },
                    delegate (float value) { BodyIkLeftArmPositionStrength = value; },
                    setting.HandPositionStrength, tweenTime);
            }
        }
        if (setting.LookEyeStrength >= 0 && setting.LookEyeStrength != LookAtIkEyeComponent)
        {
            DOTween.To(delegate { return LookAtIkEyeComponent; },
                delegate(float value) { LookAtIkEyeComponent = value; },
                setting.LookEyeStrength, tweenTime);
        }
        if (setting.LookBodyStrength >= 0 && setting.LookBodyStrength != LookAtIkBodyComponent)
        {
            DOTween.To(delegate { return LookAtIkBodyComponent; },
                delegate(float value) { LookAtIkBodyComponent = value; },
                setting.LookBodyStrength, tweenTime);
        }
        if (setting.LookHeadStrength >= 0 && setting.LookHeadStrength != LookAtIkHeadComponent)
        {
            DOTween.To(delegate { return LookAtIkHeadComponent; },
                delegate(float value) { LookAtIkHeadComponent = value; },
                setting.LookHeadStrength, tweenTime);
        }
        if (setting.LookStrength >= 0 && setting.LookStrength != LookAtIkStrength)
        {
            DOTween.To(delegate { return LookAtIkStrength; },
                delegate(float value) { LookAtIkStrength = value; },
                setting.LookStrength, tweenTime);
        }
    }
*/
    #region Animator Parameters

    [SerializeField] protected float _blinkDurationMax = 10;
    [SerializeField] protected float _blinkDurationMin = 2;

    [SerializeField] protected float _blinkPeriodMax = .2f;
    [SerializeField] protected float _blinkPeriodMin = .125f;

    [SerializeField] protected float _idleShiftiness;
    [SerializeField] protected float _postureShiftiness;

    [SerializeField] protected float _randomSacadeFrequency;

    #endregion

/*    #region IK Components

    [HideInInspector] [SerializeField] protected FullBodyBipedIK _fullBodyBipedIk;

    [HideInInspector] [SerializeField] protected SimpleArmIk _simpleArmIk;

    [HideInInspector] [SerializeField] protected AgentGazeTracking _gazeTracking;

    [HideInInspector] [SerializeField] protected SimpleLookIk _simpleLookIk;

    [HideInInspector] [SerializeField] protected LookAtIK _lookAtIk;

    #endregion

    #region IK Parameters

    [HideInInspector] [Range(0, 1)] public float BodyIkLeftArmOrientationStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkLeftArmPositionStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkLeftElbowPositionStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkRightArmOrientationStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkRightArmPositionStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkRightElbowPositionStrength;

    [HideInInspector] [Range(0, 1)] public float BodyIkStrength;

    [HideInInspector] [Range(0, 1)] public float LookAtIkBodyComponent;

    [HideInInspector] [Range(0, 1)] public float LookAtIkEyeComponent;

    [HideInInspector] [Range(0, 1)] public float LookAtIkHeadComponent;

    [HideInInspector] [Range(0, 1)] public float LookAtIkStrength;

    #endregion
    */

    //a callback for calculating IK
    public bool ikActive = true;
    public Transform rightHandLocation = null;
    public Transform targetLocation = null;
    public float rightElbowWeight = 1f;
    public float rightHandWeight = 1f;
    public Transform rightElbowObj = null;
    public Transform finalPointerLocation = null;
    public Transform pointerLocation = null;

    public SkinnedMeshRenderer paperRenderer;

    public float speed = 5f;

    private bool firstIk = false;

    void OnAnimatorIK()
    {
        if(_animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {
                if(!firstIk){
                    Debug.Log("turning on the ik");
                    targetLocation.position = new Vector3(rightHandLocation.position.x,rightHandLocation.position.y,rightHandLocation.position.z + .5f);
                    targetLocation.rotation = finalPointerLocation.rotation;
                    rightHandWeight = 1f;
                    firstIk = true;
                }
                float step = speed * Time.deltaTime;
                //Debug.Log(step);
                targetLocation.position = Vector3.MoveTowards(targetLocation.position, finalPointerLocation.position, step);
                //lerp towards the new target


//                rightHandWeight = Mathf.Lerp(rightHandWeight,1,.2f);
                /*// Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    */

                // Set the right hand target position and rotation, if one has been assigned
               // if(rightHandObj != null) {
                    //Set Elbow hint
                    _animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightElbowObj.transform.position);
                    _animator.SetIKHintPositionWeight (AvatarIKHint.RightElbow, rightElbowWeight);
                    //set right hand position
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,rightHandWeight);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,rightHandWeight);  
                    //set right hand rotation
                    _animator.SetIKPosition(AvatarIKGoal.RightHand,targetLocation.position);
                    _animator.SetIKRotation(AvatarIKGoal.RightHand,targetLocation.rotation);
               // }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                firstIk = false;
                rightHandWeight = 0;
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                _animator.SetLookAtWeight(0);
            }
        }
    }  
}