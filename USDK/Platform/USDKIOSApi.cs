/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           20:51:13                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// IOS SDK
    /// </summary>
    public class USDKIOSApi : IUSDkApi
    {
        [DllImport("__Internal")]
        private static extern void __CallPlugin(string pluginName, string methodName, string[] parameters);
        public void CallPlugin(string pluginName, string methodName, params object[] parameters)
        {
            string[] args = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                args[i] = parameters[i].ToString();
            __CallPlugin(pluginName, methodName, args);
        }

        [DllImport("__Internal")]
        private static extern string __CallPluginR(string pluginName, string methodName, string[] parameters);
        public R CallPlugin<R>(string pluginName, string methodName, params object[] parameters)
        {
            string[] args = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                args[i] = parameters[i].ToString();

            string ret = __CallPluginR(pluginName, methodName, args);
            R retR = (R)Convert.ChangeType(ret, typeof(R));
            return retR;
        }

        public void CallPluginOlny(string methodName, object parameters)
        {

        }
    }
}