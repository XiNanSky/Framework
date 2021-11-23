/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-11                    *
* Nowtime:           11:51:43                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace ThinkingAnalytics
{
    using System;
    using System.Collections.Generic;


    public class TADataBase : IDynamicSuperProperties
    {
        public Dictionary<string, object> GetDynamicSuperProperties()
        {
            return new Dictionary<string, object>()
            {
                 {"KEY_UTCTime", DateTime.UtcNow}
            };
        }
    }

    /// <summary>
    /// 数数科技 SDK 管理类
    /// </summary>
    public static class TASDKAPI
    {
        //TDFirstEvent          首次事件
        //TDUpdatableEvent      可更新事件
        //TDOverWritableEvent   可重写事件

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(bool b, string appId, string serverUrl)
        {
            if (b)
            {//ThinkingData SDK 需要您确保用户在同意 《隐私政策》后初始化SDK

                ThinkingAnalyticsAPI.TAMode mode = ThinkingAnalyticsAPI.TAMode.NORMAL;
                ThinkingAnalyticsAPI.TATimeZone timeZone = ThinkingAnalyticsAPI.TATimeZone.Local;
                ThinkingAnalyticsAPI.Token token = new ThinkingAnalyticsAPI.Token(appId, serverUrl, mode, timeZone);
                ThinkingAnalyticsAPI.Token[] tokens = new ThinkingAnalyticsAPI.Token[1];
                tokens[0] = token;
                ThinkingAnalyticsAPI.StartThinkingAnalytics(tokens);

                // 创建轻实例，返回轻实例的 token （类似于 APP ID）
                //string token = CreateLightInstance();
                //Login("anotherAccount", token);
                //Track("TEST_EVENT", token);
            }
        }

        /// <summary>
        /// 设置访客ID
        /// </summary>
        public static void SetIdentify(string userid, string appid = "")
        {
            ThinkingAnalyticsAPI.Identify(userid, appid);
        }

        /// <summary>
        /// 获取访客ID
        /// </summary>
        public static string GetIdentify(string appid = "")
        {
            return ThinkingAnalyticsAPI.GetDistinctId(appid);
        }

        /// <summary>
        /// 记录事件时长
        /// </summary>
        /// 调用 TimeEvent() 来开始计时，配置您想要计时的事件名称，当上传该事件时，
        /// 将会自动在您的事件属性中加入 #duration 这一属性来表示记录的时长，单位为秒。
        public static void TimeEvent(string events, string appid = "")
        {
            ThinkingAnalyticsAPI.TimeEvent(events, appid);
        }

        /*
         自 v1.4.3 开始，可以配置 SDK 实例的默认时区。
         如果配置了非 Local 的时区，则所有的事件时间都会对齐到该时区，
         传入的 DateTime 的 Kind 属性将被忽略。
        */

        #region 上传事件

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track(string properties, string appid = "")
        {
            ThinkingAnalyticsAPI.Track(properties, appid);
        }

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track<T>(T properties, string appid = "") where T : ThinkingAnalyticsEvent
        {
            ThinkingAnalyticsAPI.Track(properties, appid);
        }

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track<T>(string events, T properties, DateTime time, string appid = "") where T : IDynamicSuperProperties
        {
            ThinkingAnalyticsAPI.Track(events, properties.GetDynamicSuperProperties(), time, appid);
        }

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track<T>(string events, T properties, string appid = "") where T : IDynamicSuperProperties
        {
            ThinkingAnalyticsAPI.Track(events, properties.GetDynamicSuperProperties(), appid);
        }

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track(string events, Dictionary<string, object> properties, DateTime time, string appid = "")
        {
            ThinkingAnalyticsAPI.Track(events, properties, time, appid);
        }

        /// <summary>
        /// 上报事件 关键函数
        /// </summary>
        public static void Track(string events, Dictionary<string, object> properties, string appid = "")
        {
            ThinkingAnalyticsAPI.Track(events, properties, appid);
        }

        #endregion

        #region 功能开关

        /// <summary>
        /// 开启自动采集事件
        /// </summary>
        /// 注意：如果您需要设置自定义访客 ID，或者公共事件属性，需要在打开自动采集事件之前完成。自动采集事件目前不支持动态公共属性
        public static void EnableAutoTrack(AUTO_TRACK_EVENTS events, string appId = "")
        {
            ThinkingAnalyticsAPI.EnableAutoTrack(events, appId);
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        /// <param name="account">账号</param>
        public static void Login(string account, string appid = "")
        {
            ThinkingAnalyticsAPI.Login(account, appid);
        }

        /// <summary>
        /// 注销账号
        /// </summary>
        public static void Logout(string appid = "")
        {
            ThinkingAnalyticsAPI.Logout(appid);
        }


        /// <summary>
        /// 创建轻量级实例，轻量级实例与主实例共享项目ID. 访客ID、账号ID、公共属性不共享
        /// </summary>
        public static string CreateLightInstance(string appid = "")
        {
            return ThinkingAnalyticsAPI.CreateLightInstance(appid);
        }

        /// <summary>
        /// 暂停/停止数据上报
        /// Ture  = 恢复默认实例的上报
        /// False = 暂停默认实例的上报，已缓存数据和已经设置的信息不被清除
        /// </summary>
        public static void EnableTracking(bool b, string appid = "")
        {
            ThinkingAnalyticsAPI.EnableTracking(b, appid);
        }

        /// <summary>
        /// 停止上报数据，并且清空本地缓存数据(未上报的数据、已设置的访客ID、账号ID、公共属性)
        /// </summary>
        public static void OptOutTracking(string appid = "")
        {
            ThinkingAnalyticsAPI.OptOutTracking(appid);
        }

        /// <summary>
        /// 重新开启上报
        /// </summary>
        public static void OptInTracking(string appid = "")
        {
            ThinkingAnalyticsAPI.OptInTracking(appid);
        }

        /// <summary>
        /// 删除该用户在 TA 集群中的用户数据
        /// 会在停止 SDK 实例的功能前，上报一条 UserDelete数据，以删除该用户的用户数据。
        /// </summary>
        public static void OptOutTrackingAndDeleteUser(string appid = "")
        {
            ThinkingAnalyticsAPI.OptOutTrackingAndDeleteUser(appid);
        }

        /// <summary>
        /// 设置公共事件属性. 公共事件属性指的就是每个事件都会带有的属性.
        /// </summary>
        /// <param name="superProperties">公共事件属性</param>
        /// <param name="appId">项目 ID(可选)</param>
        /// <see cref="https://docs.thinkingdata.cn/ta-manual/v3.4/installation/installation_menu/client_sdk/unity_sdk_installation/unity_sdk_installation.html#_3-2-%E8%AE%BE%E7%BD%AE%E9%9D%99%E6%80%81%E5%85%AC%E5%85%B1%E5%B1%9E%E6%80%A7"/>
        public static void SuperPropertiesSet(Dictionary<string, object> keys, string appid = "")
        {
            ThinkingAnalyticsAPI.SetSuperProperties(keys, appid);
        }

        /// <summary>
        /// 清空所有公共属性
        /// </summary>
        /// <see cref="https://docs.thinkingdata.cn/ta-manual/v3.4/installation/installation_menu/client_sdk/unity_sdk_installation/unity_sdk_installation.html#_3-2-%E8%AE%BE%E7%BD%AE%E9%9D%99%E6%80%81%E5%85%AC%E5%85%B1%E5%B1%9E%E6%80%A7"/>
        public static void SuperPropertiesClear(string appid = "")
        {
            ThinkingAnalyticsAPI.ClearSuperProperties(appid);
        }

        /// <summary>
        /// 清除指定属性名 的公共属性
        /// </summary>
        /// <see cref="https://docs.thinkingdata.cn/ta-manual/v3.4/installation/installation_menu/client_sdk/unity_sdk_installation/unity_sdk_installation.html#_3-2-%E8%AE%BE%E7%BD%AE%E9%9D%99%E6%80%81%E5%85%AC%E5%85%B1%E5%B1%9E%E6%80%A7"/>
        public static void SuperPropertyUnset(string name, string appid = "")
        {
            ThinkingAnalyticsAPI.UnsetSuperProperty(name, appid);
        }

        /// <summary>
        /// 获取所有公共属性
        /// </summary>
        /// <see cref="https://docs.thinkingdata.cn/ta-manual/v3.4/installation/installation_menu/client_sdk/unity_sdk_installation/unity_sdk_installation.html#_3-2-%E8%AE%BE%E7%BD%AE%E9%9D%99%E6%80%81%E5%85%AC%E5%85%B1%E5%B1%9E%E6%80%A7"/>
        public static void SuperPropertyGet(string appid = "")
        {
            ThinkingAnalyticsAPI.GetSuperProperties(appid);
        }

        // 注意：如果事件属性出现重名，动态公共属性的优先级大于公共事件属性，小于 Track 中设置的事件属性。

        /// <summary>
        /// 动态设置公共属性
        /// </summary>
        public static void SetDynamicSuperProperties<T>(T type, string appid = "") where T : IDynamicSuperProperties
        {
            ThinkingAnalyticsAPI.SetDynamicSuperProperties(type, appid);
        }

        /// <summary>
        /// 动态设置公共属性
        /// </summary>
        public static void SetDynamicSuperProperties<T>(string appid = "") where T : IDynamicSuperProperties
        {
            ThinkingAnalyticsAPI.SetDynamicSuperProperties(Activator.CreateInstance<T>(), appid);
        }

        #endregion

        #region 用户属性

        /// <summary>
        /// 上传的用户属性只要设置一次 使用该接口上传的属性将会覆盖原有的属性值
        /// </summary>
        public static void UserSetOnce(Dictionary<string, object> data, string appid = "")
        {
            ThinkingAnalyticsAPI.UserSetOnce(data, appid);
        }

        /// <summary>
        /// 使用该接口上传的属性将会覆盖原有的属性值
        /// </summary>
        public static void UserSet(Dictionary<string, object> data, string appid = "")
        {
            ThinkingAnalyticsAPI.UserSet(data, appid);
        }

        /// <summary>
        /// 对属性进行累加操作 object 只允许数值类型
        /// </summary>
        public static void UserAdd(Dictionary<string, object> data, string appid = "")
        {
            ThinkingAnalyticsAPI.UserAdd(data, appid);
        }

        /// <summary>
        /// 对属性进行重置
        /// </summary>
        public static void UserUnset(string property, string appid = "")
        {
            ThinkingAnalyticsAPI.UserUnset(property, appid);
        }

        /// <summary>
        /// 对属性进行重置
        /// </summary>
        public static void UserUnset(params string[] property)
        {
            List<string> listProps = new List<string>(property);
            ThinkingAnalyticsAPI.UserUnset(listProps);
        }

        /// <summary>
        /// 对属性删除
        /// </summary>
        public static void UserDelete(string appid = "")
        {
            ThinkingAnalyticsAPI.UserDelete(appid);
        }

        /// <summary>
        /// 删除用户数据并指定操作时间.
        /// </summary>
        public static void UserDelete(DateTime date, string appid = "")
        {
            ThinkingAnalyticsAPI.UserDelete(date, appid);
        }

        /// <summary>
        /// 对用户属性追加元素
        /// </summary>
        public static void UserAppend(string attribute, object obj, string appid = "")
        {
            var data = new Dictionary<string, object>()
                {
                    {attribute, obj}
                };
            ThinkingAnalyticsAPI.UserAppend(data, appid);
        }

        #endregion
    }
}