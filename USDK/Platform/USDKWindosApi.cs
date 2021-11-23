/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-09                    *
* Nowtime:           09:06:09                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System;
    using System.Reflection;
    using UnityEngine;

    public class USDKWindosApi : IUSDkApi
    {
        public USDKWindosApi()
        {

        }

        public void CallPlugin(string pluginName, string methodName, params object[] parameters)
        {
            MethodInfo method = GetType().GetMethod(methodName);
            BindingFlags flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            if (method != null)
                method.Invoke(this, flag, Type.DefaultBinder, parameters, null);
        }

        public R CallPlugin<R>(string pluginName, string methodName, params object[] parameters)
        {
            MethodInfo method = GetType().GetMethod(methodName);
            BindingFlags flag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            if (method != null)
                return (R)method.Invoke(this, flag, Type.DefaultBinder, parameters, null);
            return default;
        }

        public void CallPluginOlny(string methodName, object parameters)
        {

        }

        private void SendCallBack(int code)
        {
            SendCallBack(code, null);
        }

        /// <summary>
        /// 发送回调 使用GameObject.Find SendMessage
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ret"></param>
        private void SendCallBack(int code, string ret)
        {
            string msg = string.Format("errorCode={0}", code);
            if (!string.IsNullOrEmpty(ret))
            {
                msg = string.Format("{0}&paramString={1}", msg, ret);
            }
            GameObject.Find("UsdkCallBack").SendMessage("CallBack", msg);
        }
    }
}