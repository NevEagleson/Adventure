//© EagleDragonGames 2018
using UnityEngine;

[CreateAssetMenu(menuName = "SharedValue/bool")]
public class SharedBool : SharedValue<bool>
{
    [System.Serializable]
    public class ValueChangedEvent
    {
        [SerializeField]
        private SharedEvent m_event;
        [SerializeField]
        private SharedBoolEvent m_boolEvent;
        [SerializeField]
        private BoolComparison m_comparison;
        [SerializeField]
        private bool m_useBoolEvent;

        public void OnValueChanged(bool prev, bool current)
        {
            bool trigger = false;

            switch (m_comparison)
            {
                case BoolComparison.Any:
                    trigger = true;
                    break;
                case BoolComparison.IsTrue:
                    trigger = current;
                    break;
                case BoolComparison.IsFalse:
                    trigger = !current;
                    break;
            }

            if (trigger)
            {
                if (!m_useBoolEvent && m_event != null)
                    m_event.Invoke();
                if (m_useBoolEvent && m_boolEvent != null)
                    m_boolEvent.Invoke(current);
            }

        }
    }

    [SerializeField]
    private ValueChangedEvent[] m_onChangedEvents = new ValueChangedEvent[0];

    protected override void OnValueChanged(bool prev, bool current)
    {
        if (current == prev)
            return;

        for (int i = 0; i < m_onChangedEvents.Length; ++i)
        {
            m_onChangedEvents[i].OnValueChanged(prev, current);
        }
    }

    public void Toggle()
    {
        Value = !Value;
    }
}