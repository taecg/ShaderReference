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
        //主按钮样式
        private static GUIStyle style01;
        private static GUIStyle Style01
        {
            get
            {
                if (style01 == null)
                {
                    style01 = new GUIStyle("label");
                    style01.alignment = TextAnchor.MiddleLeft;
                    style01.wordWrap = false;
                    style01.fontStyle = FontStyle.Bold;
                    style01.fontSize = ShaderReferenceEditorWindow.FONTSIZE;
                }
                return style01;
            }
        }

        //说明样式
        private static GUIStyle style02;
        private static GUIStyle Style02
        {
            get
            {
                if (style02 == null)
                {
                    style02 = new GUIStyle("label");
                    style02.wordWrap = true;
                    style02.richText = true;
                    style02.fontSize = ShaderReferenceEditorWindow.FONTSIZE - 4;
                }
                return style02;
            }
        }

        //说明样式
        private static GUIStyle style03;
        private static GUIStyle Style03
        {
            get
            {
                if (style03 == null)
                {
                    style03 = new GUIStyle("box");
                }
                return style03;
            }
        }

        public static Dictionary<string, string> SearchDic = new Dictionary<string, string>();

        /// <summary>
        /// 绘制一条内容
        /// </summary>
        /// <param name="str">大标题内容</param>
        /// <param name="message">小标题内容</param>
        public static void DrawOneContent(string str, string message = null)
        {
            EditorGUILayout.BeginVertical(Style03);
            EditorGUILayout.TextArea(str, Style01);
            EditorGUILayout.TextArea(message, Style02);
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