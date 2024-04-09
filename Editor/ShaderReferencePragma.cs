/**
 * @file         ShaderReferencePragma.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2019-01-17
 * @updated      2023-11-20
 *
 * @brief        Pass中的Pragma
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferencePragma : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        #region [绘制界面]

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            ShaderReferenceUtil.DrawTitle("Pragma");
            ShaderReferenceUtil.DrawOneContent("#pragma target 2.0", "Shader编绎目标级别，默认值为2.5\n" + "可以通过#if (SHADER_TARGET < 30)来做分支判断\n" + "● 2.0: \n" + "● 2.5: derivatives\n" + "● 3.0: 2.5 + interpolators10 + samplelod + fragcoord\n" + "● 3.5(相当于OpenGL ES3.0): 3.0 + interpolators15 + mrt4 + integers + 2darray + instancing\n" + "● 4.0: 3.5 + geometry\n" + "● 4.5(相当于OpenGL ES3.1): 3.5 + compute + randomwrite\n" + "● 4.6: 4.0 + cubearray + tesshw + tessellation\n" + "● 5.0: 4.0 + compute + randomwrite + tesshw + tessellation");
            ShaderReferenceUtil.DrawOneContent("#pragma target 2.0 KEYWORD", "此指令仅在指定Keyword激活时启用");

            ShaderReferenceUtil.DrawOneContent("#pragma require xxx", "表明shader需要的特性功能\n" + "● interpolators10: 至少支持10个插值器(从顶点到片断)\n" + "● interpolators15: 至少支持15个插值器(从顶点到片断)\n" + "● interpolators32: 至少支持32个插值器(从顶点到片断)\n" + "● mrt4: 至少支持4个Multiple Render Targets\n" + "● mrt8: 至少支持8个Multiple Render Targets\n" + "● derivatives: 片断着色器支持偏导函数(ddx/ddy)\n" + "● samplelod: 纹理LOD采样\n" + "● fragcoord: 将像素的位置(XY为屏幕上的坐标,ZW为齐次裁剪空间下的深度)传入到片断着色器中\n" + "● integers: 支持真正的整数类型,包括位/移位操作\n" + "● 2darray: 2D纹理数组\n" + "● cubearray: Cubemap纹理数组\n" + "● instancing: GPU实例化\n" + "● geometry: 几何着色器\n" + "● compute: Compute Shader\n" + "● randomwrite: 可以编写任意位置的一些纹理和缓冲区 (UAV,unordered access views)\n" + "● tesshw: GPU支持硬件的tessellation\n" + "● tessellation: Tessellation hull/domain Shader\n" + "● msaatex: 能够访问多采样纹理\n" + "● framebufferfetch: 主要用于在延迟渲染中减少采样的带宽消耗");
            ShaderReferenceUtil.DrawOneContent("#pragma require xxx : KEYWORD", "此指令仅在指定Keyword激活时启用.");

            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_fwdbase novertexlight nodynlightmap nodirlightmap", "定义在LightMode = ForwardBase的Pass中,在此Pass中仅只持一个平行灯(逐像素)以及其它逐顶点灯和SH当照.这个指令的作用是一次性生成Unity在ForwardBase中需要的各种内置宏.\n" + "DIRECTIONAL DIRLIGHTMAP_COMBINED DYNAMICLIGHTMAP_ON LIGHTMAP_ON LIGHTMAP_SHADOW_MIXING LIGHTPROBE_SH SHADOWS_SCREEN SHADOWS_SHADOWMASK VERTEXLIGHT_ON\n" + "1. DIRECTIONAL :主平行灯下的效果开启,fowwardBase下必开宏\n" + "2. DIRLIGHTMAP_COMBINED :烘焙界面中的DirecitonalMode设置为Directional\n" + "3. DYNAMICLIGHTMAP_ON :RealtimeGI是否开启\n" + "4. LIGHTMAP_ON：当对象标记为LightMap Static并且场景烘焙后开启\n" + "5. LIGHTMAP_SHADOW_MIXING:当灯光设置为Mixed,光照烘焙模式设置为Subtractive或者shadowMask时开启,Baked Indirect情况下无效\n" + "6. LIGHTPROBE_SH:开启光照探针,动态物体会受到LightProbe的影响,静态物体与此不相关\n" + "7. SHADOWS_SCREEN:在硬件支持屏幕阴影的情况下，同时处理阴影的距离范围内时开启\n" + "8. SHADOWS_SHADOWMASK:当灯光设置为Mixed,光照烘焙模式设置为shadowMask时开启\n" + "9. VERTEXLIGHT_ON ：是否受到逐顶点的照明");
                    ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_fwdadd", "定义在LightMode=ForwardAdd的Pass中，在此Pass中用来计算其它的逐像素光照.而此指令的作用是一次性生成Unity在ForwardAdd中需要的各种内置宏.\n" + "DIRECTIONAL DIRECTIONAL_COOKIE POINT POINT_COOKIE SPOT\n" + "1. DIRECTIONAL :判断当前灯是否为平行灯.\n" + "2. DIRECTIONAL_COOKIE :判断当前灯是否为Cookie平行灯.\n" + "3. POINT :判断当前灯是否为点灯.\n" + "4. POINT_COOKIE :判断当前灯是否为Cookie点灯.\n" + "5. SPOT :判断当前灯是否为聚光灯.");
                    ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_shadowcaster", "定义在LightMode=ShadowCaster的Pass中，会自动生成两个宏:\n" + "1. SHADOWS_DEPTH :用于生成直线光和聚光灯阴影.\n" + "2. SHADOW_CUBE :用于生成点光源阴影.");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("#pragma prefer_hlslcc gles", "将HLSL代码转换为GLSL代码时，优先使用HLSLcc转换器(性能优、兼容性更好)。在Unity2020以后不再需要添加此指令了。");
                    ShaderReferenceUtil.DrawOneContent("#include \"XXX.hlsl\"", "引入hlsl文件");
                    ShaderReferenceUtil.DrawOneContent("#include_with_pragmas \"XXX.hlsl\"", "引入hlsl文件,同时也会使用hlsl文件中的#pragma指令.(Caching Shader Preprocessor要开启)");
                    ShaderReferenceUtil.DrawOneContent("#pragma editor_sync_compilation", "强制某个Shader以同步的方式进行编绎(当此Shader的某个变体被第一次渲染时，在还没有编绎完成前不会渲染出来;如果不加此指令则会先用一个青色的临时占位进行渲染显示。)");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("#pragma shader_feature", "变体声明，常用于不需要程序控制开关的关键字，在编缉器的材质上设置，打包时会自动过滤");
            ShaderReferenceUtil.DrawOneContent("#pragma shader_feature_local", "声明本地变体(shader_feature)，unity2019才支持的功能，每个Shader最多可以有64个本地变体，不占用全局变体的数量.");
            //ShaderReferenceUtil.DrawOneContent("#pragma shader_feature_local_fragment", "");
            ShaderReferenceUtil.DrawOneContent("#pragma multi_compile", "变体声明，在打包时会把所有变体都打包进去，这是它与feature的区别.\n" + "定义关键字时如果加两个下划线，则表示定义一个空的变体，主要目的是为了节省关键字.\n" + "当使用shader变体时，记住在unity中全局关键字最多只有256个,而且在内部已经用了60个了,所以记得不要超标了.");
            ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_local", "声明本地变体(multi_compile)，unity2019才支持的功能，每个Shader最多可以有64个本地变体，不占用全局变体的数量.");
            //ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_fragment", "");
            ShaderReferenceUtil.DrawOneContent("#pragma multi_compile_fog", "雾类型定义\nFOG_EXP FOG_EXP2 FOG_LINEAR");
            ShaderReferenceUtil.DrawOneContent("#pragma skip_variants XXX01 XXX02...", "剔除指定的变体，可同时剔除多个");
            ShaderReferenceUtil.DrawOneContent("#pragma fragmentoption ARB_precision_hint_fastest", "最快的,意思就是会用低精度(一般是指fp16),以提升片段着色器的运行速度,减少时间.");
            ShaderReferenceUtil.DrawOneContent("#pragma fragmentoption ARB_precision_hint_nicest", "最佳的,会用高精度(一般是指fp32),可能会降低运行速度,增加时间.");
            ShaderReferenceUtil.DrawOneContent("#pragma enable_d3d11_debug_symbols", "开启调试，便于在调试工具中进行查看分析.但是会禁用Graphics API上的优化，建议调试完删除。");
            ShaderReferenceUtil.DrawOneContent("#pragma skip_optimizations <gles/vulkan...>", "调试用,禁用某平台的优化,以便于在调试看到更接近原始意途的代码.但是会降低部分性能，建议调试完删除。");
            ShaderReferenceUtil.DrawOneContent("#pragma shader_feature EDITOR_VISUALIZATION", "开启Material Validation,Scene视图中的模式，用于查看超出范围的像素颜色");
            ShaderReferenceUtil.DrawOneContent("#pragma only_renderers", "仅编译指定平台的Shader\n" + "1. d3d11 - Direct3D 11/12\n" + "2. glcore - OpenGL 3.x/4.x\n" + "3. gles - OpenGL ES 2.0\n" + "4. gles3 - OpenGL ES 3.x\n" + "5. metal - iOS/Mac Metal\n" + "6. vulkan - Vulkan\n" + "7. d3d11_9x - Direct3D 11 9.x功能级别，通常在WSA平台上使用\n" + "8. xboxone - Xbox One\n" + "9. ps4 - PlayStation 4\n" + "10.psp2 - PlayStation Vita\n" + "11.n3ds - Nintendo 3DS\n" + "12.wiiu - Nintendo Wii U");
            ShaderReferenceUtil.DrawOneContent("#pragma exclude_renderers", "剔除掉指定平台的相关代码,列表参考上面");
            ShaderReferenceUtil.DrawOneContent("#pragma once", "通常添加在通用的.hlsl文件中,以此来避免多次或者重复引用和编绎.(Caching Shader Preprocessor要开启)");
            ShaderReferenceUtil.DrawOneContent("#define NAME", "定义一个叫NAME的字段，在CG代码中可以通过#if defined(NAME)来判断走不同的分支。");
            ShaderReferenceUtil.DrawOneContent("#define NAME 1", "定义一个叫NAME的字段并且它的值为1.\n" + "1.可以通过#if defined(NAME)来判断走不同的分支。\n" + "2.可以通过#if NAME来判断走不同的分支。（此时值为非0时才有效，为0时不走此分支）\n" + "3.还可以直接通过NAME来得到它的值，比如上面的1。");
            ShaderReferenceUtil.DrawOneContent("#error xxx", "多用于分支的判断中，利用此语句可直接输出一条报错信息，内容为xxx");

            EditorGUILayout.EndScrollView();
        }

        #endregion
    }
}
#endif