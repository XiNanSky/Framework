/***************************************************
* Copyright(C) 2020 by XN                          *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2020-06-02                    *
* Nowtime:           00:59:43                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.IO;
    using UnityEngine;

    /// <summary> 
    /// 文件读写操作工具集
    /// </summary>
    public static partial class IOKit
    {
        /// <summary> 空字节数组 </summary>
        public static readonly byte[] EMPTY_BYTES = { };

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        public static bool ExistsFile(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        public static bool ExistsFloder(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="filepath">文件相对路径</param>
        public static bool DeleteFile(string filepath)
        {
            if (string.IsNullOrEmpty(@filepath) ||
                !File.Exists(@filepath))
                return false;
            File.Delete(@filepath);
            return true;
        }


        /// <summary> 
        /// 复制文件夹及文件 部分 根文件名不会复制 适合重命名
        /// </summary> 
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        public static void CopyFolderPart(string sourceFolder, string destFolder)
        {
            try
            {
                var destfolderdir = Path.Combine(destFolder, Path.GetFileName(sourceFolder));
                foreach (string file in Directory.GetFileSystemEntries(sourceFolder))//遍历所有的文件和目录
                {
                    if (Directory.Exists(file))
                    {
                        var currentdir = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(currentdir)) Directory.CreateDirectory(currentdir);
                        CopyFolderPart(file, destfolderdir);
                    }
                    else
                    {
                        var srcfileName = Path.Combine(destfolderdir, Path.GetFileName(file));
                        if (!Directory.Exists(destfolderdir)) Directory.CreateDirectory(destfolderdir);
                        File.Copy(file, srcfileName);
                    }
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    UnityEditor.EditorUtility.DisplayDialog("提示", e.Message, "确定");
                }
#else
                Debug.LogError(e);
#endif
            }
            finally { }
        }

        /// <summary> 
        /// 复制文件夹及文件 全部
        /// </summary> 根文件名一起复制
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        public static void CopyFolderAll(string sourceFolder, string destFolder, bool overwirte = false)
        {
            try
            {   //如果目标路径不存在,则创建目标路径
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);
                foreach (string file in Directory.GetFiles(sourceFolder))
                {   //得到原文件根目录下的所有文件
                    var dest = Path.Combine(destFolder, Path.GetFileName(file));
                    File.Copy(file, dest, overwirte);//复制文件
                }
                foreach (string folder in Directory.GetDirectories(sourceFolder))
                {   //得到原文件根目录下的所有文件夹
                    var dest = Path.Combine(destFolder, Path.GetFileName(folder));
                    CopyFolderAll(folder, dest, overwirte);//构建目标路径,递归复制文件
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    UnityEditor.EditorUtility.DisplayDialog("提示", e.Message, "确定");
                }
#else
                Debug.LogError(e);
#endif
            }
            finally { }
        }

        /// <summary> 
        /// 删除文件夹
        /// </summary> 
        public static void DeleteFloder(string sourceFolder)
        {
            try
            {   //如果目标路径不存在
                if (!Directory.Exists(sourceFolder)) return;
                foreach (string file in Directory.GetFiles(sourceFolder))
                {   //得到原文件根目录下的所有文件
                    var dest = Path.Combine(sourceFolder, Path.GetFileName(file));
                    File.Delete(file);
                }
                foreach (string folder in Directory.GetDirectories(sourceFolder))
                {   //得到原文件根目录下的所有文件夹
                    var dest = Path.Combine(sourceFolder, Path.GetFileName(folder));
                    DeleteFloder(folder);
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    UnityEditor.EditorUtility.DisplayDialog("提示", e.Message, "确定");
                }
#else
                Debug.LogError(e);
#endif
            }
            finally { }
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="directory">文件夹路径</param>
        /// <param name="clear">清除</param>
        public static void CreateDirectory(string directory, bool clear)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return;
            }
            if (clear)
            {
                Directory.Delete(directory, true);
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 清空当前文件夹
        /// </summary>
        public static bool ClearDirectory(string directory)
        {
            try
            {
                if (string.IsNullOrEmpty(directory)
                    || !Directory.Exists(directory))
                {
                    return true;  // 如果参数为空，则视为已成功清空
                }
                // 删除当前文件夹下所有文件
                foreach (string strFile in Directory.GetFiles(directory))
                {
                    File.Delete(strFile);
                }
                // 删除当前文件夹下所有子文件夹(递归)
                foreach (string strDir in Directory.GetDirectories(directory))
                {
                    Directory.Delete(strDir, true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"清空 {directory} 异常, 消息:{ex.Message}, 堆栈:{ex.StackTrace}");
                return false;
            }
        }


        /// <summary>
        /// 复制大文件
        /// </summary>
        /// <param name="fromPath">源文件的路径</param>
        /// <param name="toPath">文件保存的路径</param>
        /// <param name="eachReadLength">每次读取的长度</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyFile(string fromPath, string toPath, int eachReadLength)
        {
            //将源文件 读取成文件流
            FileStream fromFile = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
            //已追加的方式 写入文件流
            FileStream toFile = new FileStream(toPath, FileMode.Append, FileAccess.Write);
            //实际读取的文件长度
            int toCopyLength;
            //如果每次读取的长度小于 源文件的长度 分段读取
            if (eachReadLength < fromFile.Length)
            {
                byte[] buffer = new byte[eachReadLength];
                long copied = 0;
                while (copied <= fromFile.Length - eachReadLength)
                {
                    toCopyLength = fromFile.Read(buffer, 0, eachReadLength);
                    fromFile.Flush();
                    toFile.Write(buffer, 0, eachReadLength);
                    toFile.Flush();
                    //流的当前位置
                    toFile.Position = fromFile.Position;
                    copied += toCopyLength;

                }
                int left = (int)(fromFile.Length - copied);
                //toCopyLength = fromFile.Read(buffer, 0, left);
                fromFile.Flush();
                toFile.Write(buffer, 0, left);
                toFile.Flush();

            }
            else
            {
                //如果每次拷贝的文件长度大于源文件的长度 则将实际文件长度直接拷贝
                byte[] buffer = new byte[fromFile.Length];
                fromFile.Read(buffer, 0, buffer.Length);
                fromFile.Flush();
                toFile.Write(buffer, 0, buffer.Length);
                toFile.Flush();
            }
            fromFile.Close();
            toFile.Close();
            return true;
        }
    }
}