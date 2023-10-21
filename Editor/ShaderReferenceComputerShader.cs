/**
 * @file         ShaderReferenceComputerShader.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2022-07-28
 * @updated      2023-10-21
 *
 * @brief        ComputerShader相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceComputerShader : EditorWindow
    {
        #region [数据成员]
        private Vector2 scrollPos;
        #endregion

        #region [绘制界面]
        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("Computer Shader");
            ShaderReferenceUtil.DrawOneContent("SystemInfo.supportsComputeShaders", "C#中检测硬件是否支持Computer Shader.\nOpenGLES 3.1及以上支持Compute Shader.");
            ShaderReferenceUtil.DrawOneContent("#pragma kernel CSMain SOME_DEFINE DEFINE_WITH_VALUE=1337", "声明执行函数为CSMain,可通过多行声明多个函数.\n" +
            "SOME_DEFINE和DEFINE_WITH_VALUE是预定义宏，此项为可选项.");
            // ShaderReferenceUtil.DrawTitle("语义");
            ShaderReferenceUtil.DrawOneContent("ComputerShader.Dispatch(k,X,Y,Z)", "k表示要执行的核函数索引,XYZ表示开启X*Y*Z个线程组,注意是组(每个组中会有具体的线程)!");
            ShaderReferenceUtil.DrawOneContent("[numthreads(x,y,z)]", "每个线程组中的总线程数(x*y*z).");
            ShaderReferenceUtil.DrawOneContent("SV_GroupID", "当前线程所在的线程组ID.[(0,0,0)~(X-1,Y-1,Z-1)]");
            ShaderReferenceUtil.DrawOneContent("SV_GroupThreadID", "当前线程在所在线程组内的ID.[(0,0,0)~(x-1,y-1,z-1)]");
            ShaderReferenceUtil.DrawOneContent("SV_DispatchThreadID", "当前线程的全局唯一ID.值为线程组*线程数+当前线程,是个三维坐标.");
            ShaderReferenceUtil.DrawOneContent("SV_GroupIndex", "当前线程在所在线程内的下标,int类型.[0~X*Y*Z-1]");
            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif