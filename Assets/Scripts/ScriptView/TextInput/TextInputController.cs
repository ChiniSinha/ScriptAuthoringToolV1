using UnityEngine;
using System.Collections;

public class TextInputController : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool objectPool;

   
    public void addMenu()
    {

        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
        newObject.transform.Reset();
        this.transform.parent.GetComponent<TextInputPanel>().usermenu.Add(newObject.GetComponent<MenuInputPanelObject>());
    }

    public void delete()
    {
        Destroy(this.transform.parent.gameObject);
        TextInputPanel inputPanel = this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<TextInputPanel>();
        inputPanel.usermenu.Remove(this.transform.parent.GetComponent<MenuInputPanelObject>());
    }
}
