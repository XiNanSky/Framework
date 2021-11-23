/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors.InspectorIDE.GUILayout 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-04
* Time:             12:40:51
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using UnityEngine;

    public static class GSkinTool
    {
        private static GUISkin _skin;

        public static GUISkin skin
        {
            get { if (_skin == null) defualtSkin(); return _skin; }
            private set { }
        }

        private static void defualtSkin()
        {
            if (_skin == null)
            {
                _skin = (GUISkin)Resources.Load("InspectorIDESkin");
                if (_skin == null)
                {
                    Debug.LogError("error: not find EditorSkin.");
                    return;
                }
                //文本字段中光标的颜色
                _skin.settings.cursorColor = Color.green;
                //文本字段光标闪烁的速度
                _skin.settings.cursorFlashSpeed = 1f;
                //文本字段中选择矩形的颜色。
                _skin.settings.selectionColor = Color.red;
                //应该双击文本字段中的select words
                _skin.settings.doubleClickSelectsWord = true;
                //应该三次点击选择文本字段中的整个文本
                _skin.settings.tripleClickSelectsLine = true;
            }
        }
    }
}