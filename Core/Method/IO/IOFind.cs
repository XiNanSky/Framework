/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           15:51:49                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary> 
    /// 文件读写操作工具集
    /// </summary>
    public static partial class IOKit
    {
        /// <summary>
        /// 查询匹配 返回符合条件的路径
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="partterns">条件 "*value*"</param>
        /// <param name="op">匹配模式</param>
        /// <returns></returns>
        public static List<string> FindPaths(string dir, SearchOption op = SearchOption.AllDirectories, params string[] partterns)
        {
            if (!Directory.Exists(dir))
                return null;
            List<string> result = new List<string>();
            if (partterns == null)
                partterns = new string[] { "*.*" };
            for (int i = 0; i < partterns.Length; ++i)
            {
                var paths = Directory.GetFiles(dir, partterns[i], op);
                foreach (string path in paths)
                {
                    result.Add(@path.Replace("\\", "/"));
                }
            }
            return result;
        }
    }
}