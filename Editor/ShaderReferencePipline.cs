#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferencePipline : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("应用程序阶段（ Application Stage）");
            ShaderReferenceUtil.DrawOneContent("0.Application Stage", "此阶段一般由CPU将需要在屏幕上绘制的几何体、摄像机位置、光照纹理等输出到管线的几何阶段.");

            ShaderReferenceUtil.DrawTitle("几何阶段（ Geometry Stage）");
            ShaderReferenceUtil.DrawOneContent("1.模型和视图变换（Model and View Transform）", "模型和视图变换阶段分为模型变换和视图变换.\n模型变换的目的是将模型从本地空间变换到世界空间当中，而视图变换的目的是将摄像机放置于坐标原点（以使裁剪和投影操作更简单高效），将模型从世界空间变换到相机空间(观察空间)，以便后续步骤的操作。");
            ShaderReferenceUtil.DrawOneContent("2.顶点着色（Vertex Shading）", "顶点着色阶段的目的在于确定模型上顶点处的光照效果,其输出结果（颜色、向量、纹理坐标等）会被发送到光栅化阶段以进行插值操作。");
            ShaderReferenceUtil.DrawOneContent("3.几何、曲面细分着色器", "【可选项】分为几何着色器(Geometry Shader)和曲面细分着色器(Tessellation Shader)，主要是对顶点进行增加与删除修改等操作.");
            ShaderReferenceUtil.DrawOneContent("4.投影（Projection）", "投影阶段分为正交投影与透视投影,将上面的观察空间变换到齐次裁剪空间。");
            ShaderReferenceUtil.DrawOneContent("5.裁剪（Clipping）", "齐次裁剪空间会通过透视除法变换到归一化的设备坐标NDC中,然后再根据图元在视体的位置分为三种裁剪情况：\n1.当图元完全位于视体内部，那么它可以直接进行下一个阶段。\n2.当图元完全位于视体外部，则不会进入下一个阶段，直接丢弃。\n3.当图元部分位于视体内部，则需要对位于视体内的图元进行裁剪处理。\n最终的目的就是对部分位于视体内部的图元进行裁剪操作，以使处于视体外部不需要渲染的图元进行裁剪丢弃。");
            ShaderReferenceUtil.DrawOneContent("6.屏幕映射（Screen Mapping）", "屏幕映射阶段的主要目的，是将之前步骤得到的坐标映射到对应的屏幕坐标系上。");

            ShaderReferenceUtil.DrawTitle("光栅化阶段（Rasterizer Stage）");
            ShaderReferenceUtil.DrawOneContent("7.三角形设定（Triangle Setup）", "此阶段主要是将从几何阶段得到的一个个顶点通过计算来得到一个个三角形网格。");
            ShaderReferenceUtil.DrawOneContent("8.三角形遍历（Triangle Traversal）", "此阶段将进行逐像素遍历检查操作，以检查出该像素是否被上一步得到的三角形所覆盖，这个查找过程被称为三角形遍历.");
            ShaderReferenceUtil.DrawOneContent("9.像素着色(Pixel Shading)", "对应于ShaderLab中的frag函数,主要目的是定义像素的最终输出颜色.");
            ShaderReferenceUtil.DrawOneContent("10.混合（Merging）", "主要任务是合成当前储存于缓冲器中的由之前的像素着色阶段产生的片段颜色。此阶段还负责可见性问题（深度测试、模版测试等）的处理.");

            ShaderReferenceUtil.DrawTitle("Shader Lab");
            ShaderReferenceUtil.DrawOneContent("1.appdata", "将应用程序阶段的内容传递到顶点着色器中.");
            ShaderReferenceUtil.DrawOneContent("2.vertex(顶点着色器)", "本地空间>(本地到世界空间矩阵)>世界空间>(世界到观察空间矩阵)>观察空间>(投影矩阵)>齐次裁剪空间");
            ShaderReferenceUtil.DrawOneContent("3.透视除法", "齐次裁剪空间作透视除法(clip.xyzw/clip.w)，变换到归一化设备坐标NDC.");
            ShaderReferenceUtil.DrawOneContent("4.视口变换", "从NDC坐标变换到屏幕坐标.");
            ShaderReferenceUtil.DrawOneContent("5.fragment(片断着色器)", "用从顶点着色器的输出来当输入进行逐片断的颜色计算并输出.");
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif