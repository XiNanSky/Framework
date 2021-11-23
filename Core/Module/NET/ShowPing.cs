/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-04 
*NOWTIME:          00:13:11 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using Sirenix.OdinInspector;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class ShowPing : MonoBehaviour
    {
        [LabelText("IP OR Address")]
        public string IP = "www.baidu.com";
        private Ping ping;
        private Text pingtext;

        private int addTime = 0;
        private int requestCount = 20 * 10; // 0.1秒 请求 1 次，所以请求次数是 n秒 x 10

        private void Awake()
        {
            pingtext = GetComponent<Text>();
        }

        protected void OnEnable()
        {
            StopAllCoroutines();
            pingtext.text = "-1ms";
            Ping();
        }

        private IEnumerator showPing(int ping)
        {
            pingtext.text = string.Concat(ping, "ms");
            addTime = 0;
            yield return new WaitForSeconds(3f);
            if (gameObject.activeInHierarchy) Ping();//Ping完之后 再次Ping     
        }

        private void Ping()
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
                        Debug.LogError("当前网络不可用 请检查!!!");
                        showPing(-1);
                    }
                    break;
            }
        }

        private IEnumerator PingConnect()
        {
            ping = new Ping(IP);
            while (!ping.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                if (addTime > requestCount)
                {// 等待请求返回
                    showPing(ping.time);
                    yield break;
                }
                addTime++;
            }
            if (ping.isDone)
            {
                showPing(ping.time);
                yield return null;
            }
        }
    }
}