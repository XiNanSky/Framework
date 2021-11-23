/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           14:07:48                      *
* Description:                                     *
* History:                                         *
***************************************************/


namespace Framework
{
    using System.Text;

    /* Tips : 转化为16进制字符串。
     * 大写X：ToString("X2")即转化为大写的16进制。
     * 小写x：ToString("x2")即转化为小写的16进制。
     * 2表示输出两位，不足2位的前面补0,如 0x0A 如果没有2,就只会输出0xA
     */

    public static class ByteExtend
    {
        /// <summary>
        /// 字节 转大写16进制字符
        /// </summary>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// 字节数组 转大写16进制字符
        /// </summary>
        public static string ToHex(this byte[] bytes)
        {
            return bytes.ToHex("X2");
        }

        /// <summary>
        /// 自定义转化为字符串
        /// </summary>
        /// <param name="format">匹配格式</param>
        /// <returns>转化结果</returns>
        public static string ToHex(this byte[] bytes, string format)
        {
            var stringBuilder = ObjPoolKit.New<StringBuilder>();
            stringBuilder.Clear();
            foreach (byte b in bytes)
            {
                stringBuilder.Append(b.ToString(format));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从指定字节数组中 获取多个字节转化为大写16进制字符串
        /// </summary>
        /// <param name="offset">开始位置</param>
        /// <param name="count">获取长度</param>
        public static string ToHex(this byte[] bytes, int offset, int count)
        {
            StringBuilder stringBuilder = ObjPoolKit.New<StringBuilder>();
            for (int i = offset; i < offset + count; ++i)
            {
                stringBuilder.Append(bytes[i].ToHex());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        public static string ToStr(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串
        /// </summary>
        public static string ToStr(this byte[] bytes, int offset, int count)
        {
            return Encoding.Default.GetString(bytes, offset, count);
        }

        /// <summary>
        /// 转化为字符串 UTF8格式
        /// </summary>
        public static string Utf8ToStr(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// 获取指定字节数组转化为字符串 UTF8格式
        /// </summary>
        public static string Utf8ToStr(this byte[] bytes, int offset, int count)
        {
            return Encoding.UTF8.GetString(bytes, offset, count);
        }
    }
}