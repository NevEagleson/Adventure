//© EagleDragonGames 2018
using UnityEngine;

[System.Serializable]
public abstract class SharedValueReference<T, V> : SharedValueReferenceBase where V : SharedValue<T>
{
    [SerializeField]
    private V m_sharedValue;
    [SerializeField]
    private T m_constantValue;

    public T Value
    {
        get { return m_useConstant || m_sharedValue == null ? m_constantValue : m_sharedValue.Value; }
        set
        {
            if (m_useConstant || m_sharedValue == null)
                m_constantValue = value;
            else
                m_sharedValue.Value = value;
        }
    }
}

[System.Serializable]
public class SharedValueReferenceBase
{
    [SerializeField]
    protected bool m_useConstant = true;
}
