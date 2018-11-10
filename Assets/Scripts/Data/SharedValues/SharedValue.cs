//© EagleDragonGames 2018
using UnityEngine;

public abstract class SharedValue<T> : ScriptableObject
{
    [SerializeField]
    protected T m_initialValue;

    protected T m_currentValue;

    public T Value
    {
        get { return m_currentValue; }
        set
        {
            T prev = m_currentValue;
            m_currentValue = value;
            OnValueChanged(prev, m_currentValue);
        }
    }

    public virtual void Reset(bool invokeEvent = false)
    {
        if (invokeEvent)
            Value = m_initialValue;
        else
            m_currentValue = m_initialValue;       
    }

    protected void OnEnable()
    {
        m_currentValue = m_initialValue;
    }

    public static implicit operator T(SharedValue<T> value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return m_currentValue.ToString();
    }

    protected virtual void OnValueChanged(T prev, T current) { }
}