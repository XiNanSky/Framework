/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit.Extend 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-02
* Time:             19:49:43
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;

    public enum Direction
    {
        [Label("左下")] LeftButtom = 0,
        [Label("正下")] Buttom,
        [Label("右下")] RightButtom,
        [Label("正右")] Right,
        [Label("右上")] RightTop,
        [Label("正上")] Top,
        [Label("左上")] LeftTop,
        [Label("正左")] Left
    }

    /// <summary> </summary>
    public static class VectorExtend
    {
        /// <summary>
        /// 计算方向
        /// </summary>
        public static Direction GetDirection(this Vector2 start, Vector2 end)
        {
            Direction direction = Direction.Buttom;
            Vector3 dir = end - start; //位置差，方向  
            float y = Vector3.Dot(Vector3.down, dir.normalized);//点乘判断前后： dot >0在前，<0在后
            float x = Vector3.Dot(Vector3.right, dir.normalized);//点乘判断左右：dot1>0在右，<0在左
            float angle = Mathf.Acos(Vector3.Dot(Vector3.down.normalized, dir.normalized)) * Mathf.Rad2Deg;//通过点乘求出夹角  

            if (angle >= 0 && angle < 22.5f)
            {
                if (y > 0) direction = Direction.Buttom;       //正下
            }
            else if (angle <= 180 && angle > 157.5f)
            {
                if (y < 0) direction = Direction.Top;          //正上
            }
            else if (angle < 112.5f && angle > 67.5f)
            {
                if (x > 0) direction = Direction.Right;        //正右
                else if (x < 0) direction = Direction.Left;    //正左
            }
            else
            {
                if (angle >= 112.5f && angle <= 157.5f)
                {
                    if (x > 0) direction = Direction.RightTop;       //上右
                    else if (x < 0) direction = Direction.LeftTop;   //上左
                }
                else if (angle >= 22.5f && angle <= 67.5f)
                {
                    if (x > 0) direction = Direction.RightButtom;       //上右
                    else if (x < 0) direction = Direction.LeftButtom;   //上左
                }
            }
            return direction;
        }
    }
}