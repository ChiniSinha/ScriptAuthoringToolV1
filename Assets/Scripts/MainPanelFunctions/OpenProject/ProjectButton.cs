using System.Collections;
using System.IO;
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

    public void SetUp(MyConfig.ProjectListData project)
    {
        projectName.text = project.projectName;
        projectLocation.text = project.projectLocationPath;
    }

    public void handleClick()
    {

        MyGlobals.PROJECTNAME = this.transform.GetChild(0).GetComponent<Text>().text;
        MyGlobals.PROJECTPATH = this.transform.GetChild(1).GetComponent<Text>().text;
        SceneManager.LoadScene(1);
    }
	
}
