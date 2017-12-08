using System.Collections.Generic;
using UnityEngine;

public class SimpleWhiteboardDisplayMediator : IWhiteboardDisplayMediator
{
    private readonly WhiteboardView _whiteboard;

    public SimpleWhiteboardDisplayMediator(WhiteboardView whiteboard)
    {
        _whiteboard = whiteboard;
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<DisplayWhiteboardEvent>(OnShowWhiteboard);
        Globals.EventBus.Register<HideWhiteboardEvent>(OnHideWhiteboard);
        UnityEngine.Debug.Log("In register");
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<DisplayWhiteboardEvent>(OnShowWhiteboard);
        Globals.EventBus.Unregister<HideWhiteboardEvent>(OnHideWhiteboard);
    }

    private void OnHideWhiteboard(HideWhiteboardEvent e)
    {
        _whiteboard.LoadImage("");
        Hide();
    }

    private void OnShowWhiteboard(DisplayWhiteboardEvent e)
    {
        UnityEngine.Debug.Log("In onshow");
        if (e.Url != "")
        {
            LoadImage(e.Url);
        } else
        {
            BuildTable(e.Contents,e.BoldTopRow);
        }
        _whiteboard.PrimaryAnimator.Show();
    }

    private void LoadImage(string Url)
    {
        _whiteboard.LoadImage(Url);
    }

    private void BuildTable(List<List<string>> contents, bool boldTopRow)
    {
        string logString = "";
        for (int i = 0; i < contents.Count; i++)
        {
            if (logString != "")
                logString += ",";
            logString += "[" + string.Join(",", contents[i].ToArray()) + "]";
        }
        Debug.Log("Displaying text prompt: [" + logString + "]");
        _whiteboard.BoldTopRow = boldTopRow;
        _whiteboard.SetupTable(contents);
        //_table.PrimaryAnimator.Show();
    }

    private void Hide()
    {
        _whiteboard.Hide();
        if (_whiteboard.PrimaryAnimator.Showing)
        {
            _whiteboard.PrimaryAnimator.Hide();
        }
    }

}