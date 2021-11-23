/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit.Extend 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-02
* Time:             20:20:45
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;

    /// <summary> </summary>
    public static class RectTransformExtend
    {

        /// <summary> 全局拉伸 </summary>
        public static void StretchAll(this RectTransform tran)
        {
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
            tran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            tran.anchorMin = Vector2.zero;
            tran.anchorMax = Vector2.one;
        }

        /// <summary> 居中向上 </summary>
        public static void StretchCenterTop(this RectTransform tran, Vector2 size)
        {
            tran.pivot = new Vector2(0.5f, 1);
            tran.anchorMin = new Vector2(0.5f, 1);
            tran.anchorMax = new Vector2(0.5f, 1);
            tran.sizeDelta = size;
        }

        /// <summary> 向左向上 </summary>
        public static void StretchLeftTop(this RectTransform tran, Vector2 size)
        {
            tran.anchorMin = new Vector2(0, 1);
            tran.anchorMax = new Vector2(0, 1);
            tran.pivot = new Vector2(0, 1);
            tran.sizeDelta = size;
        }

        /// <summary> 向左向下 </summary>
        public static void StretchLeftButtom(this RectTransform tran, Vector2 size)
        {
            tran.anchorMin = new Vector2(0, 0);
            tran.anchorMax = new Vector2(0, 0);
            tran.pivot = new Vector2(0, 0);
            tran.sizeDelta = size;
        }

        /// <summary> 向右向上 </summary>
        public static void StretchRightTop(this RectTransform tran, Vector2 size)
        {
            tran.anchorMin = Vector2.one;
            tran.anchorMax = Vector2.one;
            tran.pivot = Vector2.one;
            tran.sizeDelta = size;
        }
    }
}