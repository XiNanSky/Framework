/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           15:06:27                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Threading.Tasks;

    /// <summary> 
    /// IO ByteBuffer
    /// </summary>
    public static partial class IOKit
    {
        /// <summary> 
        /// 加载 Byte Data
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="coded">加密</param>
        public static async void ReadByteData(string path, bool coded, Action<ByteBuffer> callback)
        {
            var b = await ReadByteData(path, coded);
            callback?.Invoke(b);
        }


        /// <summary> 
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="coded">加密</param>
        public static async Task<ByteBuffer> ReadByteData(string path, bool coded)
        {
            byte[] bytes = await ReadFile(path);
            if (bytes == null || bytes.Length == 0) return null;
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = EncodingBitByte(bytes[i]);
                }
            }
            return new ByteBuffer(bytes);
        }

        /// <summary> 
        /// 写入数据
        /// </summary>
        public static void WriteByteData(string path, ByteBuffer data, bool coded)
        {
            byte[] bytes = data.ToBytes();
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = EncodingBitByte(bytes[i]);
                }
            }
            WriteFile(path, bytes, false);
        }


        /// <summary> 
        /// 写入数据
        /// </summary>
        public static async void WriteByteData(string path, ByteBuffer data, bool coded, Action<bool> callback)
        {
            byte[] bytes = data.ToBytes();
            if (coded)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = EncodingBitByte(bytes[i]);
                }
            }
            if (await WriteFile_(path, bytes, false))
            {
                callback?.Invoke(true);
            }
            else callback?.Invoke(false);
        }
    }
}