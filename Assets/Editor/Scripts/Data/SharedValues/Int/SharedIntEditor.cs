//© EagleDragonGames 2018
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SharedInt))]
public class SharedIntEditor : SharedValueEditor<int, SharedInt>
{
    protected override void DrawOnChangedEventElement(Rect pos, SerializedProperty element)
    {
        Rect singleLinePos = pos;
        singleLinePos.height = EditorGUIUtility.singleLineHeight;
        Rect contentPos = EditorGUI.PrefixLabel(singleLinePos, new GUIContent("Event"));
        Rect dropPos = new Rect(contentPos.xMax - 20f, contentPos.y, 20f, contentPos.height);
        contentPos.width -= 20f;
        SerializedProperty useIntEvent = element.FindPropertyRelative("m_useIntEvent");
        if (useIntEvent.boolValue)
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_intEvent"), GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_event"), GUIContent.none);
        }
        if (GUI.Button(dropPos, GUIContent.none, GUI.skin.GetStyle("ShurikenDropdown")))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Int Event"), useIntEvent.boolValue, UseIntEventSelected, useIntEvent);
            menu.AddItem(new GUIContent("Use Void Event"), !useIntEvent.boolValue, UseVoidEventSelected, useIntEvent);
            menu.ShowAsContext();
        }

        singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty comparison = element.FindPropertyRelative("m_comparison");
        EditorGUI.PropertyField(singleLinePos, comparison);

        if (comparison.enumValueIndex > 0)
        {
            singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(singleLinePos, element.FindPropertyRelative("m_compareValue"));
        }
    }

    protected override int DrawValue(int value)
    {
        return EditorGUILayout.IntField("Current Value", value);
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
        SerializedProperty useIntEvent = data as SerializedProperty;
        useIntEvent.boolValue = false;
        useIntEvent.serializedObject.ApplyModifiedProperties();
    }
    void UseIntEventSelected(object data)
    {
        SerializedProperty useIntEvent = data as SerializedProperty;
        useIntEvent.boolValue = true;
        useIntEvent.serializedObject.ApplyModifiedProperties();
    }
}
