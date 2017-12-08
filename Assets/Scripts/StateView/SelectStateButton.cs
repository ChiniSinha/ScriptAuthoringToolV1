using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SelectStateButton : MonoBehaviour
{

    public Button button;
    public StatePanelObject state;
    Transform stateContentPanel;

    private void Start()
    {
        button.gameObject.GetComponentInChildren<Text>().text = state.stateName.text;
        button.onClick.AddListener(handleClick);
    }

    public void handleClick()
    {
        string stateName = this.gameObject.GetComponentInChildren<Text>().text;
        GameObject scriptPanel = GameObject.Find("ScriptView");
        GameObject scriptPane = scriptPanel.transform.Find("Pane").gameObject;
        stateContentPanel = scriptPane.transform.GetChild(0)
            .transform.GetChild(0).transform.GetChild(0).transform;
        StatePanelObject[] states = stateContentPanel.GetComponentsInChildren<StatePanelObject>();
        foreach(StatePanelObject state in states)
        {
            if (state.stateName.text == stateName)
            {
                foreach(Transform child in state.gameObject.transform)
                {
                    if(child.name == "StateNameInput")
                    {
                        child.gameObject.GetComponent<InputField>().Select();
                        child.gameObject.GetComponent<InputField>().ActivateInputField();
                        break;
                    } 
                }
                break;
            }
        }
    }
}
