#region

using System;
using UnityEngine;

#endregion

public abstract class PlatformController : MonoBehaviour, IEventBroker
{
    /*#if UNITY_IOS
        [DllImport ("__Internal")]
        internal static extern void showNativeAlert(string title, string message);
        #endif
        */

    public virtual void Activate()
    {
    }


    public void Deactivate()
    {
        throw new NotImplementedException();
    }

    private void OnApplicationPause(bool paused)
    {
        /*#if !UNITY_WEBPLAYER && !UNITY_WEBGL
                if (State != LoadState.READY)
                {
                    return;
                }

                if (paused)
                {
                    RagLog.Log("Application Paused");
                    //dbController.CloseSession ("Paused");
                    Globals.Get<IDatabaseController>().SaveProperties();
                }
                else
                {
                    RagLog.Log("Application Resumed");
                    Globals.Get<IDatabaseController>().UpdateStudyDay();
                    //dbController.StartSession ();

                    // if the user only resumes after a certain time
                    // then start from the top script again
                    // default time is 120 minutes

        #if UNITY_IOS
                    if(Globals.Get<IDatabaseController>().GetSessionDuration() >= 120)
                    {
                        Globals.Get<IDatabaseController>().CloseSession ("LONG_PAUSE");
                        Globals.Get<IDatabaseController>().StartSession ();
                        Globals.Get<IDatabaseController>().UpdateStudyDay();
                        //((StandaloneController)networkController).RestartDialogue();
                    }
        #endif
                }
        #endif*/
    }

    private void OnApplicationQuit()
    {
        Globals.Get<ICommandProtocol>().Shutdown();
        //TODO Close session?
    }
}