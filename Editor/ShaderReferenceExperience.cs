/**
 * @file         ShaderReferenceExperience.cs
 * @author       taecg
 * @created      2024-03-22
 * @updated      2024-04-22
 *
 * @brief        平时积累的一些经验(大多是真机上出现的注意事项)
 */

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceExperience : EditorWindow
    {
        #region [数据成员]

        private Vector2 scrollPos;

        #endregion

        #region [绘制界面]

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawOneContent("分支", "尽量不要使用if或者switch去做大量的分支,否则在部分机型上会卡到怀疑人生!");
            ShaderReferenceUtil.DrawOneContent("Stencil", "红米9C上开启深度图同时Shader中启用Stencil时会导致资源显示不可见,关闭任一个即可解决.");
            EditorGUILayout.EndScrollView();
        }

        #endregion
    }
}
#endif