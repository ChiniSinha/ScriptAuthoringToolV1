#region

using UnityEngine;

#endregion

public class ErrorTextMediator : IErrorTextMediator
{
    private readonly ErrorText _text;

    public ErrorTextMediator(ErrorText text)
    {
        _text = text;
    }

    public string ElementName
    {
        get { return "DebugText"; }
    }

    public void OnRegister()
    {
        Globals.EventBus.Register<SetErrorTextEvent>(OnErrorTextUpdate);
        Hide();
    }

    public void OnRemove()
    {
        Globals.EventBus.Unregister<SetErrorTextEvent>(OnErrorTextUpdate);
        Object.Destroy(_text);
    }

    private void OnErrorTextUpdate(SetErrorTextEvent e)
    {
        if (string.IsNullOrEmpty(e.ErrorText))
        {
            Hide();
        }
        else
        {
            SetText(e.ErrorText);
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