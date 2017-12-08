#region

using UnityEngine;

#endregion

public class GuiBundle : MonoBehaviour
{
    private void Awake()
    {
		Debug.Log("GUI");
        Globals.EventBus.Dispatch(new GuiLoadedEvent(this));
        Globals.EventBus.Register<GuiLoadedEvent>(OnGuiLoaded);
    }

    private void OnGuiLoaded(GuiLoadedEvent e)
    {
        if (e.Bundle != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Globals.EventBus.Unregister<GuiLoadedEvent>(OnGuiLoaded);
    }
}