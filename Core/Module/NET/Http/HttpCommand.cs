/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.Net 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-03 
*NOWTIME:          17:56:02 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

using Framework;
using Framework.Extend;
using Framework.Net;
using System;
using System.Text;

/*
 * GET       : 请求指定的页面信息 并返回实体主体
 * HEAD      : 类似于GET请求 返回的响应中没有具体内容 用于获取抱头
 * POST      : 想指定资源提交数据进行处理请求 例如提交表单或者上传文件 数据被包含在请求体中 POST请求可能会导致新的资源的建立和已有资源的修改
 * PUT       : 从客户端想服务器传送的数据取代指定的文档的内容
 * DELETE    : 请求服务器删除指定的页面
 * CONNECT   : HTTP/1.1协议中 预留给能够将连接改为管道方式的代理服务器
 * OPTIONS   : 允许客户端查看服务器的性能
 * TRACE     : 回显服务器收到的请求 主要用于测试或者诊断
 */

/// <summary> 基本的HTTP通信命令基类 </summary>
public class HttpCommand : HttpPort
{
    /// <summary> 数据 </summary>
    public ByteBuffer data;

    /// <summary> 执行命令 </summary>
    public virtual void Excute<T>(Action<T> action, Action<string> onFail = null, int overtime = 10)
    {
        Callback = (o) =>
        {
            if (o != null) { action((T)o); }
            else action(default);
        };
        Fail = onFail;
        Excute(overtime);
    }

    public virtual void Excute(Action<object> action, Action<string> onFail = null, int overtime = 10)
    {
        Callback = action;
        Fail = onFail;
        Excute(overtime);
    }

    public virtual void Excute(Action action, Action onFail = null, int overtime = 10)
    {
        Callback = (o) => { action.Invoke(); };
        Fail = (o) => { onFail?.Invoke(); };
        Excute(overtime);
    }

    /// <summary> 执行命令 </summary>
    public virtual void Excute(int overtime = 10)
    {
        TimeOUT = overtime;
        HttpDataAccessHandler.Handler.Access(this);
        //HttpDataAccessHandler.AccessTask(this);
    }

    public virtual void BytesWrite() { }

    public virtual void BytesWrite(object @object)
    {
        if (Method == POST)
        {
            data = new ByteBuffer();
            data.WriteBytes(Encoding.UTF8.GetBytes(JsonKit.Serialize(@object)));
        }
        else if (Method == GET)
        {
            UrlParam = @object.GetUriParam();
        }
    }

    public virtual void BytesRead(ByteBuffer data) { }

    public BaseRet<T> FromJson<T>(string context)
    {
        return JsonKit.Deserialize<BaseRet<T>>(context);
    }
}
