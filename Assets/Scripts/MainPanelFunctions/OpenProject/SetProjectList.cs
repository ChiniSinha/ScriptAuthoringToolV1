using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProjectList : MonoBehaviour {

    List<Config.ProjectListData> projectList = new List<Config.ProjectListData>();
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    private void Start()
    {
        projectList = Config.Load().projectList;
        AddProjectButtons();
    }

    private void AddProjectButtons()
    {
        foreach(Config.ProjectListData project in projectList)
        {
            GameObject newProject = buttonObjectPool.GetObject();
            newProject.transform.SetParent(contentPanel);

            ProjectButton projectButton = newProject.GetComponent<ProjectButton>();
            projectButton.SetUp(project);
        }
    }
}
