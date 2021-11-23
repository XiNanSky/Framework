/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-08                    *
* Nowtime:           09:56:14                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;

    /// <summary>
    /// 邮件系统
    /// </summary>
    public class MailSystem
    {
        public MailMultipleHandle TestMail;

        /// <summary>
        /// 客户端 链接 服务器
        /// </summary>
        public MailSingleHandle ClientToServers;

        public MailSystem()
        {
            //ClientToServers = new MailSingleHandle(new MailSingleInfo
            //{
            //    Form = "xinanskyhailun@foxmail.com",   //客户端地址
            //    FormDisplayName = "夕楠发送器",          //可以写入用户昵称
            //    FormPort = 25,                         //端口
            //    To = "1398581458@qq.com",              //服务器地址
            //    Passwrod = "dnonitcgelzmebei",         //凭证
            //    ClientHost = "smtp.qq.com",            //Host地址
            //}
            //, "问题反馈");
            //ClientToServers.Send("hello world 5465465465465465465465", () =>
            //{
            //    Debug.Log("发送完成");
            //});

            TestMail = new MailMultipleHandle(new MailMultipInfo
            {
                Form = "xinanskyhailun@foxmail.com",   //客户端地址
                FormDisplayName = "测试器",             //可以写入用户昵称
                FormPort = 25,                         //端口
                Passwrod = "dnonitcgelzmebei",         //凭证
                ClientHost = "smtp.qq.com",            //Host地址
            }, "第二轮测试");

            TestMail.AddRecipients("1398581458@qq.com", "1143955202@qq.com", "lijie1433223@foxmail.com");
            TestMail.Send(@"自动邮件发送系统测试数据!!!!!!!! 
            -1398581458@qq.com
            -xinan", 
            () =>
            {
                Debug.Log("发送完成");
            });
        }
    }
}