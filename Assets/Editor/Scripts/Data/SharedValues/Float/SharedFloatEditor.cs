//© EagleDragonGames 2018
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SharedFloat))]
public class SharedFloatEditor : SharedValueEditor<float, SharedFloat>
{
    protected override void DrawOnChangedEventElement(Rect pos, SerializedProperty element)
    {
        Rect singleLinePos = pos;
        singleLinePos.height = EditorGUIUtility.singleLineHeight;
        Rect contentPos = EditorGUI.PrefixLabel(singleLinePos, new GUIContent("Event"));
        Rect dropPos = new Rect(contentPos.xMax - 20f, contentPos.y, 20f, contentPos.height);
        contentPos.width -= 20f;
        SerializedProperty useFloatEvent = element.FindPropertyRelative("m_useFloatEvent");
        if (useFloatEvent.boolValue)
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_floatEvent"), GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_event"), GUIContent.none);
        }
        if (GUI.Button(dropPos, GUIContent.none, GUI.skin.GetStyle("ShurikenDropdown")))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Float Event"), useFloatEvent.boolValue, UseFloatEventSelected, useFloatEvent);
            menu.AddItem(new GUIContent("Use Void Event"), !useFloatEvent.boolValue, UseVoidEventSelected, useFloatEvent);
            menu.ShowAsContext();
        }

        singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty comparison = element.FindPropertyRelative("m_comparison");
        EditorGUI.PropertyField(singleLinePos, comparison);

        if(comparison.enumValueIndex > 0)
        {
            singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(singleLinePos, element.FindPropertyRelative("m_compareValue"));
        }
    }

    protected override float DrawValue(float value)
    {
        return EditorGUILayout.FloatField("Current Value", value);
    }

    protected override void DrawAdditionalControls()
    {
        SerializedProperty clampMax = serializedObject.FindProperty("m_clampMax");
        Rect rect = GUILayoutUtility.GetRect(new GUIContent(clampMax.displayName, clampMax.tooltip), EditorStyles.toggle);
        Rect toggleRect = rect;
        toggleRect.width = EditorGUIUtility.labelWidth + 20f;
        EditorGUI.PropertyField(toggleRect, clampMax);
        if (clampMax.boolValue)
        {
            toggleRect = rect;
            toggleRect.width -= EditorGUIUtility.labelWidth + 20f;
            toggleRect.x += EditorGUIUtility.labelWidth + 20f;

            EditorGUI.PropertyField(toggleRect, serializedObject.FindProperty("m_maxValue"), GUIContent.none);
        }
        SerializedProperty clampMin = serializedObject.FindProperty("m_clampMin");
        rect = GUILayoutUtility.GetRect(new GUIContent(clampMin.displayName, clampMin.tooltip), EditorStyles.toggle);
        toggleRect = rect;
        toggleRect.width = EditorGUIUtility.labelWidth + 20f;
        EditorGUI.PropertyField(toggleRect, clampMin);
        if (clampMin.boolValue)
        {
            toggleRect = rect;
            toggleRect.width -= EditorGUIUtility.labelWidth + 20f;
            toggleRect.x += EditorGUIUtility.labelWidth + 20f;

            EditorGUI.PropertyField(toggleRect, serializedObject.FindProperty("m_minValue"), GUIContent.none);
        }
    }

    protected override float GetOnChangedEventElementHeight(SerializedProperty element)
    {
        if (element.FindPropertyRelative("m_comparison").enumValueIndex > 0)
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3f;
        else
            return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2f;
    }

    void UseVoidEventSelected(object data)
    {
        SerializedProperty useFloatEvent = data as SerializedProperty;
        useFloatEvent.boolValue = false;
        useFloatEvent.serializedObject.ApplyModifiedProperties();
    }
    void UseFloatEventSelected(object data)
    {
        SerializedProperty useFloatEvent = data as SerializedProperty;
        useFloatEvent.boolValue = true;
        useFloatEvent.serializedObject.ApplyModifiedProperties();
    }
}
