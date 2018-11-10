//© EagleDragonGames 2018
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component used to handle a shared event with no parameter
/// </summary>
[System.Serializable]
public class SharedEventListener : MonoBehaviour
{
    [SerializeField]
    private SharedEvent m_event;

    [SerializeField]
    private UnityEvent m_onEventTriggered;

    public void TriggerEvent()
    {
        m_onEventTriggered.Invoke();
    }

    protected void OnEnable()
    {
        m_event.AddListener(this);
    }
    protected void OnDisable()
    {
        m_event.RemoveListener(this);
    }
}

public interface ISharedEventListener<A>
{
    void TriggerEvent(A arg);
}

[System.Serializable]
public class SharedEventListener<A, E, U> : MonoBehaviour, ISharedEventListener<A> where E : SharedEvent<A> where U : UnityEvent<A>
{
    [SerializeField]
    private E m_event;

    [SerializeField]
    private U m_onEventTriggered;

    public void TriggerEvent(A arg)
    {
        m_onEventTriggered.Invoke(arg);
    }

    protected void OnEnable()
    {
        m_event.AddListener(this);
    }
    protected void OnDisable()
    {
        m_event.RemoveListener(this);
    }
}
