/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-10
* Time:             16:42:47
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using System.Text;

    /// <summary>
    /// 文字集合 
    /// StringBuilder 包装类
    /// </summary>
    public class CharList
    {
        ///<summary>容量</summary>
        public const int CAPACITY = 32;

        public StringBuilder builder { get; private set; }

        public char this[int index] => builder[index];

        public int Capacity => builder.Capacity;

        public int Length => builder.Length;

        public int MaxCapacity => builder.MaxCapacity;

        public CharList() : this(CAPACITY) { }

        public CharList(int capacity) => builder = new StringBuilder(capacity);

        public CharList(string value) => builder = new StringBuilder(value);

        public CharList(int capacity, int maxCapacity) => builder = new StringBuilder(capacity, maxCapacity);

        public CharList(string value, int capacity) => builder = new StringBuilder(value, capacity);

        public CharList(string value, int startIndex, int length, int capacity) => builder = new StringBuilder(value, startIndex, length, capacity);

        #region Append

        public StringBuilder Append<T>(params T[] value)
        {
            for (int i = 0; i < value?.Length; i++)
                builder.Append(value[i]);
            return builder;
        }

        public StringBuilder Append(char value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(bool value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(ulong value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(uint value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(ushort value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(byte value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(string value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(float value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(string value, int startIndex, int count)
        {
            return builder.Append(value, startIndex, count);
        }

        public StringBuilder Append(object value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(long value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(int value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(short value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(double value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(decimal value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(char[] value, int startIndex, int charCount)
        {
            return builder.Append(value, startIndex, charCount);
        }

        public StringBuilder Append(sbyte value)
        {
            return builder.Append(value);
        }

        public StringBuilder Append(char value, int repeatCount)
        {
            return builder.Append(value);
        }

        #endregion

        #region AppendFormat

        public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
        {
            return builder.AppendFormat(provider, format, args);
        }

        public StringBuilder AppendFormat(string format, object arg0)
        {
            return builder.AppendFormat(format, arg0);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1)
        {
            return builder.AppendFormat(format, arg0, arg1);
        }

        public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
        {
            return builder.AppendFormat(format, arg0, arg1, arg2);
        }

        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return builder.AppendFormat(format, args);
        }

        #endregion

        #region AppendLine 

        public StringBuilder AppendLine(string value)
        {
            return builder.AppendLine(value);
        }

        public StringBuilder AppendLine()
        {
            return builder.AppendLine();
        }

        #endregion

        #region Insert

        public StringBuilder Insert(int index, bool value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, ulong value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, uint value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, ushort value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, string value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, float value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, sbyte value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, string value, int count)
        {
            return builder.Insert(index, value, count);
        }

        public StringBuilder Insert(int index, long value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, object value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
        {
            return builder.Insert(index, value, startIndex, charCount);
        }

        public StringBuilder Insert(int index, byte value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, double value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, short value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, int value)
        {
            return builder.Insert(index, value);
        }

        public StringBuilder Insert(int index, decimal value)
        {
            return builder.Insert(index, value);
        }

        #endregion

        #region Replace

        public StringBuilder Replace(char oldChar, char newChar)
        {
            return builder.Replace(oldChar, newChar);
        }

        public StringBuilder Replace(string oldValue, string newValue)
        {
            return builder.Replace(oldValue, newValue);
        }

        public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count)
        {
            return builder.Replace(oldValue, newValue, startIndex, count);
        }

        public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
        {
            return builder.Replace(oldChar, newChar, startIndex, count);
        }

        #endregion

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        {
            builder.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public int EnsureCapacity(int capacity)
        {
            return builder.EnsureCapacity(capacity);
        }

        public bool Equals(StringBuilder sb)
        {
            return builder.Equals(sb);
        }

        public StringBuilder Remove(int startIndex, int length)
        {
            return builder.Remove(startIndex, length);
        }

        public StringBuilder Clear()
        {
            return builder.Remove(0, builder.Length);
        }

        public override string ToString()
        {
            return builder.ToString();
        }

        public string ToString(int startIndex, int length)
        {
            return builder.ToString(startIndex, length);
        }
    }
}
