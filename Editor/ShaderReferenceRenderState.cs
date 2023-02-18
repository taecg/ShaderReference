/**
 * @file         ShaderReferenceRenderState.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-12-29
 * @updated      2022-01-25
 *
 * @brief        SubShader中渲染设置
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceRenderState : EditorWindow
    {
        #region 数据成员
        private Vector2 scrollPos;
        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("Cull");
            ShaderReferenceUtil.DrawOneContent("Cull Back | Front | Off", "[Enum(UnityEngine.Rendering.CullMode)]\n" +
            "背面剔除,默认值为Back。\nBack：表示剔除背面，也就是显示正面，这也是最常用的设置。\nFront：表示剔除前面，只显示背面。\nOff:表示关闭剔除，也就是正反面都渲染。");
            ShaderReferenceUtil.DrawTitle("模版测试");
            ShaderReferenceUtil.DrawOneContent("Stencil", "模板缓冲区(StencilBuffer)可以为屏幕上的每个像素点保存一个无符号整数值,这个值的具体意义视程序的具体应用而定.在渲染的过程中,可以用这个值与一个预先设定的参考值相比较,根据比较的结果来决定是否更新相应的像素点的颜色值.这个比较的过程被称为模板测试." +
                "\n将StencilBuffer的值与ReadMask与运算，然后与Ref值进行Comp比较，结果为true时进行Pass操作，否则进行Fail操作，操作值写入StencilBuffer前先与WriteMask与运算." +
                "\n模版缓冲中的默认值为:0" +
                "\n公式：(Ref & ReadMask) Comp (StencilBufferValue & ReadMask)\n\n" +
                "Stencil\n" +
                "{\n" +
                "\n    Ref [_Stencil]" +
                "\n    ReadMask [_StencilReadMask]" +
                "\n    WriteMask [_StencilWriteMask]" +
                "\n    Comp [_StencilComp] ((UnityEngine.Rendering.CompareFunction))" +
                "\n    Pass [_StencilOp] (UnityEngine.Rendering.StencilOp)" +
                "\n    Fail [_Fail]" +
                "\n    ZFail [_ZFail]" +
                "\n}" +
                "\nRef:\t设定的参考值,这个值将用来与模板缓冲中的值进行比较.取值范围位为0-255的整数." +
                "\nReadMask:\tReadMask的值将和Ref的值以及模板缓冲中的值进行按位与（&）操作,取值范围也是0-255的整数,默认值为255(二进制位11111111),即读取的时候不对Ref的值和模板缓冲中的值产生修改,读取的还是原始值." +
                "\nWriteMask:\tWriteMask的值是当写入模板缓冲时进行的按位与操作,取值范围是0-255的整数,默认值也是255,即不做任何修改." +
                "\nComp:\t定义Ref与模板缓冲中的值比较的操作函数,默认值为always." +
                "\nPass:\t当模板测试（和深度测试）通过时,则根据（stencilOperation值）对模板缓冲值进行处理,默认值为keep." +
                "\nFail:\t当模板测试（和深度测试）失败时,则根据（stencilOperation值）对模板缓冲值进行处理，默认值为keep." +
                "\nZFail:\t当模板测试通过而深度测试失败时,则根据（stencilOperation值）对模板缓冲值进行处理，默认值为keep.");
            ShaderReferenceUtil.DrawOneContent("Comp（比较操作）",
                "\nLess：\t相当于“<”操作，即仅当左边<右边，模板测试通过，渲染像素." +
                "\nGreater：\t相当于“>”操作，即仅当左边>右边，模板测试通过，渲染像素." +
                "\nLequal：\t相当于“<=”操作，即仅当左边<=右边，模板测试通过，渲染像素." +
                "\nGequal：\t相当于“>=”操作，即仅当左边>=右边，模板测试通过，渲染像素." +
                "\nEqual：\t相当于“=”操作，即仅当左边=右边，模板测试通过，渲染像素." +
                "\nNotEqual：\t相当于“!=”操作，即仅当左边！=右边，模板测试通过，渲染像素." +
                "\nAlways：\t不管公式两边为何值，模板测试总是通过，渲染像素." +
                "\nNever:\t不管公式两边为何值，模板测试总是失败 ，像素被抛弃.");
            ShaderReferenceUtil.DrawOneContent("模版缓冲区的更新",
                "\nKeep：\t保留当前缓冲中的内容，即stencilBufferValue不变." +
                "\nZero：\t将0写入缓冲，即stencilBufferValue值变为0." +
                "\nReplace：\t将参考值写入缓冲，即将referenceValue赋值给stencilBufferValue." +
                "\nIncrSat：\t将当前模板缓冲值加1，如果stencilBufferValue超过255了，那么保留为255，即不大于255." +
                "\nDecrSat：\t将当前模板缓冲值减1，如果stencilBufferValue超过为0，那么保留为0，即不小于0." +
                "\nNotEqual：\t相当于“!=”操作，即仅当左边！=右边，模板测试通过，渲染像素." +
                "\nInvert：\t将当前模板缓冲值（stencilBufferValue）按位取反." +
                "\nIncrWrap:\t当前缓冲的值加1，如果缓冲值超过255了，那么变成0，（然后继续自增）." +
                "\nDecrWrap:\t当前缓冲的值减1，如果缓冲值已经为0，那么变成255，（然后继续自减）.");

            ShaderReferenceUtil.DrawTitle("深度缓冲");
            ShaderReferenceUtil.DrawOneContent("ZTest (Less | Greater | LEqual | GEqual | Equal | NotEqual | Never | Always)", "深度测试，拿当前像素的深度值与深度缓冲中的深度值进行比较，默认值为LEqual。" +
                "可通过在属性中添加枚举UnityEngine.Rendering.CompareFunction\n" +
                "\nDisabled: 禁用，相当于永远通过。" +
                "\nNever: 于永远不通过。" +
                "\nLess：小于，表示如果当前像素的深度值小于深度缓冲中的深度值，则通过，以下类同。" +
                "\nEqual：等于。" +
                "\nLEqual：小于等于。" +
                "\nGreater：大于。" +
                "\nNotEqual：不等于。" +
                "\nGequal：大于等于。" +
                "\nAlways：永远通过。");
            ShaderReferenceUtil.DrawOneContent("ZTest[unity_GUIZTestMode]", "unity_GUIZTestMode用于UI材质中，此值默认为LEqual,仅当UI中Canvas模式为Overlay时，值为Always.");
            ShaderReferenceUtil.DrawOneContent("ZWrite On | Off", "深度写入，默认值为On。\nOn：向深度缓冲中写入深度值。\nOff：关闭深度写入。");
            ShaderReferenceUtil.DrawOneContent("ZClip True | False", "设置GPU的深度剪辑模式以此来决定如何处理近平面和远平面之外的片元深度.\nFalse表示将GPU的深度剪辑模式设置为Clmap,这对于模板阴影渲染很有用,这意味着当几何体超出远平面时不需要特殊处理，从而减少渲染操作。但是，它可能会导致不正确的Z排序。");
            ShaderReferenceUtil.DrawOneContent("Offset Factor, Units", "深度偏移，offset = (m * factor) + (r * units)，默认值为0,0" +
                "\nm：指多边形的深度斜率（在光栅化阶段计算得出）中的最大值,多边形越是与近裁剪面平行，m值就越接近0。" +
                "\nr：表示能产生在窗口坐标系的深度值中可分辨的差异的最小值，r是由具体实现OpenGL的平台指定的一个常量。" +
                "\n结论：一个大于0的offset会把模型推远，一个小于0的offset会把模型拉近。");

            ShaderReferenceUtil.DrawTitle("颜色遮罩");
            ShaderReferenceUtil.DrawOneContent("ColorMask RGB | A | 0 | R、G、B、A的任意组合", "颜色遮罩，默认值为：RGBA，表示写入RGBA四个通道。");
            ShaderReferenceUtil.DrawTitle("混合");
            ShaderReferenceUtil.DrawOneContent("说明", "源颜色*SrcFactor + 目标颜色*DstFactor" +
                "\n颜色混合，源颜色与目标颜色以给定的公式进行混合出最终的新颜色。" +
                "\n源颜色：当前Shader计算出的颜色。" +
                "\n目标颜色：已经存在颜色缓存中的颜色。默认值为Blend Off,即表示关闭混合。" +
                "\n在混合时可以针对某个RT做混合，比如Blend 3 One One,就是对RenderTarget3做混合。" +
                "\n可在Properties中添加这个实现下拉列表选择:[Enum(UnityEngine.Rendering.BlendMode)]");
            ShaderReferenceUtil.DrawOneContent("Blend SrcFactor DstFactor", "SrcFactor为源颜色因子，DstFactor为目标颜色因子，将两者按Op中指定的操作进行混合。");
            ShaderReferenceUtil.DrawOneContent("Blend SrcFactor DstFactor, SrcFactorA DstFactorA", "对RGB和A通道分别做混合操作.\n对A的混合操作结果可以在FrameDebugger中的渲染目标RT中点选A通道来查看.");
            ShaderReferenceUtil.DrawOneContent("BlendOp Op", "混合时的操作运算符，默认值为Add（加法操作）。");
            ShaderReferenceUtil.DrawOneContent("BlendOp OpColor, OpAlpha", "对RGB和A通道分别指定混合运算符。");
            ShaderReferenceUtil.DrawOneContent("Blend factors", "混合因子" +
                "\nOne：源或目标的完整值" +
                "\nZero：0" +
                "\nSrcColor：源的颜色值" +
                "\nSrcAlpha：源的Alpha值" +
                "\nDstColor：目标的颜色值" +
                "\nDstAlpha：目标的Alpha值" +
                "\nOneMinusSrcColor：1-源颜色得到的值" +
                "\nOneMinusSrcAlpha：1-源Alpha得到的值" +
                "\nOneMinusDstColor：1-目标颜色得到的值" +
                "\nOneMinusDstAlpha：1-目标Alpha得到的值");
            ShaderReferenceUtil.DrawOneContent("Blend Types", "常用的几种混合类型" +
                "\nBlend SrcAlpha OneMinusSrcAlpha// Traditional transparency" +
                "\nBlend One OneMinusSrcAlpha// Premultiplied transparency" +
                "\nBlend One One" +
                "\nBlend OneMinusDstColor One // Soft Additive" +
                "\nBlend DstColor Zero // Multiplicative" +
                "\nBlend DstColor SrcColor // 2x Multiplicative");
            ShaderReferenceUtil.DrawOneContent("Blend operations", "混合操作的具体运算符" +
                "\nAdd：源+目标" +
                "\nSub：源-目标" +
                "\nRevSub：目标-源" +
                "\nMin：源与目标中最小值" +
                "\nMax：源与目标中最大值" +
                "\n以下仅用于DX11.1中" +
                "\nLogicalClear" +
                "\nLogicalSet" +
                "\nLogicalCopy" +
                "\nLogicalCopyInverted" +
                "\nLogicalNoop" +
                "\nLogicalInvert" +
                "\nLogicalAnd" +
                "\nLogicalNand" +
                "\nLogicalOr" +
                "\nLogicalNor" +
                "\nLogicalXor" +
                "\nLogicalEquiv" +
                "\nLogicalAndReverse" +
                "\nLogicalAndInverted" +
                "\nLogicalOrReverse" +
                "\nLogicalOrInverted");

            ShaderReferenceUtil.DrawTitle("其他");
            ShaderReferenceUtil.DrawOneContent("AlphaToMask On | Off", "是否启用GPU上的alpha-to-coverage模式(当开启MSAA时,减少AlphaTest产生的过度锯齿感).");
            ShaderReferenceUtil.DrawOneContent("Conservative True | False", "是否启用保守光栅化(指GPU对被三角形部分覆盖的像素进行光栅化,无论覆盖范围如何,这会导致更多片元着色器的调用).");
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif