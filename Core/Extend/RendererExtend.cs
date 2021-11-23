/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-08-31                    *
* Nowtime:           23:10:48                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;

    /// <summary>
    /// Renderer 扩展
    /// </summary>
    public static class RendererExtend
    {

        /// <summary> 
        /// 计算模型的中心点坐标
        /// </summary>
        public static Vector3 GetMeshFilterCenter<T>(this T trans) where T : Renderer
        {
            return new Bounds(trans.bounds.center, Vector3.zero).center;
        }

        /// <summary> 
        /// 计算模型的中心点坐标
        /// </summary>
        public static Vector3 GetMeshFilterCenter<T>(this T[] trans) where T : Renderer
        {
            if (trans == null || trans.Length == 0) return default;
            Vector3 center = Vector3.zero;
            foreach (var item in trans)
            {
                center += item.bounds.center;
            }
            center /= trans.Length;
            return new Bounds(center, Vector3.zero).center;
        }
    }
}