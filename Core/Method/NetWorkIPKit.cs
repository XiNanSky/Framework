/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-15
* Time:             11:24:10
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary> </summary>
    public static class NetWorkIPKit
    {
        /// <summary> 外网ip </summary>
        public static string OUTIP;

        /// <summary> 获取外网ip </summary>
        public static void GetOutIP(MonoBehaviour mono)
        {
            mono.StartCoroutine(getOutIp());
        }

        private static IEnumerator getOutIp()
        {
            UnityWebRequest request = new UnityWebRequest("http://pv.sohu.com/cityjson");
            yield return request.SendWebRequest();
            if (request.isDone || request.error == null)
            {
                OUTIP = request.downloadHandler.text;
            }
            yield return OUTIP;
        }
    }
}