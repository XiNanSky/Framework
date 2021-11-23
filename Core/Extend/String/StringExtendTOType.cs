/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:32:43                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using UnityEngine;

    public static partial class StringExtend
    {
        /// <summary> 
        /// #FFFFFF 转换为 16进制
        /// </summary>
        public static Color TOHtmlColor(this string value)
        {
            if (value[0] != '#') value.AddCharAtFront('#');
            ColorUtility.TryParseHtmlString(value, out Color color);
            return color;
        }

        /// <summary> 
        /// #FFFFFF 转换为 16进制
        /// </summary>
        public static Color32 TOColor32(this string hex)
        {
            int v = (int)HexKit.ParseLong(hex, 1, hex.Length - 1);
            if (v > 0xFFFFFF) //带Alpha值
                return new Color32((byte)(v >> 24), (byte)(v >> 16), (byte)(v >> 8), (byte)v);
            else return new Color32((byte)(v >> 16), (byte)(v >> 8), (byte)v, 255);
        }

        /// <summary> 
        /// 解析字符串为 ulong
        /// </summary>
        public static ulong TOULong(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt64(value);
        }

        /// <summary> 
        /// 解析字符串为 UInt
        /// </summary>
        public static uint TOUInt(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt32(value);
        }

        /// <summary> 
        /// 解析字符串为 UShort
        /// </summary>
        public static ushort TOUshort(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToUInt16(value);
        }

        /// <summary> 
        /// 解析字符串为 Double
        /// </summary>
        public static double TODouble(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToDouble(value);
        }

        /// <summary> 
        /// 解析字符串为 Decimal
        /// </summary>
        public static decimal TODecimal(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToDecimal(value);
        }

        /// <summary> 
        /// 解析字符串为 DateTime
        /// </summary>
        public static DateTime TODateTime(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToDateTime(value);
        }

        /// <summary> 
        /// 解析字符串为 Char
        /// </summary>
        public static char TOChar(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToChar(value);
        }

        /// <summary> 
        /// 解析字符串为 Byte
        /// </summary>
        public static byte TOByte(this string value)
        {
            if (value.IsNullOrEmpty()) return default;
            return Convert.ToByte(value);
        }

        /// <summary> 
        /// 解析字符串为 Boolean
        /// </summary>
        public static bool TOBoolean(this string value)
        {
            if (value.IsNullOrEmpty()) return false;
            return Convert.ToBoolean(value);
        }

        /// <summary> 
        /// 解析字符串为 SByte
        /// </summary>
        public static short TOSByte(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            return Convert.ToSByte(value);
        }

        /// <summary> 
        /// 解析字符串为 Short
        /// </summary>
        public static short TOShort(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            return Convert.ToInt16(value);
        }

        /// <summary> 
        /// 解析字符串为 Int
        /// </summary>
        public static int TOInt(this string value)
        {
            if (value.IsNullOrEmpty()) return 0;
            return Convert.ToInt32(value);
        }

        /// <summary> 
        /// 解析字符串为 Long
        /// </summary>
        public static long TOLong(this string value)
        {
            if (value.IsNullOrEmpty()) return 0L;
            return Convert.ToInt64(value);
        }

        /// <summary> 
        /// 解析字符串为 Float
        /// </summary>
        public static float TOFloat(this string value)
        {
            if (value.IsNullOrEmpty()) return 0F;
            return Convert.ToSingle(value);
        }

        /// <summary> 
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        public static int[] TOInts(this string value)
        {
            if (value.IsNullOrEmpty())
                return new int[0];
            string[] strs = value.Split(new char[] { ',' });
            int[] returns = new int[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOInt(strs[i]);
            }
            return returns;
        }

        /// <summary>  
        /// 解析字符串(以,分割)为一维数字数组 
        /// </summary>
        public static int[] TOInts(this string value, char split)
        {
            if (value.IsNullOrEmpty())
                return new int[0];
            string[] strs = value.Split(new char[] { split });
            int[] returns = new int[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOInt(strs[i]);
            }
            return returns;
        }

        /// <summary>  
        /// 解析字符串(以,|分割)为二维数字数组 
        /// </summary>
        public static int[][] TOIntss(this string value)
        {
            if (value.IsNullOrEmpty())
                return new int[0][];
            string[] strs = value.Split(new char[] { '|' });
            int[][] returns = new int[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOInts(strs[i]);
            }
            return returns;
        }

        /// <summary> 
        /// 解析字符串(以,|,:分割)为三维数字数组 
        /// </summary>
        public static int[][][] TOIntsss(this string value)
        {
            if (value.IsNullOrEmpty())
                return new int[0][][];
            string[] strs = value.Split(new char[] { ':' });
            int[][][] returns = new int[strs.Length][][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOIntss(strs[i]);
            }
            return returns;
        }

        public static int[] TOInts(this string[] value)
        {
            int[] array = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOInt(value[i]);
            }
            return array;
        }

        public static int[][] TOIntss(this string[][] value)
        {
            int[][] array = new int[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOInts(value[i]);
            }
            return array;
        }

        public static int[][][] TOIntsss(this string[][][] value)
        {
            int[][][] array = new int[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOIntss(value[i]);
            }
            return array;
        }

        /// <summary> 
        /// 解析字符串(以,分割)为一维数字数组 
        /// </summary>
        public static long[] TOLongs(this string value)
        {
            if (value.IsNullOrEmpty())
                return new long[0];
            string[] strs = value.Split(new char[] { ',' });
            long[] returns = new long[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i].TOLong();
            }
            return returns;
        }

        /// <summary> 
        /// 解析字符串(以,|分割)为二维数字数组 
        /// </summary>
        public static long[][] TOLongss(this string value)
        {
            if (value.IsNullOrEmpty())
                return new long[0][];
            string[] strs = value.Split(new char[] { '|' });
            long[][] returns = new long[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOLongs(strs[i]);
            }
            return returns;
        }

        public static long[] TOLongs(this string[] value)
        {
            long[] array = new long[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOLong(value[i]);
            }
            return array;
        }

        public static long[][] TOLongss(this string[][] value)
        {
            long[][] array = new long[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOLongs(value[i]);
            }
            return array;
        }
        public static long[][][] TOLongsss(this string[][][] value)
        {
            long[][][] array = new long[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOLongss(value[i]);
            }
            return array;
        }

        /// <summary> 
        /// 解析字符串(以,分割)为一维数字数组
        /// </summary>
        public static string[] TOStrings(this string value)
        {
            if (value.IsNullOrEmpty())
                return new string[0];
            string[] strs = value.Split(new char[] { ',' });
            string[] returns = new string[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }
            return returns;
        }

        public static string[] TOStrings(this string value, char split)
        {
            if (value.IsNullOrEmpty())
                return new string[0];
            string[] strs = value.Split(new char[] { split });
            string[] returns = new string[strs.Length];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = strs[i];
            }
            return returns;
        }

        /// <summary> 
        /// 解析字符串(以,|分割)为二维数字数组 
        /// </summary>
        public static string[][] TOStringss(this string value)
        {
            if (value.IsNullOrEmpty())
                return new string[0][];
            string[] strs = value.Split(new char[] { '|' });
            string[][] returns = new string[strs.Length][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOStrings(strs[i]);
            }
            return returns;
        }

        /// <summary> 
        /// 解析字符串(以,|分割)为三维数字数组 
        /// </summary>
        public static string[][][] TOStringsss(this string value)
        {
            if (value.IsNullOrEmpty())
                return new string[0][][];
            string[] strs = value.Split(new char[] { ':' });
            string[][][] returns = new string[strs.Length][][];
            for (int i = 0; i < returns.Length; i++)
            {
                returns[i] = TOStringss(strs[i]);
            }
            return returns;
        }

        /// <summary> 
        /// 解析字符串为指定大小的一维数组 
        /// </summary>
        public static string[] TOStrings(this string value, int size)
        {
            if (value.IsNullOrEmpty() || size <= 0)
                return new string[0];
            if (value.Length <= size)
                return new string[] { value };
            char[] chars = value.ToCharArray();
            CharBuffer buffer = new CharBuffer();

            int count = chars.Length / size;
            int len = chars.Length % size > 0 ? (count + 1) : count;
            string[] strs = new string[len];

            for (int i = 0; i < strs.Length; i++)
            {
                buffer.Clear();
                for (int j = i * size; j < chars.Length && j < size * (i + 1); j++)
                {
                    buffer.Append(chars[j]);
                }
                strs[i] = buffer.GetString();
            }

            return strs;
        }

        /// <summary>
        /// 转换为Bool 一维数组
        /// </summary>
        public static bool[] TOBools(this string[] value)
        {
            bool[] array = new bool[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = value[i].TOBoolean();
            }
            return array;
        }

        /// <summary>
        /// 转换为Bool 二维数组
        /// </summary>
        public static bool[][] TOBoolss(this string[][] value)
        {
            bool[][] array = new bool[value.Length][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOBools(value[i]);
            }
            return array;
        }

        /// <summary>
        /// 转换为Bool 三维数组
        /// </summary>
        public static bool[][][] TOBoolsss(this string[][][] value)
        {
            bool[][][] array = new bool[value.Length][][];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = TOBoolss(value[i]);
            }
            return array;
        }
    }
}