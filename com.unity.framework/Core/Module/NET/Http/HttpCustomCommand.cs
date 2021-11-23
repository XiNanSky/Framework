/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-08-30                    *
* Nowtime:           13:02:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    /// <summary> 
    /// 自定义端口
    /// 上传指定类型数据
    /// 返回指定类型数据
    /// </summary>
    public class HttpCustomCommand<P> : HttpCommand
    {
        public HttpCustomCommand(int PORT) : base()
        {
            Url = PORT;
            Method = POST;
        }

        protected override object readData(string data)
        {
            return FromJson<P>(data).data;
        }
    }

    /// <summary> 
    /// 自定义端口
    /// 不上传数据
    /// 返回指定类型数据
    /// </summary>
    public class HttpCustomCommand<T, P> : HttpCommand
    {
        public HttpCustomCommand(int PORT) : base()
        {
            Url = PORT;
            Method = POST;
        }

        public HttpCustomCommand(int PORT, T data)
        {
            Url = PORT;
            Method = POST;
            BytesWrite(data);
        }

        protected override object readData(string data)
        {
            return FromJson<P>(data).data;
        }
    }


    /// <summary> 
    /// 自定义端口
    /// 不上传数据
    /// 不返回指定类型数据
    /// </summary>
    public class HttpCustomCommand : HttpCommand
    {
        public HttpCustomCommand(int PORT) : base()
        {
            Url = PORT;
            Method = POST;
        }

        protected override object readData(string data)
        {
            return FromJson<object>(data).data;
        }
    }
}