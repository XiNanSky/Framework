/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.ToolWindow 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-06
* Time:             15:22:03
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor.ToolWindow
{
    using System.Collections.Generic;
    using Framework.Editor;
    using UnityEditor;
    using UnityEngine;

    /// <summary> </summary>
    public class Load : BaseWindowEditor
    {
        private static new EditorWindow window;

        [MenuItem("ETool/Windows/Scripting Define Symbols Editor %#h")]
        public static void Open()
        {
            if (window != null)
            {
                window.Show(false);
                window = null;
            }
            else
            {
                window = window ?? GetWindow<Load>(true, "Scripting Define Symbols Editor", true);//创建窗口
                window.wantsMouseMove = true;
                window.Show(true);//展示     
            }
        }

        public Load()
        {
            minSize = new Vector2(600, 900);
            symbolsList = new List<string>();
        }

        private List<string> symbolsList;

        protected override void onEnable()
        {
            load();
        }

        protected override void load()
        {
            symbolsList.Clear();
            for (int i = 0; i < EditorPrefs.GetInt("ScriptingDefineSymbolsNum"); i++)
            {
                symbolsList.Add(EditorPrefs.GetString($"ScriptingDefineSymbolsNum{i}"));
            }
            if (symbolsList.Count == 0)
            {
                var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
                foreach (var item in str)
                {
                    if (!symbolsList.Contains(item))
                    {
                        symbolsList.Add(item);
                    }
                }
            }
        }

        protected override void save()
        {
            EditorPrefs.SetInt("ScriptingDefineSymbolsNum", symbolsList.Count);
            for (int i = 0; i < symbolsList.Count; i++)
            {
                EditorPrefs.SetString($"ScriptingDefineSymbolsNum{i}", symbolsList[i]);
            }
        }

        /// <summary> 禁止你想要的宏定义 </summary>
        private void DelScriptingDefineSymbols(string value)
        {
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (str.Contains(value))
            {
                str = str.Replace(value, "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }

        /// <summary> 添加你想要的宏定义 </summary>
        private void AddScriptingDefineSymbols(string value)
        {
            //获取当前是哪个平台
            var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            //获得当前平台已有的的宏定义
            var str = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!str.Contains(value))
            {   //添加宏定义
                str += ";" + value;
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, str);
            }
        }

        protected override void onGUI()
        {
            ETool.BE.Horizontal(() =>
            {
                ETool.AC.FieldLabel($"路径 :");
                ETool.EA.Separator();
            });

            ETool.BE.Horizontal(() =>
            {
                if (ETool.AC.Button("Load")) { load(); }
                if (ETool.AC.Button("Save")) { save(); }
                if (ETool.AC.Button("Add")) { symbolsList.Add(""); }
                if (ETool.AC.Button("Show")) { }
                if (ETool.AC.Button("Hide")) { }
            });

            vector = ETool.BE.ScrollView(() =>
            {
                ETool.AC.FieldLabel($"宏定义");
                for (int i = 0; i < symbolsList.Count; i++)
                {
                    ETool.BE.Horizontal(() =>
                    {
                        symbolsList[i] = ETool.AC.FieldText($"NO.{i}", symbolsList[i]);
                        if (ETool.AC.Button("Enabled", GUILayout.Width(60)))
                        {
                            AddScriptingDefineSymbols(symbolsList[i]);
                        }
                        if (ETool.AC.Button("Forbid", GUILayout.Width(60)))
                        {
                            DelScriptingDefineSymbols(symbolsList[i]);
                        }
                        if (ETool.AC.Button("DEL", GUILayout.Width(60)))
                        {
                            DelScriptingDefineSymbols(symbolsList[i]);
                            symbolsList.RemoveAt(i);
                            save();
                        };
                    });
                }

            }, vector, GStyleTool.scrollView);
        }

        protected override void close()
        {
            window = null;
            save();
        }
    }
}