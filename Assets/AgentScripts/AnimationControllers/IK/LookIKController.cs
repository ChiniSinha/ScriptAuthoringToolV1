#region

using UnityEngine;

#endregion

public abstract class LookIkController : MonoBehaviour
{
    public Transform GazeTarget;
    [Range(0, 1f)] public float Strength;
}