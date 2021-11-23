/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Module.Server 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-01
* Time:             18:40:41
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using UnityEngine;

    /// <summary> ͳһ��Դ��λ�� </summary>
    public class Url
    {
        /// <summary> httpЭ�� </summary>
        public const string HTTP = "http://";

        /// <summary> httpsЭ�� </summary>
        public const string HTTPS = "https://";

        /// <summary> ftpЭ�� </summary>
        public const string FTP = "ftp://";

        /// <summary> fileЭ�� </summary>
        public const string FILE = "file://";

        /// <summary> resЭ�� </summary>
        public const string RES = "res://";

        /// <summary> Э�� </summary>
        public string protocol;

        /// <summary> ·�� </summary>
        public string path;

        /// <summary> ��׺ </summary>
        public string suffix;

        public Url(string url, string suffix = null)
        {
            if (url.StartsWith(HTTP)) protocol = HTTP;
            else if (url.StartsWith(HTTPS)) protocol = HTTPS;
            else if (url.StartsWith(FTP)) protocol = FTP;
            else if (url.StartsWith(FILE)) protocol = FILE;
            else if (url.StartsWith(RES)) protocol = RES;
            else throw new UnityException("LoadFileInfo error protocol ,url = " + url);
            path = url.Substring(protocol.Length);
            this.suffix = suffix;
        }

        public Url(string protocol, string path, string suffix)
        {
            this.protocol = protocol;
            this.path = path;
            this.suffix = suffix;
        }

        public bool isProtocol(string protocol)
        {
            return this.protocol.Equals(protocol);
        }

        public override string ToString()
        {
            if (isProtocol(RES)) return path;
            if (suffix == null) return protocol + path;
            else return string.Concat(protocol, path, '.', suffix);
        }
    }
}