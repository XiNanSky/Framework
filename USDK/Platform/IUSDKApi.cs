/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           19:34:11                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.USDK
{
    /// <summary>
    /// 通用调用SDK接口 
    /// </summary>
    public interface IUSDkApi
    {
        /// <summary>
        /// 调用SDK方法
        /// </summary>
        /// <param name="pluginName">SDK名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        void CallPlugin(string pluginName, string methodName, params object[] parameters);

        /// <summary>
        /// 返回指定类型带参数
        /// </summary>
        /// <param name="pluginName">SDK名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        R CallPlugin<R>(string pluginName, string methodName, params object[] parameters);

        /// <summary>
        /// 调用SDK 特有方法
        /// </summary>
        /// <param name="pluginName">SDK名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        void CallPluginOlny(string methodName, object parameters);
    }
}