using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectPanel : MonoBehaviour
{
 
    public GameObject openPanel;
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject settingsPanel;

    public InputField projectName;
    public InputField defaultPath;

    public void OpenProjectPane()
    {
        openPanel.SetActive(true);
        mainPanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    public void helpOnClick()
    {
        helpPanel.SetActive(true);
        mainPanel.SetActive(false);
        openPanel.SetActive(false);
    }

    public void LoadEditor(int index)
    {
        Debug.Log(projectName.text);
        if (projectName.text == "")
        {
            //projectName.placeholder = "Please Enter Project Name!!";
            projectName.placeholder.color = Color.red;
        }
        else
        {
            //TODO: Create the files and then load the scene
            SceneManager.LoadScene(index);
        }
    }

    public void cancelAction()
    {
        openPanel.SetActive(false);
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
        projectName.placeholder.color = Color.gray;
    }
}