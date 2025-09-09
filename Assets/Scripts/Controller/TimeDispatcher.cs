using System.Collections.Generic;
using UnityEngine;

public class TimeDispatcher : MonoBehaviour
{
    private static readonly List<ITickListener> listeners = new List<ITickListener>();

    public static void RegisterListener(ITickListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public static void UnregisterListener(ITickListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    void Update()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].Tick(Time.deltaTime);
        }
    }
}