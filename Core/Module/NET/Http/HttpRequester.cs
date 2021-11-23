/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          18:01:33 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;
    using System.IO;
    using System.Net;
    using UnityEngine;

    /// <summary> Http请求 </summary>
    public class HttpRequester
    {
        /** http通信 */
        public static string getResponse(string url)
        {
            Debug.Log(url);
            try
            {
                // Create a request for the URL. 		
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        /// <summary> http通信 </summary>
        public static void GetResponse(string url, Action<string> func)
        {
            string str = getResponse(url);
            func(str);
        }

        //eg:
        //string url = "http://127.0.0.1/";
        //string file = "cambrian.1.0.0.1.dll";
        //string savePath = Application.persistentDataPath + "/";
        //DownLoad(url + file, savePath + file);

        /// <summary> 下载 </summary>
        public static void DownLoad(string url, string savePath)
        {
            Debug.Log(string.Concat("Downloading File \"{0}\" from \"{1}\" .......\n\n", savePath, ",", File.Exists(savePath), ",", url));

            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, savePath);

            Debug.Log(string.Concat("Successfully Downloaded File \"{0}\" from \"{1}\"", savePath, ",", File.Exists(savePath)));
        }
    }
}