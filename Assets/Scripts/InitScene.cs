﻿using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InitScene : MonoBehaviour {

    public GameObject scriptTab;
    public GameObject topTab;
    public GameObject advancedTab;
    public GameObject scriptPanel;

    void Start()
    {
        Globals.mainCanvas = gameObject;
        scriptTab.SetActive(true);
        topTab.SetActive(false);
        advancedTab.SetActive(false);
        scriptPanel.SetActive(false);

        ProjectConfig config = new ProjectConfig();
        config.name = MyGlobals.PROJECTNAME;
        config.path = MyGlobals.PROJECTPATH;

        config.folders = GetFolders(MyGlobals.PROJECTPATH);
        config.files = GetFilesInFolder(MyGlobals.PROJECTPATH);

        ProjectConfig.save(config);
        MyGlobals.SetProjectConfig(config);
    }

    public List<ProjectConfig> GetFolders(string path)
    {
        List<ProjectConfig> returnFolders = new List<ProjectConfig>();
        string[] allDirectories = Directory.GetDirectories(path);
        foreach (var dir in allDirectories)
        {
            DirectoryInfo info = new DirectoryInfo(dir);
            ProjectConfig folder = new ProjectConfig();
            folder.name = info.Name;
            folder.path = info.FullName;
            Debug.Log("Dir Path:" + info.FullName);
            folder.files = GetFilesInFolder(info.FullName);
            folder.folders = GetFolders(info.FullName);
            returnFolders.Add(folder);
        }

        return returnFolders;
    }

    public List<ProjectConfig.Files> GetFilesInFolder(string path)
    {
        List<ProjectConfig.Files> files = new List<ProjectConfig.Files>();

        var allfiles = Directory.GetFiles(path).Where(name => !name.EndsWith(".cfg") && !name.EndsWith(".json") && !name.EndsWith(".dat"));
        foreach (var file in allfiles)
        {
            FileInfo info = new FileInfo(file);
            ProjectConfig.Files fileToAdd = new ProjectConfig.Files();
            fileToAdd.fileName = info.Name;
            fileToAdd.filePath = info.FullName;
            fileToAdd.jsonFilePath = info.FullName.Replace(".script", ".json");
            files.Add(fileToAdd);
        }

        return files;
    }
}