/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Kit 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-12
* Time:             12:57:41
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

#if Protobuf

namespace Framework
{
    using global::Google.Protobuf;

    /// <summary> Protobuf 方法库 </summary>
    public static class ProtoKit
    {
        public static string ToJson<T>(this T message) where T : IMessage<T>
        {
            return JsonFormatter.ToDiagnosticString(message);
        }
    }
}

#endif