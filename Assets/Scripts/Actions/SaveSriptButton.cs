using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class SaveSriptButton : MonoBehaviour
{
    public Transform stateContentPanel;
    public GameObject savePanel;

    public void HandleSave()
    {
        try
        {
            Debug.Log("Script name:" + MyGlobals.CURRENTSCRIPTNAME);
            Debug.Log("Script path: " + MyGlobals.CURRENTSCRIPTPATH);
            ScriptConfig config = new ScriptConfig();
            config.scriptName = MyGlobals.CURRENTSCRIPTNAME;
            config.scriptJsonPath = MyGlobals.CURRENTSCRIPTPATH;
            StatePanelObject[] states = stateContentPanel.GetComponentsInChildren<StatePanelObject>();
            Script script = new Script();
            script.States = new List<State>();
            foreach (StatePanelObject state in states)
            {
                Debug.Log("State name: " + state.stateName.text);
                State addState = GetStateForScript(state);
                script.States.Add(addState);
            }
            ScriptConfig.save(script, config.scriptJsonPath);
            savePanel.SetActive(true);
        } 
        catch(Exception ex)
        {
            Debug.Log(ex.StackTrace);
        }
    }

    private State GetStateForScript(StatePanelObject state)
    {
        State addState = new State();
        addState.ActionSets = new List<List<Action>>();
        //State Name
        addState.StateName = state.stateName.text;

        // Agent Utterances
        if (state.agentUtterances != null)
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
        }
        

        // Media
        Action mediaAction = null;
        string type = state.media.captionText.text;

        // TODO: Add other action
        if ("whiteboard".Equals(type))
        {
            mediaAction = new WhiteboardAction();
            ((WhiteboardAction)mediaAction).Url = state.url.text;
        }
                
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
            foreach(List<Action> actions in addState.ActionSets)
            {
                actions.Add(mediaAction);
            }
        }

        // Agent Action
        addState.Execute = Regex.Replace(state.action.text, "\\$", "").Trim();


        // User Menu
        if (state.usermenu != null)
        {
            RagMenu menu = new RagMenu();
            foreach (MenuInputPanelObject choice in state.usermenu)
            {
                MenuChoice menuChoice = new MenuChoice();
                string menuText = choice.userResponse.text;
                if (menuText.Contains(";"))
                {
                    menuText = menuText.Replace(";", "|");
                }
                menuChoice.Text = menuText;
                string resp = choice.nextState.text;
                if (resp.Contains("$"))
                {
                    menuChoice.Execute = Regex.Replace(resp, "\\$", "").Trim();
                }
                else
                {
                    menuChoice.NextState = resp.Trim();
                }
                menu.Menu.Add(menuChoice);
            }
            addState.Ui = menu;
        }
        return addState;
    }
}
