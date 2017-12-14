using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenProjectButton : MonoBehaviour {

    public GameObject openPanel;
    public GameObject mainPanel;

    public void onOpenClick()
    {
        openPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void cancelAction()
    {
        openPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
