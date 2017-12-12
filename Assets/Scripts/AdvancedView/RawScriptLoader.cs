using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class RawScriptLoader : MonoBehaviour
{

    public Text rawScript;

    public void ViewSscript()
    {
        string filePath = MyGlobals.CURRENTSCRIPTPATH.Replace(".json", ".script");
        rawScript.text = File.ReadAllText(filePath);
    }
}
