//© EagleDragonGames 2018
using UnityEngine;

[CreateAssetMenu(menuName = "SharedValue/string")]
public class SharedString : SharedValue<string>
{
    [System.Serializable]
    public class ValueChangedEvent
    {
        [SerializeField]
        private SharedEvent m_event;
        [SerializeField]
        private SharedStringEvent m_stringEvent;
        [SerializeField]
        private string m_stringToCompare;
        [SerializeField]
        private bool m_compareString;
        [SerializeField]
        private bool m_useStringEvent;

        public void OnValueChanged(string prev, string current)
        {
            if (m_compareString && current != m_stringToCompare)
                return;

            if (!m_useStringEvent && m_event != null)
                m_event.Invoke();
            if (m_useStringEvent && m_stringEvent != null)
                m_stringEvent.Invoke(current);

        }
    }

    [SerializeField]
    private ValueChangedEvent[] m_onChangedEvents = new ValueChangedEvent[0];

    protected override void OnValueChanged(string prev, string current)
    {
        if (current == prev)
            return;

        for (int i = 0; i < m_onChangedEvents.Length; ++i)
        {
            m_onChangedEvents[i].OnValueChanged(prev, current);
        }
    }
}
