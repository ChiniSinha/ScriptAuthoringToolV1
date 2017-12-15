using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

/*
 * Add script button class is used to call the pop up to create a new script file in the system.
 */
public class AddScriptButton : MonoBehaviour
{
    // References to the following objects in the scene
    public InputField scriptName;
    public Text scriptFolder;
    public GameObject addScriptPanel;
    public Text error;

    // Method that is invoked on the onClick event of Script button
    public void handleClick()
    {
        error.text = "";
        if (scriptName.text != "")
        {
            
            ProjectConfig.createScript(scriptFolder.text, scriptName.text);
            addScriptPanel.SetActive(false);
            MyGlobals.CURRENTSCRIPTNAME = scriptName.text;
            MyGlobals.CURRENTSCRIPTPATH = scriptFolder.text + scriptName.text + ".json";

        }
        else
        {
            error.text = "No script name entered";
        }
        
    }
}
    