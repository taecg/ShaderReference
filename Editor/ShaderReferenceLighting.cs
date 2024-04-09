/**
 * @file         ShaderReferenceLighting.cs
 * @author       Hongwei Li(taecg@qq.com)
 * @created      2019-10-21
 * @updated      2022-08-03
 *
 * @brief        Shader中的光照相关
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceLighting : EditorWindow
    {
        #region 数据成员

        private Vector2 scrollPos;

        #endregion

        public void DrawMainGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            ShaderReferenceUtil.DrawTitle("光照模型");
            ShaderReferenceUtil.DrawOneContent("Lambertian", "Diffuse = Ambient + Kd * LightColor * max(0,dot(N,L))\n" + "Diffuse:\t最终物体上的漫反射光强.\n" + "Ambient:\t环境光强度，为了简化计算，环境光强采用一个常数表示.\n" + "Kd:\t物体材质对光的反射系数.\n" + "LightColor:\t光源的强度.\n" + "N:\t顶点的单位法线向量.\n" + "L:\t顶点指向光源的单位向量.");
            ShaderReferenceUtil.DrawOneContent("Phong", "Specular = SpecularColor * Ks * pow(max(0,dot(R,V)), Shininess)\n" + "Specular:\t最终物体上的反射高光光强.\n" + "SpecularColor:\t反射光的颜色.\n" + "Ks:\t反射强度系数.\n" + "R:\t反射向量，可使用2 * N * dot(N,L) - L或者reflect (-L,N)获得.\n" + "V:\t观察方向.\n" + "N:\t顶点的单位法线向量.\n" + "L:\t顶点指向光源的单位向量.\n" + "Shininess:\t乘方运算来模拟高光的变化.");
            ShaderReferenceUtil.DrawOneContent("Blinn-Phong", "Specular = SpecularColor * Ks * pow(max(0,dot(N,H)), Shininess)\n" + "Specular:\t最终物体上的反射高光光强.\n" + "SpecularColor:\t反射光的颜色.\n" + "Ks:\t反射强度系数.\n" + "N:\t顶点的单位法线向量.\n" + "H:\t入射光线L与视线V的中间向量，也称为半角向量H = normalize(L+V).\n" + "Shininess:\t乘方运算来模拟高光的变化.");

            switch (ShaderReferenceEditorWindow.mPipeline)
            {
                case ShaderReferenceEditorWindow.Pipline.BuildIn:
                    ShaderReferenceUtil.DrawOneContent("Disney Principled BRDF", "f(l,v) = diffuse + D(h)F(v,h)G(l,v,h)/4cos(n·l)cos(n·v)\n" + "f(l,v):\t双向反射分布函数的最终值,l表示光的方向,v表示视线的方向.\n" + "diffuse:\t漫反射.\n" + "D(h):\t法线分布函数(Normal Distribution Function),描述微面元法线分布的概率,即朝向正确的法线浓度.h为半角向量,表示光的方向与反射方向的半角向量,只有物体的微表面法向m = h时,才会反射到视线中.\nD(h) = roughness^2 / π((n·h)^2(roughness^2-1)+1)^2\n" + "F(v,h):\t菲涅尔方程(Fresnel Equation),描述不同的表面角下表面所反射的光线所占的比率.\nF(v,h) = F0 + (1-F0)(1-(v·h))^5(F0是0度入射角的菲涅尔反射值)\n" + "G(l,v,h):\t几何函数(Geometry Function),描述微平面自成阴影的属性,即微表面法向m = h的并未被遮蔽的表面点的百分比.\n" + "4cos(n·l)cos(n·v):\t校正因子(correctionfactor)作为微观几何的局部空间和整个宏观表面的局部空间之间变换的微平面量的校正.");
                    ShaderReferenceUtil.DrawTitle("法线 NormalMap");
                    ShaderReferenceUtil.DrawOneContent("使用切线空间下的法线", "1.appdata中定义NORMAL与TANGENT语义.\n" + "2.v2f中声明三个变量用于组成成切线空间下的旋转矩阵.\n" + "   float3 tSpace0:TEXCOORD3;\n" + "   float3 tSpace1:TEXCOORD4;\n" + "   float3 tSpace2:TEXCOORD5;\n" + "3.在顶点着色器中执行:\n" + "   half3 worldTangent = UnityObjectToWorldDir(v.tangent);\n" + "   //v.tangent.w:DCC软件中顶点UV值中的V值翻转情况.\n" + "   //unity_WorldTransformParams.w:模型缩放是否有奇数负值. \n" + "   half tangentSign = v.tangent.w * unity_WorldTransformParams.w;\n" + "   half3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;\n" + "   o.tSpace0 = float3(worldTangent.x,worldBinormal.x,worldNormal.x);\n" + "   o.tSpace1 = float3(worldTangent.y,worldBinormal.y,worldNormal.y);\n" + "   o.tSpace2 = float3(worldTangent.z,worldBinormal.z,worldNormal.z);\n" + "4.在片断着色器中计算出世界空间下的法线,然后再拿去进行需要的计算:\n" + "   half3 normalTex = UnpackNormalWithScale(tex2D(_NormalTex,i.uv),scale);\n" + "   half3 worldNormal = half3(dot(i.tSpace0,normalTex),dot(i.tSpace1,normalTex),dot(i.tSpace2,normalTex));");
                    ShaderReferenceUtil.DrawTitle("ShadowMap阴影");
                    ShaderReferenceUtil.DrawOneContent("生成阴影", "添加\"LightMode\" = \"ShadowCaster\"的Pass.\n" + "1.appdata中声明float4 vertex:POSITION;和half3 normal:NORMAL;这是生成阴影所需要的语义.\n" + "2.v2f中添加V2F_SHADOW_CASTER;用于声明需要传送到片断的数据.\n" + "3.在顶点着色器中添加TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)，主要是计算阴影的偏移以解决不正确的Shadow Acne和Peter Panning现象.\n" + "4.在片断着色器中添加SHADOW_CASTER_FRAGMENT(i)");
                    ShaderReferenceUtil.DrawOneContent("采样阴影", "" + "1.在v2f中添加UNITY_SHADOW_COORDS(idx),unity会自动声明一个叫_ShadowCoord的float4变量，用作阴影的采样坐标.\n" + "2.在顶点着色器中添加TRANSFER_SHADOW(o)，用于将上面定义的_ShadowCoord纹理采样坐标变换到相应的屏幕空间纹理坐标，为采样阴影纹理使用.\n" + "3.在片断着色器中添加UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos)，其中atten即存储了采样后的阴影.");

                    ShaderReferenceUtil.DrawTitle("Global Illumination 全局照明GI");
                    ShaderReferenceUtil.DrawOneContent("产生间接光照", "添加\"LightMode\" = \"Meta\"的Pass.\n" + "可参考内置Shader中的Meta Pass.");

                    ShaderReferenceUtil.DrawTitle("Light Probe 光照探针");
                    ShaderReferenceUtil.DrawOneContent("规则01", "当逐像素平行灯标记为Mixed时,同时场景内有LightProbe时,那么当前平行灯的光照值会自动被LightProbe影响,所以不管物体Shader中是否有SH相关的运算,都会受到LightProbe的影响.");
                    ShaderReferenceUtil.DrawOneContent("规则02", "当逐像素平行灯标记为Baked时,同时场景内有LightProbe时,那么需要自行在物体Shader中添加SH相关的运算,才会受到LightProbe的影响.");

                    ShaderReferenceUtil.DrawTitle("Reflection Probe 反射探针");
                    ShaderReferenceUtil.DrawOneContent("反射探针的采样", "反射探针中当前激活的CubeMap存储在unity_SpecCube0当中，必须要用UNITY_SAMPLE_TEXCUBE进行采样，然后需要对其进行解码\n" + "   half3 worldView = normalize (UnityWorldSpaceViewDir (i.worldPos));\n" + "   half3 R = reflect (-worldView, N);\n" + "   half4 cubemap = UNITY_SAMPLE_TEXCUBE (unity_SpecCube0, R);\n" + "   half3 skyColor = DecodeHDR (cubemap, unity_SpecCube0_HDR);");

                    ShaderReferenceUtil.DrawTitle("Fog 雾效");
                    ShaderReferenceUtil.DrawOneContent("unity_FogColor", "内置雾效颜色值");
                    ShaderReferenceUtil.DrawOneContent("方案一:", "常规方案\n" + "1.#pragma multi_compile_fog声明雾效所需要的内置变体:FOG_LINEAR FOG_EXP FOG_EXP2.\n" + "2.UNITY_FOG_COORDS(idx): 声明顶点传入片断中的雾效插值器(fogCoord).\n" + "3.UNITY_TRANSFER_FOG(o,o.vertex): 在顶点着色器中计算雾效采样.\n" + "4.UNITY_APPLY_FOG(i.fogCoord, col): 在片断着色器中进行雾效颜色混合.");
                    ShaderReferenceUtil.DrawOneContent("方案二:", "当在v2f中有定义worldPos时,可以把worldPos.w利用起来做为雾效值.\n" + "1.#pragma multi_compile_fog声明雾效所需要的内置变体:FOG_LINEAR FOG_EXP FOG_EXP2.\n" + "2.UNITY_TRANSFER_FOG_COMBINED_WITH_WORLD_POS(o,o.positionCS): 在顶点着色器中添加，会自动取o.worldPos.z=裁剪空间下的坐标z值.\n" + "3.UNITY_EXTRACT_FOG_FROM_WORLD_POS(i): 在片断着色器中添加.\n" + "4.UNITY_APPLY_FOG(_unity_fogCoord, c): 在片断着色器中进行雾效颜色混合.");
                    break;
                case ShaderReferenceEditorWindow.Pipline.URP:
                    ShaderReferenceUtil.DrawOneContent("BRDF", "f(l,v) = diffuse + D(h)F(v,h)G(l,v,h)/4cos(n·l)cos(n·v)\n" + "f(l,v):\t双向反射分布函数的最终值,l表示光的方向,v表示视线的方向.\n" + "diffuse:\t漫反射.\n" + "D(h):\t法线分布函数(Normal Distribution Function),描述微面元法线分布的概率,即朝向正确的法线浓度.h为半角向量,表示光的方向与反射方向的半角向量,只有物体的微表面法向m = h时,才会反射到视线中.\nD(h) = roughness^2 / π((n·h)^2(roughness^2-1)+1)^2\n" + "F(v,h):\t菲涅尔方程(Fresnel Equation),描述不同的表面角下表面所反射的光线所占的比率.\nF(v,h) = F0 + (1-F0)(1-(v·h))^5(F0是0度入射角的菲涅尔反射值)\n" + "G(l,v,h):\t几何函数(Geometry Function),描述微平面自成阴影的属性,即微表面法向m = h的并未被遮蔽的表面点的百分比.\n" + "4cos(n·l)cos(n·v):\t校正因子(correctionfactor)作为微观几何的局部空间和整个宏观表面的局部空间之间变换的微平面量的校正.");
                    ShaderReferenceUtil.DrawTitle("法线 NormalMap");
                    ShaderReferenceUtil.DrawOneContent("使用切线空间下的法线", "1.Attributes中定义NORMAL与TANGENT语义.\n" + "2.Varyings中声明三个变量用于组成成切线空间下的旋转矩阵.\n" + "   half4 normalWS      :TEXCOORD3;\n" + "   half4 tangentWS     :TEXCOORD4;\n" + "   half4 bitangentWS   :TEXCOORD5;\n" + "3.在顶点着色器中执行:\n" + "   o.normalWS.xyz = TransformObjectToWorldNormal(v.normalOS);\n" + "   o.tangentWS.xyz = TransformObjectToWorldDir(v.tangentOS.xyz);\n" + "   half sign = v.tangentOS.w * GetOddNegativeScale();\n" + "   o.bitangentWS.xyz = cross(o.normalWS, o.tangentWS) * sign;\n" + "4.在片断着色器中计算出世界空间下的法线,然后再拿去进行需要的计算:\n" + "   half3 normalMap = UnpackNormalScale(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv),scale);\n" + "   half3 normalWS = mul(normalMap,half3x3(i.tangentWS.xyz, i.bitangentWS.xyz, i.normalWS.xyz));");
                    ShaderReferenceUtil.DrawOneContent("法线混合", "方法一：\n" + "return normalize(float3(A.rg + B.rg, A.b * B.b));\n" + "方法二：\n" + "float3 t = A.xyz + float3(0.0, 0.0, 1.0);\n" + "float3 u = B.xyz * float3(-1.0, -1.0, 1.0);\n" + "float3 r = (t / t.z) * dot(t, u) - u;\n" + "return r;");

                    ShaderReferenceUtil.DrawTitle("获取主平行灯");
                    ShaderReferenceUtil.DrawOneContent("Light light = GetMainLight(shadowCoord);", "获取主平行灯相关参数:\n" + "light.direction : 主平行灯的方向(_MainLightPosition.xyz),已归一化.\n" + "light.color : 主平行灯的颜色(_MainLightColor.rgb).\n" +
                                                                                                   //"light.distanceAttenuation : 主平行灯的CullingMask,当主灯的CullingMask包含当对象所在的层时返回1，否则返回0(unity_LightData.z).\n" +
                                                                                                   "light.shadowAttenuation : 在此函数下为1(half).");
                    ShaderReferenceUtil.DrawTitle("额外的灯");
                    ShaderReferenceUtil.DrawOneContent("_ADDITIONAL_LIGHTS", "是否额外的灯启用.");
                    ShaderReferenceUtil.DrawOneContent("_ADDITIONAL_LIGHTS_VERTEX", "额外的灯是否采用逐顶点照明.");
                    ShaderReferenceUtil.DrawTitle("投射阴影");
                    ShaderReferenceUtil.DrawOneContent("ShadowCaster", "添加LightMode=ShadowCaster的pass即可渲染进lightmap产生投影.");
                    ShaderReferenceUtil.DrawTitle("接收阴影");
                    ShaderReferenceUtil.DrawOneContent("_MAIN_LIGHT_SHADOWS", "是否开启主灯的阴影(管线设置中主灯的Cast Shadows是否开启)");
                    ShaderReferenceUtil.DrawOneContent("MAIN_LIGHT_CALCULATE_SHADOWS", "当管线上设置了主灯投影，并且当前对象也没有激活_RECEIVE_SHADOWS_OFF时开启");
                    ShaderReferenceUtil.DrawOneContent("_MAIN_LIGHT_SHADOWS_CASCADE", "是否开启主灯的级联阴影(当管线设置中Shadows>Cascades为No Cascades时不激活，否则激活)");
                    ShaderReferenceUtil.DrawOneContent("REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR", "当开启主灯阴影但是没有开启级联阴影时，则激活");
                    ShaderReferenceUtil.DrawOneContent("_MainLightShadowmapSize", "ShadowMap的尺寸\n" + "x=1/width y=1/height z=width w=height");
                    ShaderReferenceUtil.DrawOneContent("_MainLightShadowParams", "Shadow的参数\n" + "x=ShadowStrength(阴影强度) y=(1为软阴影,0为硬阴影)");
                    ShaderReferenceUtil.DrawOneContent("_SHADOWS_SOFT", "管线上Shadows中是否开启软阴影(Soft Shadows)");
                    ShaderReferenceUtil.DrawTitle("Fog 雾效");
                    ShaderReferenceUtil.DrawOneContent("unity_FogColor", "内置雾效颜色值");
                    ShaderReferenceUtil.DrawOneContent("实现方法", "#pragma multi_compile_fog\n" + "float fogCoord  : TEXCOORD1;\t//在Varyings中定义fogCoord\n" + "o.fogCoord = ComputeFogFactor(o.positionCS.z);\t//在顶点着色器中添加\n" + "c.rgb = MixFog(c.rgb, i.fogCoord);\t//在片断着色器中添加");
                    ShaderReferenceUtil.DrawOneContent("Linear", "线性雾公式:(end-z)/(end-start)");
                    ShaderReferenceUtil.DrawOneContent("EXP", "指数雾公式:exp(-density*z)");
                    ShaderReferenceUtil.DrawOneContent("EXP2", "指数2次方雾公式:exp(-(density*z)^2)");
                    ShaderReferenceUtil.DrawTitle("光照烘焙");
                    ShaderReferenceUtil.DrawOneContent("LIGHTMAP_ON", "是否开启光照烘焙\n" + "1.Lighting界面中的Baked Global Illumination勾选\n" + "2.场景烘焙，或者勾选自动烘焙\n" + "3.模型勾选Static中的Contribute GI");
                    ShaderReferenceUtil.DrawOneContent("_MIXED_LIGHTING_SUBTRACTIVE", "\n" + "1.Lighting界面中的Baked Global Illumination勾选\n" + "2.Lighting Mode设置为Subtractive模式\n" + "3.场景烘焙，或者勾选自动烘焙\n" + "4.模型勾选Static中的Contribute GI\n" + "5.灯光必须设置为Mixed模式");
                    ShaderReferenceUtil.DrawTitle("环境色");
                    ShaderReferenceUtil.DrawOneContent("", "1.half3 vertexSH : TEXCOORD?    在v2f中声明\n" + "2.o.vertexSH = SampleSHVertex(世界空间法线);    在顶点着色器中调用\n" + "3.half3 sh = SampleSHPixel(i.vertexSH, 世界空间法线);    在片断着色器中调用");
                    break;
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
#endif