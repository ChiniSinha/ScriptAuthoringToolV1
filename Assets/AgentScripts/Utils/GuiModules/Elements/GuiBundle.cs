#region

using UnityEngine;

#endregion

public class GuiBundle : MonoBehaviour
{
    private void Awake()
    {
		Debug.Log("GUI");
        Globals.EventBus.Register<GuiLoadedEvent>(OnGuiLoaded);
        Globals.EventBus.Dispatch(new GuiLoadedEvent(this));
    }

    private void OnGuiLoaded(GuiLoadedEvent e)
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("AgentCamera").GetComponent<Camera>();
        Globals.Ui = gameObject;
        gameObject.SetActive(false);
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