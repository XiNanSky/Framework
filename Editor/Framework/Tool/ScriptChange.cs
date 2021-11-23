/***************************************************
* Copyright(C) 2020 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.2.18                     *
* Date:              2020-10-04                    *
* Nowtime:           14:00:31                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    /*  hint
    *   const string no file path '/', attention please !
    */

    /// <summary> 生成文件信息</summary>
    public class ScriptChange : AssetModificationProcessor
    {
        /// <summary> 文件路径加文件名 </summary>
        private static StringBuilder FileName = new StringBuilder();

        /// <summary> 游戏文件夹路径 </summary>
        private const string HeaderFile = "Assets";

        /// <summary>  
        /// 此函数在Asset被创建完，文件已经生成到磁盘上，但是没有生成.meta文件和import之前被调用  
        /// </summary>
        public static void OnWillCreateAsset(string newFileMeta)
        {
            var newFilePath = newFileMeta.Replace(".meta", "");
            if (Path.GetExtension(newFilePath) != ".cs") { newFilePath = null; return; }
            var scriptname = newFileMeta.PathGetFileName();
            FileName = new StringBuilder();
            FileName.Append(newFilePath);

            var realPath = GetRealPath();
            if (File.ReadAllText(realPath, Encoding.UTF8).Contains("/**")) return;

            File.WriteAllText(realPath, SetScriptContent(scriptname).ToString(), Encoding.UTF8);

            newFilePath = realPath = null;
        }

        /// <summary> 获取真正的文件路径 </summary>
        private static string GetRealPath()
        {
#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            return Application.dataPath.Replace(HeaderFile, "") + FileName.ToString();
#elif UNITY_ANDROID
     return    "";
#elif UNITY_IOS
     return    "";
#endif //注意，Application.datapath会根据使用平台不同而不同
        }

        /// <summary> 作者名 </summary>
        public static string AUTHOR = "XiNan";

        /// <summary> 邮箱 </summary>
        private const string EMAIL = "1398581458@qq.com";

        /// <summary> 这里实现自定义的一些规则 </summary>
        private static StringBuilder SetScriptContent(string scriptname)
        {
            string times = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var c = '*'; var n = "\r\n";
            var str = new StringBuilder();
            var top = "/".AddCharToLenAtLast(51, c);

            str.Append(top).Append(n);
            /* 游戏名称 */
            str.Append(string.Concat("* Copyright(C) ", times.Substring(0, 4), " by ", PlayerSettings.companyName).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 版权所有 */
            str.Append(string.Concat("* All Rights Reserved By Author ", "lihongliu", '.').AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 编辑作者 */
            str.Append(string.Concat("* Author:".AddCharToLenAtLast(20), AUTHOR).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 编辑作者 */
            str.Append(string.Concat("* Email:".AddCharToLenAtLast(20), EMAIL).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 脚本版本 */
            str.Append(string.Concat("* Version:".AddCharToLenAtLast(20), PlayerSettings.bundleVersion).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* UNITY版本 */
            str.Append(string.Concat("* UnityVersion:".AddCharToLenAtLast(20), Application.unityVersion).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 时间日期 */
            str.Append(string.Concat("* Date:".AddCharToLenAtLast(20), times.Substring(0, 10)).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 详细时间 */
            str.Append(string.Concat("* Nowtime:".AddCharToLenAtLast(20), times.Substring(11, 8)).AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 作用目的 */
            str.Append(string.Concat("* Description:").AddCharToLenAtLast(50)).Append(c).Append(n);
            /* 历史修改 */
            str.Append(string.Concat("* History:").AddCharToLenAtLast(50)).Append(c).Append(n);

            str.Append(top.Reverse()).Append(n).Append(n);

            str.Append(string.Concat("namespace ", GetFileNameSpace(FileName).ToString())).Append(n);
            str.Append('{').Append(n);
            str.Append("    using System;").Append(n);
            str.Append("    using System.Collections;").Append(n);
            str.Append("    using System.Collections.Generic;").Append(n);
            str.Append("    using UnityEngine;").Append(n);
            str.Append("    using UnityEngine.UI;").Append(n);
            str.Append("    using Framework;").Append(n);
            //str.Append("    using Framework.UI;").Append(n);
            //str.Append("    using Framework.Kit;").Append(n);
            str.Append(n);
            str.Append($"    public class {scriptname}").Append(n);
            str.Append("    ").Append('{').Append(n);
            str.Append(n);
            str.Append("    ").Append('}').Append(n);
            str.Append('}');

            times = null;
            return str;
        }

        /// <summary> 获取文件命名空间 </summary>
        private static StringBuilder GetFileNameSpace(StringBuilder filename)
        {
            filename = FileName.Replace('/' + Path.GetFileName(FileName.ToString()), "");
            filename.Replace(HeaderFile + '/', "");
            filename.Replace("Editor" + '/', "");
            return filename.Replace('/', '.');
        }
    }
}
