/**
 * @file         ShaderReferenceComputerShader.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2022-07-28
 * @updated      2022-07-28
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
            ShaderReferenceUtil.DrawOneContent("SystemInfo.supportsComputeShaders", "C#中检测硬件是否支持ComputerShader");
            ShaderReferenceUtil.DrawOneContent("#pragma kernel CSMain SOME_DEFINE DEFINE_WITH_VALUE=1337", "声明执行函数为CSMain,可通过多行声明多个函数.\n" +
            "SOME_DEFINE和DEFINE_WITH_VALUE是预定义宏，此项为可选项.");
            // ShaderReferenceUtil.DrawTitle("语义");
            ShaderReferenceUtil.DrawOneContent("ComputerShader.Dispatch(k,a,b,c)", "表示开启a*b*c个线程组,注意是组(每个组中才是具体的线程)!");
            ShaderReferenceUtil.DrawOneContent("SV_GroupID", "当下所执行的线程组.");
            ShaderReferenceUtil.DrawOneContent("[numthreads(a,b,c)]", "每个线程组中的总线程数(a*b*c).");
            ShaderReferenceUtil.DrawOneContent("SV_GroupThreadID", "当下线程组中的具体线程.");
            ShaderReferenceUtil.DrawOneContent("SV_DispatchThreadID", "全局唯一ID,值为线程组*线程数+当前线程,是个三维坐标.");
            ShaderReferenceUtil.DrawOneContent("SV_GroupIndex", "");
            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif