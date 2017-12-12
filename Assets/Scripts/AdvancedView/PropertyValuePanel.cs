using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropertyValuePanel : MonoBehaviour
{

    public InputField propertyName;
    public InputField propertyValue;

    public void SetUp(Property property)
    {
        propertyName.text = property.property;
        propertyValue.text = property.value;
    }

}
