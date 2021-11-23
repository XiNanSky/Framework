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
    using UnityEngine;

    /// <summary>
    /// 使字段在Inspector中显示自定义的名称。
    /// </summary>
    public class LabelAttribute : PropertyAttribute
    {
        public string name;

        /// <summary>
        /// 使字段在Inspector中显示自定义的名称。
        /// </summary>
        /// <param name="name">自定义名称</param>
        public LabelAttribute(string name)
        {
            this.name = name;
        }
    }
}