using UnityEngine;
using System.Collections;

public class PreviewButton : MonoBehaviour
{
    public Camera mainCamera;
    public Camera agentCamera;

    public void ShowUIPanel()
    {
        agentCamera.enabled = false;
        mainCamera.enabled = true;
    }

    public void ShowAgentView()
    {
        agentCamera.enabled = true;
        mainCamera.enabled = false;
        Debug.Log("Current Script Json Path: " + MyGlobals.CURRENTSCRIPTPATH);
    }
}
