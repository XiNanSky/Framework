/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:44 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;

    public class PingPort : RecvPort
    {
        public PingPort()
        {
            ID = ProxyDataHandler.PORT_PING;
        }

        public override void BytesRead(ByteBuffer data)
        {
            base.BytesRead(data);
            long time = data.ReadLong();
            UnityEngine.Debug.Log(string.Concat(",lasttime=", time, " ,time= ", (TimeKit.CurrentTimeMillis - time)));
        }
    }
}