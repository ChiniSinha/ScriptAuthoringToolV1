using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddStateButton : MonoBehaviour {

    public Button addStateButton;
    public Transform contentPanel;
    public SimpleObjectPool stateObjectPool;

    public void handleClick()
    {
        GameObject newState = stateObjectPool.GetObject();
        newState.transform.SetParent(contentPanel);
    }
}
