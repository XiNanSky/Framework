/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games
* All rights reserved.
* FileName:         Framework.Kit
* Author:           XiNan
* Version:          0.4
* UnityVersion:     2019.4.10f1
* Date:             2021-07-08
* Time:             11:28:27
* E-Mail:           1398581458@qq.com
* Description:
* History:
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework
{
    // #if Newtonsoft

    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Json 工具类
    /// </summary>
    public static class JsonKit
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 解析
        /// </summary>
        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 解析
        /// </summary>
        public static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
    }

    // #endif
}
