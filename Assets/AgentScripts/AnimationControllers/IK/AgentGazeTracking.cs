#region

using UnityEngine;

#endregion

[ExecuteInEditMode]
public class AgentGazeTracking : LookIkController
{
    [SerializeField] private float _damping;

    [SerializeField] private Vector2 _eyeHardConstraints;

    [SerializeField] private Transform[] _eyes;

    [SerializeField] private Vector2 _eyeSoftConstraints;

    [HideInInspector] [SerializeField] private Transform _head;

    [SerializeField] private Vector3 _headMaxAngle;

    [SerializeField] private Vector3 _headMinAngle;

    private Quaternion _orientation;
    private Quaternion _targetOrientation;

    public Transform Head
    {
        get { return _head; }
        set
        {
            _head = value;
            _orientation = _head.localRotation;
        }
    }

    private void Start()
    {
        if (_head)
        {
            _orientation = _head.localRotation;
        }
    }

    // Do this after the animator goes
    private void LateUpdate()
    {
        if (!GazeTarget)
        {
            return;
        }

        _targetOrientation = _head.localRotation;
        Vector3 targetHeadEulers = _targetOrientation.eulerAngles;
        ;
        if (targetHeadEulers.x > 180)
        {
            targetHeadEulers.x -= 360;
        }
        if (targetHeadEulers.y > 180)
        {
            targetHeadEulers.y -= 360;
        }

        for (int i = 0; i < _eyes.Length; i++)
        {
            Quaternion idealEyeRot = Quaternion.LookRotation(GazeTarget.position - _eyes[i].position);
            Quaternion relativeIdealEyeRot = Quaternion.Inverse(_head.rotation)*idealEyeRot;

            Vector3 eyeEulers = relativeIdealEyeRot.eulerAngles;
            if (eyeEulers.y > 180)
            {
                eyeEulers.y -= 360;
            }
            if (eyeEulers.x > 180)
            {
                eyeEulers.x -= 360;
            }
            if (Mathf.Abs(eyeEulers.x) > _eyeSoftConstraints.y)
            {
                targetHeadEulers.x += (eyeEulers.x - _eyeSoftConstraints.y)/2;
            }
            if (Mathf.Abs(eyeEulers.y) > _eyeSoftConstraints.x)
            {
                targetHeadEulers.y += (eyeEulers.y - _eyeSoftConstraints.x)/2;
            }
        }
        _targetOrientation = Quaternion.Euler(targetHeadEulers);

        _orientation = Quaternion.Slerp(_orientation, _targetOrientation, 1/_damping);

        Vector3 eulerAngles = _orientation.eulerAngles;
        if (eulerAngles.x > 180)
        {
            eulerAngles.x -= 360;
        }
        if (eulerAngles.y > 180)
        {
            eulerAngles.y -= 360;
        }
        if (eulerAngles.z > 180)
        {
            eulerAngles.z -= 360;
        }
        eulerAngles = Vector3.Max(eulerAngles, _headMinAngle);
        eulerAngles = Vector3.Min(eulerAngles, _headMaxAngle);

        _orientation = Quaternion.Euler(eulerAngles);

        _head.localRotation = Quaternion.Slerp(_head.localRotation, _orientation, Strength);
        Debug.DrawRay(_head.position, _head.forward*10, Color.red);

        for (int i = 0; i < _eyes.Length; i++)
        {
            Quaternion idealEyeRot = Quaternion.LookRotation(GazeTarget.position - _eyes[i].position);
            Quaternion relativeIdealEyeRot = Quaternion.Inverse(_head.rotation)*idealEyeRot;

            Vector3 eyeEulers = relativeIdealEyeRot.eulerAngles;
            if (eyeEulers.y > 180)
            {
                eyeEulers.y -= 360;
            }
            if (eyeEulers.x > 180)
            {
                eyeEulers.x -= 360;
            }
            eyeEulers.y = Mathf.Clamp(eyeEulers.y, -_eyeHardConstraints.x, _eyeHardConstraints.x);
            eyeEulers.x = Mathf.Clamp(eyeEulers.x, -_eyeHardConstraints.y, _eyeHardConstraints.y);

            _eyes[i].localRotation = Quaternion.Euler(eyeEulers);
        }
    }
}