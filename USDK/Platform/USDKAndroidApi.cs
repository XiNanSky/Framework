/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           19:32:20                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 安卓调用SDK
    /// </summary>
    public class USDKAndroidApi : IUSDkApi
    {
        private string pluginPreName;
        private string javaCallName;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="PluginPreName">第三方集成命名空间</param>
        /// <param name="JavaClassName">Java主函数类包路径</param>
        public USDKAndroidApi(string PluginPreName, string JavaClassName)
        {
            pluginPreName = string.Concat(PluginPreName, '.');
            javaCallName = JavaClassName;

            try
            {
                usdk = new AndroidJavaClass(javaCallName);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public void CallPlugin(string pluginName, string methodName, params object[] parameters)
        {
            SendAndroidMessage(pluginName, methodName, parameters);
        }

        public R CallPlugin<R>(string pluginName, string methodName, params object[] parameters)
        {
            return SendAndroidMessage<R>(pluginName, methodName, parameters);
            //return JsonKit.Deserialize<R>(CallPluginRarg(pluginName, methodName, parameters));
        }

        private Dictionary<int, object> dic = new Dictionary<int, object>();

#if UNITY_ANDROID
        private AndroidJavaClass usdk;
#endif

        private void SendAndroidMessage(string pluginName, string method, params object[] parameters)
        {
#if UNITY_ANDROID
            try
            {
                pluginName = pluginPreName + pluginName;
                if (usdk != null)
                {
                    AndroidJavaObject context = usdk.CallStatic<AndroidJavaObject>("getPlugin", pluginName);
                    if (context != null)
                        context.Call(method, parameters);
                }
            }
            catch (Exception ex) { Debug.LogError(ex.Message); }
#endif
        }

        private R SendAndroidMessage<R>(string pluginName, string method, params object[] parameters)
        {
#if UNITY_ANDROID
            try
            {
                if (usdk != null)
                {
                    pluginName = string.Concat(pluginPreName, pluginName);
                    AndroidJavaObject context = usdk.CallStatic<AndroidJavaObject>("getPlugin", pluginName);
                    if (context != null)
                        return context.Call<R>(method, parameters);
                    else
                    {
                        Debug.LogError($"context not found : {pluginName} , {method} , {parameters.ToString()} , {typeof(R).ToString()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return default;
            }
#endif
            return default;
        }


        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="parameters">参数数据</param>
        /// <returns></returns>
        public void CallPluginOlny(string methodName, object parameters)
        {
            if (usdk != null)
            {
                try
                {
                    //var d = JsonKit.Serialize(parameters);
                    var json = JsonKit.Serialize(new
                    {
                        Type = methodName,                   //方法名
                        Data = parameters,                   //传参数据
                    });
                    usdk.CallStatic("invokePluginMethodOnly", json);
                }
                catch (Exception ex) { Debug.LogError(ex.Message); }
            }
        }
    }
}