using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScriptScene : MonoBehaviour {

    public GameObject scriptTab;
    public GameObject topTab;
    public GameObject advancedTab;

    // Use this for initialization

    public void onScriptTabClick ()
    {
        scriptTab.SetActive(true);
        topTab.SetActive(false);
        advancedTab.SetActive(false);
    }

    public void onTopTabClick()
    {
        scriptTab.SetActive(false);
        topTab.SetActive(true);
        advancedTab.SetActive(false);
    }

    public void onAdvancedTabClick()
    {
        scriptTab.SetActive(false);
        topTab.SetActive(false);
        advancedTab.SetActive(true);
    }
}