using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * AddStateButton class is used to add a new StatePanel prefab to the Script.
 */
public class AddStateButton : MonoBehaviour {

    // Consists of public variables that are povided with objects from the scene
    public Button addStateButton;
    public Transform contentPanel;
    public SimpleObjectPool stateObjectPool;
    public Button preview;

    // Method that is invoked on the click of Add State button. 
    public void handleClick()
    {
        MyGlobals.isDisplay = false;
        preview.interactable = false;
        GameObject newState = stateObjectPool.GetObject();
        newState.transform.SetParent(contentPanel);
        newState.transform.Reset();

    }
}
