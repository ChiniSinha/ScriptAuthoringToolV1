using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Linq;
using System.Text;

public class SaveSriptButton : MonoBehaviour
{
    public Transform stateContentPanel;
    public GameObject savePanel;
    public GameObject errorMessage;
    public Transform stateViewContentPanel;
    public SimpleObjectPool stateViewObjectPool;
    public Transform errorViewContentPanel;
    public SimpleObjectPool errorViewObjectPool;

    
    public void HandleSave()
    {
        hasErrors = false;
        try
        {
            foreach(Transform child in errorViewContentPanel)
            {
                Destroy(child.gameObject);
            }
            Debug.Log("Script name:" + MyGlobals.CURRENTSCRIPTNAME);
            Debug.Log("Script path: " + MyGlobals.CURRENTSCRIPTPATH);
            ScriptConfig config = new ScriptConfig();
            config.scriptName = MyGlobals.CURRENTSCRIPTNAME;
            config.scriptJsonPath = MyGlobals.CURRENTSCRIPTPATH;
            config.script = ScriptConfig.load(MyGlobals.CURRENTSCRIPTPATH);

            saveScript();
            bool result = checkErrorsInProject();
            if (result)
                errorMessage.SetActive(true);
            else
            {
               
                savePanel.SetActive(true);
            }
            
        } 
        catch(Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }

    void saveScript()
    {
        StatePanelObject[] states = stateContentPanel.GetComponentsInChildren<StatePanelObject>();
        SaveRawScriptFile(states);
        Script script = new Script();
        script.States = new List<State>();
        foreach (Transform child in stateViewContentPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (StatePanelObject state in states)
        {
            Debug.Log("State name: " + state.stateName.text);
            State addState = GetStateForScript(state);
            script.States.Add(addState);

            GameObject stateElement = stateViewObjectPool.GetObject();
            stateElement.transform.SetParent(stateViewContentPanel);
            stateElement.transform.Reset();
            stateElement.transform.GetComponent<SelectStateButton>().state = state;

        }
        ScriptConfig.save(script, MyGlobals.CURRENTSCRIPTPATH);

    }

    private void SaveRawScriptFile(StatePanelObject[] states)
    {
        StringBuilder script = new StringBuilder();
        foreach(StatePanelObject state in states)
        {
            script.AppendLine("STATE: " + state.stateName.text);
            if(state.mediaToggle.isOn)
            {
                script.AppendLine("MEDIA: [" + state.media.captionText.text + "] " + state.url.text);
            }
            if (state.agentUtterances.Count > 0)
            {
                foreach (AgentInputObject agent in state.agentUtterances)
                {
                    script.AppendLine("AGENT: " + agent.agentUtterance.text);
                }
            }
            if (state.actionToggle.isOn)
            {
                script.AppendLine("ACTION: " + state.action.text);
            }
            else if (state.menuToggle.isOn)
            {
                if(state.usermenu.Count > 0)
                {
                    script.AppendLine("USERMENU: ");
                    foreach(MenuInputPanelObject menu in state.usermenu)
                    {
                        script.AppendLine(menu.userResponse.text + " => " + menu.nextState.text);
                    } 
                }
                else if (state.checkBox != null)
                {
                    script.AppendLine("USERTEXT: ");
                    script.AppendLine("Prompt: " + state.checkBox.prompt.text);
                    script.AppendLine("Choices: ");
                    foreach (ChoicesInput c in state.checkBox.choices)
                    {
                        script.Append(c.choiceInput.text + "| ");
                    }
                    script.AppendLine();
                    script.AppendLine("Usermenu: ");
                    foreach (MenuInputPanelObject menu in state.checkBox.usermenu)
                    {
                        script.AppendLine(menu.userResponse.text + " => " + menu.nextState.text);
                    }
                }
                else if (state.inputPanel != null)
                {
                    script.AppendLine("USERTEXT: ");
                    script.AppendLine("Prompt: " + state.inputPanel.prompt.text);
                    script.AppendLine("Usermenu: ");
                    foreach (MenuInputPanelObject menu in state.inputPanel.usermenu)
                    {
                        script.AppendLine(menu.userResponse.text + " => " + menu.nextState.text);
                    }
                }
            }
            script.AppendLine();
        }

        string filePath = MyGlobals.CURRENTSCRIPTPATH.Replace(".json", ".script");
        File.WriteAllText(filePath, script.ToString());
    }

    private void CheckUIErrors(StatePanelObject state)
    {
        if(state.stateName.text == "")
        {
            hasErrors = true;
            spawnErrorViewObject("StateName is required");
        }
        if(state.mediaToggle.isOn && (state.url.text == "" || state.media.value < 1)) 
        {
            hasErrors = true;
            spawnErrorViewObject("Media not added at " + state.stateName.text);
        }
        if(state.actionToggle.isOn && state.action.text == "")
        {
            hasErrors = true;
            spawnErrorViewObject("Action not added to state: " + state.stateName.text);
        }
        if(state.agentToggle.isOn)
        {
            if (state.agentUtterances.Count > 0)
            {
                foreach (AgentInputObject agent in state.agentUtterances)
                {
                    if (agent.agentUtterance.text == "")
                    {
                        hasErrors = true;
                        spawnErrorViewObject("Agent utterance is empty at: " + state.stateName.text);
                    }
                    string pattern = "\\[\\w+\\]";
                    Regex reg = new Regex(pattern);
                    MatchCollection matches = reg.Matches(agent.agentUtterance.text);
                    if(matches.Count > 0)
                    {
                        foreach(Match match  in matches)
                        {
                            string input = match.Value.Replace("[", "").Replace("]", "");
                            string returned = Properties.GetProperty(input);
                            if (returned == null)
                            {
                                hasErrors = true;
                                spawnErrorViewObject("Property Name: '" + input + "' does not exist.");
                            }
                        }
                    }
                }
            }
            else
            {
                hasErrors = true;
                spawnErrorViewObject("Add Agent Utterances using plus sign to " + state.stateName.text);
            }
        }
        if(state.menuToggle.isOn)
        {
            //if (state.usermenu.Count > 0)
            //{
            //    foreach (MenuInputPanelObject menu in state.usermenu)
            //    {
            //        if (menu.nextState.text == "")
            //        {
            //            hasErrors = true;
            //            spawnErrorViewObject("Next state text empty at: " + state.stateName.text);
            //        }
            //        if (menu.userResponse.text == "")
            //        {
            //            hasErrors = true;
            //            spawnErrorViewObject("User Response text empty at:" + state.stateName.text);
            //        }
            //    }
            //}
            //else
            //{
            //    hasErrors = true;
            //    spawnErrorViewObject("No user response added. Please click on plus button to add more responses at " + state.stateName.text);
            //}
        }
        if (!state.mediaToggle.isOn && !state.actionToggle.isOn && !state.agentToggle.isOn && !state.menuToggle.isOn)
        {
            hasErrors = true;
            spawnErrorViewObject("Incomplete state, add other components. State:" + state.stateName.text);
        }
    }

    private State GetStateForScript(StatePanelObject state)
    {
        CheckUIErrors(state);
        State addState = new State();
        addState.ActionSets = new List<List<Action>>();
        //State Name
        addState.StateName = state.stateName.text;

        // Agent Utterances
        if (state.agentUtterances.Count > 0)
        {
            foreach (AgentInputObject agent in state.agentUtterances)
            {
                string currentString = Regex.Replace(agent.agentUtterance.text.Replace("\n", " "), "\\$", "")
                    .Replace("<BEAT>", "").Replace("(_DSM_)", "")
                    .Replace("=|", "[").Replace("|=", "]").Replace("</BEAT>", "").Trim();

                if (addState.ActionSets == null)
                    addState.ActionSets = new List<List<Action>>();
                if (addState.ActionSets.Count == 0)
                {
                    SpeechAction action = new SpeechAction();
                    action.Speech = agent.agentUtterance.text;
                    List<Action> set = new List<Action>();
                    set.Add(action);
                    addState.ActionSets.Add(set);
                }
                else if (addState.ActionSets.Count == 1)
                {
                    SpeechAction speechAction = new SpeechAction();
                    speechAction.Speech = agent.agentUtterance.text;
                    bool hasSpeech = false;
                    foreach (Action action in addState.ActionSets[0])
                    {
                        if (action is SpeechAction)
                        {
                            hasSpeech = true;
                        }
                    }
                    if (!hasSpeech)
                    {
                        addState.ActionSets[0].Add(speechAction);
                    }
                    else
                    {
                        List<Action> set = new List<Action>();
                        set.Add(speechAction);
                        addState.ActionSets.Add(set);
                    }
                }
                else
                {
                    List<Action> copy = (List<Action>)addState.ActionSets[0];
                    List<Action> set = copy;
                    foreach (Action action in set)
                    {
                        if (action is SpeechAction)
                        {
                            ((SpeechAction)action).Speech = agent.agentUtterance.text;
                        }
                    }
                }
            } 
            

            // Media
            Action mediaAction = null;
            string type = state.media.captionText.text;

            // TODO: Add other action
            if ("whiteboard".Equals(type))
            {
                mediaAction = new WhiteboardAction();
                ((WhiteboardAction)mediaAction).Url = state.url.text;
                if (addState.ActionSets == null)
                {
                    addState.ActionSets = new List<List<Action>>();
                }
                if (addState.ActionSets.Count == 0)
                {
                    List<Action> set = new List<Action>();
                    if (mediaAction != null)
                    {
                        set.Add(mediaAction);
                    }
                    addState.ActionSets.Add(set);
                }
                else
                {
                    foreach (List<Action> actions in addState.ActionSets)
                    {
                        if (mediaAction != null)
                            actions.Add(mediaAction);
                    }
                }
            }
        }
       





        // Agent Action
        addState.Execute = Regex.Replace(state.action.text, "\\$", "").Trim();
        if(addState.Execute == "")
            addState.Execute = null;


        // User Menu
        if (state.usermenu.Count > 0)
        {
            RagMenu menu = new RagMenu();
            foreach (MenuInputPanelObject choice in state.usermenu)
            {
                MenuChoice menuChoice = getMenuChoice(choice);
                menu.Menu.Add(menuChoice);
            }
            addState.Ui = menu;
        }
        else if(state.checkBox != null)
        {
            Checkbox checkbox = new Checkbox();
            checkbox.Prompt = state.checkBox.prompt.text;
            foreach(ChoicesInput choice in state.checkBox.choices)
            {
                string choiceInput = choice.choiceInput.text;
                checkbox.Choices.Add(choiceInput);
            }
            foreach(MenuInputPanelObject menuInput in state.checkBox.usermenu)
            {
                MenuChoice menu = getMenuChoice(menuInput);
                checkbox.Menu.Add(menu);
            }
            addState.Ui = checkbox;
        }
        else if(state.inputPanel != null)
        {
            TextPrompt textPrompt = new TextPrompt();
            textPrompt.Prompt = state.inputPanel.prompt.text;
            foreach (MenuInputPanelObject menuInput in state.inputPanel.usermenu)
            {
                MenuChoice menu = getMenuChoice(menuInput);
                textPrompt.Menu.Add(menu);
            }
            addState.Ui = textPrompt;
        }
        return addState;
    }

    static List<string> ScriptNames = new List<string>();
    static List<string> Transitions = new List<string>();
    static List<string> scriptNames = new List<string>();
    static bool hasErrors = false;

    bool checkErrorsInProject()
    {
        checkErrors();
        foreach(string targetStates in Transitions)
        {
            if(!scriptNames.Contains(targetStates))
            {
                spawnErrorViewObject("Bad transition: Create" + targetStates);
                hasErrors = true;
            }
        }
        return hasErrors;
    }

    private void checkErrors()
    {
        var files = Directory.GetFiles(MyGlobals.PROJECTPATH).Where(name => name.EndsWith(".json")); ;
        foreach (var file in files)
        {
            FileInfo info = new FileInfo(file);
            string scriptName = info.FullName.Replace(MyGlobals.PROJECTPATH, ".");
            if(scriptName.Contains("\\"))
            {
                scriptName = scriptName.Replace(".\\", ".");
                scriptName = scriptName.Replace("\\", ".");
            }
            else if (scriptName.Contains("/"))
            {
                scriptName = scriptName.Replace("./", ".");
                scriptName = scriptName.Replace("/", ".");
            }
            scriptName = scriptName.Replace(".json", "");
            scriptNames.Add(scriptName);
            GetStateTransitions(info.FullName);
        }
    }

    private void GetStateTransitions(string filePath)
    {
        FileInfo info = new FileInfo(filePath);
        Script script = ScriptConfig.load(filePath);
        List<string> states = new List<string>();
        List<string> transitions = new List<string>();
        foreach(State state in script.States)
        {
            states.Add(state.StateName);
            if (state.Ui != null)
            {
                List<MenuChoice> choices = null;
                if (state.Ui is RagMenu){
		    			choices = ((RagMenu) state.Ui).Menu;
	    		} else if(state.Ui is Checkbox)
                {
                    choices = ((Checkbox)state.Ui).Menu;
                } else if(state.Ui is TextPrompt)
                {
                    choices = ((TextPrompt)state.Ui).Menu;
                }
                if(choices != null)
                {
                    foreach (MenuChoice choice in choices)
                    {
                        if(choice.Text == null)
                        {
                            hasErrors = true;
                            spawnErrorViewObject("Missing Text:" + state.StateName + " Script: " + info.Name.Replace(".json", ".script"));
                        }
                        if (choice.NextState != null)
                        {
                            if (choice.NextState.Contains("GO"))
                            {
                                hasErrors = true;
                                spawnErrorViewObject("Bad syntax in state: " + state.StateName + " Script:" + info.Name.Replace(".json", ".script"));
                            }
                            if (choice.NextState.Contains("."))
                                Transitions.Add(choice.NextState);
                            else
                                transitions.Add(choice.NextState);
                            if (choice.Execute != null)
                            {
                                if (choice.Execute.Contains("GO"))
                                {
                                    string target = choice.Execute;
                                    Regex regex = new Regex("GO\\(\".* \"\\)");
                                    Match match = regex.Match(target);
                                    while (match.Success)
                                    {
                                        foreach(Group g in match.Groups)
                                        {
                                            String output = g.ToString();
                                            output = output.Replace("GO(\"", "");
                                            output = output.Replace("\")", "");
                                            if (output.Contains("."))
                                            {
                                                if (!output.StartsWith("."))
                                                    output = "." + output;
                                                hasErrors = true;
                                                spawnErrorViewObject("Bad syntax in state: " + state.StateName + " Script:" + info.Name.Replace(".json", ".script"));
                                                Transitions.Add(output);
                                            }
                                            else
                                            {
                                                transitions.Add(output);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } 
        }
        //check for mismatch
        foreach(string targetStates in transitions)
        {
            if (!states.Contains(targetStates))
            {
                hasErrors = true;
                spawnErrorViewObject("Bad transition: Create" + targetStates + " Script:" + info.Name.Replace(".json", ".script"));
            }
        }
    }

    private void spawnErrorViewObject(string error)
    {
        GameObject errorElement = errorViewObjectPool.GetObject();
        errorElement.transform.SetParent(errorViewContentPanel);
        errorElement.transform.Reset();
        errorElement.transform.GetComponent<ErrorViewPanel>().errorText.text = error;
    }

    private MenuChoice getMenuChoice(MenuInputPanelObject menuInput)
    {
        MenuChoice menuChoice = new MenuChoice();
        string menuText = menuInput.userResponse.text;
        if (menuText.Contains(";"))
        {
            menuText = menuText.Replace(";", "|");
        }
        menuChoice.Text = menuText;
        string resp = menuInput.nextState.text;
        if (resp.Contains("$"))
        {
            menuChoice.Execute = Regex.Replace(resp, "\\$", "").Trim();
        }
        else
        {
            menuChoice.NextState = resp.Trim();
        }
        return menuChoice;
    }
}
