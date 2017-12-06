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

    public static void save(ProjectConfig projectConfig, bool prettyPrint = true)
    {
        string json = JsonUtility.ToJson(projectConfig, prettyPrint);
        File.WriteAllText(Path.Combine(projectConfig.path, projectConfig.name + ".cfg"), json);
    }

    public static void createFolder(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
        
    }

    public static void createScript(string path, string name)
    {
        try
        {
            File.Create(Path.Combine(path, name + ".script"));
            File.Create(Path.Combine(path, name + ".json"));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }

    public static void updateProjectConfig()
    {
        ProjectConfig returnConfig = ProjectConfig.Load();
        InitScene refresh = new InitScene();

        returnConfig.folders = refresh.GetFolders(returnConfig.path);
        returnConfig.files = refresh.GetFilesInFolder(returnConfig.path);

        ProjectConfig.save(returnConfig);
    }

    public static ProjectConfig Load()
    {
        string json = File.ReadAllText(Path.Combine(Globals.PROJECTPATH, Globals.PROJECTNAME + ".cfg"));
        return JsonUtility.FromJson<ProjectConfig>(json);
    }

    [Serializable]
    public class Files
    {
        public string fileName;
        public string filePath;
        public string jsonFilePath;
    }

}