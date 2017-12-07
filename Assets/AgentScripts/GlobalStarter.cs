#region
using UnityEngine;

#endregion

public class GlobalStarter : MonoBehaviour
{
    public CommandQueue CommandQueue;
    public EventBus EventBus;

	public Agent agent;

    private void Awake()
    {
        Globals.SystemObject = gameObject;
        Globals.SetEventBus(EventBus);
        Globals.SetCommandQueue(CommandQueue);
		agent.SetupAgent();
    }
}