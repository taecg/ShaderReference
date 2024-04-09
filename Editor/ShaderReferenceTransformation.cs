/**
 * @file         ShaderReferenceBuildInTransformation.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2019-09-23
 * @updated      2021-03-04
 *
 * @brief        空间变换
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceTransformation : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            ShaderReferenceUtil.DrawTitle("空间变换(矩阵)");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_M", "模型变换矩阵:unity_ObjectToWorld");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_I_M", "模型变换逆矩阵:unity_WorldToObject");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_V", "视图变换矩阵:unity_MatrixV");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_I_V", "视图变换逆矩阵:unity_MatrixInvV");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_P", "投影变换矩阵:OptimizeProjectionMatrix(glstate_matrix_projection)");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_I_P", "投影变换逆矩阵:ERROR_UNITY_MATRIX_I_P_IS_NOT_DEFINED");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_VP", "视图投影变换矩阵:unity_MatrixVP");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_I_VP", "视图投影变换逆矩阵:_InvCameraViewProj");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_MV", "模型视图变换矩阵:mul(UNITY_MATRIX_V, UNITY_MATRIX_M)");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_T_MV", "模型视图变换转置矩阵:transpose(UNITY_MATRIX_MV)");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_IT_MV", "模型视图变换转置逆矩阵:transpose(mul(UNITY_MATRIX_I_M, UNITY_MATRIX_I_V))");
            ShaderReferenceUtil.DrawOneContent("UNITY_MATRIX_MVP", "模型视图投影变换矩阵:mul(UNITY_MATRIX_VP, UNITY_MATRIX_M)");

            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    // ShaderReferenceUtil.DrawOneContent("GetObjectToWorldMatrix()", "模型变换矩阵:UNITY_MATRIX_M");
                    // ShaderReferenceUtil.DrawOneContent("GetWorldToObjectMatrix()", "世界空间到本地空间的矩阵");
                    // ShaderReferenceUtil.DrawOneContent("GetWorldToViewMatrix()", "世界空间到视图空间的矩阵");
                    // ShaderReferenceUtil.DrawOneContent("GetWorldToHClipMatrix()", "世界空间到齐次裁剪空间的矩阵");
                    // ShaderReferenceUtil.DrawOneContent("GetViewToHClipMatrix()", "视图空间到齐次裁剪空间的矩阵");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("unity_WorldToCamera", "世界空间到视图空间的矩阵");
            ShaderReferenceUtil.DrawOneContent("unity_CameraToWorld", "视图空间到世界空间的矩阵");

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
                    ShaderReferenceUtil.DrawOneContent("float3 TransformObjectToWorld(float3 positionOS)", "模型空间>>世界空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformWorldToObject(float3 positionWS)", "世界空间>>模型空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformWorldToView(float3 positionWS)", "世界空间>>视图空间");
                    ShaderReferenceUtil.DrawOneContent("float4 TransformObjectToHClip(float3 positionOS)", "模型空间>>齐次裁剪空间 (比MVP高效)");
                    ShaderReferenceUtil.DrawOneContent("float4 TransformWorldToHClip(float3 positionWS)", "世界空间>>齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("float4 TransformWViewToHClip(float3 positionVS)", "视图空间>>齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("real3 TransformObjectToWorldDir(real3 dirOS)", "(向量)模型空间>>世界空间");
                    ShaderReferenceUtil.DrawOneContent("real3 TransformWorldToObjectDir(real3 dirWS)", "(向量)世界空间>>模型空间");
                    ShaderReferenceUtil.DrawOneContent("real3 TransformWorldToViewDir(real3 dirWS)", "(向量)世界空间>>视图空间");
                    ShaderReferenceUtil.DrawOneContent("real3 TransformWorldToHClipDir(real3 directionWS)", "(向量)世界空间>齐次裁剪空间");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformObjectToWorldNormal(float3 normalOS)", "(法线)模型空间>>世界空间(已归一化)");
                    ShaderReferenceUtil.DrawOneContent("float3 TransformWorldToObjectNormal(float3 normalWS)", "(法线)世界空间>>模型空间(已归一化)");
                    break;
            }

            ShaderReferenceUtil.DrawTitle("基础变换矩阵");
            ShaderReferenceUtil.DrawOneContent("平移矩阵", "float4x4 M_translate = float4x4(\n" + "\t1, 0, 0, T.x,\n" + "\t0, 1, 0, T.y,\n" + "\t0, 0, 1, T.z,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("缩放矩阵", "float4x4 M_scale = float4x4(\n" + "\tS.x, 0, 0, 0,\n" + "\t0, S.y, 0, 0,\n" + "\t0, 0, S.z, 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(X轴)", "float4x4 M_rotationX = float4x4(\n" + "\t1, 0, 0, 0,\n" + "\t0, cos(θ), -sin(θ), 0,\n" + "\t0, sin(θ), cos(θ), 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(Y轴)", "float4x4 M_rotationY = float4x4(\n" + "\tcos(θ), 0, sin(θ), 0,\n" + "\t0, 1, 0, 0,\n" + "\t-sin(θ), 0, cos(θ), 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawOneContent("旋转矩阵(Z轴)", "float4x4 M_rotationZ = float4x4(\n" + "\tcos(θ), -sin(θ), 0, 0,\n" + "\tsin(θ), cos(θ), 0, 0,\n" + "\t0, 0, 1, 0,\n" + "\t0, 0, 0, 1);");

            ShaderReferenceUtil.DrawTitle("变换规则");
            ShaderReferenceUtil.DrawOneContent("将P点从A空间变换到B空间", "P_B = M_AB * P_A\n" + "       = (M_BA)^-1 * P_A\n" + "       = (M_BA)^T * P_A)");


            EditorGUILayout.EndScrollView();
        }
    }
}
#endif