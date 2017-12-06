using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatePanelObject : MonoBehaviour
{
    public InputField stateName;
    public Dropdown media;
    public InputField url;
    public InputField action;
    public AgentInputObject[] agentUtterances;
    public MenuInputPanelObject[] usermenu;

    void Start()
    {
        Debug.Log("test media dropdown: " + media.captionText.text);
    }

    public void setUp(State state)
    {
        stateName.text = state.StateName;   
    }
   
}
