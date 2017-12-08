using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProjectList : MonoBehaviour {

    List<MyConfig.ProjectListData> projectList = new List<MyConfig.ProjectListData>();
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    private void Start()
    {
        projectList = MyConfig.Load().projectList;
        AddProjectButtons();
    }

    private void AddProjectButtons()
    {
        foreach(MyConfig.ProjectListData project in projectList)
        {
            GameObject newProject = buttonObjectPool.GetObject();
            newProject.transform.SetParent(contentPanel);
            //newProject.transform.Reset();

            ProjectButton projectButton = newProject.GetComponent<ProjectButton>();
            projectButton.SetUp(project);
        }
    }
}
