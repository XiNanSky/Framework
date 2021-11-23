/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-04 
*NOWTIME:          00:37:41 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System.Collections;
    using UnityEngine;

    public class UnityPing : MonoBehaviour
    {
        private static string s_ip = "www.baidu.com";
        private static System.Action<int> s_callback = null;
        private static UnityPing s_unityPing = null;
        private static int s_timeout = 2;

        public static void CreatePing(string ip, System.Action<int> callback)
        {
            if (string.IsNullOrEmpty(ip)) return;
            if (callback == null) return;
            if (s_unityPing != null) return;
            s_ip = ip;
            s_callback = callback;
            GameObject go = new GameObject("UnityPing");
            DontDestroyOnLoad(go);
            s_unityPing = go.AddComponent<UnityPing>();
        }

        /// <summary> 超时时间（单位秒） </summary>
        public static int Timeout
        {
            set { if (value > 0) s_timeout = value; }
            get => s_timeout;
        }

        private void Start()
        {
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaCarrierDataNetwork: // 3G/4G
                case NetworkReachability.ReachableViaLocalAreaNetwork: // WIFI
                    {
                        StopCoroutine(PingConnect());
                        StartCoroutine(PingConnect());
                    }
                    break;
                case NetworkReachability.NotReachable: // 网络不可用
                default:
                    {
                        if (s_callback != null)
                        {
                            s_callback(-1);
                            Destroy(gameObject);
                        }
                        Debug.LogError("当前网络不可用 请检查!!!");
                    }
                    break;
            }
        }
        private void OnDestroy()
        {
            s_ip = "";
            s_timeout = 20;
            s_callback = null;
            if (s_unityPing != null)
                s_unityPing = null;
        }

        private IEnumerator PingConnect()
        {
            // Ping网站
            Ping ping = new Ping(s_ip);
            int addTime = 0;
            int requestCount = s_timeout * 10; // 0.1秒 请求 1 次，所以请求次数是 n秒 x 10
                                               // 等待请求返回
            while (!ping.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                // 链接失败
                if (addTime > requestCount)
                {
                    addTime = 0;
                    if (s_callback != null)
                    {
                        s_callback(ping.time);
                        Destroy(gameObject);
                    }
                    yield break;
                }
                addTime++;
            }
            // 链接成功
            if (ping.isDone)
            {
                if (s_callback != null)
                {
                    s_callback(ping.time);
                    Destroy(gameObject);
                }
                yield return null;
            }
        }
    }
}