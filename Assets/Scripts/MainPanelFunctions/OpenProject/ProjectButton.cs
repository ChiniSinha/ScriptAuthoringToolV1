using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectButton : MonoBehaviour {

    public Button button;
    public Text projectName;
    public Text projectLocation;

    private void Start()
    {
        button.onClick.AddListener(handleClick);    
    }

    public void SetUp(Config.ProjectListData project)
    {
        projectName.text = project.projectName;
        projectLocation.text = project.projectLocationPath;
        Globals.PROJECTNAME = project.projectName;
        Globals.PROJECTPATH = project.projectLocationPath;
    }

    public void handleClick()
    {
        //TODO: How to open a project - yet to implement.
        SceneManager.LoadScene(1);
    }
	
}
