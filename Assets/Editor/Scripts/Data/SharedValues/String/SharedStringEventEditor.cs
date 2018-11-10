//© EagleDragonGames 2018
using UnityEditor;

[CustomEditor(typeof(SharedStringEvent))]
public class SharedStringEventEditor : SharedEventEditor<string>
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorGUI.BeginChangeCheck();
            m_invokeValue = EditorGUILayout.TextField("Invoke Value", m_invokeValue);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
        }
        base.OnInspectorGUI();
    }
}