using UnityEngine;

public class OptionalFloatAttribute : PropertyAttribute
{
    public float DisabledValue { get; set; }

    public OptionalFloatAttribute(float disabledValue = -1)
    {
        DisabledValue = disabledValue;
    }
}