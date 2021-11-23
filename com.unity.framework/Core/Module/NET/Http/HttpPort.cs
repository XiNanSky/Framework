/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Framework.Net.Http 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-12
* Time:             15:12:52
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Net
{
    using System;
    using System.Text;
    using UnityEngine;

    /// <summary> </summary>
    public class HttpPort
    {
        protected const string GET = "GET";
        protected const string POST = "POST";

        public const int OK = 0;

        /// <summary> 接口号 </summary>
        public int Url { get; protected set; }

        /// <summary> GET数据 </summary>
        public string UrlParam { get; protected set; }

        /// <summary> 提交方式 </summary>
        public string Method { get; protected set; }

        /// <summary> Token </summary>
        public static string Token { get; protected set; }

        /// <summary> 连接超时时间 </summary>
        public int TimeOUT { get; protected set; }

        /// <summary> 发送时记录发送时间,返回服务器消息时记录通信消耗的时间 </summary>
        public long Excutetime;

        /// <summary> 回调函数 </summary>
        protected Action<object> Callback;

        /// <summary> 失败回调函数 </summary>
        public Action<string> Fail;

        public BaseRet BaseRet { get; private set; }

        public HttpPort()
        {
            Url = -1;
            Method = "POST";
        }

        public HttpPort(int url, string method)
        {
            Url = url;
            Method = method;
        }

        public void OnCommand(ByteBuffer data)
        {
            var context = Encoding.UTF8.GetString(data.ToBytes());
            if (string.IsNullOrEmpty(context))
            {
                var obj = readData(data);
                Callback?.Invoke(obj);
            }
            else
            {
                BaseRet = JsonUtility.FromJson<BaseRet>(context);
                switch (BaseRet.statusCode)
                {
                    /*1xx:/ 信息提示 这些状态代码表示临时的响应。客户端在收到常规响应之前，应准备接收一个或多个 1xx 响应。*/
                    case 100:// Continue 继续； 始的请求已经接受，客户应当继续发送请求的其余部分。（HTTP 1.1新）

                        break;
                    case 101:// Switching Protocols 切换协议； 服务器将遵从客户的请求转换到另外一种协议（HTTP 1.1新）

                        break;
                    /*2xx: 这类状态代码表明服务器成功地接受了客户端请求。*/
                    case 200:// 成功 正常 对GET和POST请求的应答文档跟在后面
                        var obj = readData(context);
                        Callback?.Invoke(obj);
                        return;
                    case 201:// Created 已创建；服务器已经创建了文档，Location头给出了它的URL

                        break;
                    case 202:// Accepted 接受 已经接受请求，但处理尚未完成。

                        break;
                    case 203:// Non-Authoritative Information 非权威的信息 文档已经正常地返回，但一些应答头可能不正确，因为使用的是文档的拷贝，非权威性信息（HTTP 1.1新）

                        break;
                    case 204:// No Content 没有内容 浏览器应该继续显示原来的文档。如果用户定期地刷新页面，而Servlet可以确定用户文档足够新，这个状态代码是很有用的。

                        break;
                    case 205:// Reset Content 重置内容 但浏览器应该重置它所显示的内容。用来强制浏览器清除表单输入内容（HTTP 1.1新）。

                        break;
                    case 206:// Partial Content 部分内容 客户发送了一个带有Range头的GET请求，服务器完成了它（HTTP 1.1新）。

                        break;
                    case 207:// 多状态 紧跟消息体后面的是xml消息并且包含了多个单独的响应状态码，响应的数量取决于子请求的个数。

                        break;
                    case 208:// 已经报告 一个DAV的绑定成员被前一个请求枚举，并且没有被再一次包括。

                        break;
                    case 226:// IM Used 服务器已经满足了请求所要的资源，并且响应是一个或者多个实例操作应用于当前实例的结果

                        break;
                    /*3xx: 重定向 客户端浏览器必须采取更多操作来实现请求。 例如，浏览器可能不得不请求服务器上的不同的页面，或通过代理服务器重复该请求。*/
                    case 300://  客户请求的文档可以在多个位置找到，这些位置已经在返回的文档内列出。如果服务器要提出优先选择，则应该在Location应答头指明。

                        break;
                    case 301:// Moved Permanently 永久移动 客户请求的文档在其他地方，新的URL在Location头中给出，浏览器应该自动地访问新的URL。

                        break;
                    /* 但新的URL应该被视为临时性的替代，而不是永久性的。注意，在HTTP1.0中对应的状态信息是“Moved Temporatily”。
                     * 出现该状态代码时，浏览器能够自动访问新的URL，因此它是一个很有用的状态代码。注意这个状态代码有时候可以和301替换使用。
                     * 例如，如果浏览器错误地请求 http://host/~user （缺少了后面的斜杠），有的服务器返回301，有的则返回302。严格地说，
                     * 我们只能假定只有当原来的请求是GET时浏览器才会自动重定向。请参见 307。*/
                    case 302://-Found 发现

                        break;
                    /* 类似于301/302，不同之处在于，如果原来的请求是POST，Location头指定的重定向目标文档应该通过GET提取
                     * HTTP类似于301/302，不同之处在于，如果原来的请求是POST，Location头指定的重定向目标文档应该通过GET提取（HTTP 1.1新）。 */
                    case 303:// See Other 查看其它 　

                        break;
                    case 304:// Not Modified 未修改 客户端有缓冲的文档并发出了一个条件性的请求（一般是提供If-Modified-Since头表示客户只想比指定日期更新的文档）。服务器告诉客户，原来缓冲的文档还可以继续使用。

                        break;
                    case 305:// Use Proxy 使用代理 客户请求的文档应该通过Location头所指明的代理服务器提取（HTTP 1.1新）

                        break;
                    case 306:// 切换代理；　不再使用。原意是随后的请求应该使用指定的代理。

                        break;
                    case 307:// Temporary Redirect 临时跳转；许多浏览器会错误地响应302应答进行重定向，即使原来的请求是POST，即使它实际上只能在POST请求的应答是303时才能重定向。由于这个原因，HTTP许多浏览器会错误地响应302应答进行重定向，即使原来的请求是POST，即使它实际上只能在POST请求的应答是303时才能重定向。由于这个原因，HTTP 1.1新增了307，以便更加清除地区分几个状态代码：当出现303应答时，浏览器可以跟随重定向的GET和POST请求；如果是307应答，则浏览器只能跟随对GET请求的重定向。（HTTP 1.1新）

                        break;
                    case 308:// 永久转移 这个请求和以后的请求都应该被另一个URI地址重新发送。307、308和302、301有相同的表现，但是不允许HTTP方法改变。例如，请求表单到一个永久转移的资源将会继续顺利地执行。

                        break;
                    /*4xx: 客户端错误 发生错误，客户端似乎有问题。 例如，客户端请求不存在的页面，客户端未提供有效的身份验证信息。*/
                    case 400:// Bad Request 错误请求；
                        Debug.LogError(BaseRet.errors + " 错误请求");
                        break;
                    case 401:// Unauthorized 未授权；
                        Debug.LogError(BaseRet.errors + " 未授权");
                        break;
                    case 402:// 需要付款；为以后保留使用。原意是该状态码可被用于一些数字货币或者是微支付，但是目前还没有普及，所以这些代码不经常被使用。YouYube使用这个状态如果某个IP地址发出了过多的请求，并要求用户输入验证码。
                        Debug.LogError(BaseRet.errors + " 需要付款");
                        break;
                    case 403:// Forbidden 禁止访问；
                        Debug.LogError(BaseRet.errors + " 禁止访问");
                        break;
                    case 404:// Not Found 找不到；
                        Debug.LogError(BaseRet.errors + " 找不到");
                        break;
                    case 405:/* Method Not Allowed  方法不允许；请求方法（GET、POST、HEAD、Delete、PUT、TRACE等）对指定的资源不适用，用来访问本页面的 HTTP 谓词不被允许（方法不被允许）（HTTP 1.1新） */
                        break;
                    case 406:/* Not Acceptable 不可接受； 指定的资源已经找到，但它的MIME类型和客户在Accpet头中所指定的不兼容，客户端浏览器不接受所请求页面的 MIME 类型（HTTP 1.1新）。*/
                        break;
                    case 407:/* Proxy Authentication Required 需要代理认证；要求进行代理身份验证，类似于401，表示客户必须先经过代理服务器的授权。（HTTP 1.1新） */
                        break;
                    case 408:/* Request Timeout 请求超时；在服务器许可的等待时间内，客户一直没有发出任何请求。客户可以在以后重复同一请求。（HTTP在服务器许可的等待时间内，客户一直没有发出任何请求。客户可以在以后重复同一请求。（HTTP 1.1新）*/
                        break;
                    case 409:/* Conflict 冲突；通常和PUT请求有关。由于请求和资源的当前状态相冲突，因此请求不能成功。（HTTP 1.1新）*/
                        break;
                    case 410:/* Gone 遗失的；所请求的文档已经不再可用，而且服务器不知道应该重定向到哪一个地址。它和404的不同在于，返回407表示文档永久地离开了指定的位置，而404表示由于未知的原因文档不可用。（HTTP所请求的文档已经不再可用，而且服务器不知道应该重定向到哪一个地址。它和404的不同在于，返回407表示文档永久地离开了指定的位置，而404表示由于未知的原因文档不可用。（HTTP 1.1新）*/
                        break;
                    case 411:/* Length Required 长度要求；服务器不能处理请求，除非客户发送一个Content-Length头。（HTTP服务器不能处理请求，除非客户发送一个Content-Length头。（HTTP 1.1新）  */
                        break;
                    case 412:/* Precondition Failed 前置条件失败；请求头中指定的一些前提条件失败（HTTP请求头中指定的一些前提条件失败（HTTP 1.1新）。 */
                        break;
                    case 413:/* Request Entity Too Large 响应实体太大；目标文档的大小超过服务器当前愿意处理的大小。如果服务器认为自己能够稍后再处理该请求，则应该提供一个Retry: After头（HTTP 1.1新）。 */
                        break;
                    case 414:/* Request URI Too Long 请求URI太长;被提供的URI对服务器的处理来说太长。经常出现在太多被编码的数据被作为查询字符串的GET请求的结果，因此需要被转换为POST请求。（HTTP 1.1新）。 */
                        break;
                    case 415:/* 不支持的媒体类型。 请求实体的媒体类型不被服务器或者资源支持。例如，客户端上传一个image / svg + xml的图片，但是服务器需要图片使用不同的格式*/
                        break;
                    case 416:/* Requested Range Not Satisfiable 请求范围不能满足；　服务器不能满足客户在请求中指定的Range头。（HTTP 1.1新）*/
                        break;
                    case 417:/* 执行失败。服务器期望请求头字段的要求。*/
                        break;
                    case 418:/* 我是一个茶壶；这个代码是在1998年作为传统的IETF April Fools‘ jokes被定义的在RFC2324，超文本咖啡罐控制协议，但是并没有被实际的HTTP服务器实现。RFC指定了这个代码应该是由茶罐返回给速溶咖啡。 */
                        break;
                    case 419:/* 认证超时；　并不是HTTP标注的一部分，419认证超时表示以前的有效证明已经失效了。同时也被用于401未认证的替代选择为了从其它被拒绝访问的已认证客户端中指定服务器的资源。 */
                        break;
                    case 420:/* 方法失效；　不是HTTP的标准，但是被Spring定义在HTTP状态类中当方法失时使用。这个状态码已经不推荐在Spring中使用。421:误导请求; 请求被直接定向到不能产生响应的服务器上（例如因为一个连接的复用）。  */
                        break;
                    case 422:/* 不可处理的实体（WebDAV）请求符合要求但是不能接受错误由于语法错误。  */
                        break;
                    case 423:/* 锁定的 资源访问被锁定。 */
                        break;
                    case 424:/* 失败的依赖 请求由于上一个请求的失败而失败。 */
                        break;
                    case 426:/* 需要升级 客户端应该切换不同的协议例如TLS / 1.0在指定的升级的头字段里。 */
                        break;
                    case 428:/* 需要前置条件 原始服务器需要有条件的请求。当客户端GET一个资源的状态的时候，同时又PUT回给服务器，与此同时第三方修改状态到服务器上的时候，为了避免丢失更新的问题发生将会导致冲突。 */
                        break;
                    case 429:/* 过多请求 用户已经发送了太多的请求在指定的时间里。用于限制速率。 */
                        break;
                    case 431:/* 请求头部字段太大  服务器由于一个单独的请求头部字段或者是全部的字段太大而不愿意处理请求。 */
                        break;
                    case 440:/* 登陆超时（微软） 　一个微软的扩展，意味着你的会话已经超时。 */
                        break;
                    case 444:/* 无响应 被使用在Nginx的日志中表明服务器没有返回信息给客户端并且关闭了连接（在威慑恶意软件的时候比较有用）。 */
                        break;
                    case 449:/* 重试（微软） 一个微软的扩展。请求应该在执行适当的动作之后被重试。 */
                        break;
                    case 450:/* 被Windows家长控制阻塞（微软） 一个微软的扩展。这个错误是当Windows家长控制打开并且阻塞指定网页的访问的时候被指定。 */
                        break;
                    case 451:/* 由于法律原因而无效（因特网草稿）　被定义在因特网草稿“一个新的HTTP状态码用于法律限制的资源”。被用于当资源的访问由于法律原因被禁止的时候。例如检查制度或者是政府强制要求禁止访问。一个例子是1953年dystopian的小说Fahrenheit 451就是一个非法的资源。*/
                        break;
                    //case 451:/* 重定向（微软） 被用在Exchange ActiveSync中如果一个更有效的服务器能够被使用或者是服务器不能访问用户的邮箱。客户端会假定重新执行HTTP自动发现协议去寻找更适合的服务器 */
                    //    break;
                    case 494:/* 请求头太大（Nginx） Nginx内置代码和431类似，但是是被更早地引入在版本0.9.4（在2011年1月21日）。 */
                        break;
                    case 495:/* 证书错误（Nginx） Nginx内置的代码，当使用SSL客户端证书的时候错误会出现为了在日志错误中区分它和4XX和一个错误页面的重定向。。 */
                        break;
                    case 496:/* 没有证书（Nginx） Nginx内置的代码，当客户端不能提供证书在日志中分辨4XX和一个错误页面的重定向。  */
                        break;
                    case 497:/* HTTP到HTTPS（Nginx） 　Nginx内置的代码，被用于原始的HTTP的请求发送给HTTPS端口去分辨4XX在日志中和一个错误页面的重定向。 */
                        break;
                    case 498:/* 令牌超时或失效（Esri） 由ArcGIS for Server返回。这个代码意味着令牌的超时或者是失效。 */
                        break;
                    case 499:/* 客户端关闭请求（Nginx）被用在Nginx日志去表明一个连接已经被客户端关闭当服务器仍然正在处理它的请求，是的服务器无法返货状态码。 */
                        break;
                    //case 499:/*  需要令牌（Esri） 由ArcGIS for Server返回。意味着需要一个令牌（如果没有令牌被提交）。 */
                    //    break;
                    /*5xx: 服务器错误 服务器由于遇到错误而不能完成该请求。*/
                    case 500:// Internal Server Error 服务器内部错误 服务器遇到了意料不到的情况，不能完成客户的请求。
                        Debug.LogError($"{BaseRet.errors} :服务器内部错误".RichColor("red"));
                        break;
                    case 501:// 501 - Not Implemented 没有实现；
                        Debug.LogError($"{BaseRet.errors} :没有实现".RichColor("red"));//Encoding.Default.GetString(Convert.FromBase64String(BaseRet.errors))
                        break;
                }
            }
        }

        protected virtual object readData(ByteBuffer data) { return null; }

        protected virtual object readData(string data) { return null; }
    }
}