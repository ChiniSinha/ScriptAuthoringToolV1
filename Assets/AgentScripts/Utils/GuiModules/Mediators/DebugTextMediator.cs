#region

using UnityEngine;

#endregion

public class DebugTextMediator : IDebugTextMediator
{
    private readonly DebugText _text;

    public DebugTextMediator(DebugText text)
    {
        _text = text;
    }

    public string ElementName
    {
        get { return "DebugText"; }
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<SetDebugTextEvent>(OnDebugTextUpdate);
        Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<SetDebugTextEvent>(OnDebugTextUpdate);
        Object.Destroy(_text);
    }

    private void OnDebugTextUpdate(SetDebugTextEvent e)
    {
        if (string.IsNullOrEmpty(e.DebugText))
        {
            Hide();
        }
        else
        {
            SetText(e.DebugText);
            Show();
        }
    }

    public void Show()
    {
        _text.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _text.gameObject.SetActive(false);
    }

    public void OnGuiClear()
    {
        Show();
    }

    public void SetText(string text)
    {
        _text.Text.text = text;
    }
}