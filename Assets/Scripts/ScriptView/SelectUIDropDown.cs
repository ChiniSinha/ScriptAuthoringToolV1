using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectUIDropDown : MonoBehaviour
{

    public Dropdown dropdown;
    public GameObject addMenu;
    public SimpleObjectPool checkBoxPool;
    public SimpleObjectPool inputPool;
    public Transform contentPanel;

    public void setButtonActive()
    {
        if (dropdown.value == 1)
        {
            destroy();
            addMenu.SetActive(true);
            
        }
        else if (dropdown.value == 2)
        {
            destroy();
            
            if (!MyGlobals.isDisplay)
            {
                addMenu.SetActive(false);
                GameObject checkBox = checkBoxPool.GetObject();
                checkBox.transform.SetParent(contentPanel);
                checkBox.transform.Reset();
                this.transform.parent.GetComponent<StatePanelObject>().checkBox = checkBox.GetComponent<CheckBoxInputPanel>();
            }
            
        }
        else if (dropdown.value == 3)
        {
            destroy();
           
            if (!MyGlobals.isDisplay)
            {
                addMenu.SetActive(false);
                GameObject inputText = inputPool.GetObject();
                inputText.transform.SetParent(contentPanel);
                inputText.transform.Reset();
                this.transform.parent.GetComponent<StatePanelObject>().inputPanel = inputText.GetComponent<TextInputPanel>();
            }
            
        }
        else
        {
            destroy();
        }
    }

    public void destroy()
    {
        foreach (Transform child in contentPanel)
        {
            if(child.GetComponent<MenuInputPanelObject>() != null)
            {
                this.transform.parent.GetComponent<StatePanelObject>().usermenu.Remove(child.GetComponent<MenuInputPanelObject>());
            }
            else if (child.GetComponent<CheckBoxInputPanel>() != null)
            {
                this.transform.parent.GetComponent<StatePanelObject>().checkBox = null;
            }
            else if(child.GetComponent<TextInputPanel>() != null)
            {
                this.transform.parent.GetComponent<StatePanelObject>().inputPanel = null;
            }
            Destroy(child.gameObject);
        }
    }
}
