using UnityEngine;
using System.Collections; 

public class ConfigLoader : MonoBehaviour
{
    public static void LoadConfig()
    {
        Globals.EventBus.Dispatch(new ConfigurationLoadedEvent(Config.Load()));
    }
}