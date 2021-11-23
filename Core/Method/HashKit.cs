/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           17:48:05                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// hash工具
    /// </summary>
    public static class HashKit
    {
        /// <summary>
        /// 获取文件的哈希值
        /// </summary>
        public static string GetFileHash(string filepath)
        {
            if (!File.Exists(filepath))
                return null;
            SHA1 sSHA1 = SHA1.Create();
            byte[] hashByte = null;
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                hashByte = sSHA1.ComputeHash(stream);
                stream.Close();
                stream.Dispose();
            }
            sSHA1.Dispose();
            return BitConverter.ToString(hashByte).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public static string GetFileMD5(string fileName)
        {
            if (IOKit.ExistsFile(fileName))
            {
                try
                {
                    using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var result = GetMD5(file, 1024 * 16);
                        file.Close();
                        file.Dispose();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
                }
            }
            else return null;
        }

        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        /// <param name="filepath">文件地址</param>
        /// <param name="bufferSize">自定义缓冲区大小16K</param>
        /// <returns>MD5Hash</returns>
        public static string GetMD5(string filepath, long bufferSize = 1024 * 16)
        {
            if (!File.Exists(filepath)) throw new ArgumentException(string.Format("<{0}>, 不存在", filepath));
            byte[] buffer = new byte[bufferSize];
            using (Stream inputStream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return GetMD5ByHashAlgorithm(inputStream, bufferSize);
            }
        }

        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="bufferSize">自定义缓冲区大小16K</param>
        /// <returns>MD5Hash</returns>
        public static string GetMD5(Stream stream, long bufferSize = 1024 * 16)
        {
            if (stream == null) return null;
            return GetMD5ByHashAlgorithm(stream, bufferSize);
        }

        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <param name="bufferSize">自定义缓冲区大小16K</param>
        /// <returns>MD5Hash</returns>
        private static string GetMD5ByHashAlgorithm(Stream stream, long bufferSize = 1024 * 16)
        {
            var buffer = new byte[bufferSize];
            using (Stream inputStream = stream)
            {
                var hashAlgorithm = new MD5CryptoServiceProvider();
                var readLength = 0; //每次读取长度
                var output = new byte[bufferSize];
                while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {   //计算MD5
                    hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                }
                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                string md5 = BitConverter.ToString(hashAlgorithm.Hash);
                hashAlgorithm.Clear();
                inputStream.Close();
                hashAlgorithm.Dispose();
                inputStream.Dispose();
                md5 = md5.Replace("-", "").ToLower();
                return md5;
            }
        }

        /// <summary>
        /// 获取MD5值 推荐使用GetMD5ByHashAlgorithm
        /// </summary>
        /// 判断文件是否被修改过
        [Obsolete("推荐使用GetMD5ByHashAlgorithm")]
        public static string GetMD5(string input)
        {
            var MD5 = new MD5CryptoServiceProvider();
            var data = MD5.ComputeHash(Encoding.UTF8.GetBytes(input));
            MD5.Dispose();
            return ToHash(data);
        }

        /// <summary>
        /// 获取MD5值 推荐使用GetMD5ByHashAlgorithm
        /// </summary>
        /// 判断数据是否被修改过
        [Obsolete("推荐使用GetMD5ByHashAlgorithm")]
        public static string GetMD5(Stream input)
        {
            var MD5 = new MD5CryptoServiceProvider();
            var data = MD5.ComputeHash(input);
            MD5.Dispose();
            return ToHash(data);
        }

        //public static bool VerifyMd5Hash(string input, string hash)
        //{
        //    var comparer = StringComparer.OrdinalIgnoreCase;
        //    return 0 == comparer.Compare(input, hash);
        //}

        //public static string GetCRC32Hash(Stream input)
        //{
        //    var data = crc32.ComputeHash(input);
        //    return ToHash(data);
        //}

        //public static uint GetCrc(byte[] bytes)
        //{
        //    return CRC32.Compute(bytes);
        //}

        //public static string GetCRC32Hash(byte[] bytes)
        //{
        //    var data = crc32.ComputeHash(bytes);
        //    return ToHash(data);
        //}

        /// <summary>
        /// 转化为哈希值
        /// </summary>
        private static string ToHash(byte[] data)
        {
            if (data == null) return default;
            var sb = ObjPoolKit.New<StringBuilder>();
            foreach (var t in data)
                sb.Append(t.ToString("x2"));
            return sb.ToString();
        }

        //public static string GetCRC32Hash(string input)
        //{
        //    var data = crc32.ComputeHash(Encoding.UTF8.GetBytes(input));
        //    return ToHash(data);
        //}

        /// <summary>
        /// 比较32位校验码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyCrc32Hash(string input, string hash)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(input, hash);
        }

        internal class CRC32
        {
            private const uint _initialResidueValue = 0xFFFFFFFF;
            private static readonly object _globalSync = new object();
            private static uint[] _crc32Table;
            private static readonly byte[][] _maskingBitTable =
            {
                new byte[] { 2 },
                new byte[] { 0, 3 },
                new byte[] { 0, 1, 4 },
                new byte[] { 1, 2, 5 },
                new byte[] { 0, 2, 3, 6 },
                new byte[] { 1, 3, 4, 7 },
                new byte[] { 4, 5 },
                new byte[] { 0, 5, 6 },
                new byte[] { 1, 6, 7 },
                new byte[] { 7 },
                new byte[] { 2 },
                new byte[] { 3 },
                new byte[] { 0, 4 },
                new byte[] { 0, 1, 5 },
                new byte[] { 1, 2, 6 },
                new byte[] { 2, 3, 7 },
                new byte[] { 0, 2, 3, 4 },
                new byte[] { 0, 1, 3, 4, 5 },
                new byte[] { 0, 1, 2, 4, 5, 6 },
                new byte[] { 1, 2, 3, 5, 6, 7 },
                new byte[] { 3, 4, 6, 7 },
                new byte[] { 2, 4, 5, 7 },
                new byte[] { 2, 3, 5, 6 },
                new byte[] { 3, 4, 6, 7 },
                new byte[] { 0, 2, 4, 5, 7 },
                new byte[] { 0, 1, 2, 3, 5, 6 },
                new byte[] { 0, 1, 2, 3, 4, 6, 7 },
                new byte[] { 1, 3, 4, 5, 7 },
                new byte[] { 0, 4, 5, 6 },
                new byte[] { 0, 1, 5, 6, 7 },
                new byte[] { 0, 1, 6, 7 },
                new byte[] { 1, 7 }
            };
            private uint _residue = _initialResidueValue;

            internal CRC32()
            {
                lock (_globalSync)
                {
                    if (_crc32Table == null) PrepareTable();
                }
            }

            internal uint crc => ~_residue;

            /// <summary>
            /// 比较
            /// </summary>
            internal uint Compute(Stream stream)
            {
                var buffer = new byte[0x1000];

                for (; ; )
                {
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                        Accumulate(buffer, 0, bytesRead);
                    else
                        break;
                }

                return crc;
            }

            /// <summary>
            /// 存储
            /// </summary>
            /// <param name="buffer">源数据</param>
            /// <param name="offset">开始位置</param>
            /// <param name="count">长度</param>
            internal void Accumulate(byte[] buffer, int offset, int count)
            {
                for (var i = offset; i < count + offset; i++)
                    _residue = ((_residue >> 8) & 0x00FFFFFF)^
                    _crc32Table[(_residue ^ buffer[i]) & 0x000000FF];
            }

            /// <summary>
            /// 清空
            /// </summary>
            internal void ClearCrc()
            {
                _residue = _initialResidueValue;
            }

            /// <summary>
            /// 注册表
            /// </summary>
            private static void PrepareTable()
            {
                _crc32Table = new uint[256];

                for (uint tablePosition = 0; tablePosition < _crc32Table.Length; tablePosition++)
                    for (byte bitPosition = 0; bitPosition < 32; bitPosition++)
                    {
                        var bitValue = false;
                        foreach (var maskingBit in _maskingBitTable[bitPosition]) bitValue ^= GetBit(maskingBit, tablePosition);

                        SetBit(bitPosition, ref _crc32Table[tablePosition], bitValue);
                    }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bitOrdinal"></param>
            /// <param name="data"></param>
            /// <returns></returns>
            private static bool GetBit(byte bitOrdinal, uint data)
            {
                return ((data >> bitOrdinal) & 0x1) == 1;
            }

            private static void SetBit(byte bitOrdinal, ref uint data, bool value)
            {
                if (value) data |= (uint)0x1 << bitOrdinal;
            }
        }

        /// <summary>
        /// 对比校验码
        /// </summary>
        public static uint ComputeCRC32(Stream stream)
        {
            var crc32 = new CRC32();
            return crc32.Compute(stream);
        }

        /// <summary>
        /// 对比校验码
        /// </summary>
        public static uint ComputeCRC32(string filename)
        {
            if (!File.Exists(filename)) return 0;

            using (var stream = File.OpenRead(filename))
            {
                return ComputeCRC32(stream);
            }
        }
    }
}