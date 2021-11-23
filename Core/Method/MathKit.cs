/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-02
* Time:             01:11:34
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Kit
{
    using System;
    using UnityEngine;

    public enum AngleFixed
    {
        [Label("180")] DegreesA180 = 180,
        [Label("135")] DegreesA135 = 135,
        [Label("90")] DegreesA90 = 90,
        [Label("45")] DegreesA45 = 45,

        [Label("0")] Zero = 0,

        [Label("-45")] DegreesS45 = -45,
        [Label("-90")] DegreesS90 = -90,
        [Label("-135")] DegreesS135 = -135,
        [Label("-180")] DegreesS180 = -180,
    }

    /// <summary> 常用方法 </summary>
    public class MathKit
    {
        private static System.Random _random = new System.Random();

        /// <summary> 返回一个指定范围内的随机数。[min,max) </summary>
        /// <param name="min">返回的随机数的下界 随机数可取该下界值</param>
        /// <param name="max">返回的随机数的上界 随机数不能取该上界值 maxValue 必须大于等于 minValue </param>
        public static int Random(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary> 两点距离 </summary>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return Abs(x1 - x2) + Abs(y1 - y2);
        }

        /// <summary> 两点距离 </summary>
        public static float Distance(Vector2 one, Vector2 two)
        {
            return Abs(one.x - two.x) + Abs(one.y - two.y);
        }

        /// <summary> 矩形相交 </summary>
        public static bool IsRect(Rect one, Rect two)
        {
            Vector2 point = new Vector2(two.x, two.y);//左上角
            for (int i = 0; i < 4; i++)
            {
                if (one.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(two.x, two.y + two.height);
                else if (i == 1)
                    point = new Vector2(two.x + two.width, two.y);
                else if (i == 2)
                    point = new Vector2(two.x + two.width, two.y + two.width);
            }
            point = new Vector2(one.x, one.y);
            for (int i = 0; i < 4; i++)
            {
                if (two.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(one.x, one.y + one.height);
                else if (i == 1)
                    point = new Vector2(one.x + one.width, one.y);
                else if (i == 2)
                    point = new Vector2(one.x + one.width, one.y + one.width);
            }
            return false;
        }

        /// <summary> 矩形相交 </summary>
        public static bool IsRect(float x1, float y1, float width1, float height1, float x2, float y2, float width2, float height2)
        {
            Rect rect1 = new Rect(x1, y1, width1, height1);
            Rect rect2 = new Rect(x2, y2, width2, height2);
            Vector2 point = new Vector2(x2, y2);//左上角
            for (int i = 0; i < 4; i++)
            {
                if (rect1.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(x2, y2 + height2);
                else if (i == 1)
                    point = new Vector2(x2 + width2, y2);
                else if (i == 2)
                    point = new Vector2(x2 + width2, y2 + width2);
            }
            point = new Vector2(x1, y1);
            for (int i = 0; i < 4; i++)
            {
                if (rect2.Contains(point))
                    return true;
                if (i == 0)
                    point = new Vector2(x1, y1 + height1);
                else if (i == 1)
                    point = new Vector2(x1 + width1, y1);
                else if (i == 2)
                    point = new Vector2(x1 + width1, y1 + width1);
            }
            return false;
        }

        /// <summary> 根据余弦值求角度 </summary>
        public static float Sin(float f)
        {
            return Mathf.Sin(f * Mathf.Deg2Rad);
        }

        /// <summary> 根据余弦值求角度 </summary>
        public static float Asin(float f)
        {
            return Mathf.Asin(f) * Mathf.Rad2Deg;
        }

        /// <summary> 根据角度求余弦值 </summary>
        public static float Cos(float f)
        {
            return Mathf.Cos(f * Mathf.Deg2Rad);
        }

        /// <summary> 根据余弦值求角度 </summary>
        public static float Acos(float f)
        {
            return Mathf.Acos(f) * Mathf.Rad2Deg;
        }

        /// <summary> 绝对值 </summary>
        public static int Abs(int value)
        {
            return Mathf.Abs(value);
        }
        /// <summary> 绝对值 </summary>
        public static float Abs(float value)
        {
            return Mathf.Abs(value);
        }

        public static long Abs(long value)
        {
            return (long)Mathf.Abs(value);
        }

        /// <summary> 四舍五入 </summary>
        public static int Round(float value)
        {
            return (int)Mathf.Round(value);
        }

        /// <summary> 大于等于该数字的最接近的整数 </summary>
        public static int Ceil(float value)
        {
            return (int)Mathf.Ceil(value);
        }
        /// <summary> 小于等于指定数字的最接近的整数 </summary>
        public static int Floor(float value)
        {
            return (int)Mathf.Floor(value);
        }


        /// <summary> 余数(正) </summary>
        public static int Mod(int value, int space)
        {
            value %= space;
            if (value < 0) value += space;
            return value;
        }

        private const double EARTH_RADIUS = 6378137;

        /// <summary>  
        /// 计算两个经纬度之间的距离
        /// 计算两点位置的距离，返回两点的距离，单位 米  
        /// 该公式为GOOGLE提供，误差小于0.2米  
        /// </summary>  
        /// <param name="lat1">第一点纬度</param>  
        /// <param name="lng1">第一点经度</param>  
        /// <param name="lat2">第二点纬度</param>  
        /// <param name="lng2">第二点经度</param>  
        public static double GetGPSDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary> 经纬度转化成弧度 </summary>  
        private static double Rad(double d)
        {
            return d * Math.PI / 180d;
        }

        /// <summary> 获取距离,大于1000时，显示km </summary>
        public static string GetDistance(double value)
        {
            if (value < 0) return "0m";
            if (value > 1000)
            {
                value = value / 1000;
                value = Math.Round(value, 2);
                return value + "km";
            }
            else
            {
                value = Math.Round(value);
                return value + "m";
            }
        }

        /// <summary> 转换成千,万 </summary>
	    public static string GetDecimal(long number)
        {
            long temp = Math.Abs(number);//取绝对值
            if (temp < 100000) return number.ToString();
            if (temp >= 100000000)
                return number / 100000000 + (number / 10000000 % 100 * 0.01) + "亿";//大于1亿保留到百万位
            if (temp > 10000000)
                return number / 10000 + (number / 1000 % 10 * 0.1) + "万";          //大于1000W保留到千位
            if (temp >= 100000)
                return number / 10000 + (number / 100 % 100 * 0.01) + "万";         //大于1W 保留到百位
            return "";
        }
    }
}