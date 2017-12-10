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
            GameObject scriptTab = GameObject.Find("ScriptView").transform.GetChild(1).gameObject;
            scriptTab.GetComponent<Button>().onClick.Invoke();

            GameObject scriptPanel = GameObject.Find("ScriptView");
            GameObject tab = scriptPanel.transform.Find("Tab").gameObject;
            Text tabLabel = tab.GetComponentInChildren<Text>();
            tabLabel.text = contentName.text;
            MyGlobals.CURRENTSCRIPTNAME = this.contentName.text;
            MyGlobals.CURRENTSCRIPTPATH = this.jsonPath;
            GameObject scriptPane = scriptPanel.transform.Find("Pane").gameObject;
            Transform contentPanel = scriptPane.transform.GetChild(0)
                .transform.GetChild(0).transform.GetChild(0).transform;
            foreach(Transform child in contentPanel)
            {
                Destroy(child.gameObject);
            }

            GameObject statePane = GameObject.Find("StateView");
            Transform stateContentPanel = statePane.transform.GetChild(0)
                .transform.GetChild(0).transform.GetChild(0).transform;
            foreach (Transform child in stateContentPanel)
            {
                Destroy(child.gameObject);
            }

            SimpleObjectPool stateViewPool = GameObject.Find("StateViewObjectPool").GetComponent<SimpleObjectPool>();
            MyScript script = ScriptConfig.load(this.jsonPath);
            if(script != null)
            {
                foreach (State state in script.States)
                {
                    GameObject stateObject = stateObjectPool.GetObject();
                    stateObject.transform.SetParent(contentPanel);
                    stateObject.transform.Reset();

                    StatePanelObject statePanel = stateObject.GetComponent<StatePanelObject>();
                    statePanel.setUp(state);

                    GameObject stateViewObject = stateViewPool.GetObject();
                    stateViewObject.transform.SetParent(stateContentPanel);
                    stateViewObject.transform.Reset();

                    SelectStateButton stateButton = stateViewObject.GetComponent<SelectStateButton>();
                    stateButton.state = statePanel;

                }
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
