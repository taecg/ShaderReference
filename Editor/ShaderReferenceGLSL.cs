/**
 * @file         ShaderReferenceMath.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-11-16
 * @updated      2022-07-19
 *
 * @brief        数学运算相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceGLSL : EditorWindow
    {
        #region [数据成员]
        private Vector2 scrollPos;
        #endregion

        #region [绘制界面]
        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("基本类型");
            ShaderReferenceUtil.DrawOneContent("void", "空类型，即不返回任何值.");
            ShaderReferenceUtil.DrawOneContent("bool", "布尔类型，即真或者假,true false.");
            ShaderReferenceUtil.DrawOneContent("int", "带符号的整数.");
            ShaderReferenceUtil.DrawOneContent("float", "带符号的浮点数.");
            ShaderReferenceUtil.DrawOneContent("vec2,vec3,vec4", "n维浮点数向量.");
            ShaderReferenceUtil.DrawOneContent("bvec2,bvec3,bvec4", "n维布尔向量.");
            ShaderReferenceUtil.DrawOneContent("ivec2,ivec3,ivec4", "n维向整数向量");
            ShaderReferenceUtil.DrawOneContent("mat2,mat3,mat4", "2x2,3x3,4x4浮点数矩阵.");
            ShaderReferenceUtil.DrawOneContent("clamp(a,min,max)", "将a限制在min和max之间.");
            ShaderReferenceUtil.DrawOneContent("sampler2D", "2D纹理.");
            ShaderReferenceUtil.DrawOneContent("samplerCube", "立方体纹理.");
            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif