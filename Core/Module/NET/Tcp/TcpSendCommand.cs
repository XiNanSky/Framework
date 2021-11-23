/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:56 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Framework.Net;

    /// <summary> 发送数据无返回 </summary>
    public class TcpSendCommand : TcpCommand
    {
        public override TcpConnect GetConnect()
        {
            return UserCommand.connect;
        }

        public override void Excute()
        {
            ByteBuffer data = BytesWrite();
            GetConnect().Send(ID, data);
            UnityEngine.Debug.Log(string.Concat("id=" + ID + " ,send , len=" + data.Length + " ,time=" + Excutetime));
        }
    }
}