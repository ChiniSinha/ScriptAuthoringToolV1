using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Event
{
}

public class EventBus : MonoBehaviour
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate void EventCallback<T>(T e) where T : Event;
    
    protected readonly Dictionary<Type, Delegate> CallbackSet = new Dictionary<Type, Delegate>();

    protected readonly List<Event> ThreadSafeEventsThisTick = new List<Event>();

    public void Register<T>(EventCallback<T> cb) where T : Event
    {
        Type t = typeof(T);
        
        if (CallbackSet.ContainsKey(t))
        {
            CallbackSet[t] = Delegate.Combine(CallbackSet[t], cb);
        }
        else
        {
            CallbackSet[t] = cb;
        }
    }

    public void Unregister<T>(EventCallback<T> cb) where T : Event
    {
        Type t = typeof(T);

        if (CallbackSet.ContainsKey(t))
        {
            Delegate currentCb = Delegate.Remove(CallbackSet[t], cb);
            if (currentCb == null)
            {
                CallbackSet.Remove(t);
            }
            else
            {
                CallbackSet[t] = currentCb;
            }
        }
    }

    public void Dispatch<T>(T e) where T : Event
    {
        Type t = typeof(T);
        
        if (CallbackSet.ContainsKey(t))
        {
            CallbackSet[t].DynamicInvoke(e);
        }
    }

    public void Dispatch(Event e, Type t)
    {
        if (e.GetType() != t)
        {
            throw new Exception("Mismatched types");
        }
        if (CallbackSet.ContainsKey(t))
        {
            CallbackSet[t].DynamicInvoke(e);
        }
    }

    public void DispatchThreadSafe<T>(T e) where T : Event
    {
        ThreadSafeEventsThisTick.Add(e);
    }

    private void Update()
    {
        Event e;
        Type t;
        for (int index = 0; index < ThreadSafeEventsThisTick.Count; index++)
        {
            e = ThreadSafeEventsThisTick[index];
            t = e.GetType();
            if (CallbackSet.ContainsKey(t))
            {
                CallbackSet[t].DynamicInvoke(e);
            }
        }

        ThreadSafeEventsThisTick.Clear();
    }
}