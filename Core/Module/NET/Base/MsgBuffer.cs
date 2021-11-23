/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:39:07 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using System;

    public class MsgBuffer : ByteBuffer
    {
        private int loc;

        /// <summary> 构建一个默认容量的ByteBuffer </summary>
        public MsgBuffer(int loc) : this(loc, CAPACITY) { }

        /// <summary> 构建一个指定容量的ByteBuffer </summary>
        public MsgBuffer(int loc, int capacity) : base(loc + capacity)
        {
            this.loc = loc;
            top = loc;
            offset = loc;
        }

        public override int Offset
        {
            get => offset;
            set
            {
                if (value < loc || value > top) throw new SystemException("setOffset, invalid offset:" + value);
                base.offset = value;
            }
        }

        public void setOffsetUncheckLock(int offset)
        {
            Offset = (offset);
        }

        public override ByteBuffer clear()
        {
            offset = loc;
            top = loc;
            return this;
        }
    }
}