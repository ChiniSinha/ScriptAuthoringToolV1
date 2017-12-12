using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropertiesView : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool propertyPool;

    void Start()
    {
        Properties properties = Properties.Load();
        foreach(Property property in properties.properties)
        {
            GameObject newProperty = propertyPool.GetObject();
            newProperty.transform.SetParent(contentPanel);
            newProperty.transform.Reset();

            PropertyValuePanel propView = newProperty.GetComponent<PropertyValuePanel>();
            propView.SetUp(property);
        }
    }
}
