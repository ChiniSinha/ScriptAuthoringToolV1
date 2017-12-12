using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropertyActions : MonoBehaviour
{

    public Button button;
    public InputField propertyName;
    public InputField propertyValue;
    
    public void HandleSave()
    {
        Property prop = new Property();
        prop.property = propertyName.text;
        prop.value = propertyValue.text;
        Properties.SetProperty(prop);
    }

    public void Delete()
    {
        Property prop = new Property();
        prop.property = propertyName.text;
        prop.value = propertyValue.text;
        Properties.DeleteProperty(prop);
        button.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
