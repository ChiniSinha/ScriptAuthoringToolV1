using UnityEngine;
using System.IO;
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

        string json = JsonUtility.ToJson(script, prettyPrint);
        File.WriteAllText(scriptJsonPath, json);
    }

    public static Script load(string scriptJsonPath)
    {
        string json = File.ReadAllText(scriptJsonPath);
        return JsonUtility.FromJson<Script>(json);
    }

}
