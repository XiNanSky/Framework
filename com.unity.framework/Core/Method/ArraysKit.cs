/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Util 
*Author:           HYZ 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-04 
*NOWTIME:          14:19:32 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;

    /// <summary>
    /// 数组扩展方法
    /// </summary>
    public class ArraysKit
    {
        /// <summary> 确认数组的容量 </summary>
        public static T[] EnsureCapacity<T>(T[] original, int capacity)
        {
            if (capacity < original.Length) return original;
            return CopyOf(original, capacity);
        }

        /// <summary> 扩充容量 </summary>
        public static T[] CopyOf<T>(T[] original, int newLength)
        {
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            return copy;
        }

        /// <summary> 是否存在重复的 </summary>
        public static bool ExistRepeat(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (array[i] == array[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary> 是否存在重复的 </summary>
        public static bool ExistRepeat(long[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (array[i] == array[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary> 是否存在重复的 </summary>
        public static bool ExistRepeat<T>(T[] array, Func<T, T, bool> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (action(array[i], array[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary> 添加 </summary>
        public static T[] Add<T>(T[] original, T value)
        {
            int newLength = original.Length + 1;
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            copy[newLength - 1] = value;
            return copy;
        }

        /// <summary> 添加 </summary>
        public static T[] Add<T>(T[] original, T[] values)
        {
            int newLength = original.Length + values.Length;
            T[] copy = new T[newLength]; //(T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, original.Length);
            Array.Copy(values, 0, copy, original.Length, values.Length);
            return copy;
        }

        /// <summary> 移除指定位置的 </summary>
        public static T[] Remove<T>(T[] original, int index)
        {
            int newLength = original.Length - 1;
            //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
            //ORIGINAL LINE: @SuppressWarnings("unchecked") T[] copy=(T[])Array.newInstance(original.getClass().getComponentType(),newLength);
            T[] copy = new T[newLength]; // (T[])Array.CreateInstance(original.GetType().GetElementType(), newLength);
            Array.Copy(original, 0, copy, 0, index);
            Array.Copy(original, index + 1, copy, index, newLength - index);
            return copy;
        }
    }
}