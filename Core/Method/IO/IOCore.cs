/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           14:55:12                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using UnityEngine;

    /// <summary> IO 核心方法 </summary>
    public static partial class IOKit
    {
        /// <summary> 将指定数据从offset开始写入length长度到文件中,是否追加到文件尾 </summary>
        /// <param name="path">路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="offset">写入内容位置</param>
        /// <param name="length">长度</param>
        /// <param name="concat">true:拼接 | false:覆盖</param>
        private static async Task<bool> Write(string @path, byte[] bytes, int offset, int length, bool concat)
        {
            var dir = Path.GetDirectoryName(@path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            FileMode mode = (concat) ? FileMode.Append : FileMode.Create;
            using (FileStream fs = new FileStream(@path, mode, FileAccess.Write))
            {
                if (fs.CanWrite)
                {
                    await fs.WriteAsync(bytes, offset, length);
                    fs.Close();
                    return true;
                }
                else
                {
                    Debug.Log("Error,文件不能写入!!!");
                    return false;
                }
            }
        }

        private static async Task<byte[]> Read(string path)
        {
            if (File.Exists(@path))
            {
                try
                {
                    using (FileStream fsSource = new FileStream(@path, FileMode.Open, FileAccess.Read))
                    {
                        var bytes = new byte[fsSource.Length];
                        var numBytesToRead = fsSource.Length;
                        var numBytesRead = 0;
                        int n = 0;
                        while (numBytesToRead > 0)
                        {
                            if (numBytesToRead > int.MaxValue)
                            {//如果读取的文件大小 超出int值 则会进行多次读取 
                                n = await fsSource.ReadAsync(bytes, numBytesRead, int.MaxValue);
                            }
                            else n = await fsSource.ReadAsync(bytes, numBytesRead, (int)numBytesToRead);
                            if (n == 0) break;
                            numBytesRead += n;
                            numBytesToRead -= n;
                        }
                        numBytesToRead = bytes.Length;
                        return bytes;
                    }
                }
                catch (FileNotFoundException ioEx)
                {
                    Debug.LogWarning(ioEx.Message);
                }
            }
            return EMPTY_BYTES;
        }

    }
}