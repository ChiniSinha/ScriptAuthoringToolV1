using System;
using System.Collections.Generic;

[Serializable]
public class MenuChoice
{
    public string Condition;
    public string Execute;
    public string NextState;
    public string ReturnState;
    public bool Revert;
    public string Text;

    public MenuChoice()
    {
    }

    public MenuChoice(string text, string execute)
    {
        Text = text;
        Execute = execute;
    }
}

[Serializable]
public class RagMenu : UI
{
    public List<MenuChoice> Menu;
    public string ElementName;

    public RagMenu()
    {
        Menu = new List<MenuChoice>();
    }
}

[Serializable]
public class Checkbox : UI
{
    public List<string> Choices;
    public List<MenuChoice> Menu;
    public string Prompt;

    public Checkbox()
    {
        Menu = new List<MenuChoice>();
        Choices = new List<string>();
    }
}

[Serializable]
public class RagQuestionnaire : UI
{
    public List<MenuChoice> Menu;
    public string Prompt;
    public List<List<string>> QuestionChoices;
    public List<string> Questions;
    public List<string> InputTypes;
}

[Serializable]
public class TextPrompt : UI
{
    public List<MenuChoice> Menu;
    public string Prompt;

    public TextPrompt()
    {
        Menu = new List<MenuChoice>();
    }
}

[Serializable]
public class TableDisplay : UI
{
    public bool BoldTopRow = false;
    public List<List<string>> Contents;
    public String Url;
    public List<MenuChoice> Menu;
}

[Serializable]
public class Exit : UI
{
}

[Serializable]
public abstract class UI
{
    public bool ForceRepeat = true;
}