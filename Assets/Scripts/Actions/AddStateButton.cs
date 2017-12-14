using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddStateButton : MonoBehaviour {

    public Button addStateButton;
    public Transform contentPanel;
    public SimpleObjectPool stateObjectPool;
    public Button preview;

    public void handleClick()
    {
        MyGlobals.isDisplay = false;
        preview.interactable = false;
        GameObject newState = stateObjectPool.GetObject();
        newState.transform.SetParent(contentPanel);
        newState.transform.Reset();

    }
}
