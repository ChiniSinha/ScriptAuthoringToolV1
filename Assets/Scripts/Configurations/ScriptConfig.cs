using UnityEngine;
using System.IO;
using TinyJSON;
using System.Collections;

public class ScriptConfig : MonoBehaviour
{
    public string scriptName;
    public string scriptJsonPath;
    public Script script;

    public ScriptConfig()
    {
        script = new Script();
    }
 
    public static void save(Script script, string scriptJsonPath, bool prettyPrint=true)
    {

        var json = JSON.Dump(script);
        File.WriteAllText(scriptJsonPath, json);
    }

    public static Script load(string scriptJsonPath)
    {
        string json = File.ReadAllText(scriptJsonPath);
        if (json.Length > 0)
            return JSON.Load(json).Make<Script>();
        else
            return null;
        
    }

}
