///* * * * * * * * * * * * * * * * * * * * * * * * 
//*Copyright(C) 2021 by xinansky 
//*All rights reserved. 
//*FileName:         Framework.EToolWindow 
//*Author:           XiNan 
//*Version:          0.1 
//*UnityVersion:     2020.3.5f1c1 
//*Date:             2021-07-04 
//*NOWTIME:          14:11:19 
//*Description:        
//*History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.EToolWindow
//{
//    using Framework.Editor;
//    using System;
//    using System.IO;
//    using System.Threading;
//    using UnityEditor;
//    using UnityEngine;
//    using Application = UnityEngine.Application;
//    using MenuItem = UnityEditor.MenuItem;

//    public enum BuildABTarget { Android, iOS, PC, }

//    public class PackageManagerEditor : BaseWindowEditor
//    {
//        private const string BuildOut = "Build Out";
//        private const string PackageOut = "Package Out";
//        private const string BackFloder = "BackAssets/";
//        private const string UnCommpress = "UnCommpress/";

//        private string PackageOutBundlePath => string.Concat(PathManager.CacheLocal.AB_RESROUCE_PATH, PackageOut, '/', BundlePlatform.ToString(), '/', bundlePath);
//        private string BackAssets => PathManager.CacheLocal.AB_RESROUCE_PATH + BackFloder;

//        public PackageManagerEditor()
//        {
//            titleContent = new GUIContent("Package Manager");
//            minSize = new Vector2(450, 600);
//            maxSize = new Vector2(500, 1000);
//            packageList = new ArrayList<string>();
//            server_requst_URL = new ArrayList<string>();
//            filelablekeys = new ArrayList<string>();
//            filelablevalues = new ArrayList<string>();
//        }

//        private static new EditorWindow window;
//        [MenuItem("ETool/Package Manager Window %#x")]
//        public static void Open()
//        {
//            window = window ?? GetWindow<PackageManagerEditor>(true, "Package Manager", true);//创建窗口
//            window.wantsMouseMove = true;
//            window.Show(true);//展示         

//            if (!FileKit.Exists(PathManager.CacheLocal.AB_RESROUCE_PATH))
//            {
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + BuildOut}/PC/");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + BuildOut}/IOS/");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + BuildOut}/Android/");

//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + PackageOut}/PC/");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + PackageOut}/IOS/");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + PackageOut}/Android/");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + BackFloder}");
//                Directory.CreateDirectory($"{PathManager.CacheLocal.AB_RESROUCE_PATH + UnCommpress}");

//            }
//        }

//        private ArrayList<string> server_requst_URL;
//        private ArrayList<string> filelablevalues;
//        private ArrayList<string> filelablekeys;
//        private ArrayList<string> packageList;

//        protected override void onGUI()
//        {
//            vector = EditorGUILayout.BeginScrollView(vector);

//            AssetBundleBuild();        //资源打包

//            SettingFilesAttribute();

//            PackageList();             //打包类型

//            EditorGUILayout.EndScrollView();
//        }

//        private BuildABTarget BundlePlatform;
//        private string bundlePath;
//        private void AssetBundleBuild()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.AC.Label("资源打包", GStyleTool.title);
//                ETool.BE.Horizontal(() =>
//                {
//                    BundlePlatform = (BuildABTarget)EditorGUILayout.EnumPopup("打包平台", BundlePlatform, GStyleTool.GetName("Popup"));
//                    if (ETool.AC.Button("打包", GStyleTool.button, GUILayout.Width(60)))
//                    {
//                        BuildAllAssetBundles(bundlePath);
//                    }
//                    if (ETool.AC.Button("打开", GStyleTool.button, GUILayout.Width(60)))
//                    {
//                        System.Diagnostics.Process.Start($"{PathManager.CacheLocal.AB_RESROUCE_PATH}Package Out/{BundlePlatform.ToString()}");
//                    }
//                });
//                ETool.BE.Horizontal(() =>
//                {
//                    bundlePath = ETool.AC.FieldText($"{BundlePlatform.ToString()} Floder", bundlePath, GStyleTool.GetName("LargeTextField"));
//                });
//            }, GStyleTool.UIContent);
//        }

//        private bool isShowABFloder;
//        private void PackageList()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.AC.Label("源文件夹资源", GStyleTool.title);

//                if (bundlePath.Length != 0 && BundlePlatform != BuildABTarget.PC)   //显示文件夹
//                {
//                    ETool.EA.Space();
//                    ETool.BE.Vertical(() =>
//                    {
//                        ETool.BE.Horizontal(() =>
//                        {
//                            if (!isShowABFloder) { if (ETool.AC.Button("显示源文件夹", GStyleTool.button)) isShowABFloder = true; }
//                            else { if (ETool.AC.Button("关闭源文件夹", GStyleTool.button)) isShowABFloder = false; }
//                            if (ETool.AC.Button(" 备份源文件 ", GStyleTool.button))    //备份源文件到新的文件夹下
//                            {
//                                var path = BackAssets;
//                                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
//                                var root = new DirectoryInfo(PathManager.CacheLocal.AB_RESROUCE_PATH);
//                                foreach (var floder in root.GetDirectories())
//                                {
//                                    if (packageList.Contains(floder.Name))
//                                    {
//                                        FileKit.CopyFolderAll(floder.FullName, string.Concat(path, floder.Name), true);
//                                    }
//                                }
//                            }
//                        });
//                        if (isShowABFloder)
//                        {
//                            var num = 0;
//                            foreach (var item in new DirectoryInfo(BackAssets).GetDirectories())
//                            {
//                                ETool.BE.Horizontal(() =>
//                                {
//                                    ETool.AC.Label(string.Concat("NO.", ++num), GUILayout.Width(100));
//                                    ETool.AC.Label(item.Name);
//                                    ETool.EA.ButtonCopyText("复制", -1, 60, item.Name);
//                                    if (ETool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
//                                    {
//                                        Directory.Delete(item.FullName, true);
//                                    }
//                                    if (ETool.AC.Button("打包", GStyleTool.button, GUILayout.Width(60)))
//                                    {
//                                        PackageFunc(item.FullName, PackageOutBundlePath, new string[] { item.Name });
//                                    }
//                                }, GStyleTool.helpBox);
//                            }
//                        }
//                    });
//                }

//                ETool.EA.Space();

//                ETool.BE.Vertical(() =>
//                {
//                    for (int i = 0; i < packageList.Count; i++)
//                    {
//                        ETool.BE.Horizontal(() =>
//                        {
//                            ETool.AC.Label(string.Concat("NO.", i + 1), GUILayout.Width(40));
//                            packageList.Set(ETool.AC.FieldText(packageList.Get(i)), i);
//                            if (ETool.AC.Button("备份", GStyleTool.button, GUILayout.Width(60)))
//                            {
//                                var s = string.Concat(PathManager.CacheLocal.AB_RESROUCE_PATH, packageList.Get(i));
//                                var d = string.Concat(BackAssets, packageList.Get(i));
//                                FileKit.CopyFolderAll(s, d, true);
//                                EditorUtility.DisplayDialog("提示", $"备份 {packageList.Get(i)} 成功", "确定");
//                            }
//                            if (ETool.AC.Button("移除", GStyleTool.button, GUILayout.Width(60))) { packageList.Remove(packageList.Get(i)); }
//                        });
//                    }

//                    ETool.EA.Space();

//                    ETool.BE.Horizontal(() =>
//                    {
//                        if (ETool.AC.Button("一键打包", GStyleTool.button))
//                        {
//                            PackageFunc(BackAssets, @PackageOutBundlePath, packageList.ToArray());
//                        }
//                        if (ETool.AC.Button("新增路径", GStyleTool.button)) packageList.Add("");
//                    });

//                    ETool.BE.Horizontal(() =>
//                    {
//                        if (ETool.AC.Button("解包本地资源", GStyleTool.button))
//                        {
//                            UnCommpressFile(PackageOutBundlePath);
//                        }
//                        if (ETool.AC.Button("新增资源路径", GStyleTool.button)) server_requst_URL.Add("");
//                    });

//                    ETool.EA.Space();

//                }, GStyleTool.content);
//            }, GStyleTool.UIContent);

//            ETool.BE.Vertical(() =>
//            {
//                for (int i = 0; i < server_requst_URL.Count; i++)
//                {
//                    ETool.BE.Horizontal(() =>
//                    {
//                        server_requst_URL.Set(ETool.AC.FieldText(server_requst_URL.Get(i), GStyleTool.textArea, GUILayout.Height(40)), i);
//                    });
//                    ETool.BE.Horizontal(() =>
//                    {
//                        ETool.AC.Label("服务器资源包路径", GStyleTool.title, GUILayout.ExpandWidth(true));
//                        if (ETool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60)))
//                        {
//                            server_requst_URL.RemoveAt(i);
//                        }
//                        ETool.EA.ButtonCopyText("复制", -1, 60, server_requst_URL.Get(i));
//                        if (ETool.AC.Button("覆盖", GStyleTool.button, GUILayout.Width(60)))
//                        {
//                            var @dest = string.Concat(server_requst_URL.Get(i), $@"\{BundlePlatform.ToString()}\");
//                            ReplacePackage(@PackageOutBundlePath, @dest);
//                        }
//                        if (ETool.AC.Button("打开", GStyleTool.button, GUILayout.Width(60)))
//                        {
//                            var @dest = string.Concat(server_requst_URL.Get(i), $@"\{BundlePlatform.ToString()}\");
//                            if (Directory.Exists(@dest)) System.Diagnostics.Process.Start(@dest);
//                        }
//                    }, GUILayout.Height(20), GUILayout.ExpandWidth(true));
//                }
//            }, GUILayout.ExpandWidth(true));
//        }

//        private bool openFilesAttribute;
//        private void SettingFilesAttribute()
//        {
//            ETool.BE.Vertical(() =>
//            {
//                ETool.EA.Space();
//                ETool.AC.Label("设置文件标签属性", GStyleTool.title);
//                ETool.BE.Horizontal(() =>
//                {
//                    if (ETool.AC.Button(" 设置全部 ", GStyleTool.button))
//                    {
//                        for (int i = 0; i < filelablekeys.Count; i++) SetVersionDirAssetName(filelablekeys.Get(i), filelablevalues.Get(i));
//                        EditorUtility.DisplayDialog("提示", "设置成功", "确定");
//                    }
//                    if (ETool.AC.Button("   新增   ", GStyleTool.button)) { filelablekeys.Add(""); filelablevalues.Add(""); }
//                    if (ETool.AC.Button("   清空   ", GStyleTool.button))
//                    {
//                        AssetDatabase.RemoveUnusedAssetBundleNames();
//                        AssetDatabase.Refresh();
//                    }
//                    if (ETool.AC.Button(!openFilesAttribute ? " 显示全部 " : " 隐藏全部 ", GStyleTool.button)) openFilesAttribute = !openFilesAttribute;
//                });

//                if (openFilesAttribute)
//                {
//                    ETool.BE.Vertical(() =>
//                    {
//                        for (int i = 0; i < filelablekeys.Count; i++)
//                        {
//                            ETool.BE.Horizontal(() =>
//                            {
//                                ETool.AC.Label("文件名", GUILayout.Width(40)); filelablekeys.Set(ETool.AC.FieldText(filelablekeys.Get(i)), i);
//                                ETool.AC.Label("标签", GUILayout.Width(30)); filelablevalues.Set(ETool.AC.FieldText(filelablevalues.Get(i)), i);
//                                if (ETool.AC.Button("删除", GStyleTool.button, GUILayout.Width(60))) { filelablekeys.RemoveAt(i); filelablevalues.RemoveAt(i); }
//                                if (ETool.AC.Button("设置", GStyleTool.button, GUILayout.Width(60)))
//                                {
//                                    SetVersionDirAssetName(filelablekeys.Get(i), filelablevalues.Get(i));
//                                    EditorUtility.DisplayDialog("提示", string.Concat("设置成功: ", filelablevalues.Get(i)), "确定");
//                                }
//                            }, GUILayout.Height(20));
//                        }
//                    }, GStyleTool.content);
//                }
//            }, GStyleTool.UIContent);
//        }

//        #region Func

//        /// <summary> 打包 </summary> 输出路径为 AB/Build Out/Platform/flodername
//        public void BuildAllAssetBundles(string flodername)
//        {
//            if (flodername.Length == 0) return;
//            var @path = string.Concat(PathManager.CacheLocal.AB_RESROUCE_PATH, BuildOut, '/', BundlePlatform.ToString(), '/', flodername);
//            if (!Directory.Exists(@path)) Directory.CreateDirectory(@path);
//            switch (BundlePlatform)
//            {
//                case BuildABTarget.Android:
//                    BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
//                    break;
//                case BuildABTarget.iOS:
//                    BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
//                    break;
//                case BuildABTarget.PC:
//                    BuildPipeline.BuildAssetBundles(@path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
//                    break;
//            }
//            AssetDatabase.SaveAssets();
//            AssetDatabase.Refresh();
//            EditorUtility.DisplayDialog("提示", $"打包 {BundlePlatform.ToString()} 完成", "确定");
//        }

//        /// <summary> 解包 </summary>
//        private void UnCommpressFile(string @out)
//        {
//            UnCompress unCommpress; DateTime time;
//            var UnCommpressPath = PathManager.CacheLocal.AB_RESROUCE_PATH + "UnCommpress/";
//            foreach (var folder in new DirectoryInfo(@out).GetFiles())
//            {
//                unCommpress = new UnCompress(string.Concat(@out, @"\", folder.Name), UnCommpressPath);
//                unCommpress.UnCompressThread.Start();
//                time = DateTime.Now;
//                while (unCommpress.UnCompressThread.ThreadState == ThreadState.Running)
//                {
//                    EditorUtility.DisplayProgressBar(
//                        string.Concat("UnCommpressFile ", PlayerSettings.applicationIdentifier, ' ', folder.Name),
//                        string.Concat(Mathf.CeilToInt(unCommpress.GetPerCent() * 100), '%'),
//                        unCommpress.GetPerCent());
//                }
//                EditorUtility.ClearProgressBar();
//                Debug.Log(string.Concat("解包 : <<color=#FF5B00>", folder.Name, "</color>> 完成! 一共用时 : <color=#FF5B00>", TimeKit.ExecDateDiff(time, DateTime.Now), "</color>"));
//            }
//            EditorUtility.DisplayDialog("提示", "解包完成", "确定");
//        }

//        /// <summary> 替换 </summary>
//        private void ReplacePackage(string source, string dest)
//        {
//            if (!Directory.Exists(source)) return;
//            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);
//            var names = "";
//            foreach (var file in new DirectoryInfo(source).GetFiles())
//            {
//                File.Copy(@file.FullName, Path.Combine(@dest, file.Name), true);
//                names += string.Concat("<<color=#FF5B00>", file.Name, "</color>> ");
//            }
//            Debug.Log(string.Concat("更换 : <<color=#FF5B00>", new DirectoryInfo(source).GetFiles().Length, "</color>> 个文件 : " + names));
//            EditorUtility.DisplayDialog("提示", "覆盖资源完成", "确定");
//        }

//        /// <summary> 打包方法 </summary>
//        /// <param name="out">输出路径</param>
//        private void PackageFunc(string @source, string @out, string[] floders)
//        {
//            if (!Directory.Exists(@out)) { Directory.CreateDirectory(@out); }

//            string fullname = "", outPutPath = ""; CompressZip compressZip; DirectoryInfo temp;
//            var root = new DirectoryInfo(@source);
//            foreach (var v in floders)
//            {
//                if (v.Length == 0 || v.Contains("/") || v.Contains(@"\")) continue;
//                foreach (var folder in root.GetDirectories())
//                {
//                    if (v == folder.Name)     //获取比较名 cfg res update gameui qycfg等
//                    {
//                        temp = root.CreateSubdirectory("temp");
//                        Directory.Move(folder.FullName, string.Format(@"{0}\{1}", temp.FullName, folder.Name));
//                        Directory.Move(temp.FullName, folder.FullName);

//                        fullname = string.Concat(root.FullName, folder.Name);
//                        outPutPath = string.Concat(@out, '/', folder.Name, ".upk");
//                        compressZip = new CompressZip(@fullname, outPutPath);
//                        compressZip.compressThread.Start();

//                        while (compressZip.compressThread.ThreadState == ThreadState.Running)
//                        {
//                            EditorUtility.DisplayProgressBar(string.Concat("Package ", PlayerSettings.applicationIdentifier, ' ', folder.Name), string.Concat(Mathf.CeilToInt(compressZip.Percent * 100), '%'), compressZip.Percent);
//                        }

//                        Directory.Move(string.Format(@"{0}\{1}", folder.FullName, folder.Name), temp.FullName);
//                        Directory.Delete(folder.FullName);
//                        Directory.Move(temp.FullName, folder.FullName);
//                        folder.Refresh();
//                        EditorUtility.ClearProgressBar();
//                        break;
//                    }
//                }
//            }
//            root.Refresh();
//            EditorUtility.DisplayDialog("提示", "打包结束", "确定");
//        }

//        private void SetVersionDirAssetName(string fullPath, string abName)
//        {
//            var relativeLen = Application.dataPath.Length - 6; // Assets 长度  
//            fullPath = string.Concat(PathManager.ResExplorerPath, fullPath, "/");
//            Debug.Log(string.Concat("Full Path = <<color=#FF5B00>", fullPath, "</color>> | <<color=#FF5B00>Exists = ", Directory.Exists(fullPath), "</color>>"));
//            if (Directory.Exists(fullPath))
//            {
//                EditorUtility.DisplayProgressBar(string.Concat("修改 <", fullPath, "> 名称"), string.Concat("设置标签 ", abName, " ..."), 0f);
//                var dir = new DirectoryInfo(fullPath);
//                var files = dir.GetFiles("*", SearchOption.AllDirectories);
//                for (var i = 0; i < files.Length; ++i)
//                {
//                    var fileInfo = files[i];
//                    if (!fileInfo.Name.EndsWith(".meta"))
//                    {
//                        EditorUtility.DisplayProgressBar(string.Concat("修改 <", fullPath, "> 名称"), string.Concat("设置标签 ", abName, " ..."), 1f * i / files.Length);
//                        var importer = AssetImporter.GetAtPath(fileInfo.FullName.Substring(relativeLen, fileInfo.FullName.Length - relativeLen));
//                        if (importer != null && importer.assetBundleName != abName)
//                            importer.assetBundleName = abName;
//                    }
//                }
//                EditorUtility.ClearProgressBar();
//            }
//        }

//        private void SetAssetNameAsPrefabName(string fullPath)
//        {
//            var relativeLen = fullPath.Length + 8; // Assets 长度  
//            fullPath = Application.dataPath + "/" + fullPath + "/";

//            if (Directory.Exists(fullPath))
//            {
//                EditorUtility.DisplayProgressBar("设置 AssetName 名称", "正在设置 AssetName 名称中...", 0f);
//                var dir = new DirectoryInfo(fullPath);
//                var files = dir.GetFiles("*", SearchOption.AllDirectories);
//                for (var i = 0; i < files.Length; ++i)
//                {
//                    var fileInfo = files[i];
//                    string abName = fileInfo.Name;
//                    EditorUtility.DisplayProgressBar("设置 AssetName 名称", "正在设置 AssetName 名称中...", 1f * i / files.Length);
//                    if (!fileInfo.Name.EndsWith(".meta"))
//                    {
//                        var basePath = fileInfo.FullName.Substring(fullPath.Length - relativeLen); //.Replace('\\', '/');  
//                        var importer = AssetImporter.GetAtPath(basePath);
//                        //abName = AssetDatabase.AssetPathToGUID(basePath);
//                        if (importer && importer.assetBundleName != abName)
//                        {
//                            importer.assetBundleName = abName;
//                        }
//                    }
//                }
//                EditorUtility.ClearProgressBar();
//            }
//        }

//        #endregion

//        protected override void onEnable()
//        {
//            BundlePlatform = (BuildABTarget)EditorPrefs.GetInt("BuildTarget");
//            for (int i = 0; i < EditorPrefs.GetInt("packageList"); i++)
//            {
//                packageList.Add(EditorPrefs.GetString(string.Concat("packageList", i)));
//            }

//            for (int i = 0; i < EditorPrefs.GetInt("server_requst_URL"); i++)
//            {
//                server_requst_URL.Add(EditorPrefs.GetString(string.Concat("server_requst_URL", i)));
//            }

//            string combie = "";
//            for (int i = 0, end; i < EditorPrefs.GetInt("filelable"); i++)
//            {
//                combie = EditorPrefs.GetString(string.Concat("filelable", i));
//                end = combie.LastIndexOf('|');
//                filelablekeys.Add(combie.Substring(0, end));
//                filelablevalues.Add(combie.Substring(end + 1, combie.Length - end - 1));
//            }

//            bundlePath = EditorPrefs.GetString("androidBundlePath");

//            switch (BundlePlatform)
//            {
//                case BuildABTarget.Android:
//                    bundlePath = EditorPrefs.GetString("androidBundlePath");
//                    break;
//                case BuildABTarget.iOS:
//                    bundlePath = EditorPrefs.GetString("iosBundlePath");
//                    break;
//                case BuildABTarget.PC:
//                    bundlePath = EditorPrefs.GetString("pcBundlePath");
//                    break;
//            }

//            if (bundlePath.Length == 0) bundlePath = "AssetBundles Normal";
//        }

//        protected override void change()
//        {
//            EditorPrefs.SetInt("BuildTarget", (int)BundlePlatform);
//            EditorPrefs.SetInt("packageList", packageList.Count);
//            for (int i = 0; i < packageList.Count; i++)                       //资源包路径 文件名
//            {
//                EditorPrefs.SetString(string.Concat("packageList", i), packageList.Get(i));
//            }

//            EditorPrefs.SetInt("server_requst_URL", server_requst_URL.Count); //服务器路径 路径名
//            for (int i = 0; i < server_requst_URL.Count; i++)
//            {
//                EditorPrefs.SetString(string.Concat("server_requst_URL", i), server_requst_URL.Get(i));
//            }

//            EditorPrefs.SetInt("filelable", filelablekeys.Count);             //标签设置
//            for (int i = 0; i < filelablekeys.Count; i++)
//            {
//                EditorPrefs.SetString(string.Concat("filelable", i), string.Concat(filelablekeys.Get(i), '|', filelablevalues.Get(i)));
//            }

//            switch (BundlePlatform)
//            {
//                case BuildABTarget.Android:
//                    EditorPrefs.SetString("androidBundlePath", bundlePath);    //AB Bunlder Aseets Android 包文件夹路径
//                    break;
//                case BuildABTarget.iOS:
//                    EditorPrefs.SetString("iosBundlePath", bundlePath);    //AB Bunlder Aseets Android 包文件夹路径
//                    break;
//                case BuildABTarget.PC:
//                    EditorPrefs.SetString("pcBundlePath", bundlePath);    //AB Bunlder Aseets Android 包文件夹路径
//                    break;
//            }
//        }

//        protected override void close()
//        {
//            window = null;
//        }
//    }
//}