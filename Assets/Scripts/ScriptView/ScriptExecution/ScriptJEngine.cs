using System;
using Jint;
using Jint.Native;
using UnityEngine;

public class ScriptJEngine : JintEngine
{
    private readonly ScriptRunner _runner;

    public ScriptJEngine(ScriptRunner runner, string functions)
    {
        _runner = runner;
        SetJEngineFunctions();
        Run(functions);
    }

    private void SetJEngineFunctions()
    {
        //Scriptrunner Functions
        SetFunction("GO", new Jint.Delegates.Action<string>(_runner.LoadState));
        SetFunction("PUSH", new Jint.Delegates.Action<string, string>(_runner.PushScript));
        SetFunction("POP", new Jint.Delegates.Action(_runner.Pop));
        SetFunction("EXIT", new Jint.Delegates.Action(_runner.Exit));
        //Database Functions
        SetFunction("SET", new Jint.Delegates.Action<string, object>(SetVariable));
        SetFunction("GET", new Jint.Delegates.Func<string, string>(GetVariable));

        //Generic Functions
        SetFunction("LOG", new Jint.Delegates.Action<object>(Log));
        SetFunction("UNDEFINED", new Jint.Delegates.Func<JsInstance, bool>(CheckUndefined));
        SetFunction("GET_EQ", new Jint.Delegates.Func<string, string, bool>(CheckEquals));
        SetFunction("GETINT", new Jint.Delegates.Func<string, JsNumber>(GetAsInt));
        SetFunction("ISEDITOR", new Jint.Delegates.Func<bool>(IsEditor));
        SetFunction("TIMESINCEDATE", new Jint.Delegates.Func<string, JsNumber>(TimeSinceDate));
        SetFunction("GETTEXT", new Jint.Delegates.Func<string>(GetText));
    }

    private void Log(object t)
    {
        Debug.Log(t);
    }

    private JsNumber TimeSinceDate(string date)
    {
        DateTime priorTime = Convert.ToDateTime(date);
        DateTime currentTime = DateTime.Now;
        JsNumber num = new JsNumber(DateTime.Compare(priorTime, currentTime), JsUndefined.Instance);
        return num;
    }

    private JsNumber GetAsInt(string input)
    {
        string result = GetVariable(input);
        int value = result.ParseInt();
        JsNumber num = new JsNumber(value, JsUndefined.Instance);
        return num;
    }

    private bool CheckUndefined(JsInstance variable)
    {
        return variable.Type == "undefined";
    }

    private bool CheckEquals(string first, string second)
    {
        return first.Equals(second);
    }


    private bool IsEditor()
    {
        return Application.isEditor;
    }

    //TO DO: Creating a Property file System
    private void SetVariable(string name, object value)
    {
        //Globals.Get<Database>().GetTable<PropertyTable>().Set(name, value.ToString());
    }

    private string GetVariable(string name)
    {
        return ""; // Globals.Get<Database>().GetTable<PropertyTable>().Get(name);
    }

    private string GetText()
    {
        return GetVariable(_runner.InputVariableName);
    }
}