using UnityEngine;
using System.Collections;

public class DeleteState : MonoBehaviour
{

    public void handleClick()
    {
        GameObject state = this.gameObject.transform.parent.gameObject;
        Destroy(state);
        if(this.transform.parent.transform.GetComponent<CheckBoxInputPanel>() != null)
        {
            this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<StatePanelObject>().checkBox = null;
        }
        if (this.transform.parent.transform.GetComponent<TextInputPanel>() != null)
        {
            this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<StatePanelObject>().inputPanel = null;
        }
    }
   
}
