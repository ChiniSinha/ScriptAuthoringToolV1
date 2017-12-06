using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AgentDoneButton : MonoBehaviour
{

    public Transform contentPanel;
    AgentInputObject[] agentUtterances; 

    public void saveUtterances()
    {
        agentUtterances = contentPanel.transform.gameObject.GetComponentsInChildren<AgentInputObject>();

        foreach(AgentInputObject agent in agentUtterances)
        {
            Debug.Log("Agent: " + agent.agentUtterance.text);
        }
    }
}
