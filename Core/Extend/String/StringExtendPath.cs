/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           01:39:12                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System.IO;

    public static partial class StringExtend
    {

        #region Path

        /// <summary> 
        /// 获取文件名
        /// </summary>
        public static string PathGetFileName(this string @path)
        {
            var r = @path.LastIndexOf('/') + 1;
            var l = @path.IndexOf('.');
            return @path.Substring(r, l - r);
        }

        /// <summary> 
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, string str2)
        {
            return Path.Combine(str1, str2).Replace(@"\", "/");
        }

        /// <summary> 
        /// 获取文件扩展名
        /// </summary>
        public static string PathGetExtension(this string path)
        {
            if (string.IsNullOrEmpty(path) || !path.Contains(".")) return null;
            var index = path.LastIndexOf('.');
            return path.Substring(index, path.Length - index);
        }

        /// <summary> 
        /// 修改文件扩展名
        /// </summary>
        public static string PathChangeExtension(this string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        /// <summary> 
        /// 获取根目录
        /// </summary>
        public static string PathGetRoot(this string path)
        {
            return Path.GetPathRoot(path);
        }

        #endregion
    }
}