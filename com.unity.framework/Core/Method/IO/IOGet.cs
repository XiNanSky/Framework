/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           14:54:05                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary> 获取属性 </summary>
    public static partial class IOKit
    {
        /// <summary> 
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static FileInfo[] GetFileInfos(string path)
        {
            if (Directory.Exists(path))
            {
                return Directory.CreateDirectory(path).GetFiles();
            }
            return null;
        }

        /// <summary> 
        /// 获取该文件夹下所有文件
        /// </summary>
        public static string[] GetFiles(string path)
        {
            List<string> list = new List<string>();
            if (Directory.Exists(path))
            {
                foreach (var item in Directory.GetFiles(path))
                {
                    list.Add(item.Replace(@"\", @"/"));
                }
                foreach (var item in Directory.GetDirectories(path))
                {
                    list.AddRange(GetFiles(item));
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 返回文件大小 单位KB
        /// </summary>
        /// <param name="filepath">文件相对路径</param>
        public static float GetFileSize(string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) return 0;
            if (!File.Exists(filepath)) return 0;
            return (new FileInfo(filepath).Length / 1024f);
        }

        /// <summary>
        /// 返回文件名，不含路径
        /// </summary>
        /// <param name="filepath">文件相对路径</param>
        /// <param name="extension">是否有后缀</param>
        public static string GetFileName(string filepath, bool extension)
        {
            if (extension)
                return Path.GetFileName(filepath);
            else
                return Path.GetFileNameWithoutExtension(filepath);//filepath.Substring(filepath.LastIndexOf(@"/") + 1);
        }

        /// <summary>
        /// 获取文件的hash值
        /// </summary>
        public static string GetFileHash(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = ObjPoolKit.New<StringBuilder>();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}