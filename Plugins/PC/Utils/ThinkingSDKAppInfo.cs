using System;
using ThinkingSDK.PC.Config;
using ThinkingSDK.PC.Constant;
using UnityEngine;

namespace ThinkingSDK.PC.Utils
{
    public class ThinkingSDKAppInfo
    {
        /// <summary>
        /// SDK版本号
        /// </summary>
        public static string LibVersion()
        {
            return ThinkingSDKPublicConfig.Version() ;
        }
        /// <summary>
        /// SDK名称
        /// </summary>
        public static string LibName()
        {
            return ThinkingSDKPublicConfig.Name();
        }
        /// <summary>
        /// app版本号
        /// </summary>
        public static string AppVersion()
        {
            return Application.version;
        }
        /// <summary>
        /// app唯一标识 包名
        /// </summary>
        public static string AppIdentifier()
        {
            return Application.identifier;
        }
     
    }
}