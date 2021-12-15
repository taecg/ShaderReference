/**
 * @file         ShaderReferenceErrorDebug.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2020-02-24
 * @updated      2021-12-15
 *
 * @brief        Shader中的常见报错信息
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceErrorDebug : EditorWindow
    {
        #region [数据成员]
        private Vector2 scrollPos;
        #endregion

        #region [绘制界面]
        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("SRPBatcher合批失败的原因");
            ShaderReferenceUtil.DrawOneContent("UnityPerMaterial CBuffer inconsistent size inside a SubShader()", "同SubShader内，所有pass的CBUFFER_START(UnityPerMaterial)内容须完全一模一样.");

            ShaderReferenceUtil.DrawTitle("常见报错");
            ShaderReferenceUtil.DrawOneContent("Did not find shader kernel 'frag'to compile at line", "找不到片断着色器，检查下是否有正确编写片断着色器fragment.");
            ShaderReferenceUtil.DrawOneContent("syntax error : unexpected token ')'at line", "这一行存在语法错误,检测这行是否少了什么，如果没有看下它的前一句是否少了最后的分号.");
            ShaderReferenceUtil.DrawOneContent("cannot implicitly convert from 'float2' to 'half4' at line", "不能隐式的将float2转换成float4,需要手动补全，使等号两边分量数量一样才可以.");
            ShaderReferenceUtil.DrawOneContent("invalid subscript 'xx' at line", "无效的下标，通常是因为xx不存在或者xx的分量不存在导致的.");
            ShaderReferenceUtil.DrawOneContent("undeclared identifier 'xx' at line", "xx未定义.");
            ShaderReferenceUtil.DrawOneContent("unrecognized identifier 'xx' at line", "xx未识别到.");
            ShaderReferenceUtil.DrawOneContent("'xx':no matching 2 parameter intrinsic function", "通常是因为xx方法后面括号内的参数不匹配(数量或者类型).");
            ShaderReferenceUtil.DrawOneContent("comma expression used where a vector constructor may have been intended at line 48 (on d3d11)", "逗号的使用场景不对，比如float4 a = (0,0,0,1);应该写成float4 a = float4(0,0,0,1);");
            ShaderReferenceUtil.DrawOneContent("redefinition of 'xx' at xxx", "xx被重复定义了，看下是否和引用的hlsl或者cginc中的重复了.");
            ShaderReferenceUtil.DrawOneContent("incorrect number of arguments to numeric-type constructor at line", "通常是因为xx方法后面括号内的参数不匹配(数量或者类型).");
            switch (ShaderReferenceEditorWindow.mPipline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("Unrecognized sampler 'xx' - does not match any texture and is not a recognized inline name (should contain filter and wrap modes).", "纹理采样器的定义不对.");
                    break;
            }

            ShaderReferenceUtil.DrawTitle("常见警告");
            ShaderReferenceUtil.DrawOneContent("implicit truncation of vector type at line xx (on d3d11)", "最好不要隐式的截断向量,参数中需要几维就写几维.");


            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif