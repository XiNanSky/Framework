/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:06 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary> 命令执行管理器 </summary>
    public class CommandManager : MonoBehaviour
    {
        private static List<TcpPort> commands;

        //用于后台运行一段时间后，自动重连
        public static long time1;

        public void Init()
        {
            commands = new List<TcpPort>();
        }

        public static void AddCommand(TcpPort command, Action<object> func)
        {
            time1 = TimeKit.CurrentTimeMillis;
            command.Callback = func;
            AddCommand(command);
        }

        public static void AddCommand(TcpPort command)
        {
            time1 = TimeKit.CurrentTimeMillis;
            commands.Add(command);
        }

        public Action<TcpPort, Exception> OnCommandException;

        private void Update()
        {
            if (commands == null) return;
            if (commands.Count <= 0) return;
            foreach (var command in commands)
            {
                excuteCommand(command);
            }
            commands.Clear();
        }

        private void excuteCommand(TcpPort command)
        {
            try { command.Excute(); }
            catch (Exception e)
            {
                OnCommandException(command, e);
            }
        }
    }
}