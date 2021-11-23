/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           20:17:12                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 解析Windos传过来的字符串数据
    /// </summary>
    public class USDKCallBackRetMsg
    {
        public int ErrorCode;
        /*
        InitSuccess,
        InitFail,
        LoginSuccess,
        LoginCancel,
        LoginFail,
        LogoutFinish,
        ExitNoChannelExiter,
        ExitSuccess,
        PaySuccess,
        PayCancel,
        PayFail,
        PayProgress,
        PayOthers
        */

        public List<string> msg;

        public USDKCallBackRetMsg(string ret)
        {
            msg = new List<string>();
            string[] retInfo = ret.Split('&');
            for (int i = 0; i < retInfo.Length; i++)
            {
                string[] subMsgs = retInfo[i].Split('=');
                if (subMsgs[0] == "errorCode")
                    ErrorCode = Convert.ToInt32(subMsgs[1]);
                else
                    msg.Add(subMsgs[1]);
            }
        }
    }

    /// <summary>
    /// SDK回调 不可销毁 需要初始化 在USDKPlatform
    /// </summary>
    public class USDKCallBack : MonoBehaviour
    {
        public Func<int, List<string>, object> OnCallBack;

        public static USDKCallBack Create(string objName )
        {
            var callBackObj = new GameObject(objName).AddComponent<USDKCallBack>();
            DontDestroyOnLoad(callBackObj);
            return callBackObj;
        }

        public static USDKCallBack Create()
        {
            var callBackObj = new GameObject("USDKCallBack").AddComponent<USDKCallBack>();
            DontDestroyOnLoad(callBackObj);
            return callBackObj;
        }

        public void CallBack(string ret)
        {
            var retInfo = new USDKCallBackRetMsg(ret);
            OnCallBack?.Invoke(retInfo.ErrorCode, retInfo.msg);
        }
    }
}