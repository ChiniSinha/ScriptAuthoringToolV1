#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif
using UnityEngine;

public class ExitApplicationCommand : BaseCommand
{
    protected string _redirectUrl;

    public ExitApplicationCommand(string redirectUrl = "")
    {
        _redirectUrl = redirectUrl;
    }


    public override void Execute()
    {
        if (!string.IsNullOrEmpty(_redirectUrl))
        {
            Application.ExternalCall("exit", _redirectUrl, 0);
        }

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.ExternalCall("close");
        }
        else
        {
            Application.Quit();
        }
    }
}