//© EagleDragonGames 2018
using UnityEngine;

[CreateAssetMenu(menuName = "SharedValue/float")]
public class SharedFloat : SharedValue<float>
{
    [System.Serializable]
    public class ValueChangedEvent
    {
        [SerializeField]
        private SharedEvent m_event;
        [SerializeField]
        private SharedFloatEvent m_floatEvent;
        [SerializeField]
        private ValueComparison m_comparison;
        [SerializeField]
        private float m_compareValue;
        [SerializeField]
        private bool m_useFloatEvent;

        public void OnValueChanged(float prev, float current)
        {
            bool trigger = false;

            switch(m_comparison)
            {
                case ValueComparison.Any:
                    trigger = true;
                    break;
                case ValueComparison.Equal:
                    trigger = current == m_compareValue;
                    break;
                case ValueComparison.Greater:
                    trigger = current > m_compareValue && prev <= m_compareValue;
                    break;
                case ValueComparison.GreaterOrEqual:
                    trigger = current >= m_compareValue && prev < m_compareValue;
                    break;
                case ValueComparison.Less:
                    trigger = current < m_compareValue && prev >= m_compareValue;
                    break;
                case ValueComparison.LessOrEqual:
                    trigger = current <= m_compareValue && prev > m_compareValue;
                    break;
            }

            if (trigger)
            {
                if (!m_useFloatEvent && m_event != null)
                    m_event.Invoke();
                if (m_useFloatEvent && m_floatEvent != null)
                    m_floatEvent.Invoke(current);
            }

        }
    }

    [SerializeField]
    private float m_maxValue;
    [SerializeField]
    private bool m_clampMax;
    [SerializeField]
    private float m_minValue;
    [SerializeField]
    private bool m_clampMin;

    [SerializeField]
    private ValueChangedEvent[] m_onChangedEvents = new ValueChangedEvent[0];

    protected override void OnValueChanged(float prev, float current)
    {
        if (m_clampMax && current > m_maxValue)
        {
            current = m_maxValue;
            m_currentValue = current;
        }
        if (m_clampMin && current < m_minValue)
        {
            current = m_minValue;
            m_currentValue = current;
        }
        if (current == prev)
            return;

        for (int i = 0; i < m_onChangedEvents.Length; ++i)
        {
            m_onChangedEvents[i].OnValueChanged(prev, current);
        }
    }
}
