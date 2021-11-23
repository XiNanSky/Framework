/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           14:12:05                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using Framework.Extend;
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// 类 对象池
    /// </summary>
    public class ObjPoolKit
    {
        /// <summary>
        /// 依据ID 缓存类对象
        /// </summary>
        private static readonly ConcurrentDictionary<int, ConcurrentQueue<object>> mCaches = new ConcurrentDictionary<int, ConcurrentQueue<object>>();

        /// <summary>
        /// 从对象池中获取一个新的类对象
        /// </summary>
        public static object New(Type type)
        {
            int key = type.GetHashCode();
            if (!mCaches.TryGetValue(key, out ConcurrentQueue<object> objects))
            {
                objects = new ConcurrentQueue<object>();
                mCaches.TryAdd(key, objects);
            }
            if (!objects.TryDequeue(out object result))
            {
                result = Activator.CreateInstance(type);
            }
            return result.Reset();
        }

        /// <summary>
        /// 从对象池中获取一个新的类对象
        /// </summary>
        public static T New<T>()
        {
            return (T)New(typeof(T));
        }

        /// <summary>
        /// 手动释放 缓存进入对象池
        /// </summary>
        public static void Release(object result)
        {
            InternalRelease(result.GetType().GetHashCode(), result);
        }

        /// <summary>
        /// 手动释放 缓存进入对象池
        /// </summary>
        public static void Release<T>(T result)
        {
            InternalRelease(typeof(T).GetHashCode(), result);
        }

        /// <summary>
        /// 手动释放
        /// </summary>
        private static void InternalRelease(int key, object result)
        {
            if (!mCaches.TryGetValue(key, out ConcurrentQueue<object> objects))
            {
                objects = new ConcurrentQueue<object>();
                mCaches.TryAdd(key, objects);
            }
            objects.Enqueue(result);
        }
    }

    /// <summary>
    /// 类 对象池 限指定类型
    /// </summary>
    /// <typeparam name="T">指定类型</typeparam>
    public class ObjPoolKit<T>
    {
        private ConcurrentQueue<T> mCached = new ConcurrentQueue<T>();

        public int Count => mCached.Count;

        public T New()
        {
            if (!mCached.TryDequeue(out T result))
            {
                result = (T)Activator.CreateInstance(typeof(T));
            }
            return result;
        }

        public void Release(T result)
        {
            mCached.Enqueue(result);
        }
    }
}