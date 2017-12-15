using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropertiesView : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool propertyPool;

    void Start()
    {
        setUpProperties();
    }


    public void setUpProperties()
    {
       
        foreach(Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        Properties properties = Properties.Load();
        if (properties != null)
        {
            foreach (Property property in properties.properties)
            {
                GameObject newProperty = propertyPool.GetObject();
                newProperty.transform.SetParent(contentPanel);
                newProperty.transform.Reset();

                PropertyValuePanel propView = newProperty.GetComponent<PropertyValuePanel>();
                propView.SetUp(property);
            }
        }
    }

}
