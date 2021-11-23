/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-04                    *
* Nowtime:           16:58:31                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class WebRequestCert : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //return base.ValidateCertificate(certificateData);
            return true;
        }
    }

    /// <summary>
    /// 下载请求
    /// </summary>
    public class UnityWebRequestCustomDownloadHandler : DownloadHandlerScript
    {
        /// <summary>
        /// 请求时间？
        /// </summary>
        private long mExpected = 0L;

        /// <summary>
        /// 接收数据？
        /// </summary>
        private long mReceived = 0;

        /// <summary>
        /// 文件路径
        /// </summary>
        private readonly string mFilepath;

        /// <summary>
        /// 文件数据流
        /// </summary>
        private readonly FileStream mFileStream;

        /// <summary>
        /// 取消下载？
        /// </summary>
        private bool mIsCanceled = false;

        /// <summary>
        /// 下载完成回调
        /// </summary>
        public event Action CompletedEvent;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UnityWebRequestCustomDownloadHandler(byte[] buffer, string filepath) : base(buffer)
        {
            mFilepath = filepath;
            //mFileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            //获取文件目录
            var directory = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            //如果文件存在 则覆盖 不存在 就创建
            if (File.Exists(mFilepath))
                mFileStream = new FileStream(mFilepath, FileMode.Append, FileAccess.Write);
            else
                mFileStream = new FileStream(mFilepath, FileMode.Create, FileAccess.Write);
            //获取文件大小
            mReceived = mFileStream.Length;
        }

        protected override byte[] GetData() { return null; }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length < 1)
            {
                return false;
            }
            mReceived += (uint)dataLength;
            if (!mIsCanceled)
                mFileStream.Write(data, 0, dataLength);
            return true;
        }

        protected override float GetProgress()
        {
            if (mExpected <= 0)
                return 0;
            return mReceived * 1f / mExpected;
        }

        protected override void CompleteContent()
        {
            mFileStream.Close();
            mFileStream.Dispose();
            CompletedEvent?.Invoke();
        }

#if UNITY_2018 || UNITY_2019
        protected override void ReceiveContentLength(int contentLength)
#else
        protected override void ReceiveContentLengthHeader(ulong contentLength)
#endif
        {
            mExpected = contentLength + mReceived;
        }

        public void Cancel()
        {
            mIsCanceled = true;
            mFileStream.Close();
            mFileStream.Dispose();
            File.Delete(mFilepath);
        }
    }

    /// <summary>
    /// Unity Web 网络请求
    /// 优化方法 将回调结果 有顺序执行 并入到统一队列 依次执行
    /// </summary>
    public class UnityWebRequestKit
    {
        /// <summary>
        /// 请求文本
        /// </summary>
        /// <param name="url">主机地址</param>
        /// <param name="onCompleted">回调 boo = ture 代表请求失败 </param>
        public static async void Get(string url, Action<bool, string> onCompleted)
        {
            var state = false; string data = null;
            using (var uwr = UnityWebRequest.Get(url))
            {
                await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                state = uwr.isNetworkError || uwr.isHttpError;
#else
                state = uwr.result == UnityWebRequest.Result.Success;
#endif
                data = uwr.downloadHandler.text;
                if (state)
                    Debug.LogError(uwr.error);
            }
            onCompleted?.Invoke(state, data);
        }

        /// <summary>
        /// POST 提交 json 或者 字符串文本
        /// </summary>
        /// <param name="url">地主</param>
        /// <param name="json">内容</param>
        /// <param name="completed">回调</param>
        public static async void PostFromJson(string url, string json, Action<bool, string> completed)
        {
            bool state = false;
            string data = null;
            using (var uwr = new UnityWebRequest(url, "POST"))
            {
                uwr.useHttpContinue = false;
                using (var uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)))
                {
                    uwr.uploadHandler = uploadHandler;
                    uwr.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
                    using (var downloadHandler = (DownloadHandler)new DownloadHandlerBuffer())
                    {
                        uwr.downloadHandler = downloadHandler;
                        await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                        state = uwr.isNetworkError || uwr.isHttpError;
#else
                        state = uwr.result == UnityWebRequest.Result.Success;
#endif
                        data = uwr.downloadHandler.text;
                        if (state)
                            Debug.LogError(uwr.error);
                    }
                }
            }
            completed?.Invoke(state, data);
        }

        /// <summary>
        /// POST 提交json表单
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="json">内容</param>
        /// <param name="completed">回调</param>
        public static async void PostFromData(string url, string json, Action<bool, string> completed)
        {
            var state = false; string data = null;
            var postData = JsonKit.Deserialize<Dictionary<string, string>>(json);
            using (var uwr = UnityWebRequest.Post(url, postData))
            {
                uwr.useHttpContinue = false;
                await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                state = uwr.isNetworkError || uwr.isHttpError;
#else
                state = uwr.result == UnityWebRequest.Result.Success;
#endif
                data = uwr.downloadHandler.text;
                if (state)
                    Debug.LogError(uwr.error);
            }
            completed?.Invoke(state, data);
        }

        /// <summary>
        /// POST 提交表单
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="formDatas">内容</param>
        /// <param name="completed">回调</param>
        public static async void PostFromData(string url,
                                      List<IMultipartFormSection> formDatas,
                                      Action<bool, string> completed)
        {
            bool state = false;
            string data = null;
            using (var uwr = UnityWebRequest.Post(url, formDatas))
            {
                uwr.useHttpContinue = false;
                await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                state = uwr.isNetworkError || uwr.isHttpError;
#else
                state = uwr.result == UnityWebRequest.Result.Success;
#endif
                data = uwr.downloadHandler.text;
                if (state)
                    Debug.LogError(uwr.error);
            }
            completed?.Invoke(state, data);
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="contentBytes">上传数据</param>
        /// <param name="completed">回调</param>
        /// <param name="contentType">文件格式</param>
        /// <param name="headerParams">头参数</param>
        public static async void Upload(string url,
                                        byte[] contentBytes,
                                        Action<bool, long> completed = null,
                                        string contentType = "application/octet-stream",
                                        Hashtable headerParams = null)
        {
            bool state = false;
            long data = 0;
            using (UnityWebRequest uwr = UnityWebRequest.Put(url, contentBytes))
            {
                if (headerParams != null)
                {
                    foreach (string key in headerParams.Keys)
                    {
                        uwr.SetRequestHeader(key, headerParams[key].ToString());
                    }
                }
                using (var uploadHandler = new UploadHandlerRaw(contentBytes))
                {
                    uploadHandler.contentType = contentType;
                    uwr.uploadHandler = uploadHandler;
                    await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                    state = uwr.isNetworkError || uwr.isHttpError;
#else
                    state = uwr.result == UnityWebRequest.Result.Success;
#endif
                    data = uwr.responseCode;
                    if (state)
                        Debug.LogError(uwr.error);
                }
            }
            completed?.Invoke(state, data);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url">下载路径</param>
        /// <param name="path">文件存放路径</param>
        /// <returns></returns>
        public static async Task<bool> Download(string url, string path)
        {
            bool result = false;
            using (var uwr = new UnityWebRequest(url))
            {
                uwr.certificateHandler = new WebRequestCert();
                uwr.downloadHandler = new UnityWebRequestCustomDownloadHandler(new byte[1024 * 2], path);
                uwr.disposeDownloadHandlerOnDispose = true;
                await uwr.SendWebRequest();
#if UNITY_2018 || UNITY_2019
                result = uwr.isNetworkError || uwr.isHttpError;
#else
                result = uwr.result == UnityWebRequest.Result.Success;
#endif
                if (result)
                    Debug.LogError(uwr.error);
            }
            return result;
        }
    }
}