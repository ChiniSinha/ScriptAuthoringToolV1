#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class PointOfInterest : MonoBehaviour
{
    public static Dictionary<string, PointOfInterest> PoIs = new Dictionary<string, PointOfInterest>();

    public string Label;

    protected virtual void Awake()
    {
        PoIs[Label] = this;
        Globals.EventBus.Dispatch(new PoIAvailableEvent(Label));
    }

    protected virtual void OnDestroy()
    {
        Globals.EventBus.Dispatch(new PoIRemovedEvent(Label));
        PoIs.Remove(Label);
    }

    protected void OnEnable()
    {
        PoIs[Label] = this;
        Globals.EventBus.Dispatch(new PoIAvailableEvent(Label));
    }

    protected void OnDisable()
    {
        Globals.EventBus.Dispatch(new PoIRemovedEvent(Label));
        PoIs.Remove(Label);
    }
}