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

    void Start()
    {
        Debug.Log("test media dropdown: " + media.captionText.text);
    }

    public void setUp(State state)
    {
        stateName.text = state.StateName;
        if(action.text != "")
        {
            this.gameObject.transform.Find("ActionToggle").GetComponent<Toggle>().isOn = true;
            action.text = state.Execute;
            
        }
        foreach(List<Action> set in state.ActionSets)
        {
            foreach(Action action in set)
            {
                if(action is WhiteboardAction)
                {
                    this.gameObject.transform.Find("MediaToggle").GetComponent<Toggle>().isOn = true;
                    media.value = 1;
                    url.text = ((WhiteboardAction)action).Url;
                    break;
                }
                if (action is SpeechAction)
                {
                    this.gameObject.transform.Find("AgentToggle").GetComponent<Toggle>().isOn = true;
                    GameObject agentObject = agentPool.GetObject();
                    agentObject.transform.SetParent(agentContent);
                    agentObject.transform.Reset();

                    AgentInputObject agent = agentObject.GetComponent<AgentInputObject>();
                    agent.SetUp((SpeechAction)action);
                    agentUtterances.Add(agent);
                }
            }
        }
        UI ui = state.Ui;
        if(ui is RagMenu)
        {
            this.gameObject.transform.Find("MenuToggle").GetComponent<Toggle>().isOn = true;
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
