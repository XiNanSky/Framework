/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           18:28:28                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;

    /// <summary>
    /// 单类 同种类元素合计
    /// </summary>
    public abstract class Singleton<T> where T : class, new()
    {
        public static T Instance => SingletonCreator.Instance;

        private class SingletonCreator
        {
            internal static T mInstance;
            public static T Instance
            {
                get
                {
                    if (mInstance == null)
                    {   //如果是引用类型创建一个T实例，如果是值类型返回值的默认值  
                        mInstance = default(T) ?? Activator.CreateInstance<T>();
                    }
                    return mInstance;
                }
                set => mInstance = value;
            }
        }
    }
}