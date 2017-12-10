using UnityEngine;
using System.IO;
using TinyJSON;
using System.Collections;

public class ScriptConfig : MonoBehaviour
{
    public string scriptName;
    public string scriptJsonPath;
    public MyScript script;

    public ScriptConfig()
    {
        script = new MyScript();
    }
 
    public static void save(MyScript script, string scriptJsonPath, bool prettyPrint=true)
    {

        var json = JSON.Dump(script);
        File.WriteAllText(scriptJsonPath, json);
    }

    public static MyScript load(string scriptJsonPath)
    {
        string json = File.ReadAllText(scriptJsonPath);
        if (json.Length > 0)
            return JSON.Load(json).Make<MyScript>();
        else
            return null;
        
    }

}
