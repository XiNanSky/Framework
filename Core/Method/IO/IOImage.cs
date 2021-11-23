/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           15:02:23                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System.IO;

    /// <summary> 
    /// IO Image
    /// </summary>
    public static partial class IOKit
    {
        /// <summary>
        /// 读取图片
        /// </summary>
        public static byte[] ReadPhoto(string path)
        {
            if (File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                var br = new BinaryReader(fs);
                return br.ReadBytes((int)fs.Length);
            }
            return EMPTY_BYTES;
        }

    }
}