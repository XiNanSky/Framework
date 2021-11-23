///* * * * * * * * * * * * * * * * * * * * * * * *
//* Copyright(C) 2020 by XN
//* All rights reserved. 
//* FileName:         Editors.ToolWindow
//* Author:           XiNan 
//* Version:          0.1 
//* UnityVersion:     2019.2.18f1 
//* Date:             2020-05-21
//* Time:             00:00:42
//* E-Mail:           1398581458@qq.com
//* Description:        
//* History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.Editor.ToolWindow
//{
//    using Framework.Editor;
//    using Framework.EditorMnager;
//    using Framework.Kit;
//    using Framework.Tool;
//    using UnityEditor;
//    using UnityEngine;

//    public class EditorManagerEditor : BaseWindowEditor
//    {
//        private static EditorWindow windows;

//        private EditorBean bean;

//        [MenuItem("ETool/Windows/Editor Manager %#g")]
//        protected static void ShowWindow()
//        {
//            windows = windows ?? GetWindow<EditorManagerEditor>();
//            windows.Show();
//        }

//        public EditorManagerEditor()
//        {
//            titleContent = new GUIContent("Editor Manager");
//            minSize = new Vector2(300, 300);
//        }


//        private bool SystemOF, SystemInfoOF;

//        private bool ScreenOF, ScreenInfoOF;

//        protected override void onGUI()
//        {
//            if (bean == null) { ETool.AC.ButtonRepeat(load, "Load", GStyleTool.GetName("button")); return; }      /* 加载 */

//            vector = ETool.BE.ScrollView(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("开发者");
//                    bean.Author = ETool.AC.FieldText(bean.Author);
//                });

//                ETool.BE.Vertical(() => { SystemOF = ETool.BE.FoldoutHeaderGroup(System_Level_1, SystemOF, "Ediotr System"); }, GStyleTool.content);

//                ETool.BE.Vertical(() => { SystemInfoOF = ETool.BE.FoldoutHeaderGroup(SystemInfo_Level_1, SystemInfoOF, "Ediotr System Info"); }, GStyleTool.content);

//                ETool.BE.Vertical(() => { ScreenOF = ETool.BE.FoldoutHeaderGroup(Screen_Level_1, ScreenOF, "Ediotr Screen"); }, GStyleTool.content);

//                ETool.BE.Vertical(() => { ScreenInfoOF = ETool.BE.FoldoutHeaderGroup(ScreenInfo_Level_1, ScreenInfoOF, "Ediotr Screen Info"); }, GStyleTool.content);

//            }, vector, GStyleTool.scrollView);

//            ETool.BE.Vertical(() =>   /* 保存 */
//            {
//                ETool.AC.ButtonRepeat(save, "Save", GStyleTool.GetName("button"));
//            });
//        }

//        private void System_Level_1()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    bean.DeveloperMode = ETool.AC.Toggle("开发者模式", bean.DeveloperMode);
//                });
//            });
//        }

//        private void SystemInfo_Level_1()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("运行平台");
//                    ETool.AC.HelpBox(Application.platform.ToString(), MessageType.None);
//                });
//            });
//        }

//        private void Screen_Level_1()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.BE.Vertical(() =>
//                {
//                    ETool.AC.Label("屏幕旋转", GStyleTool.title);

//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("屏幕方向:");
//                        bean.PlayerSettingOrientation = ETool.AC.PopupEnum(bean.PlayerSettingOrientation);
//                    });

//                    bean.AutorotateToLandscapeLeft = ETool.AC.Toggle("向左旋转", bean.AutorotateToLandscapeLeft);
//                    bean.AutorotateToLandscapeRight = ETool.AC.Toggle("向右旋转", bean.AutorotateToLandscapeRight);
//                    bean.AutorotateToPortrait = ETool.AC.Toggle("向上旋转", bean.AutorotateToPortrait);
//                    bean.AutorotateToPortraitUpsideDown = ETool.AC.Toggle("向下旋转", bean.AutorotateToPortraitUpsideDown);

//                });

//                ETool.EA.Space();

//                ETool.BE.Vertical(() =>
//                {
//                    ETool.AC.Label("分辨率", GStyleTool.title);

//#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
//#elif UNITY_EDITOR_WIN || UNITY_EDITOR 
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("屏高");
//                        bean.ScreenHeight = ETool.AC.SliderInt(bean.ScreenHeight, 600, 1080);
//                    });

//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.LabelPrefix("屏宽");
//                        bean.ScreenWidth = ETool.AC.SliderInt(bean.ScreenWidth, 1200, 1920);
//                    });
//#endif
//                    ETool.BE.Horizontal(() =>
//                    {
//                        bean.ScreenFull = ETool.AC.Toggle("全屏", bean.ScreenFull);
//                        ETool.AC.LabelSelectable("全屏模式和窗口模式之间切换", GStyleTool.helpBox, GUILayout.Height(20));
//                    });
//                });

//                ETool.EA.Space();

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("屏幕亮度");
//                    bean.Brightness = ETool.AC.SliderInt(bean.Brightness, 30, 100);
//                });

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("刷新率");
//                    bean.RefreshRate = ETool.AC.PopupEnum(bean.RefreshRate);
//                });

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("睡眠模式");
//                    bean.NeverSleep = ETool.AC.PopupEnum(bean.NeverSleep);
//                });

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("屏幕模式");
//                    bean.ScreenFullMode = ETool.AC.PopupEnum(bean.ScreenFullMode);
//                });

//                ETool.EA.Space();
//            });
//        }

//        private bool screenInfo_resolutions;

//        private void ScreenInfo_Level_1()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("Screen Slef SafeArea");
//                    ETool.AC.HelpBox(string.Concat("W:", Screen.safeArea.width, " H:", Screen.safeArea.height, " X:", Screen.safeArea.x, " Y:", Screen.safeArea.y), MessageType.None);
//                });

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("Screen Resolution");
//                    ETool.AC.HelpBox(string.Concat("W:", Screen.currentResolution.width, " H:", Screen.currentResolution.height), MessageType.None);
//                });

//                ETool.BE.Horizontal(() =>
//                {
//                    ETool.AC.LabelPrefix("Screen DPI");
//                    ETool.AC.HelpBox(Screen.dpi.ToString(), MessageType.None);
//                });

//                ETool.BE.Vertical(() =>
//                {
//                    ETool.BE.Horizontal(() =>
//                    {
//                        screenInfo_resolutions = ETool.AC.Toggle("Full Screen Resolution", screenInfo_resolutions);
//                        ETool.AC.LabelSelectable("Current Display Support", GStyleTool.helpBox, GUILayout.Height(20));
//                    });

//                    if (screenInfo_resolutions && Screen.resolutions.Length != 0)
//                    {
//                        for (int i = 0; i < Screen.resolutions.Length; i++)
//                        {
//                            ETool.BE.Horizontal(() =>
//                            {
//                                ETool.AC.LabelSelectable(string.Concat("NO:", (i + 1)), GStyleTool.helpBox, GUILayout.Height(20));
//                                ETool.AC.LabelSelectable(string.Concat("W:", Screen.resolutions[i].width), GStyleTool.helpBox, GUILayout.Height(20));
//                                ETool.AC.LabelSelectable(string.Concat("H:", Screen.resolutions[i].height), GStyleTool.helpBox, GUILayout.Height(20));
//                                ETool.AC.LabelSelectable(string.Concat("Hz:", Screen.resolutions[i].refreshRate), GStyleTool.helpBox, GUILayout.Height(20));
//                            });
//                        }
//                    }
//                });
//            });
//        }

//        protected override void change()
//        {
//            //ScriptChange.AUTHOR = bean.Author;

//            if (EditorPrefs.GetBool("DeveloperMode") != bean.DeveloperMode)
//                EditorPrefs.SetBool("DeveloperMode", bean.DeveloperMode);

//            PlayerSettings.defaultInterfaceOrientation = (UIOrientation)bean.PlayerSettingOrientation;
//            PlayerSettings.accelerometerFrequency = bean.AccelerometerFrequency; //加速度计更新频率。
//            //web
//            PlayerSettings.defaultWebScreenHeight = bean.ScreenHeight;
//            PlayerSettings.defaultWebScreenWidth = bean.ScreenWidth;

//            PlayerSettings.defaultScreenHeight = bean.ScreenHeight;
//            PlayerSettings.defaultScreenWidth = bean.ScreenWidth;

//            PlayerSettings.fullScreenMode = (UnityEngine.FullScreenMode)bean.ScreenFullMode;
//            PlayerSettings.allowFullscreenSwitch = bean.ScreenFull;

//            Screen.SetResolution(bean.ScreenHeight, bean.ScreenHeight, (UnityEngine.FullScreenMode)bean.ScreenFullMode, (int)bean.RefreshRate);
//            Screen.brightness = bean.Brightness;
//            Screen.fullScreen = bean.ScreenFull;
//        }

//        protected override void load()
//        {
//            bean = new EditorBean();
//            bean.byteRead(new ByteBuffer(FileKit.Read(PathManager.SaveData.Config)));
//        }

//        protected override void save()
//        {
//            if (bean != null)
//            {
//                FileKit.Write(PathManager.SaveData.Config, bean.byteWrite(new ByteBuffer()).ToBytes());
//            }
//        }

//        protected override void close()
//        {
//            bean = null;
//            windows = null;
//        }
//    }
//}