/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:08:45 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;

    public abstract class TcpCommand : TcpPort
    {
        public TcpCommand() { }

        public TcpCommand(short id) { ID = id; }

        public abstract TcpConnect GetConnect();

        public virtual ByteBuffer BytesWrite() { return null; }

        public virtual void BytesRead(ByteBuffer data) { }
    }
}