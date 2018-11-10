//© EagleDragonGames 2018
using UnityEditor;

[CustomEditor(typeof(SharedIntEvent))]
public class SharedIntEventEditor : SharedEventEditor<int>
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            EditorGUI.BeginChangeCheck();
            m_invokeValue = EditorGUILayout.IntField("Invoke Value", m_invokeValue);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
        }
        base.OnInspectorGUI();
    }
}