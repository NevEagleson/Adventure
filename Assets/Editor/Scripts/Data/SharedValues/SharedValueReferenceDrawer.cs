//© EagleDragonGames 2018
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SharedValueReferenceBase), true)]
public class SharedValueReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUIContent propLabel = EditorGUI.BeginProperty(position, label, property);
        Rect contentPos = EditorGUI.PrefixLabel(position, propLabel);
        Rect dropPos = new Rect(contentPos.xMax - 20f, contentPos.y, 20f, contentPos.height);
        contentPos.width -= 20f;
        SerializedProperty useConstant = property.FindPropertyRelative("m_useConstant");
        if (useConstant.boolValue)
        {
            EditorGUI.PropertyField(contentPos, property.FindPropertyRelative("m_constantValue"), GUIContent.none);
        }
        else
        {
            SerializedProperty sharedValueProp = property.FindPropertyRelative("m_sharedValue");
            EditorGUI.PropertyField(contentPos, sharedValueProp, GUIContent.none);

            if(sharedValueProp.objectReferenceValue != null && Event.current.type == EventType.MouseMove && contentPos.Contains(Event.current.mousePosition))
            {
                GUI.tooltip = sharedValueProp.objectReferenceValue.ToString();
            }
        }
        if(GUI.Button(dropPos, GUIContent.none, GUI.skin.GetStyle("ShurikenDropdown")))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Constant"), useConstant.boolValue, UseConstantSelected, useConstant);
            menu.AddItem(new GUIContent("Use Shared Value"), !useConstant.boolValue, UseSharedValueSelected, useConstant);
            menu.ShowAsContext();
        }
        EditorGUI.EndProperty();
    }

    void UseConstantSelected(object data)
    {
        SerializedProperty useConstant = data as SerializedProperty;
        useConstant.boolValue = true;
        useConstant.serializedObject.ApplyModifiedProperties();
    }
    void UseSharedValueSelected(object data)
    {
        SerializedProperty useConstant = data as SerializedProperty;
        useConstant.boolValue = false;
        useConstant.serializedObject.ApplyModifiedProperties();
    }
}