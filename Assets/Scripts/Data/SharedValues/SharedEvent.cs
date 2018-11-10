//© EagleDragonGames 2018
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="SharedEvent/void")]
public class SharedEvent : ScriptableObject
{
    private List<SharedEventListener> m_listeners = new List<SharedEventListener>();

    public void Invoke()
    {
        for (int i = m_listeners.Count - 1; i >= 0; i--)
        {
            m_listeners[i].TriggerEvent();
        }
    }
    public void AddListener(SharedEventListener listener)
    {
        m_listeners.Add(listener);
    }
    public void RemoveListener(SharedEventListener listener)
    {
        m_listeners.Remove(listener);
    }

#if UNITY_EDITOR
    public List<SharedEventListener> ListenerList {  get { return m_listeners; } }
#endif
}

public abstract class SharedEvent<T> : ScriptableObject
{
    private List<ISharedEventListener<T>> m_listeners = new List<ISharedEventListener<T>>();

    public void Invoke(T arg)
    {
        for (int i = m_listeners.Count - 1; i >= 0; i--)
        {
            m_listeners[i].TriggerEvent(arg);
        }
    }
    public void AddListener(ISharedEventListener<T> listener)
    {
        m_listeners.Add(listener);
    }
    public void RemoveListener(ISharedEventListener<T> listener)
    {
        m_listeners.Remove(listener);
    }

#if UNITY_EDITOR
    public List<ISharedEventListener<T>> ListenerList { get { return m_listeners; } }
#endif
}
