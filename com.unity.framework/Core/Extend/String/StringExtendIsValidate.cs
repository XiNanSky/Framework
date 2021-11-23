/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:35:59                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Text.RegularExpressions;

    public static partial class StringExtend
    {

        #region IsValidate

        /// <summary> 判断字符是否为空 </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary> 
        /// 是否为Numeric
        /// </summary>
        public static bool IsValidateNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary> 
        /// 是否为Int
        /// </summary>
        public static bool IsValidateInt(this string value)
        {
            return Regex.IsMatch(value, "^-?\\d+$");
        }

        /// <summary>
        /// 是否为Bool
        /// </summary>
        public static bool IsValidateBool(this string value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// 是否为整数
        /// </summary>
        public static bool IsValidateNum(string strNum)
        {
            return Regex.IsMatch(strNum, "^[0-9]*$");
        }

        /// <summary> 
        /// 是否为Unsign
        /// </summary>
        public static bool IsValidateUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        /// <summary>
        /// 是否为日期
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static bool IsValidateDate(this string value)
        {
            //验证YYYY-MM-DD格式,基本上把闰年和2月等的情况都考虑进去
            bool bValid = Regex.IsMatch(value, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            return (bValid && value.CompareTo("1753-01-01") >= 0);

            //将平年和闰年的日期验证表达式合并，我们得到最终的验证日期格式为YYYY-MM-DD的正则表达式为：

            //(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|
            //[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-
            //(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|
            //(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|
            //[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        public static bool IsValidateDateString(this string value)
        {
            return Regex.IsMatch(value, @"（\d{4}）-（\d{1,2}）-（\d{1,2}）");
        }

        /// <summary>
        /// 验证文本框输入为电话号码
        /// </summary>
        public static bool IsValidatePhone(this string value)
        {
            return Regex.IsMatch(value, @"\d{3,4}-\d{7,8}");
        }

        /// <summary>
        /// 验证文本框输入为传真号码
        /// </summary>
        public static bool IsValidateFax(this string value)
        {
            return Regex.IsMatch(value, @"86-\d{2,3}-\d{7,8}");
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        public static bool IsValidateIP(this string value)
        {
            return Regex.IsMatch(value, @"^（（2[0-4]\d|25[0-5]|[01]?\d\d?）\.）{3}（2[0-4]\d|25[0-5]|[01]?\d\d?）$");
        }

        /// <summary>
        /// 是否为ip分段
        /// </summary>
        public static bool IsIPSect(this string value)
        {
            return Regex.IsMatch(value, @"^（（2[0-4]\d|25[0-5]|[01]?\d\d?）\.）{2}（（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）\.）（2[0-4]\d|25[0-5]|[01]?\d\d?|\*）$");
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        public static bool IsValidateEmail(this string value)
        {
            return Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary> 
        /// 是否为Tel
        /// </summary>
        public static bool IsValidateTel(this string value)
        {
            return Regex.IsMatch(value, @"\d{3}-\d{8}|\d{4}-\d{7}");
        }

        #endregion

    }
}