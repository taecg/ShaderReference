/**
 * @file         ShaderReferenceSubstancePainter.cs
 * @author       taecg
 * @created      2022-07-19
 * @updated      2022-08-17
 *
 * @brief        SubstancePainter中的shader语法
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceSubstancePainter : EditorWindow
    {
        #region [数据成员]
        private Vector2 scrollPos;
        #endregion

        #region [绘制界面]
        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("参考网址");
            ShaderReferenceUtil.DrawOneContent("URL", "https://substance3d.adobe.com/documentation/spdoc/shader-api-89686018.html");
            ShaderReferenceUtil.DrawTitle("材质参数");
            ShaderReferenceUtil.DrawOneContent("颜色(RGB)", "//: param custom {\"default\":[1,0.9568,0.8392],\"label\":\"灯光颜色(RGB)\",\"widget\":\"color\",\"group\":\"灯光\",\"description\":\"tooltip在这里\"}\n" +
            "uniform vec3 _LightColor;\n" +
            "uniform vec4 _BaseColor;(当为4维颜色时,alpha会变成滑条)");
            ShaderReferenceUtil.DrawOneContent("整型", "//: param custom { \"default\": 0, \"label\": \"Int\",\"min\": 0, \"max\": 10,\"step\": 1,\"group\":\"Int\" }\n" +
            "uniform int u_int1;\n" +
            "uniform ivec2 u_int2;\n" +
            "uniform ivec3 u_int3;\n" +
            "uniform ivec4 u_int4;");
            ShaderReferenceUtil.DrawOneContent("浮点值", "//: param custom { \"default\": 1, \"label\": \"Float\", \"min\": 0, \"max\": 2,\"step\": 0.1,\"group\":\"Float\" }\n" +
            "uniform float u_float1;\n" +
            "uniform vec2 u_float2;\n" +
            "uniform vec3 u_float3;\n" +
            "uniform vec4 u_float4;");
            ShaderReferenceUtil.DrawOneContent("开关", "//: param custom { \"default\": false, \"label\": \"Boolean\",\"group\":\"Toogle\" }\n" +
            "uniform bool u_bool;");
            ShaderReferenceUtil.DrawOneContent("枚举", "//: param custom {\n" +
            "//:   \"default\": -1,\n" +
            "//:   \"label\": \"Combobox\",\n" +
            "//:   \"widget\": \"combobox\",\n" +
            "//:   \"values\": {\n" +
            "//:     \"Value -1\": -1,\n" +
            "//:     \"Value 0\": 0,\n" +
            "//:     \"Value 10\": 10\n" +
            "//:   },\n" +
            "//:   \"group\":\"Enum\"\n" +
            "//: }\n" +
            "uniform int u_combobox;");
            ShaderReferenceUtil.DrawOneContent("自定义纹理", "//: param custom { \"default\": \"\", \"default_color\": [1.0, 1.0, 1.0, 1.0], \"label\": \"Texture\",\"usage\": \"texture\",\"group\":\"Texture\" }\n" +
            "//: param custom { \"default\": \"\", \"label\": \"Texture\",\"usage\": \"environment\",\"group\":\"Texture\" }\n" +
            "uniform sampler2D u_sampler1;\n" +
            "vec4 tex = texture(u_sampler1, inputs.tex_coord);");
            ShaderReferenceUtil.DrawOneContent("内置通道图", "//: param auto channel_basecolor\n" +
            "//: param auto channel_ambientocclusion\n" +
            "//: param auto channel_anisotropyangle\n" +
            "//: param auto channel_anisotropylevel\n" +
            "//: param auto channel_blendingmask\n" +
            "//: param auto channel_diffuse\n" +
            "//: param auto channel_displacement\n" +
            "//: param auto channel_emissive\n" +
            "//: param auto channel_glossiness\n" +
            "//: param auto channel_height\n" +
            "//: param auto channel_ior\n" +
            "//: param auto channel_metallic\n" +
            "//: param auto channel_normal\n" +
            "//: param auto channel_opacity\n" +
            "//: param auto channel_reflection\n" +
            "//: param auto channel_roughness\n" +
            "//: param auto channel_scattering\n" +
            "//: param auto channel_specular\n" +
            "//: param auto channel_specularlevel\n" +
            "//: param auto channel_transmissive\n" +
            "uniform SamplerSparse channel_tex;\n" +
            "vec4 tex = textureSparse(channel_basecolor,i.sparse_coord);");
            ShaderReferenceUtil.DrawOneContent("用户自定义通道图", "//: param auto channel_user0\n" +
            "//: param auto channel_user1\n" +
            "//: param auto channel_user2\n" +
            "//: param auto channel_user3\n" +
            "//: param auto channel_user4\n" +
            "//: param auto channel_user5\n" +
            "//: param auto channel_user6\n" +
            "//: param auto channel_user7\n" +
            "uniform SamplerSparse channel_userTex;");
            ShaderReferenceUtil.DrawOneContent("模型纹理", "//: param auto texture_ambientocclusion (AO)\n" +
            "//: param auto texture_curvature (曲率)\n" +
            "//: param auto texture_id (ID图)\n" +
            "//: param auto texture_normal (切线空间下的法线纹理)\n" +
            "//: param auto texture_normal_ws (世界空间下的法线纹理)\n" +
            "//: param auto texture_position (世界空间下的位置纹理)\n" +
            "//: param auto texture_thickness (厚度纹理)\n" +
            "uniform sampler2D u_meshTexture;");
            ShaderReferenceUtil.DrawTitle("片断输出");
            ShaderReferenceUtil.DrawOneContent("最终输出(emissiveColorOutput + albedoOutput * diffuseShadingOutput + specularShadingOutput)", "void alphaOutput(1);\n" +
            "diffuseShadingOutput(vec3(0,0,0));\n" +
            "specularShadingOutput(vec3(0,0,0));\n" +
            "emissiveColorOutput(vec3(0,0,0));\n" +
            "albedoOutput(vec3(1,1,1));\n" +
            "sssCoefficientsOutput(vec4(0,0,0,0));");
            EditorGUILayout.EndScrollView();
        }
        #endregion
    }
}
#endif