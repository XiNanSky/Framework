/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:28 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Framework.Net;
    using System;

    /// <summary> Lua 接收消息处理 </summary>
    public class LuaTcpRecvCommand : RecvPort
    {
        private Action<object> back;

        public LuaTcpRecvCommand(int cmd, Action<object> back)
        {
            ID = (short)cmd;
            this.back = back;
        }


        public override void BytesRead(ByteBuffer data)
        {
            base.BytesRead(data);
            back(data);
        }
    }
}