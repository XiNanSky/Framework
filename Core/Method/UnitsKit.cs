/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-10
* Time:             16:43:54
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using Framework.Kit;
    using System;

    public static class UnitsKit
    {
        public static readonly string[] TextureSuffix = new string[] { ".png", ".bmp", ".jpeg", ".jpg", ".psd" };

        public static readonly string[] VideoSuffix = new string[] { ".mp4", ".avi" };

        public static readonly string[] SoundSuffix = new string[] { ".mp3", ".ogg" };

        public static readonly string[] TextSuffix = new string[] { ".txt", ".json", ".xml" };

        public static readonly string[] ShaderSuffix = new string[] { ".shader" };

        public static readonly string[] FontSuffix = new string[] { ".ttf" };

        public static readonly string[] Animation = new string[] { ".anim" };

        public static readonly string[] Animator = new string[] { ".controller" };

        public static readonly string[] Material = new string[] { ".mat" };

        public static readonly string[] PrefabSuffix = new string[] { ".prefab" };

        public static readonly string[] RenderTexture = new string[] { ".renderTexture" };

        /// <summary> 数字单位:个数组 </summary>
        private static readonly string[] CNSNum = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

        /// <summary> 数字单位:位数组 </summary>
        private static readonly string[] CNSDigit = { "", "十", "百", "千" };

        /// <summary> 数字单位:单位数组 </summary>
        private static readonly string[] CNSUnits = { "", "万", "亿", "万亿" };

        private static readonly CharList str = new CharList(64);               //返回值  

        /// <summary> 
        /// 阿拉伯数字全部转化为中文数字 有单位 传入需全部为数字字符 简体中文 
        /// </summary>
        /// <param name="unitNum">单位截止下标,默认0,1:万后,2:亿后,3:万亿</param>
        public static string ToUnitsCNS(this string num, int unitNum = 0)
        {
            if (num.Length == 0) return num;
            str.Clear();
            var index = 0; //下标
            var intM = num.Length % 4;
            var intK = intM > 0 ? num.Length / 4 + 1 : num.Length / 4;

            for (int i = intK; i > unitNum; i--)
            {
                var intL = (i == intK && intM != 0) ? intM : 4;
                var four = num.Substring(index, intL);                 //得到一组四位数  
                for (int j = 0, n; j < four.Length; j++)               //内层循环在该组中的每一位数上循环 
                {
                    n = Convert.ToInt32(four.Substring(j, 1));         //处理组中的每一位数加上所在的位  
                    if (n == 0)
                    {
                        if (j < four.Length - 1
                            && Convert.ToInt32(four.Substring(j + 1, 1)) > 0
                            && !str.ToString().EndsWith(CNSNum[n]))
                            str.Append(CNSNum[n]);
                    }
                    else
                    {
                        if (!(n == 1
                            && (str.ToString().EndsWith(CNSNum[0]) | str.Length == 0)
                            && j == four.Length - 2))
                            str.Append(CNSNum[n]);
                        str.Append(CNSDigit[four.Length - j - 1]);
                    }
                }
                index += intL;
                if (i < intK)                                            //如果不是最高位的一组,每组最后加上一个单位:",万,",",亿," 等   
                {
                    if (Convert.ToInt32(four) != 0)
                        str.Append(CNSUnits[i - 1]);                     //如果所有4位不全是0则加上单位",万,",",亿,"等 
                }
                else str.Append(CNSUnits[i - 1]);                        //处理最高位的一组,最后必须加上单位 
            }
            return str.ToString();
        }

        /// <summary> 
        /// 阿拉伯数字全部转化为中文数字 无单位 传入需全部为数字字符 简体中文
        /// </summary>
        public static string ToNoUnitsCNS(this string num)
        {
            if (num.Length == 0) return num;
            str.Remove(0, str.Length);
            var index = 0; //下标
            var intM = num.Length % 4;
            var intK = intM > 0 ? num.Length / 4 + 1 : num.Length / 4;

            for (int i = intK; i > 0; i--)
            {
                var intL = (i == intK && intM != 0) ? intM : 4;
                var four = num.Substring(index, intL);                 //得到一组四位数  
                for (int j = 0, n; j < four.Length; j++)               //内层循环在该组中的每一位数上循环 
                {
                    n = Convert.ToInt32(four.Substring(j, 1));         //处理组中的每一位数加上所在的位  
                    if (n == 0)
                    {
                        if (j < four.Length - 1
                            && Convert.ToInt32(four.Substring(j + 1, 1)) > 0
                            && !str.ToString().EndsWith(CNSNum[n]))
                            str.Append(CNSNum[n]);
                    }
                    else
                    {
                        if (!(n == 1
                            && (str.ToString().EndsWith(CNSNum[0]) | str.Length == 0)
                            && j == four.Length - 2))
                            str.Append(CNSNum[n]);
                        str.Append(CNSDigit[four.Length - j - 1]);
                    }
                }
                index += intL;
            }
            return str.ToString();
        }

    }
}