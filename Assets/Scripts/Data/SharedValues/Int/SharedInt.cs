//© EagleDragonGames 2018
using UnityEngine;

[CreateAssetMenu(menuName = "SharedValue/int")]
public class SharedInt : SharedValue<int>
{
    [System.Serializable]
    public class ValueChangedEvent
    {
        [SerializeField]
        private SharedEvent m_event;
        [SerializeField]
        private SharedIntEvent m_intEvent;
        [SerializeField]
        private ValueComparison m_comparison;
        [SerializeField]
        private int m_compareValue;
        [SerializeField]
        private bool m_useIntEvent;

        public void OnValueChanged(int prev, int current)
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
                if (!m_useIntEvent && m_event != null)
                    m_event.Invoke();
                if (m_useIntEvent && m_intEvent != null)
                    m_intEvent.Invoke(current);
            }

        }
    }

    [SerializeField]
    private int m_maxValue;
    [SerializeField]
    private bool m_clampMax;
    [SerializeField]
    private int m_minValue;
    [SerializeField]
    private bool m_clampMin;

    [SerializeField]
    private ValueChangedEvent[] m_onChangedEvents = new ValueChangedEvent[0];

    protected override void OnValueChanged(int prev, int current)
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
