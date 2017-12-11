using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals {

    [Flags]
    public enum Flag
    {
        GUI_READY = 1 << 0,
        GUI_WATING_FOR_DUPLICATE_COMMAND = 1 << 1
    }

    public static GameObject Ui;

    public static Config Config { get; private set; }

    public static Flag Flags;

    private static readonly Dictionary<Type, object> Registry = new Dictionary<Type, object>();

    [SerializeField] public static GameObject SystemObject;

    public static EventBus EventBus { get; private set; }

    public static CommandQueue CommandQueue { get; private set; }

    public static void Register<T>(T obj)
    {
        Type t = typeof(T);
        Registry[t] = obj;
    }

    public static T Get<T>()
    {
        object obj;
        if (!Registry.TryGetValue(typeof(T), out obj))
        {
            Debug.Log("Globals: Trying to find a " + typeof(T) + " but it's not registered.");
        }
        return (T)obj;
    }

    public static void Clear<T>()
    {
        Type t = typeof(T);
        Registry.Remove(t);
    }

    public static bool HasRegistered<T>()
    {
        return Registry.ContainsKey(typeof(T));
    }

    public static void StartMediator<T>(T mediator) where T : IMediator
    {
        object existingMediator;
        if (Registry.TryGetValue(typeof(T), out existingMediator))
        {
            ((IMediator)existingMediator).Cleanup();
        }
        Register(mediator);
        mediator.Setup();
    }

    public static void RegisterEventBroker<T>(T broker) where T : IEventBroker
    {
        object existingBroker;
        if (Registry.TryGetValue(typeof(T), out existingBroker))
        {
            ((IEventBroker)existingBroker).Deactivate();
        }
        Register(broker);
        broker.Activate();
    }

    public static void SetConfig(Config config)
    {
        Config = config;
    }

    public static void SetEventBus(EventBus e)
    {
        if (EventBus)
        {
            throw new Exception(
                "Trying to register another global event bus! This will break all event handlers and is unsupported");
        }

        EventBus = e;
    }

    public static void SetCommandQueue(CommandQueue q)
    {
        if (CommandQueue)
        {
            throw new Exception(
                "Trying to register another global event bus! This will break all event handlers and is unsupported");
        }

        CommandQueue = q;
    }
}
