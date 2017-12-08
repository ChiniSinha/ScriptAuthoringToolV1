#region

using System;
using UnityEngine;
//using RogoDigital.Lipsync;

#endregion

[RequireComponent(typeof(AgentAnimationController))]
public class Agent : MonoBehaviour
{
    private SingleAgentMediator _mediator;
    public AgentAnimationController AgentAnimationController;
    public AudioSource AudioSource;
    //public LipSync lipSync;

//    public Paper Paper;

    public Transform LeftHandAttach;
    public Transform RightHandAttach;

    public GameObject LeftHandHeldObject { get; protected set; }
    public GameObject RightHandHeldObject { get; protected set; }    

    public TTSController Tts;
   // public LipSyncPlayer LipSyncPlayer;
    public float HandSize;

    public delegate void GazeChangeDelegate(string target);
    public static event GazeChangeDelegate GazeChangeEvent;
/*    public void HoldObject(GrabbableObject obj, Side hand)
    {
        if (hand == Side.LEFT)
        {
            obj.transform.parent = LeftHandAttach;
            LeftHandHeldObject = obj.gameObject;
        }
        else
        {
            obj.transform.parent = RightHandAttach;
            RightHandHeldObject = obj.gameObject;
        }
        obj.transform.localRotation = Quaternion.Inverse(obj.transform.rotation) * obj.GrabPoint.rotation;
        Vector3 pos = -obj.GrabPoint.localPosition;
        pos.Scale(obj.transform.localScale);
        obj.transform.localPosition = pos;
    }

    public bool IsHoldingObject(Side hand = Side.CENTER)
    {
        if (hand == Side.LEFT)
        {
            return LeftHandAttach.childCount > 0;
        }
        if (hand == Side.RIGHT)
        {
            return RightHandAttach.childCount > 0;
        }
        return LeftHandAttach.childCount > 0 || RightHandAttach.childCount > 0;
    }

    public void ReleaseObject(GrabbableObject obj)
    {
        if (obj.transform.parent == LeftHandAttach)
        {
            LeftHandHeldObject = null;
            obj.transform.parent = null;
        }
        else if (obj.transform.parent == RightHandAttach)
        {
            RightHandHeldObject = null;
            obj.transform.parent = null;
        }
    }
*/
    public void OnGazeChange(string target)
    {
        if (GazeChangeEvent != null)
        {
            GazeChangeEvent(target);
        }
    }

    public void SetupAgent()
    {
//        if (Globals.Config != null)
//        {
            //SetupLipSync();
            SetupTts();
//        }
//        else
//        {
//            Globals.EventBus.Register<ConfigurationLoadedEvent>(OnConfigLoaded);
//        }
        _mediator = gameObject.AddComponent<SingleAgentMediator>();
        _mediator._primaryAgent = this;
//        _mediator = new SingleAgentMediator(this);
        _mediator.Setup();

        Globals.Register(this);
        Globals.EventBus.Dispatch(new AgentReadyEvent(this));
    }

    private void Start()
    {
        Globals.EventBus.Dispatch(new AgentChangeGazeEvent("TOWARDS"));
        Globals.CommandQueue.Enqueue(new GazeCommand("TOWARDS"));
    }

    private void SetupTts()
    {
        //TODO: Add drop down to select different tts modes
        Config.TtsMode ttsMode = Config.TtsMode.LOCAL_CEREVOICE;

        switch (ttsMode)
        {
            case Config.TtsMode.NATIVE:
                if (Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    Tts = gameObject.AddComponent<WindowsTTS>();
                }
                else if (Application.platform == RuntimePlatform.OSXEditor ||
                         Application.platform == RuntimePlatform.OSXPlayer)
                {
                    Tts = gameObject.AddComponent<MacTTS>();
                }
                else if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    Tts = gameObject.AddComponent<WebTTS>();
                }
                break;

            case Config.TtsMode.WEB_CEREVOICE:
                Tts = gameObject.AddComponent<WebTTS>();
                Globals.Register(gameObject.AddComponent<ExternalTTSServer>());
                break;

            case Config.TtsMode.LOCAL_CEREVOICE:
                if (Application.platform == RuntimePlatform.IPhonePlayer ||
                    Application.platform == RuntimePlatform.OSXEditor ||
                    Application.platform == RuntimePlatform.OSXPlayer ||
                    Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    Tts = gameObject.AddComponent<CereprocTts>();
                }
                break;
        }

        if (!Tts)
        {
            throw new Exception("Unsupported TTS config: " + Globals.Config.Tts.Mode + " on " + Application.platform);
        }

        Tts.Agent = this;
        Tts.InitTts();
    }
/*
    private void SetupLipSync(){
        LipSyncPlayer = gameObject.AddComponent<LipSyncPlayer>();
        LipSyncPlayer.Setup (this);
        Globals.Register(LipSyncPlayer);
    }
*/

    private void OnConfigLoaded(ConfigurationLoadedEvent e)
    {
        //SetupLipSync();
        SetupTts();
    }

    private void OnDestroy()
    {
        _mediator.Cleanup();
    }
}