using UnityEngine;

public class HelpProjectButton : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject helpPanel;

    public void helpOnClick()
    {
        helpPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void cancelAction()
    {
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

}
