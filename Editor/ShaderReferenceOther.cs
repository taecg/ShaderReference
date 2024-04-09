/**
 * @file         ShaderReferenceOther.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-12-30
 * @updated      2020-12-15
 *
 * @brief        Shader中的其它语法
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceOther : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("Other");
            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("CGPROGRAM/ENDCG", "cg代码的开始与结束.");
                    ShaderReferenceUtil.DrawOneContent("CGINCLUDE/ENDCG", "通常用于定义多段vert/frag函数，然后这段CG代码会插入到所有Pass的CG中，根据当前Pass的设置来选择加载.");
                    ShaderReferenceUtil.DrawOneContent("Fallback \"name\"", "备胎，当Shader中没有任何SubShader可执行时，则执行FallBack。默认值为Off,表示没有备胎。\n示例:FallBack \"Diffuse\"");
                    ShaderReferenceUtil.DrawOneContent("GrabPass", "GrabPass{} 抓取当前屏幕存储到_GrabTexture中，每个有此命令的Shader都会每帧执行。\nGrabPass { \"TextureName\" } 抓取当前屏幕存储到自定义的TextureName中，每帧中只有第一个拥有此命令的Shader执行一次。\nGrabPass也支持Name与Tags。");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("include", "#include \"Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl\"\n" + "#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl\"\n" + "#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl\"\n" + "#include \"Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl\"\n" + "#include \"Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl\"");
                    ShaderReferenceUtil.DrawOneContent("CBUFFER_START(UnityPerMaterial)/CBUFFER_END", "将材质属性面板中的变量定义在这个常量缓冲区中，用于支持SRP Batcher.");
                    ShaderReferenceUtil.DrawOneContent("HLSLPROGRAM/ENDHLSL", "HLSL代码的开始与结束.");
                    ShaderReferenceUtil.DrawOneContent("HLSLINCLUDE/ENDHLSL", "通常用于定义多段vert/frag函数，然后这段CG代码会插入到所有Pass的CG中，根据当前Pass的设置来选择加载.");
                    ShaderReferenceUtil.DrawOneContent("Fallback \"name\"", "备胎，当Shader中没有任何SubShader可执行时，则执行FallBack。默认值为Off,表示没有备胎。\n比如URP下默认的紫色报错Shader:Fallback \"Hidden/Universal Render Pipeline/FallbackError\"");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("LOD", "Shader LOD，可利用脚本来控制LOD级别，通常用于不同配置显示不同的SubShader。注意SubShader要从高往低写，要不然会无法生效.");
            ShaderReferenceUtil.DrawOneContent("Category{}", "定义一组所有SubShader共享的命令，位于SubShader外面。\n");
            ShaderReferenceUtil.DrawOneContent("CustomEditor \"name\"", "自定义材质面板，name为自定义的脚本名称。可利用此功能对材质面板进行个性化自定义。");
            ShaderReferenceUtil.DrawOneContent("Name \"MyPassName\"", "给当前Pass指定名称，以便利用UsePass进行调用。");
            ShaderReferenceUtil.DrawOneContent("UsePass \"Shader/NAME\"", "调用其它Shader中的Pass，注意Pass的名称要全部大写！Shader的路径也要写全，以便能找到具体是哪个Shader的哪个Pass。另外加了UsePass后，也要注意相应的Properties要自行添加。");
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif