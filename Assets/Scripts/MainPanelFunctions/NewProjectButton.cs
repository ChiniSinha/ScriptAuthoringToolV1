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
        string projectPath = Path.Combine(MyConfig.Load().projectData.projectPath, projectName.text);
        try
        {
            //Adding Project details to the config file
            MyConfig config = MyConfig.Load();
            List<MyConfig.ProjectListData> list = config.projectList;
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
                File.Create(Path.Combine(projectPath, "Top.json"));
                File.Create(Path.Combine(projectPath, "functions.txt"));
                File.Create(Path.Combine(projectPath, UsedValues.propertyFile));
                MyConfig.ProjectListData newElement = MyConfig.GetNewProjectListItem(projectName.text, projectPath);
                list.Add(newElement);
                config.projectList = list;
                Debug.Log(JsonUtility.ToJson(config));
                MyConfig.Save(config);
                MyGlobals.PROJECTNAME = projectName.text;
                MyGlobals.PROJECTPATH = projectPath;
                SceneManager.LoadScene(index);
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Exception: " + ex.StackTrace);
        }
    }

    private bool CheckDuplicateProject(MyConfig config)
    {
        List<MyConfig.ProjectListData> list = MyConfig.Load().projectList;
        bool duplicate = false;
        foreach (MyConfig.ProjectListData data in list)
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
