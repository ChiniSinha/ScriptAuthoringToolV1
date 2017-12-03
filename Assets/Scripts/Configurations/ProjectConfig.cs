using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ProjectConfig
{
    public string name;
    public string path;
    public List<Files> files;
    public List<ProjectConfig> folders;

    public ProjectConfig()
    {
        files = new List<Files>();
        folders = new List<ProjectConfig>();
    }

    public static void save(ProjectConfig projectConfig, bool prettyPrint = false)
    {
        string json = JsonUtility.ToJson(projectConfig, prettyPrint);
        File.WriteAllText(Path.Combine(projectConfig.path, projectConfig.name + ".cfg"), json);
    }

    public static Config Load()
    {
        string json = File.ReadAllText(Path.Combine(Globals.PROJECTPATH, Globals.PROJECTNAME + ".cfg"));
        return JsonUtility.FromJson<Config>(json);
    }

    [Serializable]
    public class Files
    {
        public string fileName;
        public string filePath;
        public string jsonFilePath;
    }

}