///* * * * * * * * * * * * * * * * * * * * * * * * 
//*Copyright(C) 2021 by xinansky 
//*All rights reserved. 
//*FileName:         Framework.Editor 
//*Author:           XiNan 
//*Version:          0.1 
//*UnityVersion:     2020.3.5f1c1 
//*Date:             2021-07-04 
//*NOWTIME:          14:11:19 
//*Description:        
//*History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.Editor
//{
//    using System.Collections.Generic;
//    using System.IO;
//    using UnityEditor;
//    using UnityEngine;

//    /// <summary> Proto 文件管理 </summary>
//    public class ProtoEditorWindow : BaseWindowEditor
//    {

//        private static new EditorWindow window;

//        private string exepath;
//        private string floderpath;
//        private string outpath;

//        [MenuItem("ETool/Windows/Proto File Manager %#q")]
//        public static void Open()
//        {
//            if (window != null)
//            {
//                window.Show(false);
//                window.Close();
//                window = null;
//            }
//            else
//            {
//                window = window ?? GetWindow<ProtoEditorWindow>(true, "Proto File Manager", true);//创建窗口
//                window.wantsMouseMove = true;
//                window.Show(true);//展示      
//            }
//        }

//        public ProtoEditorWindow()
//        {
//            minSize = new Vector2(600, 800);

//            protofiles = new List<FileInfo>();
//            csfiles = new List<FileInfo>();
//        }

//        private List<FileInfo> protofiles;
//        private List<FileInfo> csfiles;

//        protected override void onEnable()
//        {
//            exepath = PathManager.DataPath.PathCombine("Editor/Framework/Proto/");
//            outpath = PathManager.DataPath.PathCombine("Function/Command/Proto/");
//            floderpath = PathManager.LuaPath.Proto;
//            Refresh();
//        }

//        private void Refresh()
//        {//获取所有.proto文件
//            protofiles.Clear();
//            foreach (var item in FileKit.LoadFiles(floderpath))
//            {
//                if (FileKit.Exists(outpath, item.Name.Replace(".proto", ".cs")))
//                    csfiles.Add(item);
//                else protofiles.Add(item);
//            }
//        }

//        private bool pshow, cshow;

//        protected override void onGUI()
//        {
//            ETool.AC.FieldLabel($"Proto 文件管理", GStyleTool.label);

//            ETool.BE.Horizontal(() =>
//            {
//                if (ETool.AC.Button("打开文件")) { ProcessKit.OpenFolder(floderpath); }
//                if (ETool.AC.Button("拉取文件")) { }
//                if (ETool.AC.Button("刷新列表")) { Refresh(); }
//            });

//            vector = ETool.BE.ScrollView(() =>
//            {
//                ETool.BE.Vertical(() =>
//                {
//                    ETool.AC.FieldLabel($"未生成目录".RichColor("red"), GStyleTool.label);
//                    ETool.BE.Horizontal(() =>
//                    {
//                        if (ETool.AC.Button(!pshow ? "显示列表" : "隐藏列表")) { pshow = !pshow; }
//                        if (ETool.AC.Button("全部生成")) { CreateCSFileAll(protofiles.ToArray()); }
//                    });
//                    if (pshow) for (int i = 0; i < protofiles.Count; i++)
//                        {
//                            PFileInfoView(protofiles[i], i + 1);
//                        }
//                }, GStyleTool.UIContent);

//                ETool.BE.Vertical(() =>
//                {
//                    ETool.AC.FieldLabel($"已生成目录", GStyleTool.label);
//                    ETool.BE.Horizontal(() =>
//                    {
//                        if (ETool.AC.Button(!cshow ? "显示列表" : "隐藏列表")) { cshow = !cshow; }
//                        if (ETool.AC.Button("全部覆盖")) { CreateCSFileAll(csfiles.ToArray()); }
//                    });
//                    if (cshow) for (int i = 0; i < csfiles.Count; i++)
//                        {
//                            CFileInfoView(csfiles[i], i + 1);
//                        }
//                }, GStyleTool.UIContent);
//            }, vector, GStyleTool.scrollView);
//        }

//        private void PFileInfoView(FileInfo info, int num)
//        {
//            ETool.BE.Horizontal(() =>
//            {
//                ETool.AC.Label($"NO.{num}", GUILayout.Width(150));
//                ETool.AC.HelpBox(info.Name, MessageType.None);
//                if (ETool.AC.Button("打开", GUILayout.Width(60))) ProcessKit.OpenTextFile(info.FullName);
//                if (ETool.AC.Button("生成", GUILayout.Width(60))) { CreateCSFile(info.FullName); }
//            });
//        }

//        private void CFileInfoView(FileInfo info, int num)
//        {
//            ETool.BE.Horizontal(() =>
//            {
//                ETool.AC.Label($"NO.{num}", GUILayout.Width(150));
//                ETool.AC.HelpBox(info.Name, MessageType.None);
//                if (ETool.AC.Button("打开", GUILayout.Width(60))) ProcessKit.OpenTextFile(info.FullName);
//                if (ETool.AC.Button("覆盖", GUILayout.Width(60))) { CreateCSFile(info.FullName); }
//            });
//        }

//        private void CreateCSFileAll(FileInfo[] path)
//        {
//            string[] s = new string[path.Length];
//            for (int i = 0; i < path.Length; i++)
//            {
//                s[i] = path[i].FullName;
//            }
//            CreateCSFile(s);
//        }

//        private void CreateCSFile(params string[] path)
//        {
//            List<string> list = new List<string>();
//            list.Add(exepath[0] + ":");
//            list.Add($@"cd '{exepath}'".Replace('\'', '\"'));
//            foreach (var item in path)
//            {
//                list.Add($@"protoc.exe -I='{floderpath}' --csharp_out='{outpath}' '{item}'".Replace('\'', '\"'));
//            }
//            ProcessKit.Bat(exepath + "protoc", false, list.ToArray());
//        }

//        protected override void change()
//        {

//        }
//    }
//}
