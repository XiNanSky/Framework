/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:37:29                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    public static partial class StringExtend
    {
        #region Split

        public static string[] SplitOnce(this string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[0];
            int index = src.IndexOf(ch);
            if (index == -1) return new string[] { src };
            string[] array = new string[2];
            array[0] = (index == 0 ? "" : src.Substring(0, index));
            array[1] = (index == src.Length - 1 ? "" : src.Substring(index + 1));
            return array;
        }

        public static string[] Split(this string src, char ch)
        {
            if (src == null) return null;
            if (src.Length == 0) return new string[0];
            int i = 0, j = 0, k = 1;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                i = j + 1;
                k++;
            }
            string[] array = new string[k];
            if (k == 1)
            {
                array[0] = src;
                return array;
            }
            i = j = k = 0;
            while ((j = src.IndexOf(ch, i)) >= 0)
            {
                array[k++] = (i == j ? "" : src.Substring(i, j - i));
                i = j + 1;
            }
            array[k] = (i >= src.Length ? "" : src.Substring(i));
            return array;
        }


        /// <summary> 
        /// 将字符串以行拆分为数组 
        /// </summary>
        public static string[] SplitLine(this string str)
        {
            string[] array = Split(str, '\n');
            for (int k = 0; k < array.Length; k++)
            {
                int i = 0;
                int j = array[k].Length;
                if (j != 0)
                {
                    if (array[k][0] == '\r')
                    {
                        i++;
                        j--;
                    }
                    if (array[k][array[k].Length - 1] == '\r') j--;
                    if (j <= 0)
                        array[k] = "";
                    else if (j < array[k].Length) array[k] = array[k].Substring(i, j);
                }
            }
            return array;
        }

        #endregion
    }
}