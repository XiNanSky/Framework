/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:09:21 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;

    public class LuaCommand : UserCommand
    {
        private ByteBuffer buffer;

        public LuaCommand(int command_id, ByteBuffer data)
        {
            ID = (short)command_id;
            buffer = data;
        }

        public override ByteBuffer BytesWrite()
        {
            var data = new ByteBuffer();
            data.Write(buffer);
            return data;
        }

        public override void BytesRead(ByteBuffer data)
        {
            Callbackobj = data;
        }
    }
}