using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefaultEvent : ScriptableObject
{
    private List<DefaultEventListener> listeners = new List<DefaultEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }
    public void RegisterListener(DefaultEventListener listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterListener(DefaultEventListener listener)
    {
        listeners.Remove(listener);
    }
}
