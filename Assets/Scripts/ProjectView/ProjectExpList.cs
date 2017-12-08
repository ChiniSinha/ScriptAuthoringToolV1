using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public enum ContentType
{
    FILE, FOLDER
};

[Serializable]
public class ExpItem
{
    public string contentName;
    public Sprite contentIcon;
    public string contentPath;
    public string jsonPath;
    public ContentType contentType;
    public List<ProjectExpButton> childButtons;

    public ExpItem()
    {
        childButtons = new List<ProjectExpButton>();
    }
}

public class ProjectExpList : MonoBehaviour {

    ProjectConfig project;
    public Text projectName;
    public Transform contentPanel;
    public SimpleObjectPool expButtonPool;
    public Sprite fileSprite;
    public Sprite folderSprite;

    // Use this for initialization
	void Start () {
        Init();
	}

    public void Init()
    {
        Debug.Log("Name: " + MyGlobals.PROJECTNAME + " Path:" + MyGlobals.PROJECTPATH);
        if (File.Exists(Path.Combine(MyGlobals.PROJECTPATH, MyGlobals.PROJECTNAME + ".cfg")))
        {
            // nNew project flow
            if (ProjectConfig.Load() == null)
            {
                project = new ProjectConfig();
                project.name = MyGlobals.PROJECTNAME;
                project.path = MyGlobals.PROJECTPATH;

                ProjectConfig.save(project);
            }
            else
            {
                project = ProjectConfig.Load();
                ProjectConfig.updateProjectConfig();
            }
            Debug.Log(JsonUtility.ToJson(project, true));
            
            foreach (Transform child in contentPanel)
            {
                GameObject.Destroy(child.gameObject);
            }
            ProjectConfig.updateProjectConfig();
            float counter = 0f;
            ProjectConfig conf = ProjectConfig.Load();

            projectName.text = conf.name;
            setUpFiles(conf.files, counter, null);
            setUpProject(conf.folders, counter);
        }
    }

    private ExpItem setUpFiles(List<ProjectConfig.Files> files, float counter, ExpItem folderItem)
    {
        foreach(ProjectConfig.Files file in files)
        {
            GameObject newFile = expButtonPool.GetObject();
            newFile.transform.SetParent(contentPanel);

            ExpItem fileItem = new ExpItem();
            fileItem.contentName = file.fileName;
            fileItem.contentPath = file.filePath;
            fileItem.contentIcon = fileSprite;
            fileItem.jsonPath = file.jsonFilePath;
            fileItem.contentType = ContentType.FILE;

            ProjectExpButton fileButton = newFile.GetComponent<ProjectExpButton>();
            fileButton = changePosition(fileButton, counter);
            fileButton.setUp(fileItem);
            if (folderItem != null)
            {
                folderItem.childButtons.Add(fileButton);
                fileButton.gameObject.SetActive(false);
            }
        }

        return folderItem;
    }

    private void setUpProject(List<ProjectConfig> folders, float counter)
    {
        foreach(ProjectConfig folder in folders)
        {
            GameObject newFolder = expButtonPool.GetObject();
            newFolder.transform.SetParent(contentPanel);

            ExpItem folderItem = new ExpItem();
            folderItem.contentName = folder.name;
            folderItem.contentPath = folder.path;
            folderItem.contentIcon = folderSprite;
            folderItem.contentType = ContentType.FOLDER;

            ProjectExpButton folderButton = newFolder.GetComponent<ProjectExpButton>();
            folderButton = changePosition(folderButton, counter);
            folderButton.setUp(folderItem);

            folderItem = setUpFiles(folder.files, counter+10f, folderItem);
            setUpProject(folder.folders, counter+10f);
        }
    }

    ProjectExpButton changePosition(ProjectExpButton projectExp, float counter)
    {
        Vector3 imagePos = projectExp.contentIcon.transform.position;
        Debug.Log("x: " + imagePos.x + " y: " + imagePos.y + " z:" + imagePos.z);
        imagePos.x += counter;
        Debug.Log("x: " + imagePos.x + " y: " + imagePos.y + " z:" + imagePos.z);
        projectExp.contentIcon.transform.position = imagePos;

        Vector3 textPos = projectExp.contentName.transform.position;
        textPos.x += counter;
        projectExp.contentName.transform.position = textPos;

        return projectExp;
    }
}
