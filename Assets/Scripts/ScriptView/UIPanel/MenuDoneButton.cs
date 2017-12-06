using UnityEngine;
using System.Collections;

public class MenuDoneButton : MonoBehaviour
{

    public Transform contentPanel;
    MenuInputPanelObject[] userMenus;

    public void saveUtterances()
    {
        userMenus = contentPanel.transform.gameObject.GetComponentsInChildren<MenuInputPanelObject>();
       // Globals.agentUtterances = userMenus;

    }

}
