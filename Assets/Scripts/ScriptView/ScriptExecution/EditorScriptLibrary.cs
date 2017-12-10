using System;
using System.IO;
using TinyJSON;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class EditorScriptLibrary : Object, IScriptLibrary
{
    private readonly string _projectName;

    public EditorScriptLibrary(string projectName)
    {
        _projectName = projectName;
    }

    public string FunctionFileContents
    {
        get
        {
#if UNITY_EDITOR
            return  File.ReadAllText(Path.Combine(MyGlobals.PROJECTPATH, "functions.txt"));
#else
            throw new NotImplementedException();
#endif
        }
    }

    public Script GetScript(string scriptName)
    {
#if UNITY_EDITOR
        string[] chunks = scriptName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        string scriptPath = MyGlobals.CURRENTSCRIPTPATH;
        string contents = File.ReadAllText(scriptPath);
        return JSON.Load(contents).Make<Script>();
#else
        throw new NotImplementedException();
#endif
    }
}