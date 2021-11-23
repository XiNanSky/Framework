/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           14:37:38                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// IO 读取
    /// </summary>
    public static partial class IOKit
    {

        /// <summary>
        /// 使用异步 从文件中读取数据
        /// </summary>
        public static async void ReadFile(string path, Action<byte[]> callback)
        {
            var b = await ReadFile(path);
            callback?.Invoke(b);
        }

        /// <summary>
        /// 使用异步 从文件中读取数据
        /// </summary>
        public static async Task<byte[]> ReadFile(string path)
        {
            return await Read(path);
        }

        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static async Task<bool> WriteFile_(string path, byte[] bytes, bool concat = false)
        {
            return await Write(path, bytes, 0, bytes.Length, concat);
        }

        /// <summary>
        /// 将数据写入文件,是否追加到文件尾 默认覆盖文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static async void WriteFile(string path, byte[] bytes, bool concat = false)
        {
            await Write(path, bytes, 0, bytes.Length, concat);
        }
    }
}
