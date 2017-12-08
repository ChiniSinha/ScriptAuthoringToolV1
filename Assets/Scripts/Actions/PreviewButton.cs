using UnityEngine;
using System.Collections;

public class PreviewButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera agentCamera;
    public GameObject agentCanvas;

    public void ShowUIPanel()
    {
        agentCamera.enabled = false;
        mainCamera.enabled = true;
        agentCanvas.SetActive(false);
    }

    public void ShowAgentView()
    {
        agentCamera.enabled = true;
        mainCamera.enabled = false;
        agentCanvas.SetActive(true);
    }
}
