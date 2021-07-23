/**
 * @file         ShaderReferenceProperties.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-11-17
 * @updated      2020-09-29
 *
 * @brief        绘制相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceUtil
    {
        public static Dictionary<string, string> SearchDic = new Dictionary<string, string>();

        /// <summary>
        /// 绘制一条内容
        /// </summary>
        /// <param name="str">大标题内容</param>
        /// <param name="message">小标题内容</param>
        public static void DrawOneContent(string str, string message = null)
        {
            //主按钮样式
            GUIStyle style01 = new GUIStyle("label");
            style01.alignment = TextAnchor.MiddleLeft;
            style01.wordWrap = false;
            style01.fontStyle = FontStyle.Bold;
            style01.fontSize = ShaderReferenceEditorWindow.FONTSIZE;

            //说明样式
            GUIStyle style02 = new GUIStyle("label");
            style02.wordWrap = true;
            style02.richText = true;
            style02.fontSize = ShaderReferenceEditorWindow.FONTSIZE - 4;

            EditorGUILayout.BeginVertical(new GUIStyle("Box"));
            EditorGUILayout.TextArea(str, style01);
            EditorGUILayout.TextArea(message, style02);
            EditorGUILayout.EndVertical();

            //添加到搜索字典中
            if (!SearchDic.ContainsKey(str))
            {
                SearchDic.Add(str, message);
            }
        }

        public static void DrawTitle(string str)
        {
            EditorGUILayout.LabelField(str, EditorStyles.miniButton);
        }
    }
}
#endif