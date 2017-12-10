using System;
using System.IO;
using System.Collections.Generic;
using Jint.Native;
using UnityEngine;
using Random = System.Random;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class ScriptRunner
{
    private static bool inRevert;
    public static readonly string ScriptLoadTag = "scriptload";

    private readonly int _defaultTimeout = 10*60*1000; //10 minute default timeout

    private readonly Random _random = new Random();

    private readonly List<string> engineLog = new List<string>();

    public readonly string InputVariableName = "lastInput";

    protected readonly Stack<Script> scriptStack = new Stack<Script>();
    protected State _currentState;
    protected string _currentStateName;
    protected string _currentScriptName = "";
    private char[] charsToTrim = { '.' };

    protected ScriptJEngine _jEngine;

    protected List<MenuChoice> _lastChoices = new List<MenuChoice>();

    // TA: I still really don't like that these values are hardcoded
    protected MenuChoice _repeatChoice = new MenuChoice("Could you repeat that?|Can you say that again?|I didn't catch that", "Repeat");

    protected IScriptLibrary _scriptLibrary;

    private bool allowExecute = true;
    private string lastAction = "";

    private DefaultRAG2Deserializer rag2Deserializer = new DefaultRAG2Deserializer();

    private bool shouldProcessState;

    protected bool usingSensors = false;

    private bool requiresWaitForInput = false;

    public void Init()
    {
        LoadScriptLibrary(new EditorScriptLibrary(MyGlobals.PROJECTNAME));
    }

    public void LoadScriptLibrary(IScriptLibrary library)
    {
        _scriptLibrary = library;
        _jEngine = new ScriptJEngine(this, library.FunctionFileContents);
        Globals.EventBus.Dispatch(new NetworkStatusEvent(NetworkStatusEvent.AuthStatus.CONNECTION_ESTABLISHED));
    }

    public void Start()
    {
        LoadScript(MyGlobals.CURRENTSCRIPTNAME);
    }

    public void PushScript(string scriptName, string returnName)
    {
        if (returnName.Contains("."))
        {
            Script nextScript = _scriptLibrary.GetScript(returnName);
            scriptStack.Push(nextScript);
        }
        else
        {
            scriptStack.Peek().SetNextState(returnName);
        }

        Script script = _scriptLibrary.GetScript(scriptName);
        scriptStack.Push(script);
        _currentState = scriptStack.Peek().GetStartingState();
        allowExecute = true;
        shouldProcessState = true;
    }

    protected void LoadScript(string scriptName)
    {
        MyScript myScript = ScriptConfig.load(MyGlobals.CURRENTSCRIPTPATH);
        Script script = new Script();
        script.States = myScript.States;
        scriptStack.Push(script);
        _currentState = scriptStack.Peek().GetStartingState();
        // Update the current state in the StaticClass' "currentState" variable:
        _currentScriptName = MyGlobals.CURRENTSCRIPTNAME;
        allowExecute = true;
        shouldProcessState = true;
    }

    public void LoadState(string stateName)
    {
        if (inRevert)
        {
            return;
        }
        // Check if script has a new path:
        if (stateName.Contains("."))
        {
            LoadScript(stateName);
            return;
        }
        State nextState = scriptStack.Peek().GetState(stateName);
        if (nextState != null)
        {
			Debug.Log ("Loading next state of name:" + stateName);
            _currentState = nextState;
            // Update the current state in the StaticClass' "currentState" variable:
            allowExecute = true;
            shouldProcessState = true;
        }
        else
        {
            Debug.LogError("Could not find state:" + stateName);
        }
    }

    public object ProcessExecute(string action)
    {
        object result = null;
        engineLog.Add(lastAction);
        string errors = "";
        try
        {
            result = _jEngine.Run(action);
        }
        catch (Exception e)
        {
            Debug.LogError("Javascript engine encountered the following error:" + e.Message + "\n" + e.StackTrace);
        }
        lastAction = action;
        return result;
    }

    public bool CheckMenuCondition(string action)
    {
        object result = ProcessExecute(action);
        if (result is JsBoolean)
        {
            return (result as JsBoolean).ToBoolean();
        }
        return true;
    }

    protected void Revert()
    {
        //TODO: Revise this function
        /*
        inRevert = true;
        //TODO: Invoke some transaction function on the db
        //InitJEngine(customStateClass);
        _jEngine.Run(engineLog);
        lastAction = "";
        inRevert = false;
        */
    }

    protected void ProcessActionSets(List<List<Action>> sets)
    {
        int index = _random.Next(0, sets.Count);
        List<Action> set = sets[index];
        foreach (Action action in set)
        {
            if (action is SpeechAction)
            {
                string speech = (action as SpeechAction).Speech;
                speech = ReplaceVariables(speech);
                (action as SpeechAction).Speech = speech;
            }
            EnqueueCommand(action.GetCommand());
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit ();
		#endif
    }

    public string ReplaceVariables(string input)
    {
        string output = "";
        string variable = "";
        string result = "";
        int startIndex = 0;
        int endIndex = 0;
        input = cleanSyntax(input);
        while (input.Contains("["))
        {
            output += input.Substring(0, input.IndexOf("["));
            startIndex = input.IndexOf("[") + 1;
            endIndex = input.IndexOf("]");
            variable = input.Substring(startIndex, endIndex - startIndex);
            result = ""; // Globals.Get<Database>().GetTable<PropertyTable>().Get(variable);
            if (result == null)
            {
                Debug.LogError("[ScriptRunner] Variable [" + variable + "] not found in DB");
                result = "";
            }
            output += result;
            input = input.Substring(endIndex + 1);
        }
        output += input;
        return output;
    }

    protected string cleanSyntax(string input)
    {
        //Should be cleaing out any potential microsoft symbol
        string output = input;
        if (output.IndexOf('\u2013') > -1)
        {
            output = output.Replace('\u2013', '-');
        }
        if (output.IndexOf('\u2014') > -1)
        {
            output = output.Replace('\u2014', '-');
        }
        if (output.IndexOf('\u2015') > -1)
        {
            output = output.Replace('\u2015', '-');
        }
        if (output.IndexOf('\u2017') > -1)
        {
            output = output.Replace('\u2017', '_');
        }
        if (output.IndexOf('\u2018') > -1)
        {
            output = output.Replace('\u2018', '\'');
        }
        if (output.IndexOf('\u2019') > -1)
        {
            output = output.Replace('\u2019', '\'');
        }
        if (output.IndexOf('\u201a') > -1)
        {
            output = output.Replace('\u201a', ',');
        }
        if (output.IndexOf('\u201b') > -1)
        {
            output = output.Replace('\u201b', '\'');
        }
        if (output.IndexOf('\u201c') > -1)
        {
            output = output.Replace('\u201c', '\"');
        }
        if (output.IndexOf('\u201d') > -1)
        {
            output = output.Replace('\u201d', '\"');
        }
        if (output.IndexOf('\u201e') > -1)
        {
            output = output.Replace('\u201e', '\"');
        }
        if (output.IndexOf('\u2026') > -1)
        {
            output = output.Replace("\u2026", "...");
        }
        if (output.IndexOf('\u2032') > -1)
        {
            output = output.Replace('\u2032', '\'');
        }
        if (output.IndexOf('\u2033') > -1)
        {
            output = output.Replace('\u2033', '\"');
        }
        return output;
    }

    protected void EnqueueCommand(BaseCommand cmd)
    {
        Globals.CommandQueue.Enqueue(cmd);
    }

    public void ProcessTextInput(string input, int buttonPressed)
    {
        //Store the last input
        // - Globals.Get<Database>().GetTable<PropertyTable>().Set(InputVariableName, input);
        ProcessMenuChoice(buttonPressed);
    }
    

    public void ProcessCheckboxInput(List<int> selectedOptions, int buttonPressed)
    {
        Checkbox ragMenu = (Checkbox) _currentState.Ui;
        if (buttonPressed >= _lastChoices.Count)
        {
            shouldProcessState = true;
            return;
        }
        string result = "";
        foreach (int choice in selectedOptions)
        {
            if (result != "")
            {
                result += "|";
            }
            result += ragMenu.Choices[choice];
        }
        //TODO Store in a different way? - Yes in file - CHini
        //Globals.Get<Database>().GetTable<PropertyTable>().Set(InputVariableName, result);
        //Globals.Get<Database>().GetTable<PropertyTable>().Set("lastCheckboxCount", selectedOptions.Count + "");
        ProcessMenuChoice(buttonPressed);
    }

    public void ProcessMenuInput(int selectedChoice)
    {
        if (selectedChoice >= _lastChoices.Count)
        {
            shouldProcessState = true;
            return;
        }
        ProcessMenuChoice(selectedChoice);
    }

    protected void ProcessMenuChoice(int choice)
    {
        MenuChoice menuChoice = _lastChoices[choice];
        if (menuChoice.Revert)
        {
            Revert();
        }
        if (menuChoice.ReturnState != null)
        {
            if (menuChoice.ReturnState.Contains("."))
            {
                Script nextScript = _scriptLibrary.GetScript(menuChoice.ReturnState);
                scriptStack.Push(nextScript);
            }
            else
            {
                scriptStack.Peek().SetNextState(menuChoice.ReturnState);
            }
        }

        if (menuChoice.Execute != null)
        {
            ProcessExecute(menuChoice.Execute);
        }

        if (menuChoice.NextState != null)
        {
            LoadState(menuChoice.NextState);
        }
    }

    public void ProcessTableDisplay(int selectedChoice)
    {
        if (selectedChoice >= _lastChoices.Count)
        {
            shouldProcessState = true;
            return;
        }
        ProcessMenuChoice(selectedChoice);
    }

    public void OnTimeout()
    {
        string timeoutScript = Globals.Config.Script.TimeoutScript;
        ProcessExecute("PUSH(\""+ timeoutScript +"\",\"" + _currentState.StateName + "\")");
    }

    public void Pop()
    {
        if (inRevert)
        {
            return;
        }
        scriptStack.Pop();
        if (scriptStack.Count == 0)
        {
            Exit();
        }
        if (scriptStack.Peek().GetNextState() != null)
        {
            _currentState = scriptStack.Peek().GetNextState();
        }
        else
        {
            _currentState = scriptStack.Peek().GetStartingState();
        }
        allowExecute = true;
        shouldProcessState = true;
    }

    public void ProcessUILogic(UI ui)
    {
        EnqueueCommand(GetUICommand(ui));
        Globals.CommandQueue.Enqueue(new WaitForUserInputCommand(_currentState.Timeout, _defaultTimeout));
    }

    //Start of scriptobject functions
    protected BaseCommand GetUICommand(UI ui)
    {
        string[] menuOptions;
        if (ui.GetType() == typeof(RagMenu))
        {
            RagMenu ragMenu = (RagMenu) ui;
            menuOptions = GetMenuOptions(ragMenu.Menu, _repeatChoice, ragMenu.ForceRepeat);
            return new ShowMenuCommand(menuOptions, ragMenu.ElementName);
        }
        if (ui.GetType() == typeof(Checkbox))
        {
            Checkbox checkbox = (Checkbox) ui;
            string[] checkboxOptions = checkbox.Choices.ToArray();
            menuOptions = GetMenuOptions(checkbox.Menu, _repeatChoice,checkbox.ForceRepeat);
            return new ShowChecklistCommand(checkbox.Prompt, checkboxOptions, menuOptions);
        }
        
        if (ui.GetType() == typeof(TextPrompt))
        {
            TextPrompt textPrompt = (TextPrompt) ui;
            menuOptions = GetMenuOptions(textPrompt.Menu, _repeatChoice, textPrompt.ForceRepeat);
            return new ShowTextInputCommand(textPrompt.Prompt, menuOptions);
        }
        if (ui.GetType() == typeof(TableDisplay))
        {
            TableDisplay tableDisplay = (TableDisplay) ui;
            List<List<string>> Contents = tableDisplay.Contents;
            List<string> Options = null;
            if (tableDisplay.Menu!= null && tableDisplay.Menu.Count > 0)
            {
                menuOptions = GetMenuOptions(tableDisplay.Menu, _repeatChoice, tableDisplay.ForceRepeat);
                Options = new List<string>(menuOptions);
            } else
            {
                Debug.LogError("Error in TableDisplay Syntax, no menu options found, use tabledisplay action instaed!");
                Options = new List<string>();
            }
            bool BoldTopRow = tableDisplay.BoldTopRow;
            for (int i = 0; i < Contents.Count; i++)
            {
                for (int j = 0; j < Contents[i].Count; j++)
                {
                    Contents[i][j] = ReplaceVariables(Contents[i][j]);
                }
            }
            return new DisplayTableWithMenuCommand(Contents, Options, BoldTopRow);
        }
        if (ui.GetType() == typeof(Exit))
        {
            return new ExitApplicationCommand();
        }
        Debug.LogError("Could not find UI of type " + ui.GetType());
        return null;
    }

    protected string[] GetMenuOptions(List<MenuChoice> choices, MenuChoice repeatOption = null, bool forceRepeat = true)
    {
        List<string> menuOptions = new List<string>();
        _lastChoices.Clear();
        string[] choiceVarients;
        int choiceNumber = 0;
        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].Condition != null)
            {
                if (!CheckMenuCondition(choices[i].Condition))
                {
                    continue;
                }
            }
            if (choices[i].Text.Contains("|"))
            {
                choiceVarients = choices[i].Text.Split('|');
                choiceNumber = _random.Next(0, choiceVarients.Length);
                menuOptions.Add(choiceVarients[choiceNumber]);
            }
            else
            {
                menuOptions.Add(choices[i].Text);
            }
            _lastChoices.Add(choices[i]);
        }
        if (repeatOption != null && forceRepeat)
        {
            menuOptions.Add(repeatOption.Text);
        }
        return menuOptions.ToArray();
    }


    //end of scriptobject functions

    public void OnUpdate()
    {
        if (!shouldProcessState)
        {
            return;
        }
        shouldProcessState = false;
        State tempState = _currentState;
        //Check for actions
        if (tempState.ActionSets != null)
        {
            ProcessActionSets(tempState.ActionSets);
        }
        //Check for executes
        if ((tempState.Execute != null) && allowExecute)
        {
            allowExecute = false;
            EnqueueCommand(new ProcessExecuteCommand(tempState.Execute));
        }
        //Check for UI
        if (tempState.Ui != null)
        {
            //This is done to ensure executes occur before creating any ui logic is done
            EnqueueCommand(new ProcessUILogicCommand(tempState.Ui));
        }
    }
}