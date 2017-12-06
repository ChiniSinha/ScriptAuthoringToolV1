#region

using System.Collections.Generic;
using UnityEngine;

#endregion

public class CommandQueue : MonoBehaviour
{
    public readonly List<BaseCommand> Queue = new List<BaseCommand>();
    public readonly List<BaseCommand> ThreadSafeQueue = new List<BaseCommand>();

    public bool Debug;

    public BaseCommand Head
    {
        get { return Queue[0]; }
    }

    public void Enqueue(BaseCommand c)
    {
        if (Queue.Count == 0)
        {
            c.Execute();
            if (!c.InProgress)
            {
                return;
            }
        }
        Queue.Add(c);
    }

    public void EnqueueThreadSafe(BaseCommand c)
    {
        ThreadSafeQueue.Add(c);
    }

    protected void Update()
    {
        if (Queue.Count > 0)
        {
            Head.OnUpdate();
            CheckQueue();
        }

        for(int index=0; index<ThreadSafeQueue.Count; index++)
        {
            ThreadSafeQueue[index].Execute();
        }
        ThreadSafeQueue.Clear();
    }

    protected void FixedUpdate()
    {
        if (Queue.Count > 0)
        {
            Head.OnFixedUpdate();
            CheckQueue();
        }
    }

    protected void CheckQueue()
    {
        if (Queue.Count <= 0)
        {
            return;
        }

        while (!Head.InProgress)
        {
            Head.OnComplete();
            Queue.RemoveAt(0);

            if (Queue.Count <= 0)
            {
                break;
            }

            if (!Head.InProgress)
            {
                Head.Execute();
            }
        }
    }

    void OnGUI()
    {
        if (Debug)
        {
            string debugString = "";
            debugString += 1/Time.deltaTime + " fps\n";
            foreach (BaseCommand baseCommand in Queue)
            {
                debugString += baseCommand + "\n";
            }
            GUIStyle s = new GUIStyle();
            s.normal.textColor = Color.red;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), debugString, s);
        }
    }

    /// <summary>
    ///     Interrupts the currently executing action with a new one.
    ///     Really, don't do this too often, k?
    /// </summary>
    /// <param name="command"></param>
    public void Interrupt(BaseCommand command)
    {
        Queue.Insert(0, command);
        command.Execute();
        CheckQueue();
    }
}