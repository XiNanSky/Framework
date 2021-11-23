/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-04-22
* Time:             12:34:10
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using UnityEngine;

    /// <summary> GLayTool Content </summary>
    public class GLayTool : BaseGUILayout
    {
        /// <summary> 滚动视图格式 </summary>
        public static GUILayoutOption LayScroll()
        {
            GUILayoutOption content;

            content = GUILayout.ExpandHeight(true);
            content = GUILayout.ExpandWidth(true);

            return content;
        }

        /// <summary> 内容格式 </summary>
        public static GUILayoutOption LayContent()
        {
            GUILayoutOption content;

            content = GUILayout.Height(15);
            content = GUILayout.ExpandHeight(false);

            content = GUILayout.Width(100);
            content = GUILayout.ExpandWidth(true);

            return content;
        }

        /// <summary> 横行格式 </summary>
        public static GUILayoutOption LayHTitle()
        {
            GUILayoutOption content;

            content = GUILayout.Height(15);
            content = GUILayout.ExpandHeight(false);

            content = GUILayout.Width(400);
            content = GUILayout.ExpandWidth(true);

            return content;
        }

        /// <summary> 标签格式 </summary>
        public static GUILayoutOption LayLalbe()
        {
            GUILayoutOption content;

            content = GUILayout.Height(15);
            content = GUILayout.ExpandHeight(false);

            content = GUILayout.Width(240);
            content = GUILayout.MinWidth(240);
            content = GUILayout.MaxWidth(240);
            content = GUILayout.ExpandWidth(true);

            return content;
        }
    }
}