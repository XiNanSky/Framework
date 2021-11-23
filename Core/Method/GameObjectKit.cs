/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-08-31                    *
* Nowtime:           22:44:03                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;

    public class GameObjectKit
    {

        /// <summary> 
        /// 生成一个游戏物体 添加组件 设置父物体 初始位置 
        /// </summary>
        public static GameObject CreateGameObject(Transform target, Vector3 vector)
        {
            GameObject obj = new GameObject();
            obj.SetParent(target, false);
            obj.transform.localPosition = vector;
            return obj;
        }

        /// <summary> 
        /// 生成一个游戏物体 添加组件 设置父物体 
        /// </summary>
        public static GameObject CreateAddComponent<T>(string name, Transform target) where T : Component
        {
            GameObject obj = new GameObject();
            obj.name = name;
            obj.AddComponent<T>();
            obj.SetParent(target, false);
            return obj;
        }

        /// <summary> 
        /// 生成一个游戏物体 添加组件 设置父物体 
        /// </summary>
        public static GameObject CreateAddComponent<T>(string name, Transform target, Vector3 vector) where T : Component
        {
            GameObject obj = new GameObject();
            obj.name = name;
            obj.AddComponent<T>();
            obj.SetParent(target, false);
            obj.transform.localPosition = vector;
            return obj;
        }

        /// <summary> 
        /// 生成一个游戏物体 设置父物体 
        /// </summary>
        public static GameObject CreateGameObject(Transform target)
        {
            GameObject obj = new GameObject();
            obj.SetParent(target, false);
            return obj;
        }

        /// <summary> 
        /// 生成一个游戏物体 设置父物体 
        /// </summary>
        public static GameObject CreateGameObject(string name, Transform target)
        {
            GameObject obj = new GameObject();
            obj.SetParent(target, false);
            obj.name = name;
            return obj;
        }

        /// <summary> 
        /// 生成一个游戏物体 添加组件 设置父物体 初始位置 
        /// </summary>
        public static GameObject CreateGameObject(string name, Transform target, Vector3 vector)
        {
            GameObject obj = new GameObject();
            obj.SetParent(target, false);
            obj.transform.localPosition = vector;
            obj.name = name;
            return obj;
        }
    }
}