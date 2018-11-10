using UnityEngine;
using UnityEditor;

public class GUISkinDirectory : EditorWindow
{
    [MenuItem("Editor/GUI Skin Directory")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow<GUISkinDirectory>();
        window.Show();
    }

    private GUIContent m_text;
    private GUIContent m_img;
    private Vector2 m_scroll;

    void OnGUI()
    {
        if (m_text == null)
        {
            m_text = new GUIContent("Example Text");
            m_img = new GUIContent(AssetDatabase.GetBuiltinExtraResource<Texture2D>("UI/Skin/Knob.psd"));
        }

        GUIStyle[] styles = GUI.skin.customStyles;

        m_scroll = EditorGUILayout.BeginScrollView(m_scroll);
        foreach (GUIStyle style in styles)
        {
            GUILayout.Label(style.name);
            Rect rect = GUILayoutUtility.GetRect(0f, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight * 2f, 128f, style, GUILayout.ExpandWidth(true));
            Rect a = rect;
            a.width /= 3f;
            Rect c = new Rect(rect.width - a.width, rect.y, a.width, rect.height);
            Rect b = new Rect(a.xMax, rect.y, c.xMin - a.xMax, rect.height);

            GUI.Label(a, GUIContent.none, style);
            GUI.Label(b, m_text, style);
            GUI.Label(c, m_img, style);
        }
        EditorGUILayout.EndScrollView();
    }
}