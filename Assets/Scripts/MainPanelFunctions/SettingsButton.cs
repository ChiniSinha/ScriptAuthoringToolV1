﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject settingsPanel;

    public InputField defaultPath;

    public void settingsOnClick()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void cancelAction()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        settingsPanel.GetComponentInChildren<Text>().text = "";
    }

    public void getDefaultPath()
    {
        defaultPath.text = Config.Load().projectData.projectPath;
    }

    public void updateDefaultPath()
    {
        string documentsPath = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        Debug.Log(documentsPath);

        Config config = new Config();
        config.projectData.projectPath = settingsPanel.GetComponentInChildren<InputField>().text;
        Config.Save(config);
        Debug.Log("Config value: " + config.projectData.projectPath);
        Globals.SetConfig(config);
        Debug.Log("Value from file: " + JsonUtility.ToJson(Globals.Config.projectData));

        settingsPanel.GetComponentInChildren<Text>().text = "Update Successful!";
    }

    public void FolderOnClick()
    {
        defaultPath.text = EditorUtility.OpenFolderPanel("Select Project Folder", "", "");
    }
}