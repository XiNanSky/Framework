/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           14:47:43                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IO 写入
    /// </summary>
    public static partial class IOKit
    {
        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        public static async Task<bool> WriteUTF8_(string path, string text, bool concat = false)
        {
            return await WriteText(path, text, "utf-8", concat);
        }

        /// <summary>
        /// 将字符串按照UTF-8写入文件,默认覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        public static async void WriteUTF8(string path, string text, bool concat = false)
        {
            await WriteText(path, text, "utf-8", concat);
        }

        /// <summary>
        /// 将字符串按照UTF-8写入文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="concat">Ture:追加 False:覆盖</param>
        public static void WriteUTF8(string path, string text, bool concat, Action<bool> callback)
        {
            WriteText(path, text, "utf-8", concat, callback);
        }

        /// <summary>
        /// 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="charset">保存文本格式</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static async Task<bool> WriteText(string path, string text, string charset, bool concat)
        {
            var b = Encoding.GetEncoding(charset).GetBytes(text);
            return await Write(path, b, 0, b.Length, concat);
        }

        /// <summary>
        /// 将字符串按照指定编码写入文件,是否追加到文件尾
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">内容</param>
        /// <param name="charset">保存文本格式</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        public static async void WriteText(string path, string text, string charset, bool concat, Action<bool> callback)
        {
            var b = Encoding.GetEncoding(charset).GetBytes(text);
            var c = await Write(path, b, 0, b.Length, concat);
            callback?.Invoke(c);
        }

        /// <summary> 
        /// 按照UTF-8读取文本文件
        /// </summary>
        public static void ReadUTF8(string path, Action<string> callback)
        {
            ReadText(path, "utf-8", callback);
        }

        /// <summary> 
        /// 按照UTF-8读取文本文件
        /// </summary>
        public static async Task<string> ReadUTF8(string path)
        {
            return await ReadText(path, "utf-8");
        }

        /// <summary> 
        /// 按照指定编码读取文本文件
        /// </summary>
        public static async void ReadText(string path, string charset, Action<string> callback)
        {
            var c = Encoding.GetEncoding(charset).GetString(await ReadFile(path));
            callback?.Invoke(c);
        }

        /// <summary> 
        /// 按照指定编码读取文本文件
        /// </summary>
        public static async Task<string> ReadText(string path, string charset)
        {
            return Encoding.GetEncoding(charset).GetString(await ReadFile(path));
        }
    }
}