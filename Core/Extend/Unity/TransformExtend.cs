/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved.
* FileName:         Framework.Kit.extend
* Author:           XiNan
* Version:          0.1
* UnityVersion:     2019.2.18f1
* Date:             2020-04-08
* Time:             22:47:47
* E-Mail:           1398581458@qq.com
* Description:      Vector3 扩展方法
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System.Collections.Generic;
    using System.Text;

    using UnityEngine;

    public static class TransformExtend
    {
        /// <summary>
        /// 获取所有子物体
        /// </summary>
        public static List<Transform> GetAllChilds(this Transform trans)
        {
            List<Transform> objs = new List<Transform>();
            for (int i = 0; i < trans.childCount; i++)
            {
                objs.Add(trans.GetChild(i));
            }
            return objs;
        }

        /// <summary>
        /// 获取全部子物体相同组件
        /// </summary>
        public static List<T> GetAllChildComponents<T>(this Transform trans) where T : Component
        {
            List<T> list = new List<T>();
            foreach (var item in trans.GetAllChilds())
            {
                foreach (var comp in item.GetComponents<T>())
                {
                    list.Add(comp);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取子节点 (对象) 脚本
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T GetTheChildNodeComponetScripts<T>(GameObject goParent, string childName) where T : Component
        {
            return goParent.FindTheChildNode(childName)?.GetComponent<T>();            //查找特定子节点
        }

        /// <summary>
        /// 销毁全部子物体
        /// </summary>
        public static void DestroyAllChlids(this Transform obj)
        {
            if (obj == null) return;
            while (obj?.transform.childCount > 0)
            {
                //依次从第一个开始销毁 如果第一个子物体销毁完成 则销毁第二个
                if (obj.transform.GetChild(0).childCount == 0)
                    obj.transform.GetChild(0).Destroy();
                else obj.transform.GetChild(0).DestroyAllChlids();
            }
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetAllLayer(this Transform tran, int layer)
        {
            tran.gameObject.layer = layer;
            foreach (Transform child in tran)
            {
                child.SetAllLayer(layer);
            }
        }

        /// <summary>
        /// 设置自己及其子对象的所属标签
        /// </summary>
        public static void SetAllTag(this Transform trans, string tag)
        {
            trans.gameObject.tag = tag;
            foreach (Transform item in trans)
            {
                item.gameObject.tag = tag;
                if (item.childCount != 0)
                {
                    SetAllTag(item, tag);
                }
            }
        }

        /// <summary>
        /// 返回找到指定父物体为止的索引下标
        /// </summary>
        /// <param name="name">父物体名称</param>
        public static int[] GetTransformSiblingIndex(this Transform transform, string name)
        {
            if (transform != null && transform != default)
            {
                var str = new StringBuilder();
                var trans = transform;
                while (trans.name != name)
                {
                    str.Append(trans.GetSiblingIndex());
                    trans = trans.parent;
                    if (trans == null || trans.name == name) break;
                    str.Append('.');
                }
                var strs = str.ToString().Split('.');
                var indexs = new int[strs.Length];
                for (int i = strs.Length - 1; i >= 0; i--)
                {
                    indexs[strs.Length - i - 1] = strs[i].TOInt();
                }
                return indexs;
            }
            return new int[] { };
        }

        /// <summary>
        /// 根据索引 获取当前物体下指定物体的子物体
        /// </summary>
        public static Transform TransformSiblingIndexToObj(this Transform trans, int[] index)
        {
            if (index.Length == 0) return default;
            for (int i = 0; i < index.Length; i++)
            {
                trans = trans.GetChild(index[i]);
            }
            return trans;
        }

        /// <summary>
        /// 设置物体的Scale X
        /// </summary>
        public static void SetScaleX(this Transform tran, float value)
        {
            Vector3 scale = tran.localScale;
            scale.x = value;
            tran.localScale = scale;
        }

        /// <summary>
        /// 设置物体的Scale Y
        /// </summary>
        public static void SetScaleY(this Transform tran, float value)
        {
            Vector3 scale = tran.localScale;
            scale.y = value;
            tran.localScale = scale;
        }

        /// <summary>
        /// 设置物体的Scale XY
        /// </summary>
        public static void SetScaleXY(this Transform tran, float value)
        {
            Vector3 scale = tran.localScale;
            scale.x = value;
            scale.y = value;
            tran.localScale = scale;
        }

        /// <summary>
        /// 设置物体的Scale XY
        /// </summary>
        public static void SetScaleXY(this Transform tran, float x, float y)
        {
            Vector3 scale = tran.localScale;
            scale.x = x;
            scale.y = y;
            tran.localScale = scale;
        }

        /* 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2) */

        public static void SetRoate(this Transform tran, float x, float y, float z)
        {
            tran.rotation = Quaternion.Euler(x, y, z);
        }

    }
}
