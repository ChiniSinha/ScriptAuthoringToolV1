using UnityEngine;
using System.Collections;

public class PreviewButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera agentCamera;
    public GameObject viewPanel;

    public void ShowUIPanel()
    {
        //Globals.EventBus.Dispatch(new ClearUiEvent());

        //Globals.SetCommandQueue(new CommandQueue());
        //Globals.SetEventBus(new EventBus());
       
        Camera main = GameObject.Find("Main Camera").GetComponent<Camera>();
        main.enabled = true;
        Camera agent = GameObject.Find("AgentCamera").GetComponent<Camera>();
        agent.enabled = false;
        Globals.Ui.SetActive(false);
        Globals.mainCanvas.SetActive(true);
        //GameObject gameObject = GameObject.Find("Starter");
        //gameObject.GetComponent<GlobalStarter>().init();
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

       viewPanel.SetActive(false);
    }
}
