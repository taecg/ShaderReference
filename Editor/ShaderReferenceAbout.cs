#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceAbout : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.Space();

            var labelStyle = new GUIStyle("label")
            {
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.TextArea("Git<https://github.com/taecg/ShaderReference.git>", labelStyle);
            EditorGUILayout.LabelField("by taecg@qq.com  updated 2024/04/09", labelStyle);

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif