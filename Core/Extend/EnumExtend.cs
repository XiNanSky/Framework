/***************************************************
* Copyright(C) 2019 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2019-05-16                    *
* Nowtime:           19:38:52                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;

    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtend
    {
        /// <summary>
        /// 转化为Int
        /// </summary>
        public static int TOInt(this Enum e)
        {
            return e.GetHashCode();
        }

        /// <summary>
        /// 获取自定义属性值
        /// </summary>
        public static string GetDescription<T>(this Enum value) where T : EnumDescriptionAttribute
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }
}
