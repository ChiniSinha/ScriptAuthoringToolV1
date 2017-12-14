using UnityEngine;
using System.Collections;

public class CheckBoxController : MonoBehaviour
{

    public Transform contentPanel;
    public SimpleObjectPool objectPool;

    public void addChoice()
    {
        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
        newObject.transform.Reset();
        this.transform.parent.GetComponent<CheckBoxInputPanel>().choices.Add(newObject.GetComponent<ChoicesInput>());
    }
    public void addMenu()
    {

        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
        newObject.transform.Reset();
        this.transform.parent.GetComponent<CheckBoxInputPanel>().usermenu.Add(newObject.GetComponent<MenuInputPanelObject>());
    }

    public void delete()
    {
        Destroy(this.transform.parent.gameObject);
        CheckBoxInputPanel choicePanel = this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<CheckBoxInputPanel>();
        choicePanel.choices.Remove(this.transform.parent.GetComponent<ChoicesInput>());
        choicePanel.usermenu.Remove(this.transform.parent.GetComponent<MenuInputPanelObject>());
    }

}
