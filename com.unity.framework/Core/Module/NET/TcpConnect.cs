/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:57:18 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using System;
    using System.Net.Sockets;
    using UnityEngine;

    /// <summary> TCP连接 </summary>
    public class TcpConnect : MonoBehaviour
    {
        /// <summary> Ping间隔时间 </summary>
        public static int pingintervaltime = 5000;

        /// <summary> 发送超时时间，毫秒 </summary>
        private const int TIMEOUT = 1000;
         
        /// <summary> 默认的接收消息的最大长度，400k </summary>
        public const int MAX_DATA_LENGTH = 1024 * 1024;

        //头信息
        public const int HEAD_SEND_SIZE = 16;
        public const int HEAD_ACCESS_SIZE = 22;
        public const int HEAD_SEND = 1;
        public const int HEAD_ACCESS = 2;

        /// <summary> 流水号 </summary>
        private int uid;

        public string title;

        /// <summary> 连接的地址 </summary>
        public string host;

        /// <summary> 连接的端口 </summary>
        public int port;

        /// <summary> opened </summary>
        public bool opened;

        /// <summary> 是否启用ping </summary>
        public bool pingEnable;

        /// <summary> ping值 </summary>
        public int pingValue;

        /// <summary> 最近ping时间 </summary>
        private long lastpingtime;

        /// <summary> 连接的socket </summary>
        private TcpClient tcpClient;

        /// <summary> 连接的消息传送处理器 </summary>
        private ProxyDataHandler handler;

        /// <summary> 数据长度 </summary>
        private int len = 0;

        /// <summary> 等待读取头数据 </summary>
        private bool waitHead = true;

        /// <summary> 缓存已读数据 </summary>
        private ByteBuffer data;

        private Action<TcpConnect, Exception> onConnectException;


        /// <summary> 获得消息处理器 </summary>
        public ProxyDataHandler GetTransmitHandler()
        {
            return handler;
        }

        /// <summary> 获取流水号 </summary>
        public int GetUid()
        {
            return ++uid;
        }

        /// <summary> 设置消息处理器 </summary>

        public void SetTransmitHandler(ProxyDataHandler handler)
        {
            this.handler = handler;
        }

        /// <summary> 初始化 </summary>
        public void Init(string title, string host, int port, bool pingEnable, Action<TcpConnect, Exception> onConnectException)
        {
            this.enabled = false;
            this.pingEnable = pingEnable;
            this.title = title;
            this.host = host;
            this.port = port;
            this.onConnectException = onConnectException;

            this.tcpClient = new TcpClient();
            ProxyDataHandler handler = new ProxyDataHandler();
            handler.SetRecvCommand(DataAccessHandler.GetInstance());
            SetTransmitHandler(handler);
        }

        /// <summary> 连接 </summary>
        /// <param name="onConnect">连接结果回调</param>
        public void connect(Action<TcpConnect, bool> onConnect)
        {
            try
            {
                if (data == null) data = new ByteBuffer();
                data.clear();
                len = 4;
                waitHead = true;
                if (tcpClient.Connected) tcpClient.Close();

                tcpClient = new TcpClient();
                tcpClient.Connect(host, port);
                tcpClient.SendTimeout = TIMEOUT;
                tcpClient.SendBufferSize = 8196 * 2;
                tcpClient.ReceiveBufferSize = MAX_DATA_LENGTH;
                tcpClient.SendBufferSize = MAX_DATA_LENGTH;
                tcpClient.NoDelay = false;
                enabled = true;
                opened = true;
                onConnect(this, true);
            }
            catch (Exception e)
            {
                Debug.LogWarning("_connect error:\n" + e);
                onConnect(this, false);
            }
            // onConnect(this, opened);
        }


        public void Close()
        {
            opened = false;
            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }
        }

        public void Send(short port, ByteBuffer buffer)
        {
            int len = 10 + buffer.Length;
            ByteBuffer head = new ByteBuffer(len);
            head.WriteInt(len); //4
            head.WriteShort(port); //2
            //head.WriteInt(CRC32.getValue(buffer.Arrays, buffer.Offset, buffer.Length)); //4  加密
            head.Write(buffer);
            Send(head.Arrays, head.Offset, head.Length);
        }

        public void Send(short port, short ackPort, int seq, ByteBuffer buffer)
        {
            int len = 16 + buffer.Length;
            MsgBuffer head = new MsgBuffer(16, buffer.Length);

            head.Write(buffer);
            head.SetShort(ackPort, 10); //2

            head.SetInt(seq, 12); //4
            head.setOffsetUncheckLock(10);

            head.SetInt(len, 0); //4
            head.SetShort(port, 4); //2
            //head.SetInt(CRC32.getValue(head.Arrays, head.Offset, head.Length), 6); //4   解密
            head.setOffsetUncheckLock(0);

            Send(head.Arrays, head.Offset, head.Length);
        }

        public void Send(byte[] bytes, int offset, int len)
        {
            NetworkStream ns = this.tcpClient.GetStream();
            ns.Write(bytes, offset, len);
            ns.Flush();
        }

        public void Ping(long time)
        {
            if (time - lastpingtime < pingintervaltime) return;
            lastpingtime = time;
            try
            {
                byte[] bytes = new byte[10];
                ByteKit.writeInt(bytes, 10, 0);
                ByteKit.writeShort(bytes, ProxyDataHandler.PORT_ECHO, 4);
                ByteKit.writeInt(bytes, pingValue, 6);
                Send(bytes, 0, 10);
            }
            catch (Exception)
            {
                onConnectException(this, new DataAccessException("网络中断"));
            }
        }

        /// <summary> 最近通信时间 </summary>
        private long lasttime;

        private void Update()
        {
            if (!opened) return;
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            try { Receive(); }
            catch (Exception e) { onConnectException(this, e); return; }
#else
            Receive();
#endif
            if (pingEnable)
            {
                long time = TimeKit.CurrentTimeMillis;
                if (time < 0) return;
                //ping消息发送10秒后还没收到返回消息
                if (lastpingtime > 0 && time - lastpingtime > 4000)
                {
                    if (lasttime > 0 && lastpingtime > lasttime)
                    {
                        onConnectException(this, new DataAccessException("网络超时"));
                        return;
                    }
                }
                Ping(time);
            }
        }

        /// <summary> 重置连接状态 </summary>
        public void ResetStatus()
        {
            if (tcpClient != null && tcpClient.Connected) tcpClient.Close();
            opened = false;
            enabled = false;
            lastpingtime = 0;
            lasttime = 0;
        }

        private void Receive()
        {
            if (tcpClient == null || !tcpClient.Connected) return;

            NetworkStream ns = tcpClient.GetStream();
            while (ReadData(ns)) ;
        }

        private bool ReadData(NetworkStream ns)
        {
            if (tcpClient.Available == 0) return false;// 无数据可读
            int remainning = tcpClient.Available;
            if (remainning < this.len)// 数据小于待读长度
            {
                int top = data.Top;
                ns.Read(data.Arrays, top, remainning);
                data.Top = (top + remainning);
                len -= remainning;
                return false; // 一条消息数据未完，继续等待
            }
            else// 数据大于待读长度
            {
                int top = data.Top;
                ns.Read(data.Arrays, top, this.len);
                data.Top = (top + len);
                if (waitHead)
                {
                    len = data.ReadInt() - 4;
                    data.clear().Capacity = (len);
                    waitHead = false;
                }
                else
                {
                    Transmit(data);
                    data.clear();
                    waitHead = true;
                    len = 4;
                }
                return true;
            }
        }

        /// <summary> 连接的消息接收方法 </summary>
        private void Transmit(ByteBuffer data)
        {
            long time = TimeKit.CurrentTimeMillis;

            int port = data.GetUnsignedShort(0);

            if (port == ProxyDataHandler.PORT_PING)
            {
                lasttime = time;
                pingValue = (int)(time - lastpingtime);
            }
            else
            {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
                try
                {
                    handler.Transmit(this, data);
                }
                catch (Exception e) { }
#else
                this.handler.Transmit(this, data);
#endif
            }
        }
    }
}