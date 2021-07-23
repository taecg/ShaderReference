/**
 * @file         ShaderReferenceSearch.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2020-02-24
 * @updated      2020-02-24
 *
 * @brief        搜索结果显示
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceSearch : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        #endregion

        public void DrawMainGUI ()
        {
            scrollPos = EditorGUILayout.BeginScrollView (scrollPos);
            foreach (var i in ShaderReferenceUtil.SearchDic)
            {
                if (i.Key.ToLower ().Contains (ShaderReferenceEditorWindow.SEARCH_TEXT.ToLower ()) || i.Value.ToLower ().Contains (ShaderReferenceEditorWindow.SEARCH_TEXT.ToLower ()))
                {
                    ShaderReferenceUtil.DrawOneContent (i.Key, i.Value);
                }
            }
            EditorGUILayout.EndScrollView ();
        }
    }
}
#endif