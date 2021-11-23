/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net.Command 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:28:00 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    /// <summary> 用户通信基类 </summary>
    public class UserCommand : AccessCommand
    {
        public static TcpConnect connect;

        public override TcpConnect GetConnect()
        {
            return connect;
        }
    }
}