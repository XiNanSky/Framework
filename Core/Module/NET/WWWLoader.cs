/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          23:52:48 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary> 资源加载器 </summary>
    public class WWWLoader : MonoBehaviour
    {
        public static WWWLoader Inst { get; private set; }

        public void Init()
        {
            Inst = this;
        }

        /// <summary> 加载文本 </summary>
        public void LoadTextAsset(string url, Action<TextAsset> action, Action<float> update = null)
        {
            StartCoroutine(wwwLoadTextAsset(url.ToString(), action, update));
        }

        private IEnumerator wwwLoadTextAsset(string url, Action<TextAsset> action, Action<float> update)
        {
            using (var www = new UnityWebRequest(url))
            {
                yield return wait(www, update);
                action(new TextAsset(www.downloadHandler.text));
            }
        }

        /// <summary> 获取图片精灵 </summary>
        public void LoadSprite(string url, Action<Sprite> action, Action<float> update = null)
        {
            StartCoroutine(wwwLoadSprite(url, action, update));
        }

        /// <summary> 加载图片精灵 </summary>
        private IEnumerator wwwLoadSprite(string url, Action<Sprite> action, Action<float> update)
        {
            using (var www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return wait(www, update);
                var pic = DownloadHandlerTexture.GetContent(www);
                var sprite = Sprite.Create(pic, new Rect(0, 0, pic.width, pic.height), new Vector2(0.5f, 0.5f)); //设定精灵的轴心点
                action(sprite);
            }
        }

        /// <summary> 获取2D纹理 </summary>
        public void LoadTexture2D(string url, Action<Texture2D> action, Action<float> update = null)
        {
            StartCoroutine(getLoadTexture2D(url, action, update));
        }

        /// <summary> 加载纹理 </summary>
        private IEnumerator getLoadTexture2D(string url, Action<Texture2D> action, Action<float> update)
        {
            using (var www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return wait(www, update);
                action(DownloadHandlerTexture.GetContent(www));
            }
        }

        /// <summary> 获取文本信息 </summary>
        public void LoadText(string url, Action<string> action, Action<float> update = null)
        {
            StartCoroutine(getContxt(url, action, update));
        }

        private IEnumerator getContxt(string url, Action<string> action, Action<float> update)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return wait(www, update);
                string text = www.downloadHandler.text;
                if (www.responseCode != 200) text = "";
                action(text);
            }
        }

        /// <summary> 请求二进制数据 </summary>
        public void LoadBytes(string url, Action<byte[]> action, Action<float> update = null)
        {
            StartCoroutine(getBytes(url, action, update));
        }

        private IEnumerator getBytes(string url, Action<byte[]> action, Action<float> update)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return wait(www, update);
                action(www.downloadHandler.data);
            }
        }

        /// <summary> 发送数据 </summary>
        public void Post(string url, ByteBuffer data, Action<ByteBuffer> action, Action<float> update = null)
        {
            StartCoroutine(post_send(action, url, data.ToBytes(), update));
        }

        private IEnumerator post_send(Action<ByteBuffer> obj, string url, byte[] data, Action<float> update)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(url, data))
            {
                yield return wait(www, update);
                obj(new ByteBuffer(www.downloadHandler.data));
            }
        }

        private IEnumerator wait(UnityWebRequest webRequest, Action<float> update)
        {
            var www = webRequest.SendWebRequest();// 
            while (!www.isDone)
            {
                update?.Invoke(webRequest.downloadProgress);
            }
            if (webRequest.error != null
#if UNITY_2020
               || webRequest.result != UnityWebRequest.Result.Success
#endif
            )
            {
                Debug.LogError(string.Concat(
                    " |URL: ", webRequest.url,
                    " |Method: ", webRequest.method,
                    " |Error: ", webRequest.error,
#if UNITY_2020
                    " |Result: ", webRequest.result,
#endif
                    " |Data: ", webRequest.downloadHandler.data));
            }
            yield return webRequest;
        }

        public void OnDestroy()
        {
            Inst = null;
        }
    }
}