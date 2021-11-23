/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:55:19 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework.Kit;
    using System;
    using UnityEngine;

    /// <summary> HTTP通信条目 </summary>
    public class HttpEntry
    {
        /// <summary> 通信序列号 </summary>
        public int ID;

        /// <summary> 发送时间 </summary>
        public long SendTime;

        /// <summary> 返回数据 </summary>
        public ByteBuffer Value = null;

        public HttpCommand Command;

        public HttpEntry(int id, long time, HttpCommand command)
        {
            ID = id;
            SendTime = time;
            Command = command;
        }

        public void OnCommand()
        {
#if UNITY_EDITOR
            //if (GameManager.config.ShowHttpTime)
            {
                var value = Value == null ? "" : System.Text.Encoding.UTF8.GetString(Value.ToBytes());
                var times = TimeKit.ExecDateDiff(SendTime, DateTime.Now.ToUniversalTime().Ticks);
                UnityEngine.Debug.Log(string.Concat($"<color=#53ff53> Port : {Command.Url} | Time : {times} | Value : {value} </color>"));
            }
#endif
            Command.Excutetime = GetTime();
            if (Value != null)
            {
                try { Command.OnCommand(Value); }
                catch (UnityException e) { Debug.LogException(e); }
            }
        }

        public long GetTime()
        {
            return DateTime.Now.ToUniversalTime().Ticks - SendTime;
        }
    }
}