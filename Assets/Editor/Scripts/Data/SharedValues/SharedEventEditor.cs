//© EagleDragonGames 2018
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SharedEvent))]
public class SharedEventEditor : Editor
{
    public SharedEvent TargetEvent { get { return target as SharedEvent; } }
    public SharedEvent GetTargetEvent(int index) { return targets[index] as SharedEvent; }

    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (GUILayout.Button("Invoke"))
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    for (int i = 0; i < targets.Length; ++i)
                    {
                        GetTargetEvent(i).Invoke();
                    }
                }
                else
                {
                    TargetEvent.Invoke();
                }
            }

            if (serializedObject.isEditingMultipleObjects)
                return;

            List<SharedEventListener> listeners = TargetEvent.ListenerList;
            EditorGUILayout.LabelField("Number of Listeners", listeners.Count.ToString());
            EditorGUI.indentLevel++;
            for (int i = 0; i < listeners.Count; ++i)
            {
                if (GUILayout.Button(listeners[i].gameObject.name, EditorStyles.label))
                {
                    if (Event.current.clickCount > 1)
                    {
                        Selection.SetActiveObjectWithContext(listeners[i].gameObject, listeners[i]);
                    }
                    else
                    {
                        EditorGUIUtility.PingObject(listeners[i].gameObject);
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
        else
        {
            EditorGUILayout.LabelField("Only available in Play mode");
        }
    }
}

public class SharedEventEditor<T> : Editor
{
    public SharedEvent<T> TargetEvent { get { return target as SharedEvent<T>; } }
    public SharedEvent<T> GetTargetEvent(int index) { return targets[index] as SharedEvent<T>; }

    [SerializeField]
    protected T m_invokeValue;

    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            if (GUILayout.Button("Invoke"))
            {
                if (serializedObject.isEditingMultipleObjects)
                {
                    for (int i = 0; i < targets.Length; ++i)
                    {
                        GetTargetEvent(i).Invoke(m_invokeValue);
                    }
                }
                else
                {
                    TargetEvent.Invoke(m_invokeValue);
                }
            }

            if (serializedObject.isEditingMultipleObjects)
                return;

            List<ISharedEventListener<T>> listeners = TargetEvent.ListenerList;
            EditorGUILayout.LabelField("Number of Listeners", listeners.Count.ToString());
            EditorGUI.indentLevel++;
            for (int i = 0; i < listeners.Count; ++i)
            {
                MonoBehaviour behaviour = listeners[i] as MonoBehaviour;
                if (GUILayout.Button(behaviour.gameObject.name, EditorStyles.label))
                {
                    if (Event.current.clickCount > 1)
                    {
                        Selection.SetActiveObjectWithContext(behaviour.gameObject, behaviour);
                    }
                    else
                    {
                        EditorGUIUtility.PingObject(behaviour.gameObject);
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
        else
        {
            EditorGUILayout.LabelField("Only available in Play mode");
        }
    }
}