using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextInputPanel : MonoBehaviour
{

    public InputField prompt;
    public List<MenuInputPanelObject> usermenu;

    public Transform menuPanel;

    public SimpleObjectPool menuPool;
    
    public void SetUp(UI ui)
    {

    }
}
