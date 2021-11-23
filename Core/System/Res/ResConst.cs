/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-22                    *
* Nowtime:           19:03:05                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Res
{
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// 资源路径配置
    /// </summary>
    public static class ResConst
    {
#if UNITY_EDITOR
        /// <summary>
        /// 编辑器模式 Package包
        /// </summary>
        public static readonly int EditorProviderID = "com.self.core.editor.AssetDatabaseProvider".GetHashCode();
#endif

        private static string _Platform = null;
        /// <summary>
        /// 当前运行平台
        /// </summary>
        public static string Platform
        {
            get
            {
                if (string.IsNullOrEmpty(_Platform))
                {
                    _Platform = Application.platform.ToString();
                }
                return _Platform;
            }
        }

        private static bool _EditorMode = false;
        /// <summary>
        /// 运行模式
        /// </summary>
        public static bool EditorMode
        {
            get => Application.isEditor && _EditorMode;
            set { if (Application.isEditor) _EditorMode = value; }
        }

        private static string _RuntimePath = null;
        /// <summary>
        /// 运行平台路径
        /// Android /data/data/xxx.xxx.xxx/files
        /// IOS Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/Documents
        /// </summary>
        public static string RuntimePath
        {
            get
            {
                if (_RuntimePath != null) return _RuntimePath;
                //如果当前为Win 并且不是编辑器模式 
                if (Application.platform == RuntimePlatform.WindowsPlayer && !Application.isEditor)
                    _RuntimePath = $"{Directory.GetCurrentDirectory().Replace("\\", "/")}/Runtime/";
                else//如果当前为编辑模式 则获取当前环境路径 否则取对应平台可持续化路径
                    _RuntimePath = Application.isEditor ?
                        ($"{System.Environment.CurrentDirectory}/Runtime/").Replace("\\", "/") :
                        $"{Application.persistentDataPath}/";
                if (!Directory.Exists(_RuntimePath))
                    Directory.CreateDirectory(_RuntimePath);

                if (!Application.isEditor) Debug.Log($"RuntimePath -> {_RuntimePath}");
                return _RuntimePath;
            }
        }

        private static string _StreamingAssetsPath = null;
        /// <summary>
        /// 流数据路径 安卓在!assets目录下
        /// Android	jar:file:///data/app/xxx.xxx.xxx.apk/!/assets
        /// IOS Application/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/xxx.app/Data/Raw
        /// </summary>
        public static string StreamingAssetsPath
        {
            get
            {
                if (_StreamingAssetsPath != null) return _StreamingAssetsPath;

                _StreamingAssetsPath = $"{Application.streamingAssetsPath}/";
                if (Application.isEditor || Application.platform != RuntimePlatform.Android)
                    _StreamingAssetsPath = $"file://{_StreamingAssetsPath}";

                if (!Application.isEditor)
                    Debug.Log($"StreamingAssetsPath -> {_StreamingAssetsPath}");

                return _StreamingAssetsPath;
            }
        }

        /// <summary>
        /// ResourceManifest
        /// </summary>
        public const string ResourceManifestFile = "ResourceManifest";

        /// <summary>
        /// 流数据的缓存目录
        /// </summary>
        public const string StreamingAssetsManifestFile = "StreamingAssetsManifest";

        /// <summary>
        /// 资源配置文件
        /// </summary>
        public const string ResourceConfigFile = "ResourceConfig";

        /// <summary>
        /// 程序版本
        /// </summary>
        public static string AppVersion { get; private set; }

        /// <summary>
        /// 资源版本
        /// </summary>
        public static string ResVersion { get; private set; }

    }
}