using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class UIPanelSelection : MonoBehaviour
{

    public string type;
    public InputField stateName;

    
    public void SetType()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (type == "AGENT")
        {
            GameObject agentPanel = canvas.transform.Find("AgentUIPanel").gameObject;
            GameObject scrollView = agentPanel.transform.Find("Agent Scroll View").gameObject;
            AgentInputObject[] agents = scrollView.GetComponentsInChildren<AgentInputObject>(); 
            if (agents != null) 
            {
                foreach (AgentInputObject agent in agents)
                {
                    agent.gameObject.SetActive(false);
                }
            }
            agentPanel.SetActive(true);
        }
        else if (type == "MENU")
        {
            GameObject menuPanel = canvas.transform.Find("MenuUIPanel").gameObject;
            GameObject scrollView = menuPanel.transform.Find("Menu Scroll View").gameObject;
            MenuInputPanelObject[] menus = scrollView.GetComponentsInChildren<MenuInputPanelObject>();
            if (menus != null)
            {
                foreach (MenuInputPanelObject menu in menus)
                {
                    menu.gameObject.SetActive(false);
                }
            }
            menuPanel.SetActive(true);
        }
    }
}
