/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:36 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Framework;

    /// <summary> lua 发送消息 无返回类型 </summary>
    public class LuaTcpSendCommand : TcpSendCommand
    {
        private ByteBuffer buffer;

        public LuaTcpSendCommand(int cmd, ByteBuffer data)
        {
            ID = (short)cmd;
            buffer = data;
        }

        public override ByteBuffer BytesWrite()
        {
            ByteBuffer data = new ByteBuffer();
            data.Write(buffer);
            return data;
        }
    }
}