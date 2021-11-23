/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-10
* Time:             15:20:14
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /// <summary> 
    /// 指令方法
    /// </summary>
    public static class ProcessKit
    {
        /// <summary>
        /// 打开软件
        /// </summary>
        public static void Open(ProcessStartInfo info)
        {
            Process.Start(info);
        }

        /// <summary>
        /// 打开软件 path = .exe 路径
        /// </summary>
        public static void Open(string path)
        {
            if (IOKit.ExistsFile(path)) Process.Start(path);
        }

        /// <summary> 
        /// 打开记事本 输入指定内容
        /// </summary>
        public static void OpenTextFile(string arguments)
        {
            if (IOKit.ExistsFile(arguments)) Process.Start("notepad.exe", arguments);
        }

        /// <summary> 
        /// 打开文件夹
        /// </summary>
        public static void OpenFolder(string path)
        {
            if (Directory.Exists(path)) Process.Start(path);
        }

        /// <summary> 
        /// 打开文件夹
        /// </summary>
        public static void OpenFolder(string str1, string str2)
        {
            var path = str1.PathCombine(str2);
            if (IOKit.ExistsFile(path)) Process.Start(path);
        }

        /// <summary> 
        /// CMD指令
        /// </summary>
        public static string CMD(bool show, params string[] inputinfo)
        {
            StringBuilder str = ObjPoolKit.New<StringBuilder>();
            try
            {    //设置要启动的应用程序
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.Arguments = @"C:\Windows\System32";
                info.LoadUserProfile = true;
                info.WorkingDirectory = $"{Application.dataPath}";
                //是否使用操作系统shell启动
                info.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                info.RedirectStandardInput = true;
                //输出信息
                info.RedirectStandardOutput = true;
                //输出错误
                info.RedirectStandardError = true;
                //不显示程序窗口
                info.CreateNoWindow = show;
                foreach (var item in inputinfo)
                {
                    str.Append(item);
                }
                using (Process p = Process.Start(info))
                {
                    //向cmd窗口发送输入信息
                    p.StandardInput.WriteLine(str.ToString());
                    //自动
                    str.Clear();
                    str.Append(p.StandardOutput.ReadToEnd()).Append('\n'); ;
                    p.StandardInput.AutoFlush = true;
                    //等待程序执行完退出进程
                    p.WaitForExit();
                    p.Close();
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log(ex.Message + "\r\n跟踪;" + ex.StackTrace);
            }
            return str.ToString();
        }

        /// <summary> 
        /// CMD指令
        /// </summary>
        public static string CMD(string inputinfo, bool show = true)
        {
            return CMD(inputinfo, show);
        }

        /// <summary> 
        /// 生成Bat文件执行
        /// </summary>
        /// <param name="concat">是否覆盖</param>
        public static async void Bat(string path, bool concat, params string[] context)
        {
            try
            {
                //不存在则退出
                if (!Directory.Exists(@path.Substring(0, path.LastIndexOf('/')).Replace("/", @"\"))) return;

                //修改后缀
                if (path.Contains(".")) Path.ChangeExtension(path, ".bat");
                else path += ".bat";

                StringBuilder s = new StringBuilder();
                foreach (var item in context) s.Append(item).Append('\n');

                //写入内容
                await IOKit.WriteUTF8_(path, s.ToString(), concat);

                //开始执行
                await Process.Start(path);
            }
            catch (Exception e) { UnityEngine.Debug.LogError(e); }
#if UNITY_EDITOR
            finally { AssetDatabase.Refresh(); }
#endif
        }
    }
}