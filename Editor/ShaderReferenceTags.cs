/**
 * @file         ShaderReferenceTags.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-12-08
 * @updated      2021-07-13
 *
 * @brief        SubShader中的内容
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceTags : EditorWindow
    {
#region 数据成员
        private Vector2 scrollPos;
#endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("Tags");
            ShaderReferenceUtil.DrawOneContent("Tags { \"TagName1\" = \"Value1\" \"TagName2\" = \"Value2\" }", "Tag的语法结构，通过Tags{}来表示需要添加的标识,大括号内可以添加多组Tag（所以才叫Tags嘛）,名称（TagName）和值（Value）是成对成对出现的，并且全部用字符串表示。");

            switch (ShaderReferenceEditorWindow.mPipline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawTitle("RenderPipeline");
                    ShaderReferenceUtil.DrawOneContent("\"RenderPipeline\" = \"UniversalPipeline\"", "渲染管线标记，对应的管线C#代码UniversalRenderPipeline.cs中的Shader.globalRenderPipeline = UniversalPipeline,LightweightPipeline,只有带有UniversalPipeline或LightweightPipeline的Tag的SubShader才会生效." +
                        "\n主要作用是用于标记当前这个SubShader是属于哪个管线下的.");
                    break;
            }

            ShaderReferenceUtil.DrawTitle("Queue");
            ShaderReferenceUtil.DrawOneContent("Queue", "渲染队列直接影响性能中的重复绘制，合理的队列可极大的提升渲染效率。\n渲染队列数<=2500的对象都被认为是不透明的物体（从前往后渲染），>2500的被认为是半透明物体（从后往前渲染）。\n\"Queue\" = \"Geometry+1\" 可通过在值后加数字的方式来改变队列。");
            ShaderReferenceUtil.DrawOneContent("\"Queue\" = \"Background\"", "值为1000，此队列的对象最先进行渲染。");
            ShaderReferenceUtil.DrawOneContent("\"Queue\" = \"Geometry\"", "默认值，值为2000，通常用于不透明对象，比如场景中的物件与角色等。");
            ShaderReferenceUtil.DrawOneContent("\"Queue\" = \"AlphaTest\"", "值为2450，要么完全透明要么完全不透明，多用于利用贴图来实现边缘透明的效果，也就是美术常说的透贴。");
            ShaderReferenceUtil.DrawOneContent("\"Queue\" = \"Transparent\"", "值为3000，常用于半透明对象，渲染时从后往前进行渲染，建议需要混合的对象放入此队列。");
            ShaderReferenceUtil.DrawOneContent("\"Queue\" = \"Overlay\"", "值为4000,此渲染队列用于叠加效果。最后渲染的东西应该放在这里（例如镜头光晕等）。");

            ShaderReferenceUtil.DrawTitle("LightMode(Pass中)");
            switch (ShaderReferenceEditorWindow.mPipline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("ForwardBase", "用于前向渲染路径，支持环境光、主像素光、球谐光照与烘焙光照。");
                    ShaderReferenceUtil.DrawOneContent("ForwardAdd", "用于前向渲染路径，支持额外的逐像素光照，每盏灯一个Pass。");
                    ShaderReferenceUtil.DrawOneContent("Deferred", "用于延迟渲染。");
                    ShaderReferenceUtil.DrawOneContent("ShadowCaster", "深度渲染与Shadowmap。");
                    ShaderReferenceUtil.DrawOneContent("MotionVectors", "运动矢量。");
                    ShaderReferenceUtil.DrawOneContent("PrepassBase", "旧版延迟渲染，法线与高光处理。");
                    ShaderReferenceUtil.DrawOneContent("PrepassFinal", "旧版延迟渲染，最终颜色。");
                    ShaderReferenceUtil.DrawOneContent("Vertex", "旧版顶点渲染。");
                    ShaderReferenceUtil.DrawOneContent("VertexLMRGBM", "旧版顶点渲染，支持烘焙光照。");
                    ShaderReferenceUtil.DrawOneContent("VertexLM", "旧版顶点渲染，支持烘焙光照，解码为双LDR。");
                    ShaderReferenceUtil.DrawOneContent("Always", "永远渲染。");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"UniversalForward\"", "用于前向渲染路径，所有的灯光都在这一个pass中执行，包括GI、自发光、雾效.(在不需要光照的pass中，可以不写LightMode)");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"SRPDefaultUnlit\"", "用于在额外需要一个pass时使用.");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"ShadowCaster\"", "用于生成阴影贴图ShadowMap(灯光视角下的深度信息)");
                    // ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"UniversalGBuffer\"", "");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"DepthOnly\"", "用于生成相机下的深度信息,当管线资源上开启MSAA时会调用此pass.");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"DepthNormals\"", "用于生成相机下的深度法线信息.");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"Meta\"", "仅在光照烘焙时才会使用此Pass,用于间接光的反弹.");
                    ShaderReferenceUtil.DrawOneContent("\"LightMode\" = \"Universal2D\"", "用于URP使用2D渲染器时绘制物体的Pass，不受光照影响.");
                    ShaderReferenceUtil.DrawOneContent("\"ShaderModel\" = \"2.0\"", "相当于#pragma target 2.0.");
                    break;
            }

            ShaderReferenceUtil.DrawTitle("RenderType");
            ShaderReferenceUtil.DrawOneContent("RenderType", "用来区别这个Shader要渲染的对象是属于什么类别的，你可以想像成是我们把各种不同的物体按我们需要的类型来进行分类一样。\n当然你也可以根据需要改成自定义的名称，这样并不会影响到Shader的效果。\n此Tag多用于摄像机的替换材质功能(Camera.SetReplacementShader)。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"Opaque\"", "大多数不透明着色器。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"Transparent\"", "大多数半透明着色器，比如粒子、特效、字体等。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"TransparentCutout\"", "透贴着色器，多用于植被等。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"Background\"", "多用于天空盒着色器。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"Overlay\"", "GUI、光晕着色器等。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"TreeOpaque\"", "Terrain地形中的树干。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"TreeTransparentCutout\"", "Terrain地形中的树叶。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"TreeBillboard\"", "Terrain地形中的永对面树。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"Grass\"", "Terrain地形中的草。");
            ShaderReferenceUtil.DrawOneContent("\"RenderType\" = \"GrassBillboard\"", "Terrain地形中的永对面草。");

            ShaderReferenceUtil.DrawTitle("DisableBatching");
            ShaderReferenceUtil.DrawOneContent("DisableBatching", "在利用Shader在模型的顶点本地坐标下做一些位移动画，而当此模型有批处理时会出现效果不正确的情况，这是因为批处理会将所有模型转换为世界坐标空间，因此“本地坐标空间”将丢失。");
            ShaderReferenceUtil.DrawOneContent("\"DisableBatching\" = \"True\"", "禁用批处理。");
            ShaderReferenceUtil.DrawOneContent("\"DisableBatching\" = \"False\"", "不禁用批处理。");
            ShaderReferenceUtil.DrawOneContent("\"DisableBatching\" = \"LODFading\"", "仅当LOD激活时禁用批处理。");

            ShaderReferenceUtil.DrawTitle("ForceNoShadowCasting");
            ShaderReferenceUtil.DrawOneContent("ForceNoShadowCasting", "是否强制关闭投射阴影。");
            ShaderReferenceUtil.DrawOneContent("\"ForceNoShadowCasting\" = \"True\"", "强制关闭阴影投射。");
            ShaderReferenceUtil.DrawOneContent("\"ForceNoShadowCasting\" = \"False\"", "不关闭阴影投射。");

            ShaderReferenceUtil.DrawTitle("IgnoreProjector");
            ShaderReferenceUtil.DrawOneContent("IgnoreProjector", "是否忽略Projector投影器的影响。");
            ShaderReferenceUtil.DrawOneContent("\"IgnoreProjector\" = \"True\"", "不受投影器影响。");
            ShaderReferenceUtil.DrawOneContent("\"IgnoreProjector\" = \"False\"", "受投影器影响。");

            ShaderReferenceUtil.DrawTitle("CanUseSpriteAtlas");
            ShaderReferenceUtil.DrawOneContent("CanUseSpriteAtlas", "是否可用于打包图集的精灵。");
            ShaderReferenceUtil.DrawOneContent("\"CanUseSpriteAtlas\" = \"True\"", "支持精灵打包图集。");
            ShaderReferenceUtil.DrawOneContent("\"CanUseSpriteAtlas\" = \"False\"", "不支持精灵打包图集。");

            ShaderReferenceUtil.DrawTitle("PreviewType");
            ShaderReferenceUtil.DrawOneContent("PreviewType", "定义材质面板中的预览的模型显示,默认不写或者不为Plane与Skybox时则为圆球。");
            ShaderReferenceUtil.DrawOneContent("\"PreviewType\" = \"Plane\"", "平面。");
            ShaderReferenceUtil.DrawOneContent("\"PreviewType\" = \"Skybox\"", "天空盒。");

            ShaderReferenceUtil.DrawTitle("PerformanceChecks");
            ShaderReferenceUtil.DrawOneContent("PerformanceChecks", "是否对shader在当前平台进行性能检测，并在材质面板进行警告提示");
            ShaderReferenceUtil.DrawOneContent("\"PerformanceChecks\" = \"True\"", "开启性能检测提示");
            ShaderReferenceUtil.DrawOneContent("\"PerformanceChecks\" = \"False\"", "关闭性能检测提示");

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif