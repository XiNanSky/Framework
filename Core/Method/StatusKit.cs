/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-02
* Time:             17:17:00
* E-Mail:           1398581458@qq.com
* Description:        
* HIstory:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Kit
{
    /// <summary> 
    /// 状态比较
    /// </summary>

    public class StatusKit
    {
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static short SetStatus(short source, short status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (short)((~status) & 0xFFFF);
            return source;
        }

        /// <summary>
        /// 是否是指定状态 仅仅是指定状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool IsStatus(short source, short status)
        {
            return source == status;
        }

        /// <summary>
        /// 是否有指定状态（包含指定状态，但不限于指定状态）
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool HasStatus(short source, short status)
        {
            return (source & status) == status;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static int SetStatus(int source, int status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (~status);
            return source;
        }

        /// <summary>
        /// 是否是指定状态（仅仅是指定状态）
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool IsStatus(int source, int status)
        {
            return source == status;
        }

        /// <summary>
        /// 是否有指定状态（包含指定状态，但不限于指定状态）
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool HasStatus(int source, int status)
        {
            if (source < 0) return false;
            return (source & status) == status;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static long SetStatus(long source, long status, bool b)
        {
            if (b)
                source |= status;
            else
                source &= (~status);
            return source;
        }

        /// <summary>
        /// 是否是指定状态（仅仅是指定状态）
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool IsStatus(long source, long status)
        {
            return source == status;
        }

        /// <summary>
        /// 是否有指定状态（包含指定状态，但不限于指定状态）
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        public static bool HasStatus(long source, long status)
        {
            return (source & status) == status;
        }
 
        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>新状态</returns>
        public static byte Del(byte source, byte status)
        {
            source = (byte)(source & (~status));
            return source;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>新状态</returns>
        public static short Del(short source, short status)
        {
            source = (short)(source & (~status));
            return source;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>新状态</returns>
        public static int Del(int source, int status)
        {
            source &= (~status);
            return source;
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>新状态</returns>
        public static long Del(long source, long status)
        {
            source &= (~status);
            return source;
        }

        /// <summary>
        /// 源状态和指定状态是否有交集
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>true有相交</returns>
        public static bool Mix(byte source, byte status)
        {
            return (source & status) != 0;
        }

        /// <summary>
        /// 源状态和指定状态是否有交集
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>true有相交</returns>
        public static bool Mix(short source, short status)
        {
            return (source & status) != 0;
        }

        /// <summary>
        /// 源状态和指定状态是否有交集
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>true有相交</returns>
        public static bool Mix(int source, int status)
        {
            return (source & status) != 0;
        }

        /// <summary>
        /// 源状态和指定状态是否有交集
        /// </summary>
        /// <param name="source">源状态</param>
        /// <param name="status">操作状态</param>
        /// <returns>true有相交</returns>
        public static bool Mix(long source, long status)
        {
            return (source & status) != 0;
        }

    }
}