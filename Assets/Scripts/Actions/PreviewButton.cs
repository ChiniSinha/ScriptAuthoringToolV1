using UnityEngine;
using System.Collections;

public class PreviewButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera agentCamera;
    public GameObject starter;
    //public GameObject viewPanel;

    public void ShowUIPanel()
    {
        agentCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public void ShowAgentView()
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
 
        //starter.SetActive(true);
        //TODO: Enable vert3
       // viewPanel.SetActive(true);
    }
}
