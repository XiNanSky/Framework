/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:10:43 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using System;

    /// <summary> 发送数据有返回 </summary>
    public abstract class AccessCommand : TcpCommand
    {
        public AccessCommand() { }

        public AccessCommand(short id) : base(id) { }

        public override void Excute()
        {
            ByteBuffer data = BytesWrite();
            Excutetime = DateTime.UtcNow.Ticks / 10000;
#if UNITY_EDITOR
            UnityEngine.Debug.Log(string.Concat("id=", ID, " ,send , len=", data.Length, " ,time=", Excutetime));
#endif

            DataAccessHandler.GetInstance().Access(GetConnect(), ID, data, onReceived);
        }

        /// <summary> 接收消息</summary>
        private void onReceived(ByteBuffer data)
        {
            int type = data.ReadUnsignedShort();

            if (type == OK) BytesRead(data);
            else
            {
                if (type > 100 && type < 200)
                {
                    //注销登录数据
                    //User.logout(User.user);
                }
                string str = data.ReadUTF();
            }
            Callback?.Invoke(Callbackobj);
        }
    }
}