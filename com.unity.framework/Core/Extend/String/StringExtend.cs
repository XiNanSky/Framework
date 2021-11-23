/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN                      *
* All rights reserved.                         *
* FileName:         Framework.Kit.extend       *
* Author:           XiNan                      *
* Version:          0.1                        *
* UnityVersion:     2019.2.18f1                *
* Date:             2020-05-21                 *
* Time:             02:43:44                   *
* E-Mail:           1398581458@qq.com          *
* Description:                                 *
* History:                                     *
* * * * * * * * * * * * * * * * * * * * * * * **/

namespace Framework
{
    using System.Text;

    /*
    1)、Length：获得当前字符串中字符的个数
    2)、ToUpper():将字符转换成大写形式
    3)、ToLower():将字符串转换成小写形式
    4)、Equals(lessonTwo,StringComparison.OrdinalIgnoreCase):比较两个字符串，可以忽略大小写
    5)、Split()：分割字符串，返回字符串类型的数组。注：第二个参数为：StringSplitOptions.RemoveEmptyEntries 时表示移除空格。
    6)、Substring()：截取字符串。在截取的时候包含要截取的那个位置。
    7)、IndexOf():判断某个字符串在字符串中第一次出现的位置，如果没有返回-1、值类型和引用类型在内存上存储的地方不一样。
    8)、LastIndexOf()：判断某个字符串在字符串中最后一次出现的位置，如果没有同样返回-1
    9)、StartsWith():判断是否以....开始
    10)、EndsWith():判断是否以...结束.
    11)、Replace():将字符串中某个字符串替换成一个新的字符串
    12)、Contains():判断某个字符串是否包含指定的字符串
    13)、Trim():去掉字符串中前后的空格
    14)、TrimEnd()：去掉字符串中结尾的空格
    15)、TrimStart()：去掉字符串中前面的空格
    16)、string.IsNullOrEmpty():判断一个字符串是否为空或者为null
    17)、string.Join()：将数组按照指定的字符串连接，返回一个字符串。
    */

    /// <summary> 字符工具类 </summary>
    public static partial class StringExtend
    {
        private static StringBuilder builder = new StringBuilder();

        #region  Add Str Or Char

        /// <summary> 
        /// 在最前添加指定字符到指定长度
        /// </summary>
        public static string AddCharToLenAtFront(this int s, int len, char c = ' ')
        {
            return string.Concat(c.ToString().Clone(c, len - s.ToString().Length), s);
        }

        /// <summary> 
        /// 在最后添加指定字符到指定长度
        /// </summary>
        public static string AddCharToLenAtLast(this int s, int len, char c = ' ')
        {
            return string.Concat(s, c.ToString().Clone(c, len - s.ToString().Length));
        }

        /// <summary> 
        /// 在最前添加指定字符到指定长度
        /// </summary>
        public static string AddCharToLenAtFront(this string s, int len, char c = ' ')
        {
            return string.Concat(c.ToString().Clone(c, len - s.Length), s);
        }

        /// <summary> 
        /// 在最后添加指定字符到指定长度
        /// </summary>
        public static string AddCharToLenAtLast(this string s, int len, char c = ' ')
        {
            return string.Concat(s, c.ToString().Clone(c, len - s.Length));
        }

        /// <summary> 
        /// 在最前添加指定字符
        /// </summary>
        public static string AddCharAtFront(this string s, char c = ' ', int num = 1)
        {
            if (num <= 0) return s;
            return string.Concat(c.Clone(c, num), s);
        }

        /// <summary> 
        /// 在最前添加指定字符
        /// </summary>
        public static string AddCharAtFront(this string s, char[] c)
        {
            return string.Concat(c, s);
        }

        /// <summary> 在最后添加空字符 </summary>
        public static string AddCharAtLast(this string s, char c = ' ', int num = 1)
        {
            if (num <= 0) return s;
            return string.Concat(s, c.Clone(c, num));
        }

        /// <summary> 
        /// 在最后添加空字符
        /// </summary>
        public static string AddCharAtLast(this string s, char[] c)
        {
            return string.Concat(s, c);
        }

        #endregion

        /// <summary> 字符串反转 </summary>
        public static string Reverse(this string value)
        {
            char[] a = value.ToCharArray();
            int l = a.Length;
            for (int i = 0; i < l / 2; i++)
            {
                char temp = a[i];
                a[i] = a[l - 1 - i];
                a[l - 1 - i] = temp;
            }
            return new string(a);
        }

        #region Clone

        /// <summary> 重复N此 复制传入数据 </summary>
        public static string Clone(this string s, string c, int num = 0)
        {
            if (num <= 0) return s;
            return Clone(string.Concat(s + c), c, num - 1);
        }

        /// <summary> 重复N此 复制传入数据 </summary>
        public static string Clone(this string s, char c, int num = 0)
        {
            if (num <= 0) return s;
            return Clone(string.Concat(s + c), c, num - 1);
        }

        /// <summary> 重复N此 复制传入数据 </summary>
        public static string Clone(this char s, char c, int num = 0)
        {
            if (num <= 0) return s.ToString();
            return Clone(string.Concat(s + c), c, num - 1);
        }

        /// <summary> 重复N此 复制传入数据 </summary>
        public static string Clone(this char s, string c, int num = 0)
        {
            if (num <= 0) return s.ToString();
            return Clone(string.Concat(s + c), c, num - 1);
        }

        #endregion

        #region Append

        /// <summary> 
        /// 合并字符 后面
        /// </summary>
        public static string AppendLast(this string s, params object[] c)
        {
            builder.Clear();
            builder.Append(s);
            foreach (var item in c)
            {
                builder.Append(item.ToString());
            }
            return builder.ToString();
        }

        /// <summary> 合并字符 前面 </summary>
        public static string AppendFront(this string s, params string[] c)
        {
            builder.Clear();
            foreach (var item in c)
            {
                builder.Append(item.ToString());
            }
            builder.Append(s);
            return builder.ToString();
        }

        #endregion

        #region To Format

        /// <summary> 
        /// 转换为格式:00,000,000
        /// </summary>
        public static string ToStringMoney(this long value)
        {
            long tmp = value % 1000;
            string str = tmp.ToString();
            value /= 1000;
            while (value > 0)
            {
                if (tmp >= 0 && tmp < 10) str.AppendLast("00");
                else if (tmp >= 10 && tmp < 100) str = "0" + str;
                tmp = value % 1000;
                str = tmp + "," + str;
                value /= 1000;
            }
            return str;
        }

        #endregion
    }
}