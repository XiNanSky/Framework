/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           16:52:04                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// ZIP工具
    /// </summary>
    public class ZipKit
    {
        public static void Compress(string src, string target)
        {
            ZipFile.CreateFromDirectory(src, target, CompressionLevel.NoCompression, false);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="files"></param>
        /// <param name="entryNames"></param>
        /// <param name="zip"></param>
        /// <param name="compression"></param>
        /// <param name="progressAction"></param>
        public static void Compress(List<string> files,
                                    List<string> entryNames,
                                    string zip,
                                    CompressionLevel compression,
                                    Action<float> progressAction = null)
        {
            if (!Directory.Exists(new FileInfo(zip).Directory.ToString()))
            {
                throw new ArgumentException("保存目录不存在");
            }
            foreach (string c in files)
            {
                if (!File.Exists(c))
                {
                    throw new ArgumentException(string.Format("文件{0} 不存在！", c));
                }
            }
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }
            try
            {
                using (ZipArchive za = ZipFile.Open(zip, ZipArchiveMode.Create))
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string file = files[i];
                        string entryName = null;
                        if (entryNames != null)
                            entryName = entryNames[i];
                        if (string.IsNullOrEmpty(entryName))
                            entryName = Path.GetFileName(file);
                        za.CreateEntryFromFile(file, entryName);
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="zip">解压包路径</param>
        /// <param name="save">解压存放目录</param>
        public static void Decompress(string zip, string save)
        {
            if (!File.Exists(zip))
            {
                throw new ArgumentException("要解压的文件不存在。");
            }

            if (!Directory.Exists(save))
            {
                Directory.CreateDirectory(save);
            }
            ZipFile.ExtractToDirectory(zip, save);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="zip">解压包路径</param>
        /// <param name="save">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="entryAction"></param>
        public static void Decompress(string zip,
                                      string save,
                                      Action<float> progressAction = null,
                                      Action<string> entryAction = null)
        {
            if (!File.Exists(zip))
            {
                throw new ArgumentException("要解压的文件不存在。");
            }
            if (!Directory.Exists(save))
            {
                throw new ArgumentException("要解压到的目录不存在！");
            }
#if false
             long totalLength = 0;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(sourceFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    totalLength += theEntry.Size;
                }
            }

            long currentLength = 0;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(sourceFile)))
            {
                ZipEntry theEntry;
                byte[] data = new byte[2048];
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (string.IsNullOrEmpty(fileName))
                        continue;
                    string filePath = path + theEntry.Name;
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    using (FileStream streamWriter = File.Create(path + theEntry.Name))
                    {
                        int size = 2048;
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                            currentLength += size;
                            progressAction?.Invoke(currentLength * 1f / totalLength);
                        }
                        entryAction?.Invoke(path + theEntry.Name);
                    }
                }
            }
#endif
        }

        public static void Decompress(Stream sizeStram,
                                      Stream stram,
                                      string path,
                                      Action<float> progressAction = null,
                                      Action<string> entryAction = null)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("要解压到的目录不存在！");
            }

#if false
            long totalLength = 1;
            using (ZipInputStream s = new ZipInputStream(sizeStram))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    totalLength += theEntry.Size;
                }
            }

            long currentLength = 0;
            using (ZipInputStream s = new ZipInputStream(stram))
            {
                ZipEntry theEntry;
                byte[] data = new byte[2048];
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (string.IsNullOrEmpty(fileName))
                        continue;
                    string filePath = path + theEntry.Name;
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    using (FileStream streamWriter = File.Create(path + theEntry.Name))
                    {
                        int size = 2048;
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                            currentLength += size;
                            progressAction?.Invoke(currentLength * 1f / totalLength);
                        }
                        entryAction?.Invoke(path + theEntry.Name);
                    }
                }
            }
#endif
        }
    }
}