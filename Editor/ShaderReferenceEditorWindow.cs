#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace taecg.tools.shaderReference
{
    public class ShaderReferenceEditorWindow : EditorWindow
    {
        #region 数据成员

        public enum Pipline
        {
            BuildIn = 0,
            URP = 1,
        }

        //搜索关键字
        private bool isFirst = true;
        private string searchText;
        public static string SEARCH_TEXT;

        public static Pipline mPipeline = Pipline.BuildIn;
        private readonly string[] tabNames = new string[] { "Pipeline", "Properties", "Semantics", "Tags", "Render State", "Compile Directives", "Transformations", "Other", "BuildIn Variables", "Predefined Macros", "Platform Differences", "Math", "Lighting", "Miscellaneous", "GLSL", "ComputerShader", "SubstancePainter", "Experience", "About" };
        private int selectedTabID;
        private ShaderReferencePipline pipeline;
        private ShaderReferenceProperties properties;
        private ShaderReferenceSemantics semantics;
        private ShaderReferenceTags tags;
        private ShaderReferenceRenderState renderState;
        private ShaderReferencePragma pragma;
        private ShaderReferenceOther other;
        private ShaderReferenceBuildInVariables buildInVariables;
        private ShaderReferenceTransformation transformation;
        private ShaderReferencePredefinedMacros predefinedMacros;
        private ShaderReferencePlatformDifferences platformDifferences;
        private ShaderReferenceLighting lighting;
        private ShaderReferenceMath math;
        private ShaderReferenceMiscellaneous miscellaneous;
        private ShaderReferenceGLSL glsl;
        private ShaderReferenceComputerShader computerShader;
        private ShaderReferenceSubstancePainter substancePainter;
        private ShaderReferenceExperience experience;
        private ShaderReferenceAbout about;
        private ShaderReferenceSearch search;

        #endregion

        #region 编缉器入口

        [MenuItem("Window/Shader参考大全... #R", false, -11)]
        public static void Open()
        {
            if (HasOpenInstances<ShaderReferenceEditorWindow>())
            {
                GetWindow<ShaderReferenceEditorWindow>().Close();
            }
            else
            {
                ShaderReferenceEditorWindow window = GetWindow<ShaderReferenceEditorWindow>();
                // GUIContent content = EditorGUIUtility.IconContent("d_Shader Icon");
                GUIContent content = new GUIContent();
                content.text = "Shader参考大全";
                window.titleContent = content;
                window.Show();
            }
        }

        #endregion

        #region OnEnable/OnDisable

        void OnEnable()
        {
            mPipeline = (Pipline)(EditorPrefs.HasKey("taecg_ShaderReferencemPipline") ? EditorPrefs.GetInt("taecg_ShaderReferencemPipline") : 0);
            selectedTabID = EditorPrefs.HasKey("taecg_ShaderReferenceSelectedTabID") ? EditorPrefs.GetInt("taecg_ShaderReferenceSelectedTabID") : 0;

            pipeline = ScriptableObject.CreateInstance<ShaderReferencePipline>();
            properties = ScriptableObject.CreateInstance<ShaderReferenceProperties>();
            semantics = ScriptableObject.CreateInstance<ShaderReferenceSemantics>();
            tags = ScriptableObject.CreateInstance<ShaderReferenceTags>();
            renderState = ScriptableObject.CreateInstance<ShaderReferenceRenderState>();
            pragma = ScriptableObject.CreateInstance<ShaderReferencePragma>();
            other = ScriptableObject.CreateInstance<ShaderReferenceOther>();
            buildInVariables = ScriptableObject.CreateInstance<ShaderReferenceBuildInVariables>();
            transformation = ScriptableObject.CreateInstance<ShaderReferenceTransformation>();
            predefinedMacros = ScriptableObject.CreateInstance<ShaderReferencePredefinedMacros>();
            platformDifferences = ScriptableObject.CreateInstance<ShaderReferencePlatformDifferences>();
            lighting = ScriptableObject.CreateInstance<ShaderReferenceLighting>();
            math = ScriptableObject.CreateInstance<ShaderReferenceMath>();
            miscellaneous = ScriptableObject.CreateInstance<ShaderReferenceMiscellaneous>();
            glsl = ScriptableObject.CreateInstance<ShaderReferenceGLSL>();
            computerShader = ScriptableObject.CreateInstance<ShaderReferenceComputerShader>();
            substancePainter = ScriptableObject.CreateInstance<ShaderReferenceSubstancePainter>();
            experience = ScriptableObject.CreateInstance<ShaderReferenceExperience>();
            about = ScriptableObject.CreateInstance<ShaderReferenceAbout>();
            search = ScriptableObject.CreateInstance<ShaderReferenceSearch>();

            ShaderReferenceUtil.SearchDic.Clear();
        }

        void OnDisable()
        {
            EditorPrefs.SetInt("taecg_ShaderReferencemPipline", (int)mPipeline);
            EditorPrefs.SetInt("taecg_ShaderReferenceSelectedTabID", selectedTabID);
        }

        #endregion

        #region OnGUI

        void OnGUI()
        {
            DrawSearchGUI();
            //如果还没有搜索,则绘制侧边栏与显示区域
            if (string.IsNullOrEmpty(searchText))
            {
                EditorGUILayout.BeginHorizontal();

                #region [左侧边栏]

                float _width = 150;
                float _heigth = position.height - 30;

                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MaxWidth(_width), GUILayout.MinHeight(_heigth));
                //渲染管线选择
                mPipeline = (Pipline)EditorGUILayout.EnumPopup(mPipeline);

                //功能选择
                selectedTabID = GUILayout.SelectionGrid(selectedTabID, tabNames, 1);

                EditorGUILayout.EndVertical();

                #endregion

                #region [绘制右侧内容区]

                EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.MinWidth(position.width - _width), GUILayout.MinHeight(_heigth));
                DrawMainUI(selectedTabID);
                EditorGUILayout.EndVertical();

                #endregion

                EditorGUILayout.EndVertical();
            }
            else
            {
                #region [收集信息]

                //- -！由于之前没有考虑搜索功能，所以只能在初始化时再全部画一次，以便读取所有的数据。
                GUI.BeginClip(new Rect(0, 0, 0, 0));
                if (isFirst)
                {
                    CollectionInformation();
                    isFirst = false;
                }

                GUI.EndClip();

                #endregion

                search.DrawMainGUI();
            }

            Repaint();
        }

        #endregion

        #region [搜索功能相关]

        void DrawSearchGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(100f);
            searchText = EditorGUILayout.TextField("", searchText, "SearchTextField");
            if (GUILayout.Button("", "SearchCancelButton", GUILayout.Width(18f)))
            {
                GUIUtility.keyboardControl = 0;
                searchText = string.Empty;
            }

            SEARCH_TEXT = searchText;

            GUILayout.Space(100f);
            GUILayout.EndHorizontal();
        }

        #endregion

        #region [绘制分级UI]

        void DrawMainUI(int selectedTabID)
        {
            switch (selectedTabID)
            {
                case 0:
                    pipeline.DrawMainGUI();
                    break;
                case 1:
                    properties.DrawMainGUI();
                    break;
                case 2:
                    semantics.DrawMainGUI();
                    break;
                case 3:
                    tags.DrawMainGUI();
                    break;
                case 4:
                    renderState.DrawMainGUI();
                    break;
                case 5:
                    pragma.DrawMainGUI();
                    break;
                case 6:
                    transformation.DrawMainGUI();
                    break;
                case 7:
                    other.DrawMainGUI();
                    break;
                case 8:
                    buildInVariables.DrawMainGUI();
                    break;
                case 9:
                    predefinedMacros.DrawMainGUI();
                    break;
                case 10:
                    platformDifferences.DrawMainGUI();
                    break;
                case 11:
                    math.DrawMainGUI();
                    break;
                case 12:
                    lighting.DrawMainGUI();
                    break;
                case 13:
                    miscellaneous.DrawMainGUI();
                    break;
                case 14:
                    glsl.DrawMainGUI();
                    break;
                case 15:
                    computerShader.DrawMainGUI();
                    break;
                case 16:
                    substancePainter.DrawMainGUI();
                    break;
                case 17:
                    experience.DrawMainGUI();
                    break;
                case 18:
                    about.DrawMainGUI();
                    break;
            }
        }

        //收集所有信息用于搜索
        void CollectionInformation()
        {
            pipeline.DrawMainGUI();
            properties.DrawMainGUI();
            semantics.DrawMainGUI();
            tags.DrawMainGUI();
            renderState.DrawMainGUI();
            pragma.DrawMainGUI();
            other.DrawMainGUI();
            buildInVariables.DrawMainGUI();
            transformation.DrawMainGUI();
            predefinedMacros.DrawMainGUI();
            platformDifferences.DrawMainGUI();
            math.DrawMainGUI();
            lighting.DrawMainGUI();
            miscellaneous.DrawMainGUI();
            glsl.DrawMainGUI();
            computerShader.DrawMainGUI();
            substancePainter.DrawMainGUI();
            about.DrawMainGUI();
        }

        #endregion
    }
}
#endif