/***************************************************
* Copyright(C) 2019 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2019-05-16                    *
* Nowtime:           19:38:52                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;

    /// <summary>
    /// Color扩展
    /// </summary>
    public static class ColorExtend
    {
        /// <summary> 
        /// 16进制 转换为 #FFFFFF
        /// </summary>
        public static string ToHtmlSting(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        /// <summary> 
        /// 16进制 转换为 #FFFFFF
        /// </summary>
        public static string ToHex(this Color32 color)
        {// RGBA 顺序不可改
            CharBuffer cb = new CharBuffer("#");
            HexKit.ToHex(color.r, cb);
            HexKit.ToHex(color.g, cb);
            HexKit.ToHex(color.b, cb);
            HexKit.ToHex(color.a, cb);
            return cb.GetString();
        }


        #region Color GetChange

        public static Color GetChangeA(this Color color, float a)
        {
            return new Color(color.r, color.g, color.b, a);
        }

        public static Color GetChangeR(this Color color, float r)
        {
            return new Color(r, color.g, color.b, color.a);
        }

        public static Color GetChangeG(this Color color, float g)
        {
            return new Color(color.r, g, color.b, color.a);
        }

        public static Color GetChangeB(this Color color, float b)
        {
            return new Color(color.r, color.g, b, color.a);
        }

        #endregion

        #region Color32 GetChange

        public static Color32 GetChangeA(this Color32 color, byte a)
        {
            return new Color32(color.r, color.g, color.b, a);
        }

        public static Color32 GetChangeR(this Color32 color, byte r)
        {
            return new Color32(r, color.g, color.b, color.a);
        }

        public static Color32 GetChangeG(this Color32 color, byte g)
        {
            return new Color32(color.r, g, color.b, color.a);
        }

        public static Color32 GetChangeB(this Color32 color, byte b)
        {
            return new Color32(color.r, color.g, b, color.a);
        }

        #endregion     
    }
}