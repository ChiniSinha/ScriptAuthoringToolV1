using UnityEngine;

public class SimpleArmIk : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [Range(0, 1f)] public float Strength;

    public Transform LeftArmEffector;
    [Range(0, 1)] public float LeftArmIkOrientationIntensity;
    [Range(0, 1)] public float LeftArmIkPositionIntensity;

    public Transform LeftElbowHint;
    [Range(0, 1)] public float LeftElbowHintPositionIntensity;

    public Transform RightArmEffector;
    [Range(0, 1)] public float RightArmIkOrientationIntensity;
    [Range(0, 1)] public float RightArmIkPositionIntensity;

    public Transform RightElbowHint;
    [Range(0, 1)] public float RightElbowHintPositionIntensity;


    private void OnAnimatorIK(int layer)
    {
        string layerName = _animator.GetLayerName(layer);
        if (layerName.Equals("LeftArmIK"))
        {
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftArmEffector.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftArmIkPositionIntensity*Strength);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftArmEffector.rotation);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftArmIkOrientationIntensity*Strength);
            _animator.SetIKHintPosition(AvatarIKHint.LeftElbow, LeftElbowHint.position);
            _animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, LeftElbowHintPositionIntensity*Strength);
        }
        else if (layerName.Equals("RightArmIK"))
        {
            _animator.SetIKPosition(AvatarIKGoal.RightHand, RightArmEffector.position);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, RightArmIkPositionIntensity*Strength);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, RightArmEffector.rotation);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, RightArmIkOrientationIntensity*Strength);
            _animator.SetIKHintPosition(AvatarIKHint.RightElbow, RightElbowHint.position);
            _animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, RightElbowHintPositionIntensity*Strength);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(LeftArmEffector.position, LeftArmEffector.rotation, Vector3.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(Vector3.zero, Vector3.one*0.2f);
        Gizmos.matrix = Matrix4x4.TRS(RightArmEffector.position, RightArmEffector.rotation, Vector3.one);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(Vector3.zero, Vector3.one*0.2f);
        Gizmos.matrix = oldMatrix;
    }
}