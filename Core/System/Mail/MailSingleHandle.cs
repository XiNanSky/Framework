/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           13:22:09                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// 邮件 一对一 传入参数
    /// </summary>
    public struct MailSingleInfo
    {
        /// <summary>
        /// 发送者 地址
        /// </summary>
        public string Form;

        /// <summary>
        /// 发送者 端口
        /// </summary>
        public int FormPort;

        /// <summary>
        /// 发送者  昵称  如果没有则用地址代替
        /// </summary>
        public string FormDisplayName;

        /// <summary>
        /// 接收者 地址
        /// </summary>
        public string To;

        /// <summary>
        /// 接收者 昵称
        /// </summary>
        public string ToDisplayName;

        /// <summary>
        /// 凭证授权码
        /// </summary>
        public string Passwrod;

        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding;

        /// <summary>
        /// 连接服务器 地址 例子:"smtp.qq.com"
        /// </summary>
        public string ClientHost;

        /// <summary>
        /// 连接服务器 SSL验证
        /// </summary>
        public bool ClientEnableSsl;

        /// <summary>
        /// 连接服务器 超时秒限制
        /// </summary>
        public int ClientTimeout;

        /// <summary>
        /// 连接服务器 连接方式
        /// </summary>
        public SmtpDeliveryMethod ClientDeliveryMethod;

        /// <summary>
        /// 连接服务器 是否使用默认凭证
        /// </summary>
        public bool ClientUseDefaultCredentials;

        public MailSingleInfo(string form, string to, string passwrod = "")
        {
            Form = form;
            FormPort = default;
            FormDisplayName = null;
            To = to;
            ToDisplayName = null;
            Passwrod = passwrod;
            Encoding = Encoding.UTF8;

            ClientDeliveryMethod = SmtpDeliveryMethod.Network;
            ClientUseDefaultCredentials = false;
            ClientTimeout = 5000;
            ClientHost = "smtp.qq.com";
            ClientEnableSsl = false;
        }
    }

    /// <summary>
    /// 单对单 直接发送
    /// </summary>
    public class MailSingleHandle : MailHandle
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public MailAddress Recipients { get; private set; } = null;

        public MailSingleHandle(MailSingleInfo Info, string subject = "")
        {
            Addresser = new MailAddress(Info.Form, Info.FormDisplayName, Info.Encoding);

            SmtpClient = new SmtpClient();
            SmtpClient.Host = Info.ClientHost;//地址
            SmtpClient.EnableSsl = Info.ClientEnableSsl;//是否启用SSL
            SmtpClient.Timeout = Info.ClientTimeout;//超时
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//连接方式
            SmtpClient.UseDefaultCredentials = false;//默认凭证
            SmtpClient.Credentials = new NetworkCredential(Addresser.Address, Info.Passwrod);//创建网络凭证

            Recipients = new MailAddress(Info.To, Info.ToDisplayName, Info.Encoding);

            MailMessage = new MailMessage();
            MailMessage.From = Addresser;   //发件人
            MailMessage.To.Add(Recipients); //收件人
            MailMessage.IsBodyHtml = true;  //是否支持内容为HTML
            MailMessage.Priority = MailPriority.High;//优先级

            Subject = subject;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Body">内容主体</param>
        /// <param name="CallBack">回调</param>
        public void Send(string Body, Action CallBack = null)
        {
            Send(Subject, Body, CallBack);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Label">邮件标题</param>
        /// <param name="Body">内容主体</param>
        /// <param name="CallBack">回调</param>
        public void Send(string Label, string Body, Action CallBack = null)
        {
            MailMessage.Subject = Label; //标题
            MailMessage.Body = Body;     //内容
            SendMail(CallBack);
        }
    }
}