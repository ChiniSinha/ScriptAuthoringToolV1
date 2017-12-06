using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public Transform contentPanel;
    public SimpleObjectPool objectPool;
    public GameObject UIpanel;

    public void addMenu()
    {
        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
    }

    public void delete()
    {
        UIpanel.SetActive(false);
    }
}
