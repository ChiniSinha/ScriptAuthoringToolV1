using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextInputPanel : MonoBehaviour
{

    public InputField prompt;
    public List<MenuInputPanelObject> usermenu;

    public Transform menuPanel;

    public SimpleObjectPool menuPool;
    
    public void SetUp(TextPrompt textPrompt)
    {
        prompt.text = textPrompt.Prompt;
        foreach (MenuChoice menu in textPrompt.Menu)
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
