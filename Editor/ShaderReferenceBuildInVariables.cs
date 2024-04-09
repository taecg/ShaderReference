/**
 * @file         ShaderReferenceBuildInVariables.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2019-02-26
 * @updated      2022-04-01
 *
 * @brief        内置变量相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceBuildInVariables : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            ShaderReferenceUtil.DrawTitle("");
            ShaderReferenceUtil.DrawOneContent("UNITY_INITIALIZE_OUTPUT(type,name)", "由于HLSL编缉器不接受没有初始化的数据，所以为了支持所有平台，从而需要使用此方法进行初始化.");
            ShaderReferenceUtil.DrawOneContent("TRANSFORM_TEX(i.uv,_MainTex)", "对UV进行Tiling与Offset变换");
            ShaderReferenceUtil.DrawOneContent("float3 UnityWorldSpaceLightDir( float3 worldPos )", "返回顶点到灯光的向量");

            ShaderReferenceUtil.DrawTitle("Camera and Screen");
            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("_WorldSpaceCameraPos", "主相机的世界坐标位置，类型：float3");
                    ShaderReferenceUtil.DrawOneContent("UnityWorldSpaceViewDir(i.worldPos)", "世界空间下的相机方向(顶点到主相机)，类型：float3");
                    ShaderReferenceUtil.DrawOneContent("深度:_CameraDepthTexture", "1.在脚本中开启相机的深度:Camera.main.depthTextureMode = DepthTextureMode.Depth;\n" + "2.sampler2D_float _CameraDepthTexture;\n" + "3.float depth = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));");
                    ShaderReferenceUtil.DrawOneContent("线性深度转换", "从深度图中得到顶点的线性深度值(相机位置=0，相机远裁剪面=1)\n" + "Linear01Depth(depthMap, _ZBufferParams);\n" + "从深度图中得到顶点的线性深度值(不是0-1的范围)\n" + "LinearEyeDepth(depthMap, _ZBufferParams);");
                    ShaderReferenceUtil.DrawOneContent("ComputeScreenPos(float4 pos)", "pos为裁剪空间下的坐标位置，返回的是某个投影点下的屏幕坐标位置" + "\n由于这个函数返回的坐标值并未除以齐次坐标，所以如果直接使用函数的返回值的话，需要使用：tex2Dproj(_ScreenTexture, uv.xyw);" + "\n也可以自己处理其次坐标,使用：tex2D(_ScreenTexture, uv.xy / uv.w);");
                    ShaderReferenceUtil.DrawOneContent("_ScreenParams", "屏幕的相关参数，单位为像素。\nx表示屏幕的宽度\ny表示屏幕的高度\nz表示1+1/屏幕宽度\nw表示1+1/屏幕高度");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("Camera相关参数", "相机在世界空间下的位置坐标(xyz):_WorldSpaceCameraPos\n" + "相机指向前方的方向:-1 * mul(UNITY_MATRIX_M, transpose(mul(UNITY_MATRIX_I_M, UNITY_MATRIX_I_V)) [2].xyz);\n" + "是否是正交相机(1为正交,0为透视):unity_OrthoParams.w\n" + "近裁剪面:_ProjectionParams.y\n" + "远裁剪面:_ProjectionParams.z\n" + "Z Buffer方向(-1为反向,1为正向):_ProjectionParams.x\n" + "正交相机的宽度:unity_OrthoParams.x\n" + "正交相机的高度:unity_OrthoParams.y");
                    ShaderReferenceUtil.DrawOneContent("_ZBufferParams", "传统Z方向:\n" + "x=1-far/near\n" + "y=far/near\n" + "z=x/far\n" + "w=y/far\n\n" + "反向Z:\n" + "x=-1+far/near\n" + "y=1\n" + "z=x/far\n" + "w=1/far");
                    ShaderReferenceUtil.DrawOneContent("深度:_CameraDepthTexture", "在PipelineAsset中勾选DepthTexture.\n" + "#define REQUIRE_DEPTH_TEXTURE\t//直接这样定义可以省去声明纹理的步骤(直接使用内部hlsl中的定义)\n" + "TEXTURE2D (_CameraDepthTexture);SAMPLER(sampler_CameraDepthTexture);\n" + "float2 screenUV = i.positionCS/_ScreenParams.xy;\n" + "half4 depthMap = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, screenUV);\n" + "half depth = Linear01Depth(depthMap, _ZBufferParams);");
                    ShaderReferenceUtil.DrawOneContent("线性深度转换", "从深度图中得到顶点的线性深度值(相机位置=0，相机远裁剪面=1)\n" + "Linear01Depth(depthMap, _ZBufferParams);\n" + "从深度图中得到顶点的线性深度值(不是0-1的范围)\n" + "LinearEyeDepth(depthMap, _ZBufferParams);");
                    ShaderReferenceUtil.DrawOneContent("抓屏:_CameraOpaqueTexture", "在PipelineAsset中勾选OpaqueTexture,同时这个抓屏只能在半透明序列下正确执行.\n" + "#define REQUIRE_OPAQUE_TEXTURE\t//直接这样定义可以省去声明纹理的步骤(直接使用内部hlsl中的定义)\n" + "TEXTURE2D (_CameraOpaqueTexture);SAMPLER(sampler_CameraOpaqueTexture);\n" + "float2 screenUV = i.positionCS.xy / _ScreenParams.xy;\n" + "half4 screenColor = SAMPLE_TEXTURE2D(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, screenUV);");
                    ShaderReferenceUtil.DrawOneContent("_ScreenParams", "屏幕的相关参数，单位为像素。\nx表示屏幕的宽度\ny表示屏幕的高度\nz表示1+1/屏幕宽度\nw表示1+1/屏幕高度");
                    ShaderReferenceUtil.DrawOneContent("_ScaledScreenParams", "同上，但是有考虑到RenderScale的影响.");
                    break;
            }

            //ShaderReferenceUtil.DrawOneContent("_ProjectionParams", "");
            //ShaderReferenceUtil.DrawOneContent("_ZBufferParams", "");
            //ShaderReferenceUtil.DrawOneContent("unity_OrthoParams", "");
            //ShaderReferenceUtil.DrawOneContent("unity_CameraProjection", "");
            //ShaderReferenceUtil.DrawOneContent("unity_CameraInvProjection", "");
            //ShaderReferenceUtil.DrawOneContent("unity_CameraWorldClipPlanes[6]", "");

            ShaderReferenceUtil.DrawTitle("Time");
            ShaderReferenceUtil.DrawOneContent("_Time", "时间，主要用于在Shader做动画,类型：float4\nx = t/20\ny = t\nz = t*2\nw = t*3");
            ShaderReferenceUtil.DrawOneContent("_SinTime", "t是时间的正弦值，返回值(-1~1): \nx = t/8\ny = t/4\nz = t/2\nw = t");
            ShaderReferenceUtil.DrawOneContent("_CosTime", "t是时间的余弦值，返回值(-1~1):\nx = t/8\ny = t/4\nz = t/2\nw = t");
            ShaderReferenceUtil.DrawOneContent("unity_DeltaTime", "dt是时间增量,smoothDt是平滑时间\nx = dt\ny = 1/dt\nz = smoothDt\nz = 1/smoothDt");

            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawTitle("Lighting(ForwardBase & ForwardAdd)");
                    ShaderReferenceUtil.DrawOneContent("_LightColor0", "主平行灯的颜色值,rgb = 颜色x亮度; a = 亮度");
                    ShaderReferenceUtil.DrawOneContent("_WorldSpaceLightPos0", "平行灯:\t(xyz=位置,z=0)),已归一化\n其它类型灯:\t(xyz=位置,z=1)");
                    ShaderReferenceUtil.DrawOneContent("unity_WorldToLight", "从世界空间转换到灯光空间下，等同于旧版的_LightMatrix0.");
                    //ShaderReferenceUtil.DrawOneContent("unity_4LightPosX0,unity_4LightPosY0,unity_4LightPosZ0", "");
                    //ShaderReferenceUtil.DrawOneContent("unity_4LightAtten0", "");
                    //ShaderReferenceUtil.DrawOneContent("unity_LightColor", "");
                    //ShaderReferenceUtil.DrawOneContent("unity_WorldToShadow", ""); 
                    ShaderReferenceUtil.DrawTitle("Fog and Ambient");
                    ShaderReferenceUtil.DrawOneContent("unity_AmbientSky", "环境光（Gradient）中的Sky Color.");
                    ShaderReferenceUtil.DrawOneContent("unity_AmbientEquator", "环境光（Gradient）中的Equator Color.");
                    ShaderReferenceUtil.DrawOneContent("unity_AmbientGround", "环境光（Gradient）中的Ground Color.");
                    ShaderReferenceUtil.DrawOneContent("UNITY_LIGHTMODEL_AMBIENT", "环境光(Color)中的颜色，等同于环境光（Gradient）中的Sky Color.");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    break;
            }

            //ShaderReferenceUtil.DrawOneContent("unity_FogColor", "");
            //ShaderReferenceUtil.DrawOneContent("unity_FogParams", "");

            //ShaderReferenceUtil.DrawTitle("Various");
            //ShaderReferenceUtil.DrawOneContent("unity_LODFade", "");
            //ShaderReferenceUtil.DrawOneContent("_TextureSampleAdd", "");

            ShaderReferenceUtil.DrawTitle("GPU Instancing");
            ShaderReferenceUtil.DrawOneContent("实现步骤", "用于对多个对象(网格一样，材质一样，但是材质属性不一样)合批,单个合批最大上限为511个对象.\n" + "1.#pragma multi_compile_instancing 添加此指令后会使材质面板上曝露Instaning开关,同时会生成相应的Instancing变体(INSTANCING_ON).\n" + "2.UNITY_VERTEX_INPUT_INSTANCE_ID 在顶点着色器的输入(appdata)和输出(v2f可选)中添加(uint instanceID : SV_InstanceID).\n" + "3.构建需要实例化的额外数据:\n" + "\t#ifdef UNITY_INSTANCING_ENABLED\n" + "\tUNITY_INSTANCING_BUFFER_START(prop自定义名字)\n" + "\tUNITY_DEFINE_INSTANCED_PROP(vector, _BaseColor)\n" + "\tUNITY_INSTANCING_BUFFER_END(prop自定义名字)\n" + "\t#endif\n" + "4.UNITY_SETUP_INSTANCE_ID(v); 放在顶点着色器/片断着色器(可选)中最开始的地方,这样才能访问到全局变量unity_InstanceID.\n" + "5.UNITY_TRANSFER_INSTANCE_ID(v, o); 当需要将实例化ID传到片断着色器时,在顶点着色器中添加.\n" + "6.UNITY_ACCESS_INSTANCED_PROP(arrayName, propName) 在片断着色器中访问具体的实例化变量.\n");
            ShaderReferenceUtil.DrawOneContent("Instancing选项", "对GPU Instancing进行一些设置.\n" + "• #pragma instancing_options forcemaxcount:batchSize 强制设置单个批次内Instancing的最大数量,最大值和默认值是511.\n" + "• #pragma instancing_options maxcount:batchSize 设置单个批次内Instancing的最大数量,仅Vulkan, Xbox One和Switch有效.");

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif