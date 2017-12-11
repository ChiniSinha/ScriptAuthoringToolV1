using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    public GameObject settingsPanel;

    public InputField defaultPath;

    public void settingsOnClick()
    {
        settingsPanel.SetActive(true);
    }

    public void cancelAction()
    {
        settingsPanel.SetActive(false);
        settingsPanel.GetComponentInChildren<Text>().text = "";
    }

    public void getDefaultPath()
    {
        defaultPath.text = MyConfig.Load().projectData.projectPath;
    }

    public void updateDefaultPath()
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        Debug.Log(documentsPath);

        MyConfig config = new MyConfig();
        config.projectData.projectPath = settingsPanel.GetComponentInChildren<InputField>().text;
        MyConfig.Save(config);
        Debug.Log("Config value: " + config.projectData.projectPath);
        MyGlobals.SetConfig(config);
        Debug.Log("Value from file: " + JsonUtility.ToJson(MyGlobals.Config.projectData));

        settingsPanel.GetComponentInChildren<Text>().text = "Update Successful!";
    }

    public void FolderOnClick()
    {
        string selectedPath = EditorUtility.OpenFolderPanel("Select Project Folder", "", "");
        if (selectedPath != "")
        {
            defaultPath.text = selectedPath;
        }
    }
}
