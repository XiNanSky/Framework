/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Net.Command 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-12
* Time:             15:00:11
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System.Text;

    /// <summary> 发送数据 没有返回值 </summary>
    public class LuaHttpSendCommand : LuaHttpCommand
    {
        public LuaHttpSendCommand(int url, ByteBuffer databuffer)
        {
            Url = url;
            Method = POST;
            data = databuffer;
        }

        public LuaHttpSendCommand(int url, string databuffer)
        {
            Url = url;
            Method = POST;
            data = new ByteBuffer();
            data.WriteBytes(Encoding.UTF8.GetBytes(databuffer));
        }
    }
}