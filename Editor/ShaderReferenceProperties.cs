/**
 * @file         ShaderReferenceProperties.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2018-11-10
 * @updated      2022-07-13
 *
 * @brief        Properties（属性相关）
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceProperties : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("Properties");
            ShaderReferenceUtil.DrawOneContent("_Int(\"Int\", Int) = 1", "类型:整型\nCg/HLSL:int\n取决于在Cg/HLSL中是用float还是int来声明的，如果定义为float则实际使用的就是浮点数，字义为int会被识别为int类型（去小数点直接取整）");

            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("_Float (\"Float\", Float ) = 0", "类型:浮点数值\nCg/HLSL:可根据需要定义不同的浮点精度\n" + "float  32位精度,常用于世界坐标位置以及UV坐标\n" + "half  16位精度,范围[-6W,6W],常用于本地坐标位置,方向等\n" + "fixed  11位精度,范围[-2,2],1/265的小数精度,常用于纹理与颜色等低精度的情况");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("_Float (\"Float\", Float ) = 0", "任何需要显式精度的函数，使用float或half限定符，当函数可以同时支持两者时，则使用real.");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("_Slider (\"Slider\", Range(0, 1)) = 0", "类型:数值滑动条\n本身还是Float类型，只是通过Range(min,max)来控制滑动条的最小值与最大值");
            ShaderReferenceUtil.DrawOneContent("_Color(\"Color\", Color) = (1,1,1,1)", "类型:颜色属性\nCg/HLSL:float4/half4/fixed4");
            ShaderReferenceUtil.DrawOneContent("_Vector (\"Vector\", Vector) = (0,0,0,0)", "类型:四维向量\n在Properties中无法定义二维或者三维向量，只能定义四维向量");
            ShaderReferenceUtil.DrawOneContent("_MainTex (\"Texture\", 2D) = \"white\" {}", "类型:2D纹理\nCg/HLSL:sampler2D/sampler2D_half/sampler2D_float\n默认值有white、black、gray、bump以及空，空就是white");
            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("_MainTexArry (\"TextureArry\", 2DArray) = \"white\" {}", "类型:2D纹理数组\n" + "UNITY_DECLARE_TEX2DARRAY(_MainTexArry);\n" + "默认值有white、black、gray、bump以及空，空就是white\n" + "仅支持DX10、OpenGL3.0、Metal及以上版本");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("_MainTexArray (\"TextureArray\", 2DArray) = \"white\" {}", "类型:2D纹理数组\n" + "TEXTURE2D_ARRAY(_MainTexArray);  SAMPLER(sampler_MainTexArray);\n" + "SAMPLE_TEXTURE2D_ARRAY(_MainTexArry, sampler_MainTexArry, i.uv,_Index);\n" + "默认值有white、black、gray、bump以及空，空就是white\n" + "仅支持DX10、OpenGL3.0、Metal及以上版本");
                    break;
            }

            ShaderReferenceUtil.DrawOneContent("_MainTex3D (\"Texture\", 3D) = \"white\" {}", "类型:3D纹理\nCg/HLSL:sampler3D/sampler3D_half/sampler3D_float");
            ShaderReferenceUtil.DrawOneContent("_MainCube (\"Texture\", Cube) = \"white\" {}", "类型:立方体纹理\nCg/HLSL:samplerCUBE/samplerCUBE_half/samplerCUBE_float");
            ShaderReferenceUtil.DrawOneContent("_MainTex (\"Texture\", Any) = \"white\" {}", "类型:通用纹理\n支持2D、3D、Cube.");

            ShaderReferenceUtil.DrawTitle("Attributes");
            ShaderReferenceUtil.DrawOneContent("[Header(xxx)]", "用于在材质面板中当前属性的上方显示标题xxx，注意只支持英文、数字、空格以及下划线");
            ShaderReferenceUtil.DrawOneContent("[HideInInspector]", "在材质面板中隐藏此条属性，在不希望暴露某条属性时可以快速将其隐藏");
            ShaderReferenceUtil.DrawOneContent("[Space(n)]", "使材质面板属性之前有间隔，n为间隔的数值大小");
            ShaderReferenceUtil.DrawOneContent("[HDR]", "标记为属性为高动态范围");
            ShaderReferenceUtil.DrawOneContent("[PowerSlider(3)]", "滑条曲率,必须加在range属性前面，用于控制滑动的数值比例");
            ShaderReferenceUtil.DrawOneContent("[IntRange]", "必须使用在Range属性之上，以使在材质面板中滑动时只能生成整数");
            ShaderReferenceUtil.DrawOneContent("[Toggle]", "开关,加在数值类型前,可使材质面板中的数值变成开关，0是关，1是开");
            ShaderReferenceUtil.DrawOneContent("[ToggleOff]", "与Toggle相当，0是开，1是关");
            ShaderReferenceUtil.DrawOneContent("[Enum(Off, 0, On, 1)]", "数值枚举,可直接在cg中使用此关键字来替代数字,最多可定义7个，超出后无法以下拉列表框的形式展现.");
            ShaderReferenceUtil.DrawOneContent("[KeywordEnum (Enum0, Enum1, Enum2, Enum3, Enum4, Enum5, Enum6, Enum7, Enum8)]", "关键字枚举,需要#pragma multi_compile _XXX_ENUM0 _XXX_ENUM1 ...来依次声明变体关键字,XXX为这条属性中声明的变量名,在cg/hlsl中可以通过#if #elif #endif分别做判断.");
            ShaderReferenceUtil.DrawOneContent("[Enum (UnityEngine.Rendering.CullMode)]", "内置枚举,可在Enum()内直接调用Unity内部的枚举.");
            ShaderReferenceUtil.DrawOneContent("[NoScaleOffset]", "只能加在纹理属性前，使其隐藏材质面板中的Tiling和Offset");
            ShaderReferenceUtil.DrawOneContent("[Normal]", "只能加在纹理属性前，标记此纹理是用来接收法线贴图的，当用户指定了非法线的纹理时会在材质面板上进行警告提示");
            ShaderReferenceUtil.DrawOneContent("[Gamma]", "Float和Vector属性默认情况下不会进行颜色空间转换，可以通过添加[Gamma]来指明此属性为sRGB值");
            ShaderReferenceUtil.DrawOneContent("[PerRendererData]", "标记当前属性将以材质属性块的形式来自于每个渲染器数据");
            ShaderReferenceUtil.DrawOneContent("[MainTexture]", "标记当前纹理为主纹理，便于C#通过Material.mainTexture调用");
            ShaderReferenceUtil.DrawOneContent("[MainColor]", "标记当前颜色为主颜色，便于C#通过Material.color调用");
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif