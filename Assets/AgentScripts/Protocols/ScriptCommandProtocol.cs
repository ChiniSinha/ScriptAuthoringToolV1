// TODO: Condense this into one class with ScriptController

using System.Collections.Generic;
using UnityEngine;

public class ScriptCommandProtocol : ICommandProtocol
{
    public ScriptCommandProtocol(ScriptRunner scriptRunner)
    {
        ScriptRunner = scriptRunner;
    }

    public ScriptRunner ScriptRunner { get; private set; }

    public void TryConnect()
    {
        ScriptRunner.Init();
    }

    public void Startup()
    {
        ScriptRunner.Start();
    }

    public void SendDummyInputResponse()
    {
        ScriptRunner.ProcessMenuInput(0);
    }

    public void Shutdown()
    {
    }

    public void SendMenuSelection(int selectedOption)
    {
        ScriptRunner.ProcessMenuInput(selectedOption);
    }

    public void SendGHSMenuselection(string selectedScriptPath)
    {
        ScriptRunner.LoadState(selectedScriptPath);
    }

    public void SendTextInput(string input, int buttonPressed)
    {
        ScriptRunner.ProcessTextInput(input, buttonPressed);
    }
    
    public void SendTableInput(int selectedOption)
    {
        ScriptRunner.ProcessTableDisplay(selectedOption);
    }

    public void SendNumberInput(int number, int buttonPressed)
    {
        ScriptRunner.ProcessTextInput(number.ToString(), buttonPressed);
    }

    public void SendCheckboxInput(int[] selectedOptions, int buttonPressed)
    {
        ScriptRunner.ProcessCheckboxInput(new List<int>(selectedOptions), buttonPressed);
    }

    public void SendVideoPlayerInput(int buttonPressed)
    {
        ScriptRunner.ProcessMenuInput(buttonPressed);
    }

    public void SendExecute(string command)
    {
        ScriptRunner.ProcessExecute(command);
    }

    public void SendUILogic(UI ui)
    {
        ScriptRunner.ProcessUILogic(ui);
    }

    public void SendTimeout()
    {
        ScriptRunner.OnTimeout();
    }

    public void SendBugReport(string message, string type)
    {
        message = message + "|" + type;
        Debug.Log(message);
        Debug.Log("In ScriptCommandProtocol: Sending bug message -> " + message);
    }
}