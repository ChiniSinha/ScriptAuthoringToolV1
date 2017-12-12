#region
using UnityEngine;
using RAG.Loading;
#endregion

public class GlobalStarter : MonoBehaviour
{
    public CommandQueue CommandQueue;
    public EventBus EventBus;
    public ConfigLoader Loader;
    public PlatformController standaloneController;


    private void Start()
    {
        init();
    }

    public void init()
    {
        Globals.SystemObject = gameObject;
        Globals.SetEventBus(EventBus);
        Globals.SetCommandQueue(CommandQueue);

        Globals.RegisterEventBroker(standaloneController);
        Globals.SystemObject.AddComponent<ModuleLoader>();

        ConfigLoader.LoadConfig();
    }
}