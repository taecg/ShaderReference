/**
 * @file         ShaderReferenceAbout.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-11-10
 * @updated      2022-03-04
 *
 * @brief       制作名单
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceAbout : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        private Texture talogo
        {
            get
            {
                return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath("0ebc080702455584d93294fd816515b1"), typeof(Texture)) as Texture;
            }
        }

        private Texture wechat
        {
            get
            {
                return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath("08a00299070e2c345b3a1656b1965dfe"), typeof(Texture)) as Texture;
            }
        }
        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            EditorGUILayout.Space();

            GUIStyle labelStyle = new GUIStyle("label");
            labelStyle.fontStyle = FontStyle.Italic;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.TextArea("Git更新地址<https://github.com/taecg/ShaderReference.git>", labelStyle);
            EditorGUILayout.LabelField("by taecg@qq.com  updated 2022/03/04", labelStyle);

            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;

            GUILayout.Box(wechat, style);
            EditorGUILayout.TextArea("Unity技术美术公众号 :  gh_8b69cca044dc", labelStyle);

            EditorGUILayout.Space();

            GUIContent content = new GUIContent();
            content.image = talogo;
            if (GUILayout.Button(content, EditorStyles.centeredGreyMiniLabel))
            {
                Application.OpenURL("https://taecg.ke.qq.com/?tuin=407b4ae");
            }
            EditorGUILayout.TextArea("点击图片进入官网,更多干货等着你~", labelStyle);



            EditorGUILayout.EndScrollView();
        }
    }
}
#endif