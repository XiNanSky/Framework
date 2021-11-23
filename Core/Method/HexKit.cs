/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Codec 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          16:35:58 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;

    /// <summary> 
    /// 16进制操作类
    /// </summary>
    public static class HexKit
    {
        /// <summary> 16进制位控制符 </summary>
        public static int HEX_BIT = 0xF;

        /// <summary> 空字节数组 </summary>
        public static byte[] EMPTY_BYTE_ARRAY = { };

        /// <summary> 大写16进制数字字符 </summary>
        public static char[] HEX_DIGIT =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        /// <summary> 小写写16进制数字字符 </summary>
        public static char[] HEX_DIGIT_LOWER =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        /// <summary> 将数字转换为16进制字符 </summary> 
        public static char ToHexChar(int v, bool lower)
        {
            return lower ? HEX_DIGIT_LOWER[v & 0xF] : HEX_DIGIT[v & 0xF];
        }

        /// <summary> 将16进制字符转换为数字 </summary> 
        public static int ToDigit(char c)
        {
            if (c >= '0' && c <= '9') return c - '0';
            if (c >= 'A' && c <= 'F') return 10 + c - 'A';
            if (c >= 'a' && c <= 'f') return 10 + c - 'a';
            throw new SystemException("Invalid HEX char: " + c);
        }

        /// <summary> 将一个char转换为16进制并添加到字节缓存，默认大写字母 </summary> 
        private static void ToHex(char c, CharBuffer cb)
        {
            ToHex(c, cb, false);
        }

        /// <summary> 将一个char转换为16进制并添加到字节缓存 </summary> 
        public static void ToHex(char c, CharBuffer cb, bool lower)
        {
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            cb.Append(table[(c >> 12) & HEX_BIT]);
            cb.Append(table[(c >> 8) & HEX_BIT]);
            cb.Append(table[(c >> 4) & HEX_BIT]);
            cb.Append(table[c & HEX_BIT]);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(byte value)
        {
            return ToHex(value, false);
        }

        /// <summary> 将一个字节转换为16进制 </summary> 
        public static string ToHex(byte value, bool lower)
        {
            char[] chs = new char[2];
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            chs[0] = table[(value >> 4) & HEX_BIT];
            chs[1] = table[value & HEX_BIT];
            return new string(chs);
        }

        /// <summary> 将一个字节转换为16进制 ，并添加到字节缓存 </summary> 
        public static void ToHex(byte value, CharBuffer cb)
        {
            char[] table = HEX_DIGIT;
            cb.Append(table[(value >> 4) & HEX_BIT]);
            cb.Append(table[value & HEX_BIT]);
        }

        /// <summary> 将一个字节转换为16进制 ，并添加到字节缓存 </summary> 
        public static void ToHex(byte value, CharBuffer cb, bool lower)
        {
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            cb.Append(table[(value >> 4) & HEX_BIT]);
            cb.Append(table[value & HEX_BIT]);
        }

        /// <summary> 将指定16进制字符串转换为10进制数字 </summary> 
        public static byte ParseByte(string src)
        {
            return (byte)ParseLong(src);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(short value)
        {
            return ToHex(value, false);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(short value, bool lower)
        {
            char[] chs = new char[4];
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            chs[0] = table[(value >> 12) & HEX_BIT];
            chs[1] = table[(value >> 8) & HEX_BIT];
            chs[2] = table[(value >> 4) & HEX_BIT];
            chs[3] = table[value & HEX_BIT];
            return new string(chs);
        }

        /// <summary> 转换为16进制 </summary> 
        public static void ToHex(short value, CharBuffer buffer)
        {
            ToHex(value, buffer, false);
        }

        /// <summary> 转换为16进制 </summary> 
        public static void ToHex(short value, CharBuffer buffer, bool lower)
        {
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            buffer.Append(table[(value >> 12) & HEX_BIT]);
            buffer.Append(table[(value >> 8) & HEX_BIT]);
            buffer.Append(table[(value >> 4) & HEX_BIT]);
            buffer.Append(table[value & HEX_BIT]);
        }

        /// <summary> 将指定16进制字符串转换为10进制数字 </summary> 
        public static short ParseShort(string src)
        {
            return (short)ParseLong(src);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(int value)
        {
            return ToHex(value, false);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(int value, bool lower)
        {
            char[] chs = new char[8];
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            chs[0] = table[(value >> 28) & HEX_BIT];
            chs[1] = table[(value >> 24) & HEX_BIT];
            chs[2] = table[(value >> 20) & HEX_BIT];
            chs[3] = table[(value >> 16) & HEX_BIT];
            chs[4] = table[(value >> 12) & HEX_BIT];
            chs[5] = table[(value >> 8) & HEX_BIT];
            chs[6] = table[(value >> 4) & HEX_BIT];
            chs[7] = table[value & HEX_BIT];
            return new string(chs);
        }

        /// <summary> 转换为16进制 </summary> 
        public static void ToHex(int value, CharBuffer buffer)
        {
            ToHex(value, buffer, false);
        }

        /// <summary> 转换为16进制 </summary> 
        public static void ToHex(int value, CharBuffer buffer, bool lower)
        {
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            buffer.Append(table[(value >> 28) & HEX_BIT]);
            buffer.Append(table[(value >> 24) & HEX_BIT]);
            buffer.Append(table[(value >> 20) & HEX_BIT]);
            buffer.Append(table[(value >> 16) & HEX_BIT]);
            buffer.Append(table[(value >> 12) & HEX_BIT]);
            buffer.Append(table[(value >> 8) & HEX_BIT]);
            buffer.Append(table[(value >> 4) & HEX_BIT]);
            buffer.Append(table[value & HEX_BIT]);
        }

        /// <summary> 将指定16进制字符串转换为10进制数字 </summary> 
        public static int ParseInt(string src)
        {
            return (int)ParseLong(src);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(long value)
        {
            return ToHex(value, false);
        }

        /// <summary> 转换为16进制 </summary> 
        public static string ToHex(long value, bool lower)
        {
            char[] chs = new char[16];
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            chs[0] = table[(int)((value >> 60) & HEX_BIT)];
            chs[1] = table[(int)((value >> 56) & HEX_BIT)];
            chs[2] = table[(int)((value >> 52) & HEX_BIT)];
            chs[3] = table[(int)((value >> 48) & HEX_BIT)];
            chs[4] = table[(int)((value >> 44) & HEX_BIT)];
            chs[5] = table[(int)((value >> 40) & HEX_BIT)];
            chs[6] = table[(int)((value >> 36) & HEX_BIT)];
            chs[7] = table[(int)((value >> 32) & HEX_BIT)];
            chs[8] = table[(int)((value >> 28) & HEX_BIT)];
            chs[9] = table[(int)((value >> 24) & HEX_BIT)];
            chs[10] = table[(int)((value >> 20) & HEX_BIT)];
            chs[11] = table[(int)((value >> 16) & HEX_BIT)];
            chs[12] = table[(int)((value >> 12) & HEX_BIT)];
            chs[13] = table[(int)((value >> 8) & HEX_BIT)];
            chs[14] = table[(int)((value >> 4) & HEX_BIT)];
            chs[15] = table[(int)(value & HEX_BIT)];
            return new string(chs);
        }

        /// <summary> 将指定16进制字符串转换为10进制数字 </summary> 
        public static long ParseLong(string src)
        {
            if (src == null || src.Length == 0) return 0;
            return ParseLong(src, 0, src.Length);
        }

        /// <summary> 将指定16进制字符串转换为10进制数字 </summary> 
        public static long ParseLong(string src, int offset, int len)
        {
            len += offset;
            if (len > src.Length) len = src.Length;
            if (len > offset + 2 && src[offset] == '0')
            {
                if (src[offset + 1] == 'x' || src[offset + 1] == 'X') offset += 2;
            }
            long value = 0L;
            while (offset < len)
            {
                value = (value << 4) | (ToDigit(src[offset++]) & 0x00000000FFFFFFFFL);
            }
            return value; // &HEX_LONG;
        }

        /// <summary> 将指定16进制字符数组转换为10进制数字 </summary> 
        private static long ParseLong(char[] src, int offset, int len)
        {
            len += offset;
            if (len > src.Length) len = src.Length;
            if (len > offset + 2 && src[offset] == '0')
            {
                if (src[offset + 1] == 'x' || src[offset + 1] == 'X') offset += 2;
            }
            long value = 0L;
            while (offset < len)
            {
                value = (value << 4) | (ToDigit(src[offset++]) & 0x00000000FFFFFFFFL);
            }
            return value; // &HEX_LONG;
        }

        /// <summary> 转换为16进制数据 </summary> 
        public static string ToHex(byte[] bytes)
        {
            return ToHex(bytes, 0, bytes.Length, false);
        }

        /// <summary> 转换为16进制数据 </summary> 
        public static string ToHex(byte[] bytes, int pos, int len)
        {
            return ToHex(bytes, pos, len, false);
        }

        /// <summary> 转换为16进制数据 </summary> 
        public static string ToHex(byte[] bytes, bool lower)
        {
            return ToHex(bytes, 0, bytes.Length, lower);
        }

        /// <summary> 转换为16进制数据 </summary> 
        public static string ToHex(byte[] bytes, int pos, int len, bool lower)
        {
            if (bytes == null || bytes.Length == 0) return "";
            char[] array = new char[len << 1];// 乘以2
            len += pos;
            char[] table = lower ? HEX_DIGIT_LOWER : HEX_DIGIT;
            for (int i = pos, index = 0; i < len; i++)
            {
                array[index++] = table[(bytes[i] >> 4) & HEX_BIT];
                array[index++] = table[bytes[i] & HEX_BIT];
            }
            return new string(array);
        }

        /// <summary> 转换为16进制数据 </summary> 
        public static void ToHex(byte[] bytes, CharBuffer cb)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                ToHex(bytes[i], cb);
            }
        }
    }
}
