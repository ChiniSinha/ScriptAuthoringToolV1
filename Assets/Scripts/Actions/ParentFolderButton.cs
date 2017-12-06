using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class ParentFolderButton : MonoBehaviour
{
    public Text defaultPath;

    public void handleClick()
    {
        string selectedPath = EditorUtility.OpenFolderPanel("Select Script Folder", Globals.PROJECTPATH, "");
        if (selectedPath != "")
        {
            defaultPath.text = selectedPath;
            ProjectConfig.updateProjectConfig();
        }
    }

}