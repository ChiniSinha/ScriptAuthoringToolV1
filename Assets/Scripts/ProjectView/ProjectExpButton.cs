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
    string contentPath;
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

    private void handleFileClick()
    {
        throw new NotImplementedException();
    }

    public void setUp(ExpItem expItem) 
    {
        contentName.text = expItem.contentName;
        contentIcon.sprite = expItem.contentIcon;
        contentPath = expItem.contentPath;
        type = expItem.contentType;
        childFiles = expItem.childButtons;
    }

}
