/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:51:20 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;

    /// <summary> 通讯接口 </summary>
    public class TcpPort
    {
        public const int OK = 0;

        /// <summary> 接口号 </summary>
        public short ID;

        /// <summary> 发送时记录发送时间,返回服务器消息时记录通信消耗的时间 </summary>
        protected long Excutetime;

        /// <summary> 回调函数 </summary>
        public Action<object> Callback;

        /// <summary> 回调数据 </summary>
        public object Callbackobj;

        /// <summary> 执行命令 </summary>
        public virtual void Excute()
        {
            throw new SystemException(" ,must override this func!");
        }
    }
}