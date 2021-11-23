/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-22                    *
* Nowtime:           18:54:28                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Res
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// 资源类型
    /// </summary>
    [Flags]
    public enum ResourceType : byte
    {
        None = 0,
        /// <summary>
        /// 文件
        /// </summary>
        Asset = 1,
        /// <summary>
        /// 集合包
        /// </summary>
        Bundle = 2,
    }

    /// <summary>
    /// 资源配置
    /// </summary>
    public class ResManifest
    {
        /// <summary>
        /// 信息
        /// </summary>
        [Serializable]
        internal class Location
        {
            /// <summary>
            /// 资源类型
            /// </summary>
            public byte ResourceType;

            /// <summary>
            /// 
            /// </summary>
            public int ResrouceProviderID;

            /// <summary>
            /// 依赖包名信息 
            /// </summary>
            public string[] DependencyLocations;

            /// <summary>
            /// 包名
            /// </summary>
            public string Label;

            /// <summary>
            /// 校验码
            /// </summary>
            public uint CRC32;

            /// <summary>
            /// 大小
            /// </summary>
            public int Size;
        }

        /// <summary>
        /// 加载包名
        /// </summary>
        public List<string> LocalLabels { get; private set; }

        /// <summary>
        /// 被删除包名
        /// </summary>
        public string[] Remotelabels { get; private set; }

        /// <summary>
        /// 数据表
        /// </summary>
        private Dictionary<string, Location> _Locations;

        /// <summary>
        /// 流数据资源包名
        /// </summary>
        private static HashSet<string> _StreamingAssetsBundles = null;

        /// <summary>
        /// 下载资源包名
        /// </summary>
        private static List<string> _DownloadLabels = null;

        internal ResManifest()
        {
            _Locations = new Dictionary<string, Location>();
        }

        /// <summary>
        /// 加载流数据缓存目录配置
        /// </summary>
        /// <returns></returns>
        internal static IEnumerator LoadStreamingAssetsManifest()
        {
            _StreamingAssetsBundles = new HashSet<string>();
            using (var uwr = new UnityWebRequest($"{ResConst.StreamingAssetsPath}{ResConst.StreamingAssetsManifestFile}", UnityWebRequest.kHttpVerbGET, new DownloadHandlerBuffer(), null))
            {
                yield return uwr.SendWebRequest();
#if UNITY_2020_1_OR_NEWER
                if (uwr.result != UnityWebRequest.Result.Success)
#else
                if (uwr.isHttpError || uwr.isNetworkError || !string.IsNullOrEmpty(uwr.error))
#endif
                    Debug.LogError($"{uwr.error} - {ResConst.StreamingAssetsPath}{ResConst.ResourceManifestFile}");
                else
                {
                    using (var ms = new MemoryStream(uwr.downloadHandler.data))
                    {//从内存流 写入 字节流
                        using (var br = new BinaryReader(ms))
                        {//序列化
                            int count = br.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                _StreamingAssetsBundles.Add(br.ReadString());
                            }
                        }
                    }
                }
                uwr.Dispose();
            }

            //判断是否存在是否需要下载的包
            if (File.Exists($"{ResConst.RuntimePath}DownloadLabels"))
                _DownloadLabels = JsonKit.Deserialize<List<string>>(File.ReadAllText($"{ResConst.RuntimePath}DownloadLabels"));
            else
                _DownloadLabels = new List<string>();
        }

        /// <summary>
        /// 创建文本表单
        /// </summary>
        internal static ResManifest CreateFromTxt(byte[] bytes)
        {
            ResManifest manifest = null;
            using (var ms = new MemoryStream(bytes))
            {
                manifest = CreateFromStream(ms);
            }
            return manifest;
        }

        /// <summary>
        /// 创建表单信息
        /// </summary>
        internal static ResManifest CreateFromStream(Stream stream)
        {
            var manifest = new ResManifest();
            try
            {
                using (var br = new BinaryReader(stream))
                {
                    var labelCount = br.ReadInt32();            //获取包名长度
                    var labels = new string[labelCount];
                    for (int i = 0; i < labelCount; i++)        //遍历
                    {
                        labels[i] = br.ReadString();
                    }
                    var localLabelCount = br.ReadInt32();       //获取本地包名长度
                    manifest.LocalLabels = new List<string>();
                    for (int i = 0; i < localLabelCount; i++)   //将包名添加进去
                    {
                        manifest.LocalLabels.Add(labels[i]);
                    }
                    if (labelCount > localLabelCount)           //如果包名长度 大于需要下载的包长度 则说明 包名中含有需要删除的包名
                    {
                        manifest.Remotelabels = new string[labelCount - localLabelCount];
                        for (int i = localLabelCount; i < labelCount; i++)//获取需要删除的包名
                        {
                            manifest.Remotelabels[i - localLabelCount] = labels[i];
                        }
                    }
                    //给需要更新或者下载的包 设置对应属性
                    var locationCount = br.ReadInt32();
                    var locations = new Dictionary<string, Location>(locationCount);
                    var keys = new string[locationCount];
                    manifest._Locations = locations;
                    for (int i = 0; i < locationCount; i++)
                    {
                        keys[i] = br.ReadString();
                        locations.Add(keys[i], new Location());
                    }

                    for (int i = 0; i < locationCount; i++)
                    {
                        var location = locations[keys[i]];
                        location.ResourceType = br.ReadByte();
                        location.ResrouceProviderID = br.ReadInt32();
                        int depCount = br.ReadInt32();
                        if (depCount > 0)
                        {
                            location.DependencyLocations = new string[depCount];
                            for (int depIndex = 0; depIndex < depCount; depIndex++)
                            {
                                location.DependencyLocations[depIndex] = keys[br.ReadInt32()];
                            }
                        }
                        location.Label = labels[br.ReadInt32()];
                        location.CRC32 = br.ReadUInt32();
                        location.Size = br.ReadInt32();
                    }
                }
                //如果需要下载包 则同样加入到本地包名中
                if (_DownloadLabels.Count > 0) manifest.LocalLabels.AddRange(_DownloadLabels);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
            return manifest;
        }

        /// <summary>
        /// 判断是否含有指定包
        /// </summary>
        internal bool Exists(string location)
        {
            return _Locations.ContainsKey(location);
        }

        /// <summary>
        /// 获取指定文件或者目录的流数据路径 如果流数据列表中 不存在 则不会返回
        /// </summary>
        internal string GetStreamingAssetsPath(string location)
        {
            if (_StreamingAssetsBundles == null) return null;
            if (!_StreamingAssetsBundles.Contains(location)) return null;
            return $"{Application.streamingAssetsPath}/{location}";
        }

        /// <summary>
        /// 获取资源类型
        /// </summary>
        internal ResourceType GetResourceType(string location)
        {
            return _Locations.TryGetValue(location, out var value) ?
                (ResourceType)value.ResourceType :
                ResourceType.None;
        }

        /// <summary>
        /// 获取
        /// </summary>
        internal int GetResourceProviderID(string location)
        {
            return _Locations.TryGetValue(location, out var value) ?
                value.ResrouceProviderID :
                ResConst.EditorProviderID;
        }

        /// <summary>
        /// 获取指定包名依赖包列表
        /// </summary>
        internal string[] GetDependencies(string location)
        {
            return _Locations.TryGetValue(location, out var value) ? value.DependencyLocations : null;
        }

        /// <summary>
        /// 获取指定包 是否含有依赖包
        /// </summary>
        internal bool LocationHasDependencies(string location)
        {
            var result = GetDependencies(location);
            if (result == null) return false;
            return result.Length > 0;
        }

        /// <summary>
        /// 获取指定包的包名
        /// </summary>
        internal string GetLabel(string location)
        {
            return _Locations.TryGetValue(location, out var value) ? value.Label : null;
        }

        /// <summary>
        /// 获取指定包的校验码
        /// </summary>
        internal uint GetCRC32(string location)
        {
            return _Locations.TryGetValue(location, out var value) ? value.CRC32 : 0u;
        }

        /// <summary>
        /// 获取指定包大小
        /// </summary>
        internal long GetSize(string location)
        {
            return _Locations.TryGetValue(location, out var value) ? value.Size : 0;
        }

        /// <summary>
        /// 根据指定标签过滤包名
        /// </summary>
        internal List<string> FilterByLabels(List<string> labels)
        {
            if (labels == null || labels.Count == 0) return null;
            if (_Locations == null) return null;
            var locations = new List<string>();
            foreach (var location in _Locations)
            {
                if (location.Value.ResourceType != (byte)ResourceType.Bundle) continue;
                if (labels.Contains(location.Value.Label)) locations.Add(location.Key);
            }
            return locations;
        }

        /// <summary>
        /// 添加需要下载的包
        /// </summary>
        internal void AddDownloadLabels(List<string> labels)
        {
            if (labels == null) return;
            if (labels.Count == 0) return;
            foreach (var label in LocalLabels)
            {
                if (labels.Contains(label)) labels.Remove(label);
            }
            foreach (var label in _DownloadLabels)
            {
                if (labels.Contains(label)) labels.Remove(label);
            }
            if (labels.Count == 0) return;
            LocalLabels.AddRange(labels);
            _DownloadLabels.AddRange(labels);
            IOKit.WriteUTF8($"{ResConst.RuntimePath}DownloadLabels", JsonKit.Serialize(_DownloadLabels));
        }

        /// <summary>
        /// 判断当前包列表是否全部在加载列表中
        /// </summary>
        internal bool IsLocalLabels(List<string> labels)
        {
            foreach (var label in labels)
            {
                if (!LocalLabels.Contains(label)) return false;
            }
            return true;
        }
    }
}
