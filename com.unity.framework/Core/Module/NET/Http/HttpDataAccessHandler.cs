/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:53:20 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Framework;
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class HttpDataAccessHandler : MonoBehaviour
    {
        /// <summary> 默认的线程等待时间为10秒,（ms） </summary>
        public const int TIMEOUT = 10;

        /// <summary> 负数通讯号 </summary>
        public static int minusId { get; private set; }

        /// <summary> Https请求队列 </summary>
        private static ArrayList<HttpCommand> https;

        /// <summary> 发送请求线程 </summary>
        public static Task RequestTask { get; private set; }

        private static CancellationToken token;

        public static HttpDataAccessHandler Handler;


        public void Init()
        {
            Handler = this;

            https = new ArrayList<HttpCommand>();

            childTasks = new ArrayList<Task>();

            cts = new CancellationTokenSource();

            token = cts.Token;

            tf = new TaskFactory(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

            GC.Collect();
        }

#if UNITY_EDITOR
        private static StringBuilder @log = new StringBuilder();
        private void debug(HttpCommand command)
        {
            @log.Clear();
            @log.Append(" HTTPS SEND ");
            @log.Append(" - PORT = ").Append(command.Url);
            @log.Append(" - METHOD = ").Append(command.Method);
            @log.Append(" - TIME = ").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"));
            if (command.data != null) @log.Append("- VALUE =").Append(Encoding.UTF8.GetString(command.data.ToBytes()));
            Debug.Log(@log.ToString());
        }
#endif
        #region TASK 子线程队列池
        private static ArrayList<Task> childTasks;
        private static CancellationTokenSource cts;
        private static TaskFactory tf;

        private void TaskBack(Task b)
        {
            if (https.Count > 0)
            {
                var bs = tf.StartNew(() => { AccessAsync(https.RemoveAt(0)); }, token, TaskCreationOptions.DenyChildAttach, tf.Scheduler).ContinueWith(TaskBack, TaskContinuationOptions.ExecuteSynchronously);
                childTasks.Add(bs);
                childTasks.Remove(b);
                b.Dispose();
            }
        }

        /// <summary> 多线程 任务 发送 </summary>
        public void AccessTask(HttpCommand command)
        {
            //if (!GameManager.config.OnLine) return;
            https.Add(command);
            if (childTasks.Count == 0)
            {
                var t = tf.StartNew(() => { StartCoroutine(AccessAsync(https.RemoveAt(0))); }, token);
                t.ContinueWith(b => { TaskBack(b); }, TaskContinuationOptions.ExecuteSynchronously);
                childTasks.Add(t);
            }
        }

        /// <summary> 多线程 任务方式 </summary>
        private IEnumerator AccessAsync(HttpCommand command)
        {
            var time = DateTime.Now.ToUniversalTime().Ticks;
            var entry = new HttpEntry(NewMinusId(), time, command);
            if (time < 0) time = (long)(Time.realtimeSinceStartup * 1000);
            var url = HttpServerManager.GetUrlRoot(command.Url);
            if (url.IndexOf('?') == -1) url += "?" + time;
            else url += "&" + time;

            var request = UrlDecode(url);
            try /* http请求 */
            {
                request.Method = command.Method;
                //Timeout 从发出请求开始算起，到与服务器建立连接的时间
                //ReadWriteTimeout 从建立连接开始，到下载数据完毕所历经的时间。
                request.Timeout = (command.TimeOUT == 0 ? TIMEOUT : command.TimeOUT) * 1000;
                if (command.data != null) using (Stream outStream = request.GetRequestStream())//await
                    {//请求的内容  
                        outStream.Write(command.data.Arrays, 0, command.data.Length);
                        outStream.Close();
                    }
                else request.ContentLength = 0;
                var bb = new ByteBuffer();
                using (var response = (HttpWebResponse)request.GetResponse())//await
                {//返回数据
                    using (var inStream = response.GetResponseStream())
                    {
                        for (int v = inStream.ReadByte(); v != -1; v = inStream.ReadByte())
                        {
                            bb.WriteByte(v);
                        }
                        inStream.Close();
                    }
                    response.Close();
                }
                entry.Value = bb;
                entry.OnCommand();
                yield break;
            }
            catch (TimeoutException) { /*entry.Command.Fail?.Invoke("连接超时!");*/ }
#if UNITY_EDITOR
            catch (WebException wex)
            {
                if (wex.Status == WebExceptionStatus.Timeout)
                {
                    Debug.LogError("Time out!");
                    //entry.Command.Fail?.Invoke("资源请求超时!");
                }
            }
#endif
        }

        #endregion

        #region 协程队列池

        /// <summary> 异步数据访问 </summary>
        public void Access(HttpCommand command)
        {
            //if (!GameManager.config.OnLine) return;

            https.Add(command);
            if (https.Count == 1)
            {
                Handler.StopAllCoroutines();
                Handler.StartCoroutine(AccessCoroutineUpdate());
            }
        }

        private IEnumerator AccessCoroutineUpdate()
        {
            while (https.Count > 0)
            {
                yield return AccessCoroutine(https.Get(0));
                https.RemoveAt(0);
            }
        }

        private IEnumerator AccessCoroutine(HttpCommand command)
        {
            var time = DateTime.Now.ToUniversalTime().Ticks;
            var entry = new HttpEntry(NewMinusId(), time, command);
            using (UnityWebRequest webRequest = new UnityWebRequest(HttpServerManager.GetUrlRoot(command.Url), command.Method))
            {
                if (command.data != null)
                {
                    webRequest.uploadHandler = new UploadHandlerRaw(command.data.ToBytes());
                    webRequest.uploadHandler.contentType = "application/json";  //设置HTTP协议的请求头，默认的请求头HTTP服务器无法识别

                }
                if (command.Method == "GET")
                {
                    webRequest.url += command.UrlParam;
                }
                else if (command.Method == "POST")
                {
                    if (time < 0) time = (long)(Time.realtimeSinceStartup * 1000);
                    if (webRequest.url.IndexOf('?') == -1) webRequest.url += "?" + time;
                    else webRequest.url += "&" + time;
                }

                if (!string.IsNullOrEmpty(HttpPort.Token)) webRequest.SetRequestHeader("Authorization", string.Concat("Bearer ", HttpPort.Token));
                webRequest.method = command.Method;
                webRequest.timeout = (command.TimeOUT == 0 ? TIMEOUT : command.TimeOUT) * 1000;


                //这里需要创建新的对象用于存储请求并响应后返回的消息体，否则报空引用的错误
                webRequest.downloadHandler = new DownloadHandlerBuffer();

#if UNITY_EDITOR
                debug(command);
#endif

                //请求并等待所需的页面。
                yield return webRequest.SendWebRequest();

                if (webRequest.error != null
#if UNITY_2019_1_OR_NEWER
                   || webRequest.result == UnityWebRequest.Result.DataProcessingError
#else
                   || webRequest.isDone == false
#endif
                    )
                {
                    var value = command.data == null ? "" : Encoding.UTF8.GetString(command.data.ToBytes());
                    Debug.LogError($"<color=red> Prot:{command.Url} | MSG:{webRequest.error } | Send Value:{value}</color>");
                }
                else
                {
                    entry.Value = new ByteBuffer(webRequest.downloadHandler.data);
                    StartCoroutine(AccessCoroutineOnCommand(entry));
                }
            }
        }

        private IEnumerator AccessCoroutineOnCommand(HttpEntry entry)
        {
            entry.OnCommand();
            yield break;
        }

        #endregion

        /// <summary> 获取新的负数通讯号 </summary>
        private static int NewMinusId()
        {
            minusId--;
            if (minusId >= 0)
                minusId = -1;
            return minusId;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private static HttpWebRequest UrlDecode(string url)
        {
            HttpWebRequest request;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Proxy = null;
            request.ContentType = "application/json;charset=utf-8";
            if (!string.IsNullOrEmpty(HttpPort.Token)) request.Headers.Add("Authorization", string.Concat("Bearer ", HttpPort.Token));
            return request;
        }

        public static void Destory()
        {
            //GameManager.config.OnLine = false;
            for (int i = 0; i < childTasks.Count; i++)
            {
                childTasks[i].Dispose();
                childTasks[i] = null;
            }
            childTasks = null;
            RequestTask = null;
            GC.Collect();
        }
    }
}