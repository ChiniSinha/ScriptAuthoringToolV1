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
        agentCamera.enabled = true;
        mainCamera.enabled = false;
        starter.SetActive(true);
       // viewPanel.SetActive(true);
    }
}
