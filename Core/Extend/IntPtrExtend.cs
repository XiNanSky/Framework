/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-04                    *
* Nowtime:           15:39:35                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Runtime.InteropServices;

    public static class IntPtrExtend
    {
        public static int[] TOInts(this IntPtr intPtr)
        {
            return (int[])GCHandle.FromIntPtr(intPtr).Target;
        }

        public static object TOObj(this IntPtr intPtr)
        {
            return GCHandle.FromIntPtr(intPtr).Target;
        }
    }
}