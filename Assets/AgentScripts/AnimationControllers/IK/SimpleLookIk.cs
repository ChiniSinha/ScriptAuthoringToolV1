#region

using UnityEngine;

#endregion

public class SimpleLookIk : LookIkController
{
    [SerializeField] protected Animator _animator;

    [Range(0, 1f)] [SerializeField] protected float _bodyStrength;
    [Range(0, 1f)] [SerializeField] protected float _clampStrength;
    [Range(0, 1f)] [SerializeField] protected float _eyeStrength;
    [Range(0, 1f)] [SerializeField] protected float _headStrength;

    public void UpdateIkParameters(float bodyStrength, float eyeStrength, float headStrength)
    {
        _bodyStrength = bodyStrength;
        _eyeStrength = eyeStrength;
        _headStrength = headStrength;
    }
    
    private void OnAnimatorIK(int layer)
    {
        if (GazeTarget && _animator)
        {
            _animator.SetLookAtPosition(GazeTarget.position);
            _animator.SetLookAtWeight(Strength, _bodyStrength, _headStrength, _eyeStrength, _clampStrength);
        }
    }
}