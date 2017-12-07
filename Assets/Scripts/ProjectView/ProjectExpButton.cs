using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectExpButton : MonoBehaviour {

    public Button button;
    public Text contentName;
    public Image contentIcon;
    public bool isClicked;
    public SimpleObjectPool stateObjectPool;
    string contentPath;
    string jsonPath;
    ContentType type;
    List<ProjectExpButton> childFiles = new List<ProjectExpButton>();


	void Start () {
        button.onClick.AddListener(handleClick);
	}

    private void handleClick()
    {
        isClicked = !isClicked;
        if (type == ContentType.FOLDER && isClicked)
        {
            foreach(ProjectExpButton exp in childFiles)
            {
                exp.gameObject.SetActive(false);
            }
        }
        else if (type == ContentType.FOLDER && !isClicked)
        {
            foreach (ProjectExpButton exp in childFiles)
            {
                exp.gameObject.SetActive(true);
            }
        }
        else
        {
            handleFileClick();
        }
    }

    public void handleFileClick()
    {
        if ("functions.txt".Equals(this.contentName.text)) 
        {
            GameObject functionPanel = GameObject.Find("FunctionView").transform.GetChild(1).gameObject;
            functionPanel.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            GameObject scriptPanel = GameObject.Find("ScriptView");
            GameObject tab = scriptPanel.transform.Find("Tab").gameObject;
            Text tabLabel = tab.GetComponentInChildren<Text>();
            tabLabel.text = contentName.text;
            Globals.CURRENTSCRIPTNAME = this.contentName.text;
            Globals.CURRENTSCRIPTPATH = this.jsonPath;
            GameObject scriptPane = scriptPanel.transform.Find("Pane").gameObject;
            scriptPane.GetComponentInChildren<Text>().gameObject.SetActive(false);
            Transform contentPanel = scriptPane.transform.GetChild(0)
                .transform.GetChild(0).transform.GetChild(0).transform;
            foreach(Transform child in contentPanel)
            {
                Destroy(child.gameObject);
            }
            Script script = ScriptConfig.load(this.jsonPath);
            foreach(State state in script.States)
            {
                GameObject stateObject = stateObjectPool.GetObject();
                stateObject.transform.SetParent(contentPanel);

                StatePanelObject statePanel = stateObject.GetComponent<StatePanelObject>();
                statePanel.setUp(state);
                
            }
        } 
    }

    public void setUp(ExpItem expItem) 
    {
        contentName.text = expItem.contentName;
        contentIcon.sprite = expItem.contentIcon;
        contentPath = expItem.contentPath;
        jsonPath = expItem.jsonPath;
        type = expItem.contentType;
        childFiles = expItem.childButtons;
    }

}
