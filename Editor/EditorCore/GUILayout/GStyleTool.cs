/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-03
* Time:             17:36:15
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using System.Collections;
    using UnityEngine;

    public class GStyleTool : BaseGUIStyle
    {
        private static Hashtable tabel = new Hashtable();

        public static GUIStyle label
        {
            get { return GSkinTool.skin.label; }
        }

        public static GUIStyle box
        {
            get { return GSkinTool.skin.box; }
        }

        public static GUIStyle button
        {
            get { return GSkinTool.skin.button; }
        }

        public static GUIStyle foldout
        {
            get { return GSkinTool.skin.FindStyle("foldout"); }
        }

        public static GUIStyle horizontalScrollbar
        {
            get { return GSkinTool.skin.horizontalScrollbar; }
        }

        public static GUIStyle horizontalScrollbarLeftButton
        {
            get { return GSkinTool.skin.horizontalScrollbarLeftButton; }
        }

        public static GUIStyle horizontalScrollbarRightButton
        {
            get { return GSkinTool.skin.horizontalScrollbarRightButton; }
        }

        public static GUIStyle horizontalScrollbarThumb
        {
            get { return GSkinTool.skin.horizontalScrollbarThumb; }
        }

        public static GUIStyle horizontalSlider
        {
            get { return GSkinTool.skin.horizontalSlider; }
        }

        public static GUIStyle horizontalSliderThumb
        {
            get { return GSkinTool.skin.horizontalSliderThumb; }
        }

        public static GUIStyle scrollView
        {
            get { return GSkinTool.skin.scrollView; }
        }

        public static GUIStyle textArea
        {
            get { return GSkinTool.skin.textArea; }
        }

        public static GUIStyle textField
        {
            get { return GSkinTool.skin.textField; }
        }

        public static GUIStyle toggle
        {
            get { return GSkinTool.skin.toggle; }
        }

        public static GUIStyle verticalScrollbar
        {
            get { return GSkinTool.skin.verticalScrollbar; }
        }

        public static GUIStyle verticalScrollbarDownButton
        {
            get { return GSkinTool.skin.verticalScrollbarDownButton; }
        }

        public static GUIStyle verticalScrollbarThumb
        {
            get { return GSkinTool.skin.verticalScrollbarThumb; }
        }

        public static GUIStyle verticalScrollbarUpButton
        {
            get { return GSkinTool.skin.verticalScrollbarUpButton; }
        }

        public static GUIStyle verticalSlider
        {
            get { return GSkinTool.skin.verticalSlider; }
        }

        public static GUIStyle verticalSliderThumb
        {
            get { return GSkinTool.skin.verticalSliderThumb; }
        }

        public static GUIStyle window
        {
            get { return GSkinTool.skin.window; }
        }

        public static GUIStyle content
        {
            get { return GSkinTool.skin.FindStyle("content"); }
        }

        public static GUIStyle UIContent
        {
            get { return GSkinTool.skin.FindStyle("UIContent"); }
        }

        public static GUIStyle title
        {
            get { return GSkinTool.skin.FindStyle("title"); }
        }

        public static GUIStyle helpBox
        {
            get { return GetName("HelpBox"); }
        }

        public static GUIStyle GetName(string name)
        {
            if (!tabel.ContainsKey(name))
            {
                var style = new GUIStyle(name);
                if (style == null)
                {
                    Debug.LogWarning("Not Find GUIStyle, This Name :" + name); return GUIStyle.none;
                }
                style.richText = true;
                tabel.Add(name, style);
            }
            return (GUIStyle)tabel[name];
        }
    }
}