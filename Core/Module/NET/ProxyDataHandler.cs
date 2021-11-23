/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:16:31 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using UnityEngine;

    /// <summary> 服务分发器 </summary>
    public class ProxyDataHandler : MonoBehaviour
    {
        /// <summary> 标准端口常量定义 </summary>
        /// <summary> 反射端口 </summary>
        public static short PORT_ECHO = 1;

        /// <summary> ping接收端口 </summary>
        public static short PORT_PING = 2;

        /// <summary> 消息访问返回端口 </summary>
        public static short PORT_ACCESS_RETURN = 0x3;

        /// <summary> 时间端口 </summary>
        public static short PORT_TIME = 6;

        /// <summary> 属性端口 </summary>
        public static short PORT_PROPERTY = 0x11;

        /// <summary> 端口改变标志常量 </summary>
        public static int HANDLER_CHANGED = 0, PORT_CHANGED = 1;

        /// <summary> 缺省的消息传送处理接口 </summary>
        private RecvPort transmitHandler;

        /// <summary> 内部端口对应的消息传送处理接口数组 </summary>
        private RecvPort[] handlerArray = new RecvPort[0xffff];

        /// <summary> 获得缺省的消息传送处理接口 </summary>
        public RecvPort GetTransmitHandler()
        {
            return transmitHandler;
        }

        /// <summary> 设置指定端口对应的消息传送处理接口 </summary>
        public void SetTransmitHandler(RecvPort handler)
        {
            transmitHandler = handler;
            Debug.Log(string.Concat("Set TransmitHandler, ", handler));
        }

        /// <summary> 获得指定端口对应的消息传送处理接口 </summary>
        public RecvPort GetRecvCommand(int port)
        {
            return handlerArray[port];
        }

        /// <summary> 设置指定端口对应的消息传送处理接口 </summary>
        public void SetRecvCommand(RecvPort handler)
        {
            if (handlerArray[handler.ID] != null)
                Debug.LogWarning($"setRecvCommand this.handlerArray[handler.id] != null{ handler}");
            handlerArray[handler.ID] = handler;
            Debug.Log(string.Concat("SetPort, Port = ", handler.ID, " ", handler));
        }

        /// <summary> 消息处理方法， 参数connect为连接， 参数data是传送的消息， </summary>
        public void Transmit(TcpConnect connect, ByteBuffer data)
        {
            int port = data.GetUnsignedShort(data.Offset);
            RecvPort handler = GetRecvCommand(port);

            if (handler != null)
            {
                handler.Transmit(connect, data);
            }
            else if (transmitHandler != null)
            {
                transmitHandler.Transmit(connect, data);
            }
        }
    }
}