/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved.
* FileName:         Framework.Kit.Extend
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.3.13f1
* Date:             2020-06-02
* Time:             18:56:48
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary> 游戏物体扩展类 </summary>
    public static class GameObjectExtend
    {
        #region Set Parent

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject obj, Transform target, bool worldPositionStays = true)
        {
            obj.transform.SetParent(target, worldPositionStays);
        }

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject obj, GameObject target, bool worldPositionStays = true)
        {
            obj.SetParent(target.transform, worldPositionStays);
        }

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject obj, Component target, bool worldPositionStays = true)
        {
            obj.SetParent(target.transform, worldPositionStays);
        }

        #endregion

        #region Destroy

        /// <summary> 销毁全部子物体 </summary>
        public static void DestroyAllChlids(this GameObject obj)
        {
            obj.transform.DestroyAllChlids();
        }

        #endregion

        #region Clone

        /// <summary>
        /// 克隆
        /// </summary>
        public static GameObject Clone(this GameObject obj)
        {
            return Object.Instantiate(obj);
        }

        /// <summary>
        /// 克隆 设置父物体
        /// </summary>
        public static GameObject Clone(this GameObject obj, Transform transform)
        {
            return Object.Instantiate(obj, transform);
        }

        /// <summary>
        /// 克隆 设置父物体 位置信息 旋转信息
        /// </summary>
        public static GameObject Clone(this GameObject obj, Vector3 postion, Quaternion rotation, Transform transform)
        {
            return Object.Instantiate(obj, postion, rotation, transform);
        }

        #endregion

        #region Component

        /// <summary>
        /// 移除组件
        /// </summary>
        public static void RemoveComponent<T>(this GameObject obj) where T : Component
        {
            Object.Destroy(obj.GetComponent<T>());
        }

        /// <summary>
        /// 移除组件
        /// </summary>
        public static void RemoveComponent(this GameObject obj, string comp)
        {
            Object.Destroy(obj.GetComponent(comp));
        }

        /// <summary>
        /// 移除组件
        /// </summary>
        public static void RemoveComponentImmediate<T>(this GameObject obj) where T : Component
        {
            Object.DestroyImmediate(obj.GetComponent<T>());
        }

        /// <summary> 给子节点添加脚本 </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T AddChildNodeCompnent<T>(this GameObject goParent, string childName) where T : Component
        {   //查找特定节点结果  查找特定子节点
            Transform searchTranform = goParent.FindTheChildNode(childName);
            //如果查找成功,则考虑如果已经有相同的脚本了,则先删除,否则直接添加。
            if (searchTranform != null)
            {   //如果已经有相同的脚本了,则先删除
                T[] componentScriptsArray = searchTranform.GetComponents<T>();
                for (int i = 0; i < componentScriptsArray.Length; i++)
                {
                    if (componentScriptsArray[i] != null)
                    {
                        componentScriptsArray[i].Destroy();
                    }
                }
                componentScriptsArray = null;
                return searchTranform.gameObject.AddComponent<T>();
            }
            else return null;//如果查找不成功,返回Null.
        }

        /// <summary>
        /// 查找子节点对象   内部使用 "递归算法"
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="chiildName">查找的子对象名称</param>
        public static Transform FindTheChildNode(this GameObject goParent, string chiildName)
        {
            Transform searchTrans = goParent.transform.Find(chiildName);//查找结果
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, chiildName);
                    if (searchTrans != null) return searchTrans;
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取子节点 (对象) 脚本
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T GetTheChildNodeComponetScripts<T>(this GameObject goParent, string childName) where T : Component
        {
            return goParent.FindTheChildNode(childName)?.gameObject.GetComponent<T>();            //查找特定子节点
        }

        #endregion

        #region SetLocal

        /// <summary>
        /// 设置本地坐标 X
        /// </summary>
        public static void SetLocalX(this GameObject obj, float x)
        {
            Vector3 point = obj.gameObject.transform.localPosition;
            point.x = x;
            obj.gameObject.transform.localPosition = point;
        }

        /// <summary>
        /// 设置本地坐标 Y
        /// </summary>
        public static void SetLocalY(this GameObject obj, float y)
        {
            Vector3 point = obj.transform.localPosition;
            point.y = y;
            obj.transform.localPosition = point;
        }

        /// <summary>
        /// 设置本地坐标 Z
        /// </summary>
        public static void SetLocalZ(this GameObject obj, float z)
        {
            Vector3 point = obj.transform.localPosition;
            point.z = z;
            obj.transform.localPosition = point;
        }

        /// <summary>
        /// 设置本地坐标 X Y
        /// </summary>
        public static void SetLocalXY(this GameObject obj, float x, float y)
        {
            Vector3 point = obj.transform.localPosition;
            point.x = x;
            point.y = y;
            obj.transform.localPosition = point;
        }

        /// <summary>
        /// 设置本地坐标 X Y Z
        /// </summary>
        public static void SetLocalXYZ(this GameObject obj, float x, float y, float z)
        {
            obj.transform.localPosition = new Vector3(x, y, z);
        }

        /// <summary>
        /// 设置世界坐标 X
        /// </summary>
        public static void SetX(this GameObject obj, float x)
        {
            Vector3 point = obj.transform.position;
            point.x = x;
            obj.transform.position = point;
        }

        /// <summary>
        /// 设置世界坐标 Y
        /// </summary>
        public static void SetY(this GameObject obj, float y)
        {
            Vector3 point = obj.transform.position;
            point.y = y;
            obj.transform.position = point;
        }

        /// <summary>
        /// 设置世界坐标 X Y
        /// </summary>
        public static void SetXY(this GameObject obj, float x, float y)
        {
            Vector3 point = obj.transform.position;
            point.x = x;
            point.y = y;
            obj.transform.position = point;
        }

        /// <summary>
        /// 设置世界坐标 X Y
        /// </summary>
        public static void SetXY(this GameObject obj, float x, float y, float z)
        {
            obj.transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// 设置世界坐标 Z
        /// </summary>
        public static void SetZ(this GameObject obj, float z)
        {
            Vector3 point = obj.transform.position;
            point.z = z;
            obj.transform.position = point;
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll(this GameObject component, int layer)
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
        public static void SetLayerAllChild(this GameObject component, int layer)
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
        public static void SetLayer(this GameObject component, int layer)
        {
            if (component == null)
                return;
            component.gameObject.layer = layer;
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll(this GameObject component, Enum layer)
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
        public static void SetLayerAllChild(this GameObject component, Enum layer)
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
        public static void SetLayer(this GameObject component, Enum layer)
        {
            if (component == null)
                return;
            component.gameObject.layer = layer.TOInt();
        }

        #endregion

    }
}
