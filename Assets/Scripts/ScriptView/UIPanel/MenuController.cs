using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public Transform contentPanel;
    public SimpleObjectPool objectPool;

    public void addAgent()
    {
        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
        newObject.transform.Reset();
        this.transform.parent.GetComponent<StatePanelObject>().agentUtterances.Add(newObject.GetComponent<AgentInputObject>());
    }
    public void addMenu()
    {

        GameObject newObject = objectPool.GetObject();
        newObject.transform.SetParent(contentPanel);
        newObject.transform.Reset();
        this.transform.parent.GetComponent<StatePanelObject>().usermenu.Add(newObject.GetComponent<MenuInputPanelObject>());
    }

    public void delete()
    {
        Destroy(this.transform.parent.gameObject);
        StatePanelObject statePanel = this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<StatePanelObject>();
        statePanel.agentUtterances.Remove(this.transform.parent.GetComponent<AgentInputObject>());
        statePanel.usermenu.Remove(this.transform.parent.GetComponent<MenuInputPanelObject>());
    }
}
