using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NewProjectButton : MonoBehaviour {

    public GameObject newProjectPanel;

    public InputField projectName;

    public void OpenNewProjectPane()
    {
        newProjectPanel.SetActive(true);
        newProjectPanel.GetComponentInChildren<Text>().text = "";
    }

    public void LoadEditor(int index)
    {
        Debug.Log("Testing panel" + newProjectPanel.GetComponentInChildren<Text>().text);
        if (projectName.text == "")
        {
            projectName.placeholder.color = Color.red;
        }
        else
        {
            CreateProjectConfigAndFiles(index);
        }
    }

    void CreateProjectConfigAndFiles(int index)
    {
        try
        {
            string projectPath = Path.Combine(Config.Load().projectData.projectPath, projectName.text);
            //Adding Project details to the config file
            Config config = new Config();
            config.projectData.projectPath = Config.Load().projectData.projectPath;
            List<Config.ProjectListData> list = Config.Load().projectList;
            if (CheckDuplicateProject(config))
            {
                newProjectPanel.GetComponentInChildren<Text>().text = "Project Name Already Exist!";
            } 
            else
            {
                // Creating basic project files and config file.
                Directory.CreateDirectory(projectPath);
                File.Create(Path.Combine(projectPath, projectName.text + ".cfg"));
                File.Create(Path.Combine(projectPath, "Top.script"));
                File.Create(Path.Combine(projectPath, "functions.txt"));
                Config.ProjectListData newElement = Config.GetNewProjectListItem(projectName.text, projectPath);
                list.Add(newElement);
                config.projectList = list;
                Debug.Log(JsonUtility.ToJson(config));
                Config.Save(config);
                Globals.PROJECTNAME = projectName.text;
                Globals.PROJECTPATH = projectPath;
                SceneManager.LoadScene(index);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Exception: " + ex.StackTrace);
        }
        
    }

    private bool CheckDuplicateProject(Config config)
    {
        List<Config.ProjectListData> list = Config.Load().projectList;
        bool duplicate = false;
        foreach (Config.ProjectListData data in list)
        {
            if (data.projectName == projectName.text)
            {
                duplicate = true;
            }
        }
        return duplicate;
    }

    public void cancelAction()
    {
        newProjectPanel.SetActive(false);
        projectName.placeholder.color = Color.gray;
        newProjectPanel.GetComponentInChildren<Text>().text = "";
    }

}
