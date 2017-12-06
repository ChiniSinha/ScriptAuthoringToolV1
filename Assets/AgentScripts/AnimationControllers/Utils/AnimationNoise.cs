#region

using UnityEngine;

#endregion

public class AnimationNoise : MonoBehaviour
{
    [Tooltip("The local axis on which you want this bone to bend")] [SerializeField] protected Vector3 _bendAxis;

    [Tooltip("If this field is greater than 0, the noise function will loop.")] [SerializeField] protected float
        _loopTime;

    protected float _offset;

    protected float _value;

    [Tooltip("The maximum offset in degrees")] public float Intensity;

    private void Awake()
    {
        if (_loopTime > 0)
        {
            _value = Random.Range(0, _loopTime);
        }
        else
        {
            _value = Random.Range(0, 200f);
        }
        _offset = Random.Range(0, 200f);
    }

    private void Update()
    {
        _value += Time.deltaTime;
        if (_loopTime > 0 && _value > _loopTime)
        {
            _value = 0;
        }
    }

    private void LateUpdate()
    {
        float angle = Mathf.Lerp(-Intensity, Intensity, Mathf.PerlinNoise(_value, _offset));
        transform.Rotate(_bendAxis, angle, Space.Self);
    }
}