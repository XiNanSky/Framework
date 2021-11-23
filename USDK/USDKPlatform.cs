/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           19:56:33                      *
* Description:                                     *
* History:                                         *
***************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.USDK
{
    /// <summary>
    /// SDK 平台化
    /// </summary>
    /// 理解: SDK为软件应用商店提供的接口方法
    /// 当前主流平台 Android IOS WINDOS Linux
    /// 所以 可分为
    /// Android QQ WX 美团 百度 支付宝 网易 等
    /// IOS     QQ WX 美团 百度 支付宝 网易 等
    /// WINDOS  QQ WX 美团 百度 支付宝 网易 等
    /// 平台为系统 SDK为软件为适配系统硬件 所出现的中间交互层
    /// 当前类 代理处理 也就是统一细化处理 因平台不同出现的SDK交互代码
    public static partial class USDKPlatform
    {
        /// <summary>
        /// 当前平台对应API 处理器
        /// </summary>
        public static IUSDkApi Api { get; private set; } = null;

        /// <summary>
        /// 事件注册表
        /// </summary>
        public static Dictionary<int, Func<object, List<string>>> Register { get; private set; } = null;

        public static void Initialize()
        {
            //设置SDK 回调
            USDKCallBack.Create("UsdkCallBack").OnCallBack = OnUSDKCallBack;

            if (Application.platform == RuntimePlatform.Android)
                Api = new USDKAndroidApi("com.usdk.plugin", "com.usdk.sdk.Usdk");
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                Api = new USDKIOSApi();
            else
                Api = new USDKWindosApi();
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="Type">消息类型</param>
        /// <param name="Data">回调数据</param>
        private static object OnUSDKCallBack(int Type, List<string> Data)
        {
            if (Register.ContainsKey(Type))
            {
                Register.TryGetValue(Type, out var func);
                return func(Data);
            }
            else Debug.LogError(string.Format("USDK CALL BACK ERROR.'{0}' undefine in 'UsdkCallBackErrorCode' DATA : {1}", Type, Data));
            return null;
        }

        /// <summary>
        /// 添加注册事件
        /// </summary>
        /// <param name="Type">事件ID</param>
        /// <param name="Func">注册函数</param>
        public static void AddUSDKRegister(int Type, Func<object, List<string>> Func)
        {
            if (!Register.ContainsKey(Type))
            {
                Register.Add(Type, Func);
            }
            else Debug.LogError("USDK CALL BACK ERROR : 当前注册事件类型已经存在!");
        }

        /// <summary>
        /// 移除注册事件
        /// </summary>
        /// <param name="Type">事件ID</param>
        public static void RemoveUSDKRegister(int Type)
        {
            if (Register.ContainsKey(Type))
            {
                Register.Remove(Type);
            }
            else Debug.LogError("USDK CALL BACK ERROR : 当前注册事件类型不存在!");
        }

        /// <summary>
        /// 调用指定SDK方法
        /// </summary>
        /// <param name="Index">SDK下标</param>
        /// <param name="Method">方法名</param>
        /// <param name="Data">数据</param>
        public static void CallPlugin(string Plugin, string Method, object Data)
        {
            Api.CallPlugin(Plugin, Method, Data);
        }

        /// <summary>
        /// 调用指定SDK方法
        /// </summary>
        /// <param name="Index">SDK下标</param>
        /// <param name="Method">方法名</param>
        /// <param name="Data">数据</param>
        [Obsolete("Java SDK 目前不建议 且不支持返回参数 类型转换复杂 可能出现数据调用顺序错误 一般采用事件回调")]
        public static R CallPlugin<R>(string Plugin, string Method, object Data)
        {
            return Api.CallPlugin<R>(Plugin, Method, Data);
        }

        /// <summary>
        /// 调用特殊SDK方法
        /// </summary>
        /// <param name="Method">方法名</param>
        /// <param name="Data">数据</param>
        public static void CallPluginOnly(string Method, object Data)
        {
            Api.CallPluginOlny(Method, Data);
        }

        /// <summary>
        /// 调用特殊通用方法
        /// </summary>
        /// <param name="Data">数据</param>
        public static void LogReport(object Data)
        {
            Api.CallPlugin(PLATFORM_NAME, "logReportCore", JsonKit.Serialize(Data));
        }

        /// <summary>
        /// 调用特殊通用方法
        /// </summary>
        /// <param name="Data">数据</param>
        public static void LogReport(string Data)
        {
            Api.CallPlugin(PLATFORM_NAME, "logReportCore", Data);
        }
    }

    /// <summary>
    /// SDK API
    /// </summary>
    public static partial class USDKPlatform
    {
        public const string PLATFORM_NAME = "PlatformProxy";

        // public static string GetConfig(string key)
        // {
        //     if (key == null) return string.Empty;
        //     return CallPlugin<string>(PLATFORM_NAME, "getConfig", key);
        // }

        public static void Login(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "login", custom_params_);
        }

        public static void Logout(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "logout", custom_params_);
        }

        public static void Pay(USDKPayInfo payInfo)
        {
            CallPlugin(PLATFORM_NAME, "pay", payInfo.ToString());
        }

        public static void Quit(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "exit", custom_params_);
        }

        public static void OpenUserCenter(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "openUserCenter", custom_params_);
        }

        public static void SwitchAccount(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "switchAccount", custom_params_);
        }

        public static void OpenAppstoreComment(string appid)
        {
            CallPlugin(PLATFORM_NAME, "openAppstoreComment", appid);
        }

        public static void ReleaseSdkResource(string custom_params_)
        {
            CallPlugin(PLATFORM_NAME, "releaseSdkResource", custom_params_);
        }
    }
}
