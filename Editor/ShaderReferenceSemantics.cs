/**
 * @file         ShaderReferenceSemantics.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-11-17
 * @updated      2020-04-08
 *
 * @brief        语义
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceSemantics : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("应用程序到顶点着色器的数据（ appdata ）");
            ShaderReferenceUtil.DrawOneContent("float4 vertex : POSITION;", "顶点的本地坐标");
            ShaderReferenceUtil.DrawOneContent("uint vid : SV_VertexID;", "顶点的索引ID");
            ShaderReferenceUtil.DrawOneContent("float3 normal : NORMAL;", "顶点的法线信息");
            ShaderReferenceUtil.DrawOneContent("float4 tangent : TANGENT;", "顶点的切线信息");
            ShaderReferenceUtil.DrawOneContent("float4 texcoord : TEXCOORD0;", "顶点的UV1信息");
            ShaderReferenceUtil.DrawOneContent("float4 texcoord1 : TEXCOORD1;", "顶点的UV2信息");
            ShaderReferenceUtil.DrawOneContent("float4 texcoord2 : TEXCOORD2;", "顶点的UV3信息");
            ShaderReferenceUtil.DrawOneContent("float4 texcoord3 : TEXCOORD3;", "顶点的UV4信息");
            ShaderReferenceUtil.DrawOneContent("fixed4 color : COLOR;", "顶点的顶点色信息");

            ShaderReferenceUtil.DrawTitle("顶点着色器到片断着色器的数据 ( v2f )");
            ShaderReferenceUtil.DrawOneContent("float4 pos:SV_POSITION;", "顶点的齐次裁剪空间下的坐标,默认情况下用POSITION也可以(PS4下不支持)，但是为了支持所有平台，所以最好使用SV_POSITION.");
            ShaderReferenceUtil.DrawOneContent("TEXCOORD0~N", "例如TEXCOORD0、TEXCOORD1、TEXCOORD2...等等");
            ShaderReferenceUtil.DrawOneContent("COLOR0~N", "例如COLOR0、COLOR1、COLOR2...等等，主要用于低精度数据，由于平台适配问题，不建议在v2f中使用COLORn");
            ShaderReferenceUtil.DrawOneContent("float face:VFACE", "如果渲染表面朝向摄像机，则Face节点输出正值1，如果远离摄像机，则输出负值-1");
            ShaderReferenceUtil.DrawOneContent("UNITY_VPOS_TYPE screenPos : VPOS", "1.当前片断在屏幕上的位置(单位是像素,可除以_ScreenParams.xy来做归一化),此功能仅支持#pragma target 3.0及以上编译指令\n" +
                "2.大部分平台下VPOS返回的是一个四维向量，部分平台是二维向量，所以需要用UNITY_VPOS_TYPE来统一区分.\n" +
                "3.在使用VPOS时，就不能在v2f中定义SV_POSITION，这样会冲突，所以需要把顶点着色器的输入放在()的参数中，并且SV_POSITION添加out.");
            ShaderReferenceUtil.DrawOneContent("uint vid : SV_VertexID", "顶点着色器可以接收具有“顶点编号”作为无符号整数的变量,当需要从纹理或ComputeBuffers中获取额外的顶点数据时比较有用，此语义仅支持#pragma target 3.5及以上");
            ShaderReferenceUtil.DrawOneContent("注意事项", "1.OpenGL ES2.0支持最多8个\n2.OpenGL ES3.0支持最多16个");

            ShaderReferenceUtil.DrawTitle("片断着色器输出的数据（ fragOutput ）");
            ShaderReferenceUtil.DrawOneContent("fixed4 color : SV_Target;", "默认RenderTarget,也是默认输出的屏幕上的颜色\n同时支持SV_Target1、SV_Target2...等等");
            ShaderReferenceUtil.DrawOneContent("fixed depth : SV_Depth;", "通过在片断着色器中输出SV_DEPTH语义可以更改像素的深度值,注意此功能相对会消耗性能，在没有特别需求的情况下尽量不要用");

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif