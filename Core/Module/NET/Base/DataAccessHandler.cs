/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:50:23 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using System;
    using UnityEngine;

    /// <summary> 有返回类型的消息处理器 </summary>
    public class DataAccessHandler : RecvPort
    {
        /// <summary> 通信超时时间为5秒 </summary>
        public const int TIMEOUT = 5 * 1000;

        /// <summary> 负数通讯号 </summary>
        private static int minusId;

        /// <summary> 当前的数据访问处理器 </summary>
        private static DataAccessHandler handler;

        /// <summary> 获得当前的数据访问处理器 </summary>
        public static DataAccessHandler GetInstance()
        {
            if (handler == null)
                handler = new DataAccessHandler();
            return handler;
        }

        /// <summary> 获取新的负数通讯号 </summary>
        public static int NewMinusId()
        {
            minusId--;
            if (minusId >= 0)
                minusId = -1;
            return minusId;
        }

        /// <summary> 通信对象等待返回条目列表 </summary>
        private Entry[] entryList = new Entry[1024];

        public DataAccessHandler()
        {
            ID = ProxyDataHandler.PORT_ACCESS_RETURN;
        }

        /// <summary> 异步数据访问,发送 </summary>
        public void Access(TcpConnect c, short port, ByteBuffer data, Action<ByteBuffer> func)
        {
            Entry entry = new Entry(NewMinusId(), TimeKit.CurrentTimeMillis, func);
            Debug.Log(string.Concat(" access,port = ", port, ",len = ", data.Length, ",entry.id = ", entry.id));
            int id = (-entry.id) % 1024;
            this.entryList[id] = entry;

            //Debug.Log("=======================c=============================" + c);
            c.Send(port, ProxyDataHandler.PORT_ACCESS_RETURN, entry.id, data);
        }

        /// <summary> 移除一个指定线程通讯号的线程访问条目,并清理超时的条目 </summary>
        private Entry RemoveEntryById(int id)
        {
            Entry entry = entryList[(-id) % 1024];
            entryList[(-id) % 1024] = null;
            return entry;
        }

        /// <summary> 返回数据执行 </summary>
        public override void Excute()
        {
            int id = data.ReadInt();// 访问序列号seq
            Entry entry = RemoveEntryById(id);

            if (entry != null)
                entry.ParseResultData(this.data);
        }

        public void Collate(long time)
        {
            Debug.Log(string.Concat("collate, ", "time=", time));
            for (int i = 0; i < entryList.Length; i++)
            {
                if (entryList[i] == null || entryList[i].time < 0) continue;
                if (entryList[i].time + TIMEOUT < time)
                {
                    this.entryList = new Entry[1024];
                    throw new DataAccessException("网络超时");
                }
            }
        }
    }
}