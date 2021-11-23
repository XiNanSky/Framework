/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-03                    *
* Nowtime:           18:11:26                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 程序集
    /// </summary>
    public static class AssemblyKit
    {
        /// <summary>
        /// 获取程序集
        /// </summary>
        public static Assembly Load(string AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }

        /// <summary>
        /// 获取类
        /// </summary>
        public static Type GetType(string AssemblyName, string TypeName)
        {
            return Load(AssemblyName).GetType(TypeName);
        }

        /// <summary>
        /// 获取类
        /// </summary>
        public static Type GetType(Assembly AssemblyName, string TypeName)
        {
            return AssemblyName.GetType(TypeName);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(string AssemblyName, string TypeName, string MethodName)
        {
            return GetType(AssemblyName, TypeName).GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(Assembly AssemblyName, string TypeName, string MethodName)
        {
            return GetType(AssemblyName, TypeName).GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo<T>(T TypeName, string MethodName) where T : Type
        {
            return TypeName.GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }
    }
}