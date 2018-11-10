//© EagleDragonGames 2018
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SharedBool))]
public class SharedBoolEditor : SharedValueEditor<bool, SharedBool>
{
    protected override void DrawOnChangedEventElement(Rect pos, SerializedProperty element)
    {
        Rect singleLinePos = pos;
        singleLinePos.height = EditorGUIUtility.singleLineHeight;
        Rect contentPos = EditorGUI.PrefixLabel(singleLinePos, new GUIContent("Event"));
        Rect dropPos = new Rect(contentPos.xMax - 20f, contentPos.y, 20f, contentPos.height);
        contentPos.width -= 20f;
        SerializedProperty useBoolEvent = element.FindPropertyRelative("m_useBoolEvent");
        if (useBoolEvent.boolValue)
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_boolEvent"), GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_event"), GUIContent.none);
        }
        if (GUI.Button(dropPos, GUIContent.none, GUI.skin.GetStyle("ShurikenDropdown")))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Bool Event"), useBoolEvent.boolValue, UseBoolEventSelected, useBoolEvent);
            menu.AddItem(new GUIContent("Use Void Event"), !useBoolEvent.boolValue, UseVoidEventSelected, useBoolEvent);
            menu.ShowAsContext();
        }

        singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        EditorGUI.PropertyField(singleLinePos, element.FindPropertyRelative("m_comparison"));
    }

    protected override bool DrawValue(bool value)
    {
        return EditorGUILayout.Toggle("Current Value", value);
    }

    protected override void DrawAdditionalControls()
    {
        if(GUILayout.Button("Toggle"))
        {
            TargetValue.Toggle();
            serializedObject.Update();
        }
    }

    protected override float GetOnChangedEventElementHeight(SerializedProperty element)
    {
        return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2f;
    }

    void UseVoidEventSelected(object data)
    {
        SerializedProperty useBoolEvent = data as SerializedProperty;
        useBoolEvent.boolValue = false;
        useBoolEvent.serializedObject.ApplyModifiedProperties();
    }
    void UseBoolEventSelected(object data)
    {
        SerializedProperty useBoolEvent = data as SerializedProperty;
        useBoolEvent.boolValue = true;
        useBoolEvent.serializedObject.ApplyModifiedProperties();
    }
}
