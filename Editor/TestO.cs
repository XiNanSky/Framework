/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-05                    *
* Nowtime:           09:58:30                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Editor
{
    public class TestO
    {
        /// <summary>
        /// 打开开发者模式
        /// </summary>
        [UnityEditor.MenuItem("ETool/开启 开发者模式", false, 0)]
        public static void OpenDeveloperMode()
        {
            UnityEditor.EditorPrefs.SetBool("DeveloperMode", true);
        }

        /// <summary>
        /// 关闭开发者模式
        /// </summary>
        [UnityEditor.MenuItem("ETool/关闭 开发者模式", false, 0)]
        public static void CloseDeveloperMode()
        {
            UnityEditor.EditorPrefs.SetBool("DeveloperMode", false);
        }

    }
}