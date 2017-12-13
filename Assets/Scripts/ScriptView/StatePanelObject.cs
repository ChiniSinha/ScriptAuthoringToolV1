using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatePanelObject : MonoBehaviour
{
    public InputField stateName;
    public Dropdown media;
    public InputField url;
    public InputField action;
    public List<AgentInputObject> agentUtterances;
    public List<MenuInputPanelObject> usermenu;
    public SimpleObjectPool agentPool;
    public SimpleObjectPool menuPool;
    public Transform agentContent;
    public Transform menuContent;
    public Toggle mediaToggle;
    public Toggle agentToggle;
    public Toggle actionToggle;
    public Toggle menuToggle;

    public void setUp(State state)
    {
        stateName.text = state.StateName;
        if(state.Execute != null)
        {
            actionToggle.isOn = true;
            action.text = "$" + state.Execute + " $";
            
        }
        if (state.ActionSets != null)
        {
            foreach (List<Action> set in state.ActionSets)
            {
                foreach (Action action in set)
                {
                    if (action is WhiteboardAction)
                    {
                        mediaToggle.isOn = true;
                        media.value = 1;
                        url.text = ((WhiteboardAction)action).Url;
                        break;
                    }
                    if (action is SpeechAction)
                    {
                        agentToggle.isOn = true;
                        GameObject agentObject = agentPool.GetObject();
                        agentObject.transform.SetParent(agentContent);
                        agentObject.transform.Reset();

                        AgentInputObject agent = agentObject.GetComponent<AgentInputObject>();
                        agent.SetUp((SpeechAction)action);
                        agentUtterances.Add(agent);

                    }
                }
            }
        }
        

        UI ui = state.Ui;
        if(ui is RagMenu && ((RagMenu)ui).Menu != null)
        {
            if(((RagMenu)ui).Menu.Count > 0)
            {
                menuToggle.isOn = true;
                foreach (MenuChoice menu in ((RagMenu)ui).Menu)
                {
                    GameObject menuObject = menuPool.GetObject();
                    menuObject.transform.SetParent(menuContent);
                    menuObject.transform.Reset();

                    MenuInputPanelObject choice = menuObject.GetComponent<MenuInputPanelObject>();
                    choice.SetUp(menu);
                    usermenu.Add(choice);
                }
            }     
        }
    }
}
