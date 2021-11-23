/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           18:13:14                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// 闭环数据流
    /// </summary>
    /// 参考链接
    /// <see cref="https://ifeve.com/dissecting-disruptor-whats-so-special/"/>
    public class RingBuffer
    {
        public const int SIZE = 4096;

        //readonly ConcurrentQueue<byte[]> mUsed = new ConcurrentQueue<byte[]>();
        //readonly ConcurrentQueue<byte[]> mFreed = new ConcurrentQueue<byte[]>();

        /// <summary>
        /// 读写队列 该队列专门处理数据读写
        /// </summary>
        private readonly Queue<byte[]> mUsed = new Queue<byte[]>();

        /// <summary>
        /// 存储队列 该队列专门存储数据作为对象池
        /// </summary>
        private readonly Queue<byte[]> mFreed = new Queue<byte[]>();

        /// <summary>
        /// 当前读写数据数组 当前只有写入时用到
        /// </summary>
        private byte[] mLastBuffer;

        /// <summary>
        /// 数据写入结束位置
        /// </summary>
        public int LastIndex { get; set; }

        /// <summary>
        /// 数据读取开始位置
        /// </summary>
        public int FirstIndex { get; set; }

        /// <summary>
        /// 第一个读取的数组长度 队列最上面的数据数组
        /// </summary>
        public byte[] First
        {
            get
            {
                if (mUsed.Count == 0)
                {
                    AddLast();
                }

                //mUsed.TryPeek(out var buffer);
                //return buffer;
                return mUsed.Peek();
            }
        }

        /// <summary>
        /// 最后读取的数组长度 队列最下面的数据数组
        /// </summary>
        public byte[] Last
        {
            get
            {
                if (mUsed.Count == 0)
                {
                    AddLast();
                }
                return mLastBuffer;
            }
        }

        /// <summary>
        /// 当前可读取数据长度
        /// </summary>
        public int Length
        {
            get
            {
                int c;
                if (mUsed.Count == 0)
                {
                    c = 0;
                }
                else
                {
                    c = (mUsed.Count - 1) * SIZE + LastIndex - FirstIndex;
                }
                if (c < 0)
                {
                    Debug.LogError($"RingBuffer count < 0: {mUsed.Count}, {LastIndex}, {FirstIndex}");
                }
                return c;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RingBuffer()
        {
            AddLast();
        }

        /// <summary>
        /// 添加 读写数据数组 进入 读写队列
        /// </summary>
        protected void AddLast()
        {
            byte[] buffer;
            if (mFreed.Count > 0)//对象池中有废弃数组 可以入列到 读写数据队列
            {
                buffer = mFreed.Dequeue();
            }
            else//否则 则重新创建一个byte[SIZE] 数组
            {
                buffer = new byte[SIZE];
            }
            mUsed.Enqueue(buffer);//
            mLastBuffer = buffer;
        }

        /// <summary>
        /// 移除最上层的数据
        /// </summary>
        protected void RemoveFirst()
        {
            mFreed.Enqueue(mUsed.Dequeue());
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">开始值 偏移量</param>
        /// <param name="count">读取长度</param>
        /// <returns>实际读取长度</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            if (buffer.Length < offset + count)
            {
                throw new Exception($"bufferList length < coutn, buffer length: {buffer.Length} {offset} {count}");
            }

            try
            {
                long length = Length;
                if (length < count)
                {//传入读取长度 比 存储容量小的时候 则默认读取 存储容量的最大值 并将实际读取长度返回
                    count = (int)length;
                }

                int alreadyCopyCount = 0;
                while (alreadyCopyCount < count)//拷贝的长度 小余 实际读取长度 则继续拷贝
                {
                    int n = count - alreadyCopyCount;//剩余拷贝长度
                    if (SIZE - FirstIndex > n)
                    {//队列数组剩余容量 大于 实际需要拷贝长度 则正常拷贝
                        Array.Copy(First, FirstIndex, buffer, alreadyCopyCount + offset, n);
                        FirstIndex += n;
                        alreadyCopyCount += n;
                    }
                    else
                    {//队列数组剩余容量不足 则先把剩余数组数据拷贝 再重置读写开始位置 最后让处理数据队列 mUsed 出列一个
                        Array.Copy(First, FirstIndex, buffer, alreadyCopyCount + offset, SIZE - FirstIndex);
                        alreadyCopyCount += SIZE - FirstIndex;
                        FirstIndex = 0;
                        RemoveFirst();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }

            return count;
        }

        /// <summary>
        /// 读取数据流
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <param name="count">长度</param>
        public void Read(Stream stream, int count)
        {
            if (count > Length)
            {
                throw new Exception($"bufferList length < count, {Length} {count}");
            }

            try
            {
                int alreadyCopyCount = 0;
                while (alreadyCopyCount < count)
                {
                    int n = count - alreadyCopyCount;
                    if (SIZE - FirstIndex > n)//实现方法同上类似
                    {
                        stream.Write(First, FirstIndex, n);
                        FirstIndex += n;
                        alreadyCopyCount += n;
                    }
                    else
                    {
                        stream.Write(First, FirstIndex, SIZE - FirstIndex);
                        alreadyCopyCount += SIZE - FirstIndex;
                        FirstIndex = 0;
                        RemoveFirst();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="buffer">写入数组</param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">长度</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                int alreadyCopyCount = 0;
                while (alreadyCopyCount < count)
                {
                    if (LastIndex == SIZE)//如果写入的开始位置 等于 容量 则在队列新增一个数组
                    {
                        AddLast();
                        LastIndex = 0;
                    }

                    int n = count - alreadyCopyCount;//实际写入长度
                    if (SIZE - LastIndex > n)//写入数组长度 大余 实际写入长度 正常写入
                    {
                        Array.Copy(buffer, alreadyCopyCount + offset, mLastBuffer, LastIndex, n);
                        LastIndex += count - alreadyCopyCount;
                        alreadyCopyCount += n;
                    }
                    else//否则 则只会写入当前写入数组容量最大值为止 再次进入 写入位置 容量大小判断 进入循环 
                    {
                        Array.Copy(buffer, alreadyCopyCount + offset, mLastBuffer, LastIndex, SIZE - LastIndex);
                        alreadyCopyCount += SIZE - LastIndex;
                        LastIndex = SIZE;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public void Write(Stream stream)
        {
            try
            {
                int count = (int)(stream.Length - stream.Position);

                int alreadyCopyCount = 0;
                while (alreadyCopyCount < count)
                {
                    if (LastIndex == SIZE)
                    {
                        AddLast();
                        LastIndex = 0;
                    }

                    int n = count - alreadyCopyCount;
                    if (SIZE - LastIndex > n)
                    {
                        stream.Read(mLastBuffer, LastIndex, n);
                        LastIndex += count - alreadyCopyCount;
                        alreadyCopyCount += n;
                    }
                    else
                    {
                        stream.Read(mLastBuffer, LastIndex, SIZE - LastIndex);
                        alreadyCopyCount += SIZE - LastIndex;
                        LastIndex = SIZE;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            while (mUsed.Count > 0)
            {
                RemoveFirst();
            }
            AddLast();
            LastIndex = 0;
            FirstIndex = 0;
        }
    }
}