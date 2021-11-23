/***************************************************
* Copyright(C) 2019 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2019-05-16                    *
* Nowtime:           19:38:52                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using UnityEngine;

    /// <summary> </summary>
    public static class AudioClipExtend
    {
        /// <summary> 
        /// AudioClip转换成字节数组
        /// </summary>
        /// <returns> 压缩过的byte </returns>
        public static byte[] AudioToBytes(this AudioClip clip, int lastpostion)
        {
            float[] data = new float[lastpostion * clip.channels * 4];
            clip.GetData(data, 0);
            byte[] bytes = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary> 
        /// 字节数组转换为AudioClip
        /// </summary>
        public static AudioClip BytesToAudio(this AudioClip clip, byte[] bytes)
        {
            float[] data = new float[bytes.Length / 4];
            Buffer.BlockCopy(bytes, 0, data, 0, data.Length);
            clip.SetData(data, 0);
            return clip;
        }
    }
}