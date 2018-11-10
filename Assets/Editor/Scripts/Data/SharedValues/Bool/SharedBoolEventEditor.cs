//© EagleDragonGames 2018
using UnityEditor;

[CustomEditor(typeof(SharedBoolEvent))]
public class SharedBoolEventEditor : SharedEventEditor<bool>
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorGUI.BeginChangeCheck();
            m_invokeValue = EditorGUILayout.Toggle("Invoke Value", m_invokeValue);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
        }
        base.OnInspectorGUI();
    }
}