/**
 * @file         ShaderReferenceSetup.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2020-03-04
 * @updated      2020-03-04
 *
 * @brief        整个工具的设置相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceSetup : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        #endregion

        public void DrawMainGUI ()
        {
            scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
            ShaderReferenceUtil.DrawTitle ("Font Size");

            ShaderReferenceUtil.DrawOneContent ("Font Size测试大小", "Font Size测试大小,通过这里查看设置的大小是否合适.");

            //字体大小设置
            ShaderReferenceEditorWindow.FONTSIZE = EditorGUILayout.IntSlider ("Font Size", ShaderReferenceEditorWindow.FONTSIZE, 16, 50);

            EditorGUILayout.EndScrollView ();
        }
    }
}
#endif