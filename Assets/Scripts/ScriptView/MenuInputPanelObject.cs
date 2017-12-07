using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuInputPanelObject : MonoBehaviour
{

    public InputField userResponse;
    public InputField nextState;

    // Use this for initialization
    void Start()
    {

    }

    public void SetUp(MenuChoice menu)
    {
        // TODO: Needs more kinds of implementation for other UI objects
        userResponse.text = menu.Text;
        if(menu.NextState != null)
        {
            nextState.text = menu.NextState;
        }
        else if (menu.Execute != null)
        {
            nextState.text = "$" + menu.Execute + "$";
        }
        
    }
}
