/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-10                    *
* Nowtime:           18:12:08                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Console
{
    using System;
    using System.Collections.Generic;

    public interface IConsoleDisplay
    {
        void Refresh();
    }

    public class Console
    {
        public static bool Enable { set => Instance.Enable = value; }
        public static int InfoCount => Instance.InfoCount;
        public static int WarnCount => Instance.WarnCount;
        public static int ErrorCount => Instance.ErrorCount;
        public static List<ConsoleLogEntry> ViewLogs => Instance.ViewLogs;

        public static bool InfoSelected { get => Instance.InfoSelected; set => Instance.InfoSelected = value; }
        public static bool WarnSelected { get => Instance.WarnSelected; set => Instance.WarnSelected = value; }
        public static bool ErrorSelected { get => Instance.ErrorSelected; set => Instance.ErrorSelected = value; }

        internal static ConsoleImpl Instance { get; private set; }

        public static void Create()
        {
            if (Instance != null)
                return;
            Instance = new ConsoleImpl();
        }

        public static void Cleanup()
        {
            if (Instance == null)
                return;
            Instance.Cleanup();
        }

        public static void SetDisplay(IConsoleDisplay display)
        {
            if (Instance == null)
                return;
            Instance.SetDisplay(display);
        }

        public static void AddCommand(string name,
                                      Action<ConsoleCommandArg[]> proc,
                                      int min_args = 0,
                                      int max_args = -1,
                                      string help = "",
                                      string hint = null)
        {
            if (Instance == null)
                return;
            Instance.AddCommand(name, proc, min_args, max_args, help, hint);
        }

        public static void RunCommand(string line)
        {
            if (Instance == null)
                return;
            Instance.RunCommand(line);
        }
    }
}