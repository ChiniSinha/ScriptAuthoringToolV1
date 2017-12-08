using UnityEngine;

public class MovingPointOfInterest : PointOfInterest
{
    public enum MoveMode
    {
        GAZE_CHANGE,
        CAMERA_CHANGE
    }

    [SerializeField] protected MoveMode _moveOn;
    private Vector3 _nextPos;
    public Bounds SafeZone;

    protected override void Awake()
    {
        base.Awake();
        if (_moveOn == MoveMode.GAZE_CHANGE)
        {
            Globals.EventBus.Register<AgentChangeGazeEvent>(OnAgentChangeGaze);
        }
        else if (_moveOn == MoveMode.CAMERA_CHANGE)
        {
            Globals.EventBus.Register<ChangeCameraTargetEvent>(OnCameraTargetChange);
        }
    }

    private void OnCameraTargetChange(ChangeCameraTargetEvent e)
    {
        if (e.NewTarget == Label)
        {
            MoveRandom();
        }
    }

    private void OnAgentChangeGaze(AgentChangeGazeEvent e)
    {
        if (e.Target == Label)
        {
            MoveRandom();
        }
    }


    [ContextMenu("Move")]
    private void MoveRandom()
    {
        _nextPos = SafeZone.center;
        _nextPos.x += Random.Range(-SafeZone.extents.x, SafeZone.extents.x);
        _nextPos.y += Random.Range(-SafeZone.extents.y, SafeZone.extents.y);
        _nextPos.z += Random.Range(-SafeZone.extents.z, SafeZone.extents.z);
        transform.position = _nextPos;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (_moveOn == MoveMode.GAZE_CHANGE)
        {
            Globals.EventBus.Unregister<AgentChangeGazeEvent>(OnAgentChangeGaze);
        }
        else if (_moveOn == MoveMode.CAMERA_CHANGE)
        {
            Globals.EventBus.Unregister<ChangeCameraTargetEvent>(OnCameraTargetChange);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(SafeZone.center, SafeZone.size);
    }
}