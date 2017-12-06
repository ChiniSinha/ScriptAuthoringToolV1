using UnityEngine;
using System.Collections.Generic;

public class SaveSriptButton : MonoBehaviour
{
    public Transform stateContentPanel;
    public Transform agentContentPanel;
    public Transform menuContentPanel;

    public void handleSave()
    {
        StatePanelObject[] states = stateContentPanel.GetComponentsInChildren<StatePanelObject>();

        foreach(StatePanelObject state in states)
        {
            Debug.Log("State name: " + state.stateName.text);
            state.agentUtterances = agentContentPanel.GetComponentsInChildren<AgentInputObject>();
            foreach(AgentInputObject agent in state.agentUtterances)
            {
                Debug.Log("InsideState: " + agent.agentUtterance.text);
            }
            state.usermenu = menuContentPanel.GetComponentsInChildren<MenuInputPanelObject>();
        }
    }


}
