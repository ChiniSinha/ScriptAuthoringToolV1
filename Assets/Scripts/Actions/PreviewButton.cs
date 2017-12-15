using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreviewButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera agentCamera;
    public GameObject viewPanel;
    public Transform statePanel;
    public GameObject noScriptPanel;

    public void ShowUIPanel()
    {
   
        Camera main = GameObject.Find("Main Camera").GetComponent<Camera>();
        main.enabled = true;
        Camera agent = GameObject.Find("AgentCamera").GetComponent<Camera>();
        agent.enabled = false;
        Globals.Ui.SetActive(false);
        Globals.mainCanvas.SetActive(true);
    }

    public void ShowAgentView()
    {
        
        if(statePanel.GetComponentsInChildren<StatePanelObject>().Length > 0)
        {
            //enable gui
            Globals.Ui.SetActive(true);
            Globals.EventBus.Dispatch(new ClearUiEvent());

            agentCamera.enabled = true;
            mainCamera.enabled = false;
            //get current script
            ScriptRunner scriptRunner = Globals.Get<ScriptRunner>();
            //call scriptrunner with script
            scriptRunner.PreviewScript();

            viewPanel.SetActive(false);
        } else
        {
            noScriptPanel.SetActive(true);
        }
        
    }
}
