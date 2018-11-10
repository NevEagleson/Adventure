//© EagleDragonGames 2018
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SharedFloatEvent))]
public class SharedFloatEventEditor : SharedEventEditor<float>
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorGUI.BeginChangeCheck();
            m_invokeValue = EditorGUILayout.FloatField("Invoke Value", m_invokeValue);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
        }
        base.OnInspectorGUI();
    }
}