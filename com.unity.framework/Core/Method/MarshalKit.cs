/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-03                    *
* Nowtime:           18:11:26                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Reflection;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;

    public static class Define  //define some constant
    {
        public const int MAX_LENGTH_OF_IDENTICARDID = 20;   //maximum length of identicardid
        public const int MAX_LENGTH_OF_NAME = 50;           //maximum length of name
        public const int MAX_LENGTH_OF_COUNTRY = 50;        //maximum length of country
        public const int MAX_LENGTH_OF_NATION = 50;         //maximum length of nation
        public const int MAX_LENGTH_OF_BIRTHDAY = 8;        //maximum length of birthday
        public const int MAX_LENGTH_OF_ADDRESS = 200;       //maximum length of address

        //MarshalAs:指示如何在托管代码和非托管代码之间封送数据
        //UnmanagedType:指定如何将参数或字段封送到非托管内存块
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = Define.MAX_LENGTH_OF_IDENTICARDID)]
    }

    /// <summary> 
    /// Marshal 类
    /// </summary>
    /// <see cref="https://msdn.microsoft.com/zh-cn/library/system.runtime.interopservices.marshal(VS.80).aspx"/>
    /// <!--提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型 此外还提供了在与非托管代码交互时使用的其他杂项方法-->
    /// <!--备注 Marshal 类中定义的 static 方法对于处理非托管代码至关重要。此类中定义的大多数方法通常由需要-->
    /// <!--此类型的任何公共静态（Visual Basic 中的 Shared）成员都是线程安全的，但不保证所有实例成员都 是线程安全的-->
    public static class MarshalKit
    {
        public static int SystemDefaultCharSize => Marshal.SystemDefaultCharSize;

        public static int SystemMaxDBCSCharSize => Marshal.SystemMaxDBCSCharSize;

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        /// <param name="obj">返回对象的非托管大小 以字节为单位</param>
        [SecurityCritical]
        public static int SizeOf<T>(T obj)
        {
            return Marshal.SizeOf(obj);
        }

        /// <summary>
        /// 获取 结构体实例 空间大小
        /// </summary>
        [SecurityCritical]
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf<T>();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。  
        /// </summary>
        /// <param name="size">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        public static IntPtr AllocHGlobal(int size)
        {
            return Marshal.AllocHGlobal(size);
        }

        [SecurityCritical]
        public static int AddRef(IntPtr pUnk)
        {
            return Marshal.AddRef(pUnk);
        }

        [SecurityCritical]
        public static IntPtr AllocCoTaskMem(int cb)
        {
            return Marshal.AllocCoTaskMem(cb);
        }

        [SecurityCritical]
        public static bool AreComObjectsAvailableForCleanup()
        {
            return Marshal.AreComObjectsAvailableForCleanup();
        }

        [SecurityCritical]
        public static object BindToMoniker(string monikerName)
        {
            return Marshal.BindToMoniker(monikerName);
        }

        [SecurityCritical]
        public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
        {
            Marshal.ChangeWrapperHandleStrength(otp, fIsWeak);
        }

        [SecurityCritical]
        public static void CleanupUnusedObjectsInCurrentContext()
        {
            Marshal.CleanupUnusedObjectsInCurrentContext();
        }

        /// <summary>
        /// 使用指定的字节数从进程的非托管内存中分配内存。  
        /// </summary>
        /// <param name="ptr">内存中所需的字节数</param>
        /// <returns>一个指向新分配内存的指针 这个内存必须使用Marshal.FreeHGlobal 来释放 </returns>
        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static IntPtr AllocHGlobal(IntPtr ptr)
        {
            return Marshal.AllocHGlobal(ptr);
        }

        /// <summary>
        /// 释放内存中指针
        /// </summary>
        /// <param name="ptr">内存中的指针</param>

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void FreeHGlobal(IntPtr hglobal)
        {
            Marshal.FreeHGlobal(hglobal);
        }

        /// <summary>
        /// 创建聚合对象
        /// </summary>
        /// <param name="ptr">指针</param>
        /// <param name="obj">数据</param>
        public static void CreateAggregatedObject<T>(IntPtr ptr, T obj)
        {
            Marshal.CreateAggregatedObject(ptr, obj);
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装数据</returns>
        public static object CreateWrapperOfType<T>(object obj)
        {
            return Marshal.CreateWrapperOfType(obj, typeof(T));
        }

        /// <summary>
        /// 创建类型的包装器
        /// </summary>
        /// <typeparam name="T">结构类型</typeparam>
        /// <typeparam name="TWrapper">包装器</typeparam>
        /// <param name="obj">数据</param>
        /// <returns>包装器</returns>
        public static TWrapper CreateWrapperOfType<T, TWrapper>(T obj)
        {
            return Marshal.CreateWrapperOfType<T, TWrapper>(obj);
        }

        #region Clone

        [SecurityCritical]
        public static void Copy(float[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, float[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, long[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, int[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, double[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, short[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }

        [SecurityCritical]
        public static void Copy(long[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(int[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(short[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(double[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(char[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
        {
            Marshal.Copy(source, startIndex, destination, length);
        }

        [SecurityCritical]
        public static void Copy(IntPtr source, char[] destination, int startIndex, int length)
        {
            Marshal.Copy(source, destination, startIndex, length);
        }


        #endregion

        #region Read

        [SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static byte ReadByte(object ptr, int ofs)
        {
            return Marshal.ReadByte(ptr, ofs);
        }

        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr)
        {
            return Marshal.ReadByte(ptr);
        }

        [SecurityCritical]
        public static byte ReadByte(IntPtr ptr, int ofs)
        {
            return Marshal.ReadByte(ptr, ofs);
        }

        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr)
        {
            return Marshal.ReadInt16(ptr);
        }

        [SecurityCritical]
        public static short ReadInt16(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt16(ptr, ofs);
        }


        [SuppressUnmanagedCodeSecurity, SecurityCritical]
        public static short ReadInt16(object ptr, int ofs)
        {
            return Marshal.ReadInt16(ptr, ofs);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr)
        {
            return Marshal.ReadInt32(ptr);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt32(ptr, ofs);
        }

        [SuppressUnmanagedCodeSecurity, SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int ReadInt32(object ptr, int ofs)
        {
            return Marshal.ReadInt32(ptr, ofs);
        }

        [SuppressUnmanagedCodeSecurity, SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(object ptr, int ofs)
        {
            return Marshal.ReadInt64(ptr, ofs);
        }

        [SecurityCritical]
        public static long ReadInt64(IntPtr ptr, int ofs)
        {
            return Marshal.ReadInt64(ptr, ofs);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static long ReadInt64(IntPtr ptr)
        {
            return Marshal.ReadInt64(ptr);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr)
        {
            return Marshal.ReadIntPtr(ptr);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
        {
            return Marshal.ReadIntPtr(ptr, ofs);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static IntPtr ReadIntPtr(object ptr, int ofs)
        {
            return Marshal.ReadIntPtr(ptr, ofs);
        }

        #endregion

        #region Write

        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteByte(object ptr, int ofs, byte val)
        {
            Marshal.WriteByte(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, int ofs, byte val)
        {
            Marshal.WriteByte(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteByte(IntPtr ptr, byte val)
        {
            Marshal.WriteByte(ptr, val);
        }

        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, char val)
        {
            Marshal.WriteInt16(ptr, val);
        }

        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, short val)
        {
            Marshal.WriteInt16(ptr, val);
        }

        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, char val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteInt16(IntPtr ptr, int ofs, short val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteInt16(object ptr, int ofs, char val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
        }

        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt16(object ptr, int ofs, short val)
        {
            Marshal.WriteInt16(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int val)
        {
            Marshal.WriteInt32(ptr, val);
        }

        [SecurityCritical]
        public static void WriteInt32(IntPtr ptr, int ofs, int val)
        {
            Marshal.WriteInt32(ptr, ofs, val);
        }

        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt32(object ptr, int ofs, int val)
        {
            Marshal.WriteInt32(ptr, ofs, val);
        }

        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        public static void WriteInt64(object ptr, int ofs, long val)
        {
            Marshal.WriteInt64(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, int ofs, long val)
        {
            Marshal.WriteInt64(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteInt64(IntPtr ptr, long val)
        {
            Marshal.WriteInt64(ptr, val);
        }

        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
        {
            Marshal.WriteIntPtr(ptr, ofs, val);
        }

        [SecurityCritical]
        public static void WriteIntPtr(IntPtr ptr, IntPtr val)
        {
            Marshal.WriteIntPtr(ptr, val);
        }

        [SecurityCritical]
        public static void WriteIntPtr(object ptr, int ofs, IntPtr val)
        {
            Marshal.WriteIntPtr(ptr, ofs, val);
        }

        #endregion

        #region Zero

        [SecurityCritical]
        public static void ZeroFreeBSTR(IntPtr s)
        {
            Marshal.ZeroFreeBSTR(s);
        }

        [SecurityCritical]
        public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
        {
            Marshal.ZeroFreeCoTaskMemAnsi(s);
        }

        [SecurityCritical]
        public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
        {
            Marshal.ZeroFreeCoTaskMemUnicode(s);
        }

        [SecurityCritical]
        public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
        {
            Marshal.ZeroFreeGlobalAllocAnsi(s);
        }

        [SecurityCritical]
        public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
        {
            Marshal.ZeroFreeGlobalAllocUnicode(s);
        }

        #endregion

        [ComVisible(true), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityCritical]
        public static void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld)
        {
            Marshal.StructureToPtr(structure, ptr, fDeleteOld);
        }
        [ComVisible(true), SecurityCritical]
        public static void DestroyStructure(IntPtr ptr, Type structuretype)
        {
            Marshal.DestroyStructure(ptr, structuretype);
        }

        [SecurityCritical]
        public static void DestroyStructure<T>(IntPtr ptr)
        {
            Marshal.DestroyStructure<T>(ptr);
        }

        [SecurityCritical]
        public static int FinalReleaseComObject(object o)
        {
            return Marshal.FinalReleaseComObject(o);
        }

        [SecurityCritical]
        public static void FreeBSTR(IntPtr ptr)
        {
            Marshal.FreeBSTR(ptr);
        }

        [SecurityCritical]
        public static void FreeCoTaskMem(IntPtr ptr)
        {
            Marshal.FreeCoTaskMem(ptr);
        }

        /// <summary>
        /// 将数据从托管对象封送到非托管内存块
        /// </summary>
        /// <typeparam name="T">保存要封送的数据的托管对象 此对象必须是格式化类的结构或实例</typeparam>
        /// <param name="obj">实例</param>
        /// <param name="ptr">指向非托管内存块的指针，在调用此方法之前必须分配该内存块</param>
        /// <param name="fDeleteOld">在ptr参数上使用DestroyStructure方法复制数据</param>
        public static void StructureToPtr<T>(T obj, IntPtr ptr, bool fDeleteOld = true)
        {
            Marshal.StructureToPtr(obj, ptr, fDeleteOld);
        }

        [SecurityCritical]
        public static void ThrowExceptionForHR(int errorCode)
        {
            Marshal.ThrowExceptionForHR(errorCode);
        }

        [SecurityCritical]
        public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            Marshal.ThrowExceptionForHR(errorCode, errorInfo);
        }

        [SecurityCritical]
        public static IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index)
        {
            return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        [SecurityCritical]
        public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
        {
            return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
        }

        [SecurityCritical]
        public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
        {
            return Marshal.QueryInterface(pUnk, ref iid, out ppv);
        }

        [SecuritySafeCritical]
        public static bool IsComObject(object o)
        {
            return Marshal.IsComObject(o);
        }

        public static IntPtr OffsetOf(Type t, string fieldName)
        {
            return Marshal.OffsetOf(t, fieldName);
        }

        public static IntPtr OffsetOf<T>(string fieldName)
        {
            return Marshal.OffsetOf<T>(fieldName);
        }

        [SecurityCritical]
        public static void Prelink(MethodInfo m)
        {
            Marshal.Prelink(m);
        }

        [SecurityCritical]
        public static void PrelinkAll(Type c)
        {
            Marshal.PrelinkAll(c);
        }

        #region String

        [SecurityCritical]
        public static IntPtr StringToBSTR(string s)
        {
            return Marshal.StringToBSTR(s);
        }

        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAnsi(string s)
        {
            return Marshal.StringToCoTaskMemAnsi(s);
        }

        [SecurityCritical]
        public static IntPtr StringToCoTaskMemAuto(string s)
        {
            return Marshal.StringToCoTaskMemAuto(s);
        }

        [SecurityCritical]
        public static IntPtr StringToCoTaskMemUni(string s)
        {
            return Marshal.StringToCoTaskMemUni(s);
        }

        [SecurityCritical]
        public static IntPtr StringToHGlobalAnsi(string s)
        {
            return Marshal.StringToHGlobalAnsi(s);
        }

        [SecurityCritical]
        public static IntPtr StringToHGlobalAuto(string s)
        {
            return Marshal.StringToHGlobalAuto(s);
        }

        [SecurityCritical]
        public static IntPtr StringToHGlobalUni(string s)
        {
            return Marshal.StringToHGlobalUni(s);
        }


        #endregion

        #region Secure String

        [SecurityCritical]
        public static IntPtr SecureStringToBSTR(SecureString s)
        {
            return Marshal.SecureStringToBSTR(s);
        }

        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
        {
            return Marshal.SecureStringToCoTaskMemAnsi(s);
        }

        [SecurityCritical]
        public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
        {
            return Marshal.SecureStringToCoTaskMemUnicode(s);
        }

        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
        {
            return Marshal.SecureStringToGlobalAllocAnsi(s);
        }

        [SecurityCritical]
        public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
        {
            return Marshal.SecureStringToGlobalAllocUnicode(s);
        }


        #endregion

        #region Re Alloc

        [SecurityCritical]
        public static IntPtr ReAllocCoTaskMem(IntPtr pv, int cb)
        {
            return Marshal.ReAllocCoTaskMem(pv, cb);
        }

        [SecurityCritical]
        public static IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb)
        {
            return Marshal.ReAllocHGlobal(pv, cb);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int Release(IntPtr pUnk)
        {
            return Marshal.Release(pUnk);
        }

        [SecurityCritical]
        public static int ReleaseComObject(object o)
        {
            return Marshal.ReleaseComObject(o);
        }

        #endregion

        #region PtrToString

        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringAnsi(ptr, len);
        }

        [SecurityCritical]
        public static string PtrToStringAnsi(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr)
        {
            return Marshal.PtrToStringAuto(ptr);
        }

        [SecurityCritical]
        public static string PtrToStringAuto(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringAuto(ptr, len);
        }

        [SecurityCritical]
        public static string PtrToStringBSTR(IntPtr ptr)
        {
            return Marshal.PtrToStringBSTR(ptr);
        }

        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr)
        {
            return Marshal.PtrToStringUni(ptr);
        }

        [SecurityCritical]
        public static string PtrToStringUni(IntPtr ptr, int len)
        {
            return Marshal.PtrToStringUni(ptr, len);
        }

        [ComVisible(true), SecurityCritical]
        public static void PtrToStructure(IntPtr ptr, object structure)
        {
            Marshal.PtrToStructure(ptr, structure);
        }

        [ComVisible(true), SecurityCritical]
        public static object PtrToStructure(IntPtr ptr, Type structureType)
        {
            return Marshal.PtrToStructure(ptr, structureType);
        }

        [SecurityCritical]
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        [SecurityCritical]
        public static void PtrToStructure<T>(IntPtr ptr, T structure)
        {
            Marshal.PtrToStructure(ptr, structure);
        }

        #endregion

        #region Get

        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
        {
            return Marshal.GetComInterfaceForObject(o, T, mode);
        }

        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
        {
            return Marshal.GetComInterfaceForObject<T, TInterface>(o);
        }

        [SecurityCritical]
        public static IntPtr GetComInterfaceForObject(object o, Type T)
        {
            return Marshal.GetComInterfaceForObject(o, T);
        }

        [SecurityCritical]
        public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
        {
            return Marshal.GetDelegateForFunctionPointer(ptr, t);
        }

        [SecurityCritical]
        public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
        {
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(ptr);
        }

        [SecurityCritical]
        public static int GetExceptionCode()
        {
            return Marshal.GetExceptionCode();
        }

        [SecurityCritical]
        public static Exception GetExceptionForHR(int errorCode)
        {
            return Marshal.GetExceptionForHR(errorCode);
        }

        [SecurityCritical]
        public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
        {
            return Marshal.GetExceptionForHR(errorCode, errorInfo);
        }

        [SecurityCritical]
        public static IntPtr GetFunctionPointerForDelegate(Delegate d)
        {
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        [SecurityCritical]
        public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
        {
            return Marshal.GetFunctionPointerForDelegate(d);
        }

        [SecurityCritical]
        public static int GetHRForException(Exception e)
        {
            return Marshal.GetHRForException(e);
        }

        [SecurityCritical, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static int GetHRForLastWin32Error()
        {
            return Marshal.GetHRForLastWin32Error();
        }
        
        [SecurityCritical]
        public static IntPtr GetIUnknownForObject(object o)
        {
            return Marshal.GetIUnknownForObject(o);
        }
        
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SecurityCritical]
        public static int GetLastWin32Error()
        {
            return Marshal.GetLastWin32Error();
        }
        
        [SecurityCritical]
        public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
        {
            Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

        [SecurityCritical]
        public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
        {
            Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
        }

        [SecurityCritical]
        public static object GetObjectForIUnknown(IntPtr pUnk)
        {
            return Marshal.GetObjectForIUnknown(pUnk);
        }

        [SecurityCritical]
        public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
        {
            return Marshal.GetObjectForIUnknown(pSrcNativeVariant);
        }

        [SecurityCritical]
        public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
        {
            return Marshal.GetObjectForNativeVariant<T>(pSrcNativeVariant);
        }

        [SecurityCritical]
        public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
        {
            return Marshal.GetObjectsForNativeVariants(aSrcNativeVariant, cVars);
        }

        [SecurityCritical]
        public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
        {
            return Marshal.GetObjectsForNativeVariants<T>(aSrcNativeVariant, cVars);
        }

        [SecurityCritical]
        public static int GetStartComSlot(Type t)
        {
            return Marshal.GetStartComSlot(t);
        }

        [SecuritySafeCritical]
        public static Type GetTypeFromCLSID(Guid clsid)
        {
            return Marshal.GetTypeFromCLSID(clsid);
        }

        [SecurityCritical]
        public static string GetTypeInfoName(ITypeInfo typeInfo)
        {
            return Marshal.GetTypeInfoName(typeInfo);
        }

        [SecurityCritical]
        public static object GetUniqueObjectForIUnknown(IntPtr unknown)
        {
            return Marshal.GetUniqueObjectForIUnknown(unknown);
        }

        #endregion
    }
}