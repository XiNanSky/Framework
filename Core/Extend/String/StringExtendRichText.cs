/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:38:28                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    public static partial class StringExtend
    {

        #region RichText

        /// <summary> 
        /// 富文本 字号 
        /// </summary>
        /// <param name="s">字号大小</param>
        public static string RichSize(this string content, int s)
        {
            return string.Concat("<size=", s, ">", content, "</size>");
        }

        /// <summary> 
        /// 富文本 颜色 
        /// </summary>  
        /// <param name="c">颜色值</param>
        public static string RichColor(this string content, string c)
        {
            return string.Concat("<color=", c, ">", content, "</color>");
        }

        /// <summary> 
        /// 富文本 斜体 
        /// </summary>  
        public static string RichItalic(this string content)
        {
            return string.Concat("<i>", content, "</i>");
        }

        /// <summary> 
        /// 富文本 加粗 
        /// </summary>  
        public static string RichBold(this string content)
        {
            return string.Concat("<b>", content, "</b>");
        }

        /// <summary> 
        /// 富文本 加粗 斜体 
        /// </summary>  
        public static string RichBoldItalic(this string content)
        {
            return string.Concat("<i><b>", content, "</b></i>");
        }

        /// <summary> 
        /// 富文本 字号 加粗 
        /// </summary>
        /// <param name="s">字号大小</param>
        public static string RichSizeBold(this string content, string s)
        {
            return string.Concat("<b><size=", s, ">", content, "</size></b>");
        }

        /// <summary> 
        /// 富文本 字号 斜体 
        /// </summary>
        /// <param name="s">字号大小</param>
        public static string RichSizeItalic(this string content, int s)
        {
            return string.Concat("<i><size=", s, ">", content, "</size></i>");
        }

        /// <summary> 
        /// 富文本 字号 颜色 
        /// </summary>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichSizeColor(this string content, int s, string c)
        {
            return string.Concat("<size=", s, "><color=", c, ">", content, "</color></size>");
        }

        /// <summary> 
        /// 富文本 颜色 加粗 
        /// </summary>  
        /// <param name="c">颜色值</param>
        public static string RichColorBold(this string content, string c)
        {
            return string.Concat("<b><color=", c, ">", content, "</color></b>");
        }

        /// <summary> 
        /// 富文本 颜色 斜体 
        /// </summary>  
        /// <param name="c">颜色值</param>
        public static string RichColorItalci(this string content, string c)
        {
            return string.Concat("<i><color=", c, ">", content, "</color></i>");
        }

        /// <summary> 
        /// 富文本 字号 加粗 斜体 
        /// </summary>
        /// <param name="s">字号大小</param>
        public static string RichSizeBoldItalic(this string content, string s)
        {
            return string.Concat("<i><b><size=", s, ">", content, "</size></b></i>");
        }

        /// <summary> 
        /// 富文本 字号 颜色 加粗 
        /// </summary>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichSizeColorBold(this string content, int s, string c)
        {
            return string.Concat("<b><size=", s, "><color=", c, ">", content, "</color></size></b>");
        }

        /// <summary> 
        /// 富文本 字号 颜色 斜体 
        /// </summary>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichSizeColorItalic(this string content, int s, string c)
        {
            return string.Concat("<i><size=", s, "><color=", c, ">", content, "</color></size></i>");
        }

        /// <summary> 
        /// 富文本 颜色 加粗 斜体 
        /// </summary>  
        /// <param name="c">颜色值</param>
        public static string RichColorBoldItalic(this string content, string c)
        {
            return string.Concat("<i><b><color=", c, ">", content, "</color></b></i>");
        }

        /// <summary> 
        /// 富文本 字号 颜色 加粗 斜体 
        /// </summary>
        /// <param name="s">字号大小</param>
        /// <param name="c">颜色值</param>
        public static string RichAll(this string content, int s, string c)
        {
            return string.Concat("<i><b><size=", s, "><color=", c, ">", content, "</color></size></b></i>");
        }

        #endregion

    }
}