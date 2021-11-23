/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-04
* Time:             23:41:52
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;

    /// <summary> 
    /// 颜色工具类
    /// </summary>
    public partial class ColorKit
    {
        /// <summary>
        /// 颜色 R G B A
        /// </summary>
        public static Color NewColor(int red, int green, int blue, int alpha)
        {
            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }

        /// <summary> 
        /// #FFFFFF 转换为 16进制
        /// </summary>
        public static string ToHex(int red, int green, int blue, int alpha)
        {// RGBA 顺序不可改
            CharBuffer cb = new CharBuffer("#");
            HexKit.ToHex((byte)red, cb);
            HexKit.ToHex((byte)green, cb);
            HexKit.ToHex((byte)blue, cb);
            HexKit.ToHex((byte)alpha, cb);
            return cb.GetString();
        }

        public static Color ParseHtmlString(string htmlString)
        {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString(htmlString, out color);
            return color;
        }

        /// <summary>
        /// hex转换到color
        /// </summary>
        public static Color HexToColor(string hex)
        {
            if (hex == null || hex.Length < 6) return Color.white;
            var tempVar = System.Globalization.NumberStyles.HexNumber;
            byte br = byte.Parse(hex.Substring(0, 2), tempVar);
            byte bg = byte.Parse(hex.Substring(2, 2), tempVar);
            byte bb = byte.Parse(hex.Substring(4, 2), tempVar);
            //byte cc = byte.Parse(hex.Substring(6, 2), tempVar);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            //float a = cc / 255f;
            return new Color(r, g, b);
        }
    }
}