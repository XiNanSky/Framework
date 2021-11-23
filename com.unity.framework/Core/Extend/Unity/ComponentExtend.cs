using System;
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

    /// <summary> </summary>
    public static class ComponentExtend
    {
        /// <summary>
        /// 销毁全部子物体
        /// </summary>
        public static void DestroyAllChlids<T>(this T obj) where T : Component
        {
            while (obj?.transform.childCount <= 0)
            {
                if (obj.transform.GetChild(0).childCount == 0)
                    obj.transform.GetChild(0).gameObject.Destroy();
                else obj.transform.GetChild(0).gameObject.DestroyAllChlids();
            }
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll<T>(this T component, int layer) where T : Component
        {
            if (component == null)
                return;
            component.gameObject.layer = layer;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null)
                return;
            foreach (Transform child in children)
            {
                child.gameObject.layer = layer;
            }
        }

        /// <summary>
        /// 设置全部子对象的所属层级
        /// </summary>
        public static void SetLayerAllChild<T>(this T component, int layer) where T : Component
        {
            if (component == null)
                return;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null)
                return;
            foreach (Transform child in children)
            {
                child.gameObject.layer = layer;
            }
        }

        /// <summary>
        /// 设置自己的所属层级
        /// </summary>
        public static void SetLayer<T>(this T component, int layer) where T : Component
        {
            if (component == null)
                return;
            component.gameObject.layer = layer;
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll<T>(this T component, Enum layer) where T : Component
        {
            if (component == null)
                return;
            component.gameObject.layer = layer.TOInt();
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null)
                return;
            foreach (Transform child in children)
            {
                child.gameObject.layer = layer.TOInt();
            }
        }

        /// <summary>
        /// 设置全部子对象的所属层级
        /// </summary>
        public static void SetLayerAllChild<T>(this T component, Enum layer) where T : Component
        {
            if (component == null)
                return;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null)
                return;
            foreach (Transform child in children)
            {
                child.gameObject.layer = layer.TOInt();
            }
        }

        /// <summary>
        /// 设置自己的所属层级
        /// </summary>
        public static void SetLayer<T>(this T component, Enum layer) where T : Component
        {
            if (component == null)
                return;
            component.gameObject.layer = layer.TOInt();
        }
    }
}
