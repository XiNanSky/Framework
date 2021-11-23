/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-04-22
* Time:             14:52:33
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using UnityEngine;

    /// <summary> Editor 基类 2D预览窗口 数据类 </summary>
    public class Base2DEditor : BaseEditor
    {
        #region 重写方法

        protected override void init() { base.init(); preview = true; }

        /// <summary> 预览内容的绘制 </summary>
        protected virtual void previewGUI(Rect r, GUIStyle background) { }

        /// <summary> 预览标题 预览窗口右上角 </summary>
        protected virtual void PreviewSettings() { base.OnPreviewSettings(); }

        /// <summary> 预览标题 预览窗口左上角 </summary>
        protected virtual GUIContent PreviewTitle()
        {
            return null;
        }

        #endregion


        #region 周期函数

        /// <summary> 如果要在预览标头中显示自定义控件,请重写此方法 </summary>
        /// 标题栏右边可以绘制其他的信息或者按钮等
        /// 重载 OnPreviewSettings 接口方便对预览窗口进行控制
        /// 预览窗口右上角
        public override void OnPreviewSettings()
        {
            PreviewSettings();
        }

        /// <summary> 预览标题 预览窗口左上角 </summary>
        /// 默认显示的是物体的名称,重载 GetPreviewTitle 接口可以更改标题名称
        public override GUIContent GetPreviewTitle()
        {
            if (PreviewTitle() == null) return PreviewTitle(); return base.GetPreviewTitle();
        }

        protected override void OnHeaderGUI()
        {
         
        }

        /// <summary> 如果该组件可以在当前状态下预览 则为True </summary>
        public override bool HasPreviewGUI()
        {
            return preview;
        }

        /// <summary> 预览内容的绘制 </summary>
        /// 最后预览内容的绘制，只需要重载 OnPreviewGUI 接口即可
        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            previewGUI(r, background);
        }

        #endregion
    }
}