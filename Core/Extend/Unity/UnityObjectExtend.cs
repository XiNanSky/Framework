/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-08-31                    *
* Nowtime:           23:25:48                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;

    public static class UnityObjectExtend
    {
        /// <summary> 
        /// 销毁物体自身
        /// </summary>
        public static void Destroy<T>(this T obj, float tiems) where T : Object
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Object.Destroy(obj, tiems);
            else Object.DestroyImmediate(obj);
#else
                Object.Destroy(obj, tiems);
#endif
        }

        /// <summary> 
        /// 销毁物体自身
        /// </summary>
        public static void Destroy<T>(this T obj) where T : Object
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Object.Destroy(obj);
            else Object.DestroyImmediate(obj);
#else
                Object.Destroy(obj);
#endif
        }

        /// <summary>
        /// 克隆 设置父物体
        /// </summary>
        public static T Clone<T>(this T obj) where T : Object
        {
            return Object.Instantiate(obj);
        }

        /// <summary>
        /// 克隆 设置父物体
        /// </summary>
        public static T Clone<T>(this T obj, Transform trans) where T : Object
        {
            return Object.Instantiate(obj, trans);
        }
    }
}