/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2018 by XDCP 
* All rights reserved. 
* FileName:         Framework.Module 
* Author:           HYZ
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2017-02-20
* Time:             16:12:06
* E-Mail:           huangyz1988@qq.com
* Description:      字节流缓冲,最大缓存400KB  
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System;
    using System.Text;
    using UnityEngine;

    /// <summary> 字节缓存类 </summary>
    /// 提供 write, read, set, get 方法
    /// write  :  将写入字节缓存,改变写入进度
    /// read   :  从字节缓存中读取,改变读取进度
    /// set    :  需要传入索引,在字节缓存的指定索引处写入一个,不影响缓存读写进度
    /// get    :  需要传入索引,在字节缓存的指定索引处读取一个,不影响缓存读写进度
    /// method_:  为倒序写入 倒序读取
    public class ByteBuffer : IDisposable
    {
        /// <summary> 默认容量:32B = 256bit </summary>      
        public const int CAPACITY = 32;

        /// <summary> 最大缓存:400KB </summary>
        public const int MAX_DATA_LENGTH = 400 * 1024;

        /// <summary>
        /// TOP = 当前写入位置 Offset = 当前读取位置
        /// </summary>
        protected int top, offset;

        /// <summary> 数据缓存 </summary>
        protected byte[] arrays;

        /// <summary> 构建一个默认容量的ByteBuffer </summary>                                                                                                                                        
        public ByteBuffer() : this(CAPACITY) { }

        /// <summary> 构建一个指定容量的ByteBuffer </summary>
        public ByteBuffer(int capacity)
        {
            if (capacity < 1) throw new SystemException(string.Concat("<init> invalid capatity : " + capacity));
            top = offset = 0;
            arrays = new byte[capacity];
        }

        /// <summary> 构建一个指定数据的ByteBuffer </summary>
        public ByteBuffer(byte[] bytes)
        {
            if (bytes == null) throw new SystemException("<init> null data");
            else if (bytes == IOKit.EMPTY_BYTES) return;
            top = bytes.Length;
            offset = 0;
            arrays = bytes;
        }

        /// <summary> 缓存写入进度: 当前首位 </summary>
        public virtual int Top
        {
            get => top;
            set
            {
                if (value < offset) throw new SystemException(string.Concat("setTop invalid top : " + value));
                if (value > arrays.Length) Capacity = value;
                top = value;
            }
        }

        /// <summary> 缓存写入进度: 当前读取游标下标 </summary>
        public virtual int Offset
        {
            get => offset;
            set
            {
                if (value < 0 || value > top) throw new SystemException(string.Concat("setOffset invalid offset : " + value));
                offset = value;
            }
        }

        /// <summary> 获得数据数组 </summary>
        public byte[] Arrays => arrays;

        /// <summary> 数据缓存:容量 </summary>
        public virtual int Capacity
        {
            get => arrays.Length;
            set
            {
                int c = arrays.Length;
                if (value > c)
                {// 每次扩大一倍 +1是防止i=0导致死循环
                    while (c < value) c = (c << 1) + 1;
                    byte[] newArray = new byte[c];
                    Array.Copy(arrays, 0, newArray, 0, top);
                    arrays = newArray;
                }
            }
        }

        /// <summary> 返回数据可读取长度 </summary>
        public virtual int Length => top - offset;

        /// <summary> 将指定字节缓冲区数据写入当前缓存区 </summary>
        public void Write(ByteBuffer data)
        {
            Write(data.arrays, data.offset, data.Length);
        }

        /// <summary> 写入byte数组 </summary>
        public void Write(byte[] bytes)
        {
            Write(bytes, 0, bytes.Length);
        }

        /// <summary> 写入byte数组(从position开始写入len个) </summary>
        public void Write(byte[] bytes, int pos, int len)
        {
            if (len <= 0) return;
            if (arrays.Length < top + len) Capacity = (top + len);
            Array.Copy(bytes, pos, arrays, top, len);
            top += len;
        }

        #region Bool

        /// <summary> 在指定位置读取一个 Bool, 不改变读取进度 </summary>
        public bool GetBool(int index) { return arrays[index] != 0; }

        /// <summary> 读取一个布尔值 </summary>
        public bool ReadBool() { return arrays[offset++] != 0; }


        /// <summary> 在缓存index处写入一个boolean,不改变写入进度 </summary>
        public void SetBool(bool b, int index) { arrays[index] = (byte)(b ? 1 : 0); }

        /// <summary> 写入一个布尔值 </summary>
        public void WriteBool(bool b)
        {
            if (arrays.Length < top + 1) Capacity = top + CAPACITY;
            arrays[top++] = (byte)(b ? 1 : 0);
        }

        #endregion

        #region Byte

        /// <summary> 在指定位置读取一个 byte, 不改变读取进度 </summary>
        public byte GetByte(int index) { return arrays[index]; }

        /// <summary> 读取一个字节 </summary>
        public byte ReadByte() { return arrays[offset++]; }

        /// <summary> 在指定位置读取一个无符号byte,不改变读取进度 </summary>
        public int GetUnsignedByte(int index) { return arrays[index] & 0xFF; }

        /// <summary> 读取一个无符号byte </summary>
        public int ReadUnsignedByte() { return arrays[offset++] & 0xFF; }


        /// <summary> 在缓存index处写入一个byte,不改变写入进度 </summary>
        public void SetByte(int value, int index) { arrays[index] = (byte)value; }

        /// <summary> 写入一个字节 </summary>
        public void WriteByte(int value)
        {
            if (arrays.Length < top + 1) Capacity = top + CAPACITY;
            arrays[top++] = (byte)value;
        }

        #endregion

        #region Short

        /// <summary> 在指定位置读取一个short,不改变读取进度 </summary>
        public short GetShort(int index) { return (short)GetUnsignedShort(index); }

        /// <summary> 读取一个有符号的short </summary>
        public short ReadShort() { return (short)ReadUnsignedShort(); }

        /// <summary> 在指定位置读取一个无符号short,不改变读取进度 </summary>
        public int GetUnsignedShort(int index) { return ((arrays[index] & 0xFF) << 8) | (arrays[index + 1] & 0xFF); }

        /// <summary> 读取一个无符号short </summary>
        public int ReadUnsignedShort()
        {
            int pos = offset;
            offset += 2;
            return ((arrays[pos] & 0xFF) << 8) | (arrays[pos + 1] & 0xFF);
        }

        /// <summary> 在缓存index处写入一个short,不改变写入进度 </summary>
        public void SetShort(int value, int index)
        {
            arrays[index] = (byte)(value >> 8);
            arrays[index + 1] = (byte)value;
        }

        /// <summary> 写入一个短整形 </summary>
        public void WriteShort(int value)
        {
            if (arrays.Length < top + 2) Capacity = top + CAPACITY;
            arrays[top] = (byte)(value >> 8);
            arrays[top + 1] = (byte)value;
            top += 2;
        }


        #endregion

        #region Enum

        /// <summary> 获取一个枚举 </summary>
        public T GetEnum<T>(T obj, int index) where T : Enum
        {
            return (T)Enum.GetValues(obj.GetType()).GetValue(
                                      ((arrays[index] & 0xFF) << 24) |
                                      ((arrays[index + 1] & 0xFF) << 16) |
                                      ((arrays[index + 2] & 0xFF) << 8) |
                                      (arrays[index + 3] & 0xFF));
        }

        /// <summary> 读取一个枚举 </summary>
        public T ReadEnum<T>() where T : Enum
        {
            T obj = default;
            int pos = offset;
            offset += 4;
            int value = (
                (arrays[pos] & 0xFF) << 24) |
                ((arrays[pos + 1] & 0xFF) << 16) |
                ((arrays[pos + 2] & 0xFF) << 8) |
                (arrays[pos + 3] & 0xFF);
            return Array.Find((T[])Enum.GetValues(obj.GetType()), (c) => c.GetHashCode() == value);
        }

        /// <summary> 读取一个枚举 </summary>
        public T ReadEnum<T>(T obj) where T : Enum
        {
            int pos = offset;
            offset += 4;
            int value = (
                (arrays[pos] & 0xFF) << 24) |
                ((arrays[pos + 1] & 0xFF) << 16) |
                ((arrays[pos + 2] & 0xFF) << 8) |
                (arrays[pos + 3] & 0xFF);
            return Array.Find((T[])Enum.GetValues(obj.GetType()), (c) => c.GetHashCode() == value);
        }

        /// <summary> 写入一个枚举 </summary>
        public void WriteEnum(Enum @enum)
        {
            int value = @enum.GetHashCode();
            if (arrays.Length < top + 4) Capacity = top + CAPACITY;
            arrays[top] = (byte)(value >> 24);
            arrays[top + 1] = (byte)(value >> 16);
            arrays[top + 2] = (byte)(value >> 8);
            arrays[top + 3] = (byte)value;
            top += 4;
        }

        #endregion

        #region Int

        /// <summary> 在指定位置读取一个int,不改变读取进度 </summary>
        public int GetInt(int index)
        {
            return (
                (arrays[index] & 0xFF) << 24) |
                ((arrays[index + 1] & 0xFF) << 16) |
                ((arrays[index + 2] & 0xFF) << 8) |
                (arrays[index + 3] & 0xFF);
        }

        /// <summary> 读取一个整形 </summary>
        public int ReadInt()
        {
            int pos = offset;
            offset += 4;
            return ((arrays[pos] & 0xFF) << 24)
                | ((arrays[pos + 1] & 0xFF) << 16)
                | ((arrays[pos + 2] & 0xFF) << 8)
                | (arrays[pos + 3] & 0xFF);
        }

        /// <summary> 写入一个Int数组 </summary>
        public int[] ReadInts()
        {
            var len = ReadInt();
            var ints = new int[len];
            for (int i = 0; i < len; i++)
            {
                ints[i] = ReadInt();
            }
            return ints;
        }

        /// <summary> 在缓存index处写入一个int,不改变写入进度 </summary>
        public void SetInt(int value, int index)
        {
            arrays[index] = (byte)(value >> 24);
            arrays[index + 1] = (byte)(value >> 16);
            arrays[index + 2] = (byte)(value >> 8);
            arrays[index + 3] = (byte)value;
        }

        /// <summary> 写入一个整形 </summary>
        public void WriteInt(int value)
        {
            if (arrays.Length < top + 4) Capacity = top + CAPACITY;
            arrays[top] = (byte)(value >> 24);
            arrays[top + 1] = (byte)(value >> 16);
            arrays[top + 2] = (byte)(value >> 8);
            arrays[top + 3] = (byte)value;
            top += 4;
        }

        /// <summary> 写入一个Int数组 </summary>
        public void WriteInts(int[] value)
        {
            WriteInt(value.Length);
            foreach (var item in value)
            {
                WriteInt(item);
            }
        }

        #endregion

        #region Vector3

        /// <summary> 在指定位置读取一个Vector3,不改变读取进度 </summary>
        public Vector3 GetVector3_(int index)
        {
            return new Vector3(GetFloat_(index), GetFloat_(index + 1), GetFloat_(index + 2));
        }

        /// <summary> 读取一个Vector3 </summary>
        public Vector3 ReadVector3_()
        {
            return new Vector3(readFloat_(), readFloat_(), readFloat_());
        }

        /// <summary> 写入一个Vector3 </summary>
        public void WriteVector3_(Vector3 value)
        {
            WriteFloat_(value.x);
            WriteFloat_(value.y);
            WriteFloat_(value.z);
        }

        /// <summary> 在缓存index处写入一个Vector3,不改变写入进度 </summary>
        public void SetVector3_(Vector3 value, int index)
        {
            SetFloat_(value.x, index);
            SetFloat_(value.y, index + 1);
            SetFloat_(value.z, index + 2);
        }

        /// <summary> 在指定位置读取一个Vector3,不改变读取进度 </summary>
        public Vector3 GetVector3(int index)
        {
            return new Vector3(GetFloat(index), GetFloat(index + 1), GetFloat(index + 2));
        }

        /// <summary> 读取一个Vector3 </summary>
        public Vector3 ReadVector3()
        {
            return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
        }

        /// <summary> 在缓存index处写入一个Vector3,不改变写入进度 </summary>
        public void SetVector3(Vector3 value, int index)
        {
            SetFloat(value.x, index);
            SetFloat(value.y, index + 1);
            SetFloat(value.z, index + 2);
        }

        /// <summary> 写入一个Vector3 </summary>
        public void WriteVector3(Vector3 value)
        {
            WriteFloat(value.x);
            WriteFloat(value.y);
            WriteFloat(value.z);
        }

        #endregion

        #region Long

        /// <summary> 在指定位置读取一个Long,不改变读取进度 </summary>
        public long GetLong(int index)
        {
            return ((arrays[index] & 0xFFL) << 56)
                | ((arrays[index + 1] & 0xFFL) << 48)
                | ((arrays[index + 2] & 0xFFL) << 40)
                | ((arrays[index + 3] & 0xFFL) << 32)
                | ((arrays[index + 4] & 0xFFL) << 24)
                | ((arrays[index + 5] & 0xFFL) << 16)
                | ((arrays[index + 6] & 0xFFL) << 8)
                | (arrays[index + 7] & 0xFFL);
        }

        /// <summary> 读取一个长整形 </summary>
        public long ReadLong()
        {
            int pos = offset;
            offset += 8;
            return ((arrays[pos] & 0xFFL) << 56)
                | ((arrays[pos + 1] & 0xFFL) << 48)
                | ((arrays[pos + 2] & 0xFFL) << 40)
                | ((arrays[pos + 3] & 0xFFL) << 32)
                | ((arrays[pos + 4] & 0xFFL) << 24)
                | ((arrays[pos + 5] & 0xFFL) << 16)
                | ((arrays[pos + 6] & 0xFFL) << 8)
                | (arrays[pos + 7] & 0xFFL);
        }

        /// <summary> 在缓存index处写入一个long,不改变写入进度 </summary>
        public void SetLong(long value, int index)
        {
            arrays[index] = (byte)(value >> 56);
            arrays[index + 1] = (byte)(value >> 48);
            arrays[index + 2] = (byte)(value >> 40);
            arrays[index + 3] = (byte)(value >> 32);
            arrays[index + 4] = (byte)(value >> 24);
            arrays[index + 5] = (byte)(value >> 16);
            arrays[index + 6] = (byte)(value >> 8);
            arrays[index + 7] = (byte)value;
        }

        /// <summary> 写入一个长整形 </summary>
        public void WriteLong(long value)
        {
            if (arrays.Length < top + 8) Capacity = top + CAPACITY;
            arrays[top] = (byte)(value >> 56);
            arrays[top + 1] = (byte)(value >> 48);
            arrays[top + 2] = (byte)(value >> 40);
            arrays[top + 3] = (byte)(value >> 32);
            arrays[top + 4] = (byte)(value >> 24);
            arrays[top + 5] = (byte)(value >> 16);
            arrays[top + 6] = (byte)(value >> 8);
            arrays[top + 7] = (byte)value;
            top += 8;
        }

        #endregion

        #region Float

        /// <summary> 在指定位置读取一个float,不改变读取进度 </summary>
        public float GetFloat_(int index) { return BitConverter.ToSingle(arrays, index); }

        /// <summary> 读取一个float </summary>
        public float readFloat_()
        {
            int pos = offset;
            offset += 4;
            return BitConverter.ToSingle(arrays, pos);
        }

        /// <summary> 在指定位置读取一个float,不改变读取进度 </summary>
        public float GetFloat(int index)
        {
            index += 3;
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = arrays[index--];
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary> 读取一个float </summary>
        public float ReadFloat()
        {
            int index = offset + 3;
            byte[] bytes = new byte[4];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = arrays[index--];
            }
            offset += 4;
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary> 在缓存index处写入一个short,不改变写入进度 </summary>
        public void SetFloat_(float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                arrays[index + i] = bytes[i];
            }
        }

        /// <summary> 写入一个float </summary>
        public void WriteFloat_(float value)
        {
            WriteBytes(BitConverter.GetBytes(value));
        }

        /// <summary> 在缓存index处写入一个short,不改变写入进度 </summary>
        public void SetFloat(float value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                arrays[index + i] = bytes[j];
            }
        }

        /// <summary> 写入一个float </summary>
        public void WriteFloat(float value)
        {
            if (arrays.Length < top + 4) Capacity = (top + CAPACITY);
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                arrays[top + i] = bytes[j];
            }
            top += 4;
        }

        #endregion

        #region Double

        /// <summary> 在指定位置读取一个double,不改变读取进度 </summary>
        public double GetDouble_(int index) { return BitConverter.ToDouble(arrays, index); }

        /// <summary> 读取一个double </summary>
        public double ReadDouble_()
        {
            int pos = offset;
            offset += 8;
            return BitConverter.ToDouble(arrays, pos);
        }

        /// <summary> 在指定位置读取一个double,不改变读取进度 </summary>
        public double GetDouble(int index)
        {
            index += 7;
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = arrays[index--];
            }
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary> 读取一个double </summary>
        public double ReadDouble()
        {
            int index = offset + 7;
            byte[] bytes = new byte[8];
            for (int i = 0; i < bytes.Length; ++i)
            {
                bytes[i] = arrays[index--];
            }
            offset += 8;
            return BitConverter.ToDouble(bytes, 0);
        }

        /// <summary> 在缓存index处写入一个long,不改变写入进度 </summary>
        public void SetDouble_(double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0; i < bytes.Length; ++i)
            {
                arrays[index + i] = bytes[i];
            }
        }

        /// <summary> 写入一个浮点double型 </summary>
        public void WriteDouble_(double value) { WriteBytes(BitConverter.GetBytes(value)); }

        /// <summary> 在缓存index处写入一个long,不改变写入进度 </summary>
        public void SetDouble(double value, int index)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                arrays[index + i] = bytes[j];
            }
        }

        /// <summary> 写入一个double </summary>
        public void WriteDouble(double value)
        {
            if (arrays.Length < top + 8) Capacity = top + CAPACITY;
            byte[] bytes = BitConverter.GetBytes(value);
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; ++i, --j)
            {
                arrays[top + i] = bytes[j];
            }
            top += 8;
        }

        #endregion

        #region String

        /// <summary> 写入一个指定编码类型的字符串 </summary>
        public void WriteString(string str, string encoding)
        {
            if (str == null) WriteInt(-1);
            else if (str.Length <= 0) WriteInt(0);
            else
            {
                byte[] bytes;
                if (encoding == null) bytes = Encoding.Default.GetBytes(str);
                else
                {
                    try { bytes = Encoding.GetEncoding(encoding).GetBytes(str); }
                    catch (Exception e) { throw new SystemException("invalid charsetName:" + encoding, e); }
                }
                WriteInt(bytes.Length);
                WriteBytes(bytes, 0, bytes.Length);
            }
        }

        /// <summary> 按指定编码格式读取一个字符串 </summary>
        public string ReadString(string encoding)
        {
            int len = ReadInt();
            if (len < 0) return null;
            if (len == 0) return "";
            if (len > MAX_DATA_LENGTH) throw new SystemException("data overflow:" + len);
            byte[] bytes = new byte[len];
            ReadBytes(bytes, 0, len);
            if (encoding == null) return Encoding.Default.GetString(bytes);

            try { return Encoding.GetEncoding(encoding).GetString(bytes); }
            catch (Exception e) { throw new SystemException("invalid charsetName:" + encoding, e); }
        }

        #endregion

        #region String UTF - 8

        /// <summary> 写入一个utf-8编码类型的字符串 </summary>
        public void WriteUTF(string str) { WriteString(str, "utf-8"); }

        /// <summary> 按utf-8编码格式读取一个字符串 </summary>
        public string ReadUTF() { return ReadString("utf-8"); }

        #endregion

        #region Length

        /// <summary> 读取一个长度 0至512M </summary>
        /// 原理 : 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 7=(8-1) 位能表示的范围,即:0~(2^7-1)
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)
        public int ReadLength()
        {   // 判定字节
            int n = arrays[offset] & 0xFF;
            // 第一个字节若小于 0x20否则该字节不能表示为长度
            if (n < 0x20) throw new SystemException("rLength, invalid number:" + n);
            // 第一个字节若大于 0x20(二进制为:0010 0000)则减去 0x20<<(8*3)(补3个字节位数)即为长度值
            if (n < 0x40) return ReadInt() & ((1 << 29) - 1);
            // 第一个字节若大于 0x40(二进制为:0100 0000)则减去 0x40<<8(补1个字节位数)即为长度值
            if (n < 0x80) return ReadUnsignedShort() & ((1 << 14) - 1);
            // 第一个字节若大于 0x80(二进制为:1000 0000)则减去 0x80即为长度值
            return ReadUnsignedByte() & ((1 << 7) - 1);
        }

        /// <summary> 写入一个长度, 0至512M </summary>     
        /// 原理: 以第一个字节二进制前三位来决定长度值占用的字节数(x表示0或1)
        /// 1xx 开头:长度值占1个字节,且值只能是剩下的 7=(8-1)   位能表示的范围,即:0~(2^7-1)
        /// 01x 开头:长度值占2个字节,且值只能是剩下的 14=(16-2) 位能表示的范围,即:0~(2^14-1)
        /// 001 开头:长度值占4个字节,且值只能是剩下的 29=(32-3) 位能表示的范围,即:0~(2^29-1)
        public void WriteLength(int len)
        {
            // 不在0~(2^29-1)范围内
            if (len >= 0x20000000 || len < 0) throw new SystemException(" wLength, invalid len:" + len);
            // 写入时,加上判定位表示的值
            if (len < 0x80)// 1xx范围0~(2^7-1)
                WriteByte(len | 0x80);
            else if (len < 0x4000)// 01x范围0~(2^14-1)
                WriteShort(len | 0x4000);
            else// 001范围0~(2^29-1)
                WriteInt(len | 0x20000000);
        }

        #endregion

        #region Read

        /// <summary> 从缓存中读取len个字节,并插入到bytes,从pos开始插入 </summary>
        public void ReadBytes(byte[] bytes) { ReadBytes(bytes, 0, bytes.Length); }

        /// <summary> 从缓存中读取len个字节,并插入到bytes,从pos开始插入 </summary>
        public void ReadBytes(byte[] bytes, int pos, int len)
        {
            Array.Copy(arrays, offset, bytes, pos, len);
            offset += len;
        }

        /// <summary> 读取字节数组 先读长度 </summary>
        public byte[] ReadLengthBytes()
        {
            int len = ReadInt();
            if (len == 0) return new byte[0];
            if (len > MAX_DATA_LENGTH) throw new SystemException("data overflow:" + len);
            byte[] bytes = new byte[len];
            ReadBytes(bytes, 0, len);
            return bytes;
        }

        #endregion

        #region Write

        /// <summary> 写入byte数组 </summary>
        public void WriteBytes(byte[] bytes) { WriteBytes(bytes, 0, bytes.Length); }

        /// <summary> 将指定字节缓冲区数据写入当前缓存区 </summary>
        public void WriteBytes(ByteBuffer data) { WriteBytes(data.Arrays, data.offset, data.Length); }

        /// <summary> 写入byte数组(从position开始写入len个) </summary>
        public void WriteBytes(byte[] bytes, int pos, int len)
        {
            if (len <= 0) return;
            if (arrays.Length < top + len) Capacity = (top + len);
            Array.Copy(bytes, pos, arrays, top, len);
            top += len;
        }

        /// <summary> 写入byte数组长度 </summary>
        public void WriteLengthBytes(byte[] bytes)
        {
            WriteInt(bytes.Length);
            WriteBytes(bytes, 0, bytes.Length);
        }

        #endregion

        #region Other

        /// <summary> 返回:有效字节数组 </summary>
        public byte[] ToBytes()
        {
            int len = top - offset;
            if (len == 0) return new byte[0];
            byte[] bytes = new byte[len];
            Array.Copy(arrays, offset, bytes, 0, len);
            return bytes;
        }

        /// <summary> 清空 </summary>
        public virtual ByteBuffer clear()
        {
            top = offset = 0;
            return this;
        }

        public override string ToString()
        {
            return string.Concat("ByteBuffer:{ offset = ", offset, ", top=", top, ", data = ", HexKit.ToHex(ToBytes()), '}');
        }

        #endregion

        public void Dispose()
        {

        }
    }
}
