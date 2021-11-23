/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Net.Command 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-07
* Time:             14:06:39
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Framework.Net;
    using System.Text;

    /// <summary> </summary>
    public class LuaHttpCommand : HttpCommand
    {
        protected override object readData(ByteBuffer data)
        {
            return Encoding.UTF8.GetString(data.ToBytes());
        }
    }
}