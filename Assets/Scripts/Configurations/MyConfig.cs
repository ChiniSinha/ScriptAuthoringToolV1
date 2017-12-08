using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class MyConfig
{
 
    public ProjectData projectData;
    public List<ProjectListData> projectList;

    public MyConfig()
    {
        projectData = new ProjectData();
        projectList = new List<ProjectListData>();
    }
 
    public static void Save(MyConfig cfg, bool prettyPrint = false)
    {
        string json = JsonUtility.ToJson(cfg, prettyPrint);
        File.WriteAllText(Path.Combine(UsedValues.DefaultFilePath, UsedValues.ConfigFileName), json);
     }
 
     public static MyConfig Load()
     {
         string json = File.ReadAllText(Path.Combine(UsedValues.DefaultFilePath, UsedValues.ConfigFileName));
         return JsonUtility.FromJson<MyConfig>(json);
     }
 
     public static ProjectListData GetNewProjectListItem(string projName, string projectLocation)
     {
         ProjectListData projectListData = new ProjectListData();
         projectListData.projectName = projName;
         projectListData.projectLocationPath = projectLocation;
         return projectListData;
     }
 
     [Serializable]
     public class ProjectData
     {
         public string projectPath;
     }
 
     [Serializable]
     public class ProjectListData
     {
         public string projectName;
         public string projectLocationPath;
     }
 }