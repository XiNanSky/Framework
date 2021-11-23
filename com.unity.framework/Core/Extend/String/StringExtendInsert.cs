/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:34:44                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;

    public static partial class StringExtend
    {
        /// <summary> 循环插入指定内容 </summary>
        public static string InsertFixed(this string str, int num, string info)
        {
            if (str.Length == 0 || num <= 0) return str;
            int h = (int)Math.Ceiling((double)(str.Length / num));
            for (int i = 1, index = 0, len; i <= h; i++)
            {
                len = index * info.Length + i * num;
                if (len < str.Length)
                {
                    str = str.Insert(len, info);
                    ++index;
                }
            }
            return str;
        }

        /// <summary> 循环插入指定内容 </summary>
        public static string InsertFixed(this string str, int num, char info)
        {
            if (str.Length == 0 || num <= 0) return str;
            int h = (int)Math.Ceiling((double)(str.Length / num));
            int infoLen = info == char.MinValue ? 0 : 1;
            for (int i = 1, index = 0, len; i <= h; i++)
            {
                len = index * infoLen + i * num;
                if (len < str.Length)
                {
                    str = str.Insert(len, info.ToString());
                    ++index;
                }
            }
            return str;
        }
    }
}