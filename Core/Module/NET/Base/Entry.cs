/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:25:47 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;
    using Framework;

    /// <summary> 通信条目 </summary>
    public class Entry
    {
        /// <summary> 线程通讯号 </summary>
        public int id;

        /// <summary> 发送时间 </summary>
        public long time;

        /// <summary> 返回消息的处理函数 </summary>
        private Action<ByteBuffer> func;

        /// <summary>  构造一个访问条目，参数用于访问调用，指定线程通讯号和线程通讯数据 </summary>
        public Entry(int id, long time, Action<ByteBuffer> func)
        {
            this.id = id;
            this.time = time;
            this.func = func;
        }

        /// <summary> 处理返回消息 </summary>
        public void ParseResultData(ByteBuffer data)
        {
            func?.Invoke(data);
        }
    }
}