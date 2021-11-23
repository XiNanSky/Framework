/***************************************************
* Copyright(C) 2021 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2020.3.12f1c1                 *
* Date:              2021-09-01                    *
* Nowtime:           00:18:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class ImageExtend
    {
        /// <summary>
        /// 设置图片透明度 eg:ColorKit.setAlpha(bar.bar_bg, 0.5f + (i % 2) * 0.5f);
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="alpha">透明度</param>
        public static void SetAlpha(this Image image, float alpha)
        {
            if (image == null) return;
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}