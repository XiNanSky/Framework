/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:53:32 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;

    /// <summary> 接收后台数据 </summary>
    public class RecvPort : TcpPort
    {
        /** 接收到的数据 */
        protected ByteBuffer data;

        public RecvPort()
        {
        }

        public RecvPort(short id)
        {
            ID = id;
        }

        public override void Excute()
        {
            BytesRead(data);
        }

        public virtual void BytesRead(ByteBuffer data)
        {

        }

        /// <summary> 消息处理方法</summary>
        /// <param name="connect">连接</param>
        /// <param name="data">传送的消息</param>
        public void Transmit(TcpConnect connect, ByteBuffer data)
        {
            this.data = data;
            int port = data.ReadUnsignedShort(); // port
            int crc = data.ReadInt(); // crc
            Excute();
        }
    }
}