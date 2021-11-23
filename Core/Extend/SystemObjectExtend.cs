/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2020-06-03                    *
* Nowtime:           04:31:16                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Extend
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using UnityEngine;

    /// <summary> 
    /// Object 基类扩展方法
    /// </summary>
    public static class SystemObjectExtend
    {
        /// <summary> 
        /// 打印对象信息,对象可以是数组,维度不限
        /// </summary>
        public static string TOString(this object obj)
        {
            if (obj == null) return "null";
            if (obj is Array)
            {
                StringBuilder builder = ObjPoolKit.New<StringBuilder>();
                char space = '\0';
                foreach (object v in (Array)obj)
                {
                    builder.Append(space).Append(TOString(v));
                    space = ',';
                }
                return builder.Append(']').ToString();
            }
            else return obj.ToString();
        }

        /// <summary> 
        /// 判断是否为空
        /// </summary>
        public static bool IsNull(this object o)
        {
            return o.Equals(null);
        }

        /// <summary> 
        /// 判断是否为 DBNull
        /// </summary>
        public static bool IsDBNull(this object o)
        {
            return Convert.IsDBNull(o); // == DBNull.Value.Equals(value);
        }

        #region TO Type

        /// <summary>
        /// 转换为地址
        /// </summary>
        public static IntPtr TOIntPtr(this object obj)
        {
            var handle = GCHandle.Alloc(obj, GCHandleType.WeakTrackResurrection);
            return GCHandle.ToIntPtr(handle);
        }

        /// <summary> 
        /// 解析字符串为 ulong
        /// </summary>
        public static ulong TOULong(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToUInt64(value);
        }

        /// <summary> 
        /// 解析字符串为 UInt
        /// </summary>
        public static uint TOUInt(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToUInt32(value);
        }

        /// <summary> 
        /// 解析字符串为 UShort
        /// </summary>
        public static ushort TOUshort(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToUInt16(value);
        }

        /// <summary> 
        /// 解析字符串为 Double
        /// </summary>
        public static double TODouble(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToDouble(value);
        }

        /// <summary> 
        /// 解析字符串为 Decimal
        /// </summary>
        public static decimal TODecimal(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToDecimal(value);
        }

        /// <summary> 
        /// 解析字符串为 DateTime
        /// </summary>
        public static DateTime TODateTime(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToDateTime(value);
        }

        /// <summary> 
        /// 解析字符串为 Char
        /// </summary>
        public static char TOChar(this object value)
        {
            if (value.IsNull()) return default;
            return Convert.ToChar(value);
        }

        /// <summary> 
        /// 解析字符串为 Byte
        /// </summary>
        public static byte[] TOBytes<T>(this T value) where T : struct
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, value);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        /// <summary> 
        /// 解析字符串为 Boolean
        /// </summary>
        public static bool TOBoolean(this object value)
        {
            if (value.IsNull()) return false;
            return Convert.ToBoolean(value);
        }

        /// <summary> 
        /// 解析字符串为 SByte
        /// </summary>
        public static short TOSByte(this object value)
        {
            if (value.IsNull()) return 0;
            return Convert.ToSByte(value);
        }

        /// <summary> 
        /// 解析字符串为 Short
        /// </summary>
        public static short TOShort(this object value)
        {
            if (value.IsNull()) return 0;
            return Convert.ToInt16(value);
        }

        /// <summary> 
        /// 解析字符串为 Int
        /// </summary>
        public static int TOInt(this object value)
        {
            if (value.IsNull()) return 0;
            return Convert.ToInt32(value);
        }

        /// <summary> 
        /// 解析字符串为 Long
        /// </summary>
        public static long TOLong(this object value)
        {
            if (value.IsNull()) return 0L;
            return Convert.ToInt64(value);
        }

        /// <summary> 
        /// 解析字符串为 Float
        /// </summary>
        public static float TOFloat(this object value)
        {
            if (value.IsNull()) return 0F;
            return Convert.ToSingle(value);
        }

        #endregion

        /// <summary> 
        /// 对象拷贝
        /// </summary>
        /// <param name="obj">被复制对象</param>
        /// <returns>新对象</returns>
        public static object CopyOjbect(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            object targetDeepCopyObj;
            Type targetType = obj.GetType();
            if (targetType.IsValueType == true)
            {//值类型  
                targetDeepCopyObj = obj;
            }
            else
            {//引用类型   
                targetDeepCopyObj = Activator.CreateInstance(targetType);   //创建引用对象   
                MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (var member in memberCollection)
                {   
                    if (member.MemberType == MemberTypes.Field)
                    {//拷贝字段
                        FieldInfo field = (FieldInfo)member;
                        var fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)//可以被克隆的属性 结构体 值类型
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, CopyOjbect(fieldValue));
                        }

                    }
                    else if (member.MemberType == MemberTypes.Property)
                    {//拷贝属性
                        PropertyInfo myProperty = (PropertyInfo)member;
                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            try
                            {
                                object propertyValue = myProperty.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                    myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue), null);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError(ex);
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }

        /// <summary>
        /// 拷贝类
        /// </summary>
        public static T CopyOjbect<T>(this T obj)
        {
            if (obj == null) return default;
            object targetDeepCopyObj;
            Type targetType = obj.GetType();

            if (targetType.IsValueType == true)
            {//值类型  
                targetDeepCopyObj = obj;
            }
            else
            {//引用类型   
                targetDeepCopyObj = Activator.CreateInstance(targetType);   //创建引用对象   
                MemberInfo[] memberCollection = obj.GetType().GetMembers();
                foreach (MemberInfo member in memberCollection)
                {
                    if (member.MemberType == MemberTypes.Field)
                    {//拷贝字段
                        FieldInfo field = (FieldInfo)member;
                        var fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, CopyOjbect(fieldValue));
                        }
                    }
                    else if (member.MemberType == MemberTypes.Property)
                    {//拷贝属性
                        PropertyInfo myProperty = (PropertyInfo)member;

                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            try
                            {
                                object propertyValue = myProperty.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                    myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue), null);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogError(ex);
                            }
                        }
                    }
                }
            }
            return (T)targetDeepCopyObj;
        }

        /// <summary>
        /// 拷贝List
        /// </summary>
        public static List<T> CopyOjbect<T>(this List<T> obj)
        {
            List<T> result = new List<T>();
            if (obj.Count == 0)
            {
                return default;
            }
            foreach (T item in obj)
            {
                result.Add(item.CopyOjbect());
            }
            return result;
        }

        /// <summary> 
        /// 设置变量值(对象,它所拥有的变量,指定变量名,值)
        /// </summary>
        public static void SetField(object obj, FieldInfo[] fields, string name, object value)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (name.Equals(fields[i].Name))
                {
                    fields[i].SetValue(obj, value);
                }
            }
            throw new Exception(string.Concat("no this field:", obj, ",", name));
        }

        /// <summary> 
        /// Model对象转换为uri网址参数形式
        /// </summary>
        /// <param name="obj">Model对象</param>
        /// <param name="url">前部分网址</param>
        public static string GetUriParam(this object obj)
        {
            PropertyInfo[] propertis = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            StringBuilder sb = new StringBuilder();
            sb.Append("?");
            foreach (var p in propertis)
            {
                var v = p.GetValue(obj, null);
                if (v == null) v = "";

                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(Uri.EscapeDataString(v.ToString()));//将字符串转换为它的转义表示形式，HttpUtility.UrlEncode是小写
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}