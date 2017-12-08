using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class AddScriptButton : MonoBehaviour
{
    public InputField scriptName;
    public Text scriptFolder;
    public GameObject addScriptPanel;
    public Text scriptTab;

    public void handleClick()
    {
        ProjectConfig.createScript(scriptFolder.text, scriptName.text);
        addScriptPanel.SetActive(false);
        scriptTab.text = scriptName.text + ".script";
        MyGlobals.CURRENTSCRIPTNAME = scriptName.text;
        MyGlobals.CURRENTSCRIPTPATH = scriptFolder.text + scriptName.text + ".json";
    }
}
    