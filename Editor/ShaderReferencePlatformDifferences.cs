/**
 * @file         ShaderReferencePlatformDifferences.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2020-04-08
 * @updated      2020-06-08
 *
 * @brief        平台间的差异性
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferencePlatformDifferences : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        #endregion

        #region [绘制界面]
        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("裁剪空间(规范化立方体)");
            ShaderReferenceUtil.DrawOneContent("OpenGL", "裁剪空间下坐标范围(-1,1)");
            ShaderReferenceUtil.DrawOneContent("DirectX", "裁剪空间下坐标范围(1,0)");
            ShaderReferenceUtil.DrawOneContent("UNITY_NEAR_CLIP_VALUE", "裁剪空间下的近剪裁值,(DX为1,OpenGL为-1).");

            ShaderReferenceUtil.DrawTitle("ReversedZ");
            ShaderReferenceUtil.DrawOneContent("Reversed direction(反向方向)", "DirectX 11、DirectX 12、PS4、Xbox One、Metal这些平台都属于反向方向.\n" +
            "深度值从近裁剪面到远裁剪面的值为[1 ~ 0]\n" +
            "裁剪空间下的Z轴范围为[near,0]");
            ShaderReferenceUtil.DrawOneContent("Traditional direction(传统方向)", "除以上反向方向的平台以外都属于传统方向.\n" +
            "深度值从近裁剪面到远裁剪面的值为[0 ~ 1]\n" +
            "裁剪空间下的Z轴范围为:\n" +
            "DX平台=[0,far]\n" +
            "OpenGL平台=[-near,far]");
            ShaderReferenceUtil.DrawOneContent("UNITY_REVERSED_Z", "判断当前平台是否开启ReversedZ");
            ShaderReferenceUtil.DrawOneContent("SystemInfo.usesReversedZBuffer", "利用C#判断当前平台是否支持ReversedZ");
            ShaderReferenceUtil.DrawTitle("其它");

            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif