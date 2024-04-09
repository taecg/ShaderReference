/**
 * @file         ShaderReferenceBuildInTransformation.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2019-09-23
 * @updated      2020-06-08
 *
 * @brief        空间变换
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceBuildInTransformation : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            ShaderReferenceUtil.DrawTitle("空间变换(矩阵)");
            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_MVP", "模型空间>>投影空间");
                    ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_MV", "模型空间>>观察空间");
                    ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_V", "视图空间");
                    ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_P", "投影空间");
                    ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_VP", "视图空间>投影空间");
                    //ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_T_MV", "");
                    //ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_IT_MV", "");
                    ShaderReferenceUtil.DrawOneContent("unity_ObjectToWorld", "本地空间>>世界空间");
                    ShaderReferenceUtil.DrawOneContent("unity_WorldToObject", "世界空间>本地空间");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("GetObjectToWorldMatrix()", "本地空间到世界空间的矩阵");
                    ShaderReferenceUtil.DrawOneContent("GetWorldToObjectMatrix()", "世界空间到本地空间的矩阵");
                    ShaderReferenceUtil.DrawOneContent("GetWorldToViewMatrix()", "世界空间到观察空间的矩阵");
                    ShaderReferenceUtil.DrawOneContent("GetWorldToHClipMatrix()", "世界空间到齐次裁剪空间的矩阵");
                    ShaderReferenceUtil.DrawOneContent("GetViewToHClipMatrix()", "观察空间到齐次裁剪空间的矩阵");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("unity_WorldToCamera", "世界空间到观察空间的矩阵");
            ShaderReferenceUtil.DrawOneContent("unity_CameraToWorld", "观察空间到世界空间的矩阵");

            ShaderReferenceUtil.DrawTitle("空间变换(方法)");
            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("UnityObjectToClipPos(v.vertex)", "将模型空间下的顶点转换到齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("UnityObjectToWorldNormal(v.normal)", "将模型空间下的法线转换到世界空间(已归一化)");
                    ShaderReferenceUtil.DrawOneContent("UnityObjectToWorldDir (v.tangent)", "将模型空间下的切线转换到世界空间(已归一化)");
                    ShaderReferenceUtil.DrawOneContent("UnityWorldSpaceLightDir (i.worldPos)", "世界空间下顶点到灯光方向的向量(未归一化)");
                    ShaderReferenceUtil.DrawOneContent("UnityWorldSpaceViewDir (i.worldPos)", "世界空间下顶点到视线方向的向量(未归一化)");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("float3 TransformObjectToWorld (float3 positionOS)", "从本地空间变换到世界空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformWorldToObject (float3 positionWS)", "从世界空间变换到本地空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformWorldToView(float3 positionWS)", "从世界空间变换到视图空间");

                    ShaderReferenceUtil.DrawOneContent("float4 TransformObjectToHClip(float3 positionOS)", "将模型空间下的顶点变换到齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("float4 TransformWorldToHClip(float3 positionWS)", "将世界空间下的顶点变换到齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("float4 TransformWViewToHClip (float3 positionVS)", "将视图空间下的顶点变换到齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformObjectToWorldNormal (float3 normalOS)", "将法线从本地空间变换到世界空间(已归一化)");
                    break;
            }

            ShaderReferenceUtil.DrawTitle("基础变换矩阵");
            ShaderReferenceUtil.DrawOneContent("平移矩阵", "float4x4 M_translate = float4x4(\n" + "\t1, 0, 0, T.x,\n" + "\t0, 1, 0, T.y,\n" + "\t0, 0, 1, T.z,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("缩放矩阵", "float4x4 M_scale = float4x4(\n" + "\tS.x, 0, 0, 0,\n" + "\t0, S.y, 0, 0,\n" + "\t0, 0, S.z, 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(X轴)", "float4x4 M_rotationX = float4x4(\n" + "\t1, 0, 0, 0,\n" + "\t0, cos(θ), sin(θ), 0,\n" + "\t0, -sin(θ), cos(θ), 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(Y轴)", "float4x4 M_rotationY = float4x4(\n" + "\tcos(θ), 0, sin(θ), 0,\n" + "\t0, 1, 0, 0,\n" + "\t-sin(θy), 0, cos(θ), 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(Z轴)", "float4x4 M_rotationZ = float4x4(\n" + "\tcos(θ), sin(θ), 0, 0,\n" + "\t-sin(θ), cos(θ), 0, 0,\n" + "\t0, 0, 1, 0,\n" + "\t0, 0, 0, 1);");

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif