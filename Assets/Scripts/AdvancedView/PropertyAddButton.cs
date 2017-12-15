using UnityEngine;
using System.Collections;

public class PropertyAddButton : MonoBehaviour
{
    public Transform contentPanel;
    public SimpleObjectPool propertyPool;

    public void addProperty()
    {
        MyGlobals.propUpdate = false;
        GameObject newProperty = propertyPool.GetObject();
        newProperty.transform.SetParent(contentPanel);
        newProperty.transform.Reset();
    }
}
