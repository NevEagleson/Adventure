//© EagleDragonGames 2018
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public abstract class SharedValueEditor<T, V> : Editor where V : SharedValue<T>
{
    protected ReorderableList m_eventList;

    public V TargetValue { get { return target as V; } }
    public V GetTargetValue(int index) { return targets[index] as V; }

    public void OnEnable()
    {
        m_eventList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_onChangedEvents"));
        m_eventList.drawElementCallback = DrawElementCallback;
        m_eventList.elementHeightCallback = ElementHeightCallback;
        m_eventList.drawHeaderCallback = DrawHeaderCallback;
    }

    public override void OnInspectorGUI()
    {    
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_initialValue"));
        EditorGUI.BeginChangeCheck();
        EditorGUI.showMixedValue = serializedObject.isEditingMultipleObjects;
        T newValue = DrawValue(TargetValue.Value);
        if (EditorGUI.EndChangeCheck())
        {
            if (serializedObject.isEditingMultipleObjects)
            {
                for (int i = 0; i < targets.Length; ++i)
                {
                    GetTargetValue(i).Value = newValue;
                    serializedObject.Update();
                }
            }
            else
            {
                TargetValue.Value = newValue;
                serializedObject.Update();
            }           
        }
        DrawAdditionalControls();
        if (GUILayout.Button("Reset"))
        {
            if (serializedObject.isEditingMultipleObjects)
            {
                for (int i = 0; i < targets.Length; ++i)
                {
                    GetTargetValue(i).Reset();
                    serializedObject.Update();
                }
            }
            else
            {
                TargetValue.Reset();
                serializedObject.Update();
            }
        }     
        if (m_eventList.serializedProperty != null)
        {
            EditorGUILayout.Space();
            m_eventList.DoLayoutList();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawElementCallback(Rect pos, int index, bool active, bool focused)
    {
        DrawOnChangedEventElement(pos, m_eventList.serializedProperty.GetArrayElementAtIndex(index));
    }
    private float ElementHeightCallback(int index)
    {
        return GetOnChangedEventElementHeight(m_eventList.serializedProperty.GetArrayElementAtIndex(index));
    }
    private void DrawHeaderCallback(Rect pos)
    {
        EditorGUI.LabelField(pos, "On Value Changed Events");
    }

    protected virtual void DrawAdditionalControls() { }
    protected abstract T DrawValue(T value);
    protected abstract void DrawOnChangedEventElement(Rect pos, SerializedProperty element);
    protected abstract float GetOnChangedEventElementHeight(SerializedProperty element);

}