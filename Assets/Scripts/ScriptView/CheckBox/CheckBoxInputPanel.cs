using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckBoxInputPanel : MonoBehaviour
{

    public InputField prompt;
    public List<ChoicesInput> choices;
    public List<MenuInputPanelObject> usermenu;

    public Transform choicePanel;
    public Transform menuPanel;

    public SimpleObjectPool choicePool;
    public SimpleObjectPool menuPool;

    public void SetUp(Checkbox chk)
    {
       
        prompt.text = chk.Prompt;
        foreach(string choice in chk.Choices)
        {
            GameObject newChoice = choicePool.GetObject();
            newChoice.transform.SetParent(choicePanel);
            newChoice.transform.Reset();

            ChoicesInput choicesInput = newChoice.GetComponent<ChoicesInput>();
            choicesInput.SetUp(choice);
            choices.Add(choicesInput);      
        }
        foreach(MenuChoice menu in chk.Menu)
        {
            GameObject menuObject = menuPool.GetObject();
            menuObject.transform.SetParent(menuPanel);
            menuObject.transform.Reset();

            MenuInputPanelObject choice = menuObject.GetComponent<MenuInputPanelObject>();
            choice.SetUp(menu);
            usermenu.Add(choice);
        }
    }

}
