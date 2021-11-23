/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           15:01:20                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    /// <summary>
    /// IO 加密
    /// </summary>
    public static partial class IOKit
    {
        /// <summary> 
        /// 字节位运算加密
        /// </summary>
        private static byte EncodingBitByte(byte num)
        {
            int state = (num & 0xFF);
            int n = 2;
            for (int i = 0; i < 8; i++)
            {
                if (n == 64) continue;
                if ((state & n) == n)
                {
                    state &= (~n);
                }
                else
                {
                    state |= n;
                }
                n = n * 2;
            }
            return (byte)state;
        }
    }
}