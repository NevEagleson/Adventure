//© EagleDragonGames 2018
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SharedString))]
public class SharedStringEditor : SharedValueEditor<string, SharedString>
{
    protected override void DrawOnChangedEventElement(Rect pos, SerializedProperty element)
    {
        Rect singleLinePos = pos;
        singleLinePos.height = EditorGUIUtility.singleLineHeight;
        Rect contentPos = EditorGUI.PrefixLabel(singleLinePos, new GUIContent("Event"));
        Rect dropPos = new Rect(contentPos.xMax - 20f, contentPos.y, 20f, contentPos.height);
        contentPos.width -= 20f;
        SerializedProperty useStringEvent = element.FindPropertyRelative("m_useStringEvent");
        if (useStringEvent.boolValue)
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_stringEvent"), GUIContent.none);
        }
        else
        {
            EditorGUI.PropertyField(contentPos, element.FindPropertyRelative("m_event"), GUIContent.none);
        }
        if (GUI.Button(dropPos, GUIContent.none, GUI.skin.GetStyle("ShurikenDropdown")))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use String Event"), useStringEvent.boolValue, UseStringEventSelected, useStringEvent);
            menu.AddItem(new GUIContent("Use Void Event"), !useStringEvent.boolValue, UseVoidEventSelected, useStringEvent);
            menu.ShowAsContext();
        }

        singleLinePos.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty compareString = element.FindPropertyRelative("m_compareString");
        Rect togglePos = singleLinePos;
        togglePos.width = EditorGUIUtility.labelWidth + 20f;
        EditorGUI.PropertyField(togglePos, compareString);
        if (compareString.boolValue)
        {
            Rect rect = singleLinePos;
            rect.width -= EditorGUIUtility.labelWidth + 20f;
            rect.x += EditorGUIUtility.labelWidth + 20f;

            EditorGUI.PropertyField(rect, element.FindPropertyRelative("m_stringToCompare"), GUIContent.none);
        }
    }

    protected override string DrawValue(string value)
    {
        return EditorGUILayout.TextField("Current Value", value);
    }

    protected override float GetOnChangedEventElementHeight(SerializedProperty element)
    {
        return (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2f;
    }

    void UseVoidEventSelected(object data)
    {
        SerializedProperty useStringEvent = data as SerializedProperty;
        useStringEvent.boolValue = false;
        useStringEvent.serializedObject.ApplyModifiedProperties();
    }
    void UseStringEventSelected(object data)
    {
        SerializedProperty useStringEvent = data as SerializedProperty;
        useStringEvent.boolValue = true;
        useStringEvent.serializedObject.ApplyModifiedProperties();
    }
}
