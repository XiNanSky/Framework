/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer 
* All rights reserved. 
* FileName:         Editors.Tool 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-04-22
* Time:             15:25:46
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Note
{
    public class EditorWindowNote
    {
        //MenuItem : 需要在 void 前加入 static 字段,在方法中为添加菜单栏按钮,点击后调用 void 方法.
        //as : [MenuItem("GenerateFolder/Open My Window")]

        //AssetDatabase.LoadAssetAtPath : 根目录指向 Assets 上一级 
        //as : AssetDatabase.LoadAssetAtPath<Sprite>("Assets/UI/activity.png)

        #region GetWindow GetWindowWithRect

        /* 推荐配置 (强制转换)GetWindow<类型>("是否需要窗口浮动","标题名","是否需要自动提示打开窗口面板");
         * system.Type t : The type of the window. Must derive from EditorWindow.
         * system.Type t : typeof(类型)类
         * utility : Set this to true, to create a floating utility window, false to create a normal window.
         * utility : 创建一浮动窗口 false 则可以与game面板等合并 true则不能 窗口是一个弹窗 为单独的一个面板
         * title : If GetWindow creates a new window, it will get this title. If this value is null, use the class name as title.
         * title : 标题 如果值为空 则名为类名
         * focus : Whether to give the window focus, if it already exists. (If GetWindow creates a new window, it will always get focus).
         * focus : 如果已经存在 是否给窗口焦点 打开已存在的窗口 值为true 则获得焦点 false 则不会里面显示窗口
        */

        #endregion

        #region EditorWindow

        /* 值
         * Vector2 = window.maxSize                    窗口最大的宽高
         * Vector2 = window.minSize                    窗口最小的宽高 
         * bool    = focusedWindow                     当前具有键盘焦点的编辑器窗口
         * bool    = mouseOverWindow                   当前在鼠标指针下的编辑器窗口

         * bool    = window.maximized                  是否最大化 
         * Rect    = window.position                   窗口在屏幕中显示的位置
         * bool    = window.autoRepaintOnSceneChange   当场景中发生改变时 是否自动重新刷新窗口
         * bool    = window.wantsMouseMove             检查这个窗口中的GUI是否接收到MouseMove事件。
         * bool    = window.wantsMouseEnterLeaveWindow 检查这个编辑器窗口中的GUI是否接收到了MouseEnterWindow和MouseLeaveWindow事件。
         * int     = window.depthBufferBits            深度缓冲块
         * HideFlags window.hideFlags;                 HideFlags. unity编辑标签

         * 如果为window.titleContent = GUIContent.none 最好不使用 目前没有实质效果
         * window.titleContent = GUIContent            用于绘制编辑器窗口标题的GUIContent。空内容 
         * window.titleContent.tooltip                 内容需要显示的备注 需要鼠标点击显示 最好不使用
         * window.titleContent.image                   内容需要显示的图片 自动显示 最好不使用
         * window.titleContent.text                    内容需要显示的文字 处于屏幕中央 最好不使用
        */

        /* 方法
         * window.Focus();                             将键盘焦点移到另一个编辑器窗口
         * window.RemoveNotification();                移除提示内容
         * window.SendEvent();                         发送事件到窗口

         * window.BeginWindows();                      在上级窗口中开始绘制窗口      
         * window.EndWindows();                        在上级窗口中结束绘制窗口 

         * window.Show();                              显示窗口
         * window.ShowTab();                           提示弹窗
         * window.ShowPopup();                         提示弹窗
         * window.ShowAsDropDown(Rect,Vector2);        下拉式窗口
         * window.ShowAuxWindow();                     在辅助窗口中显示编辑器窗口。
         * window.ShowUtility();                       程序化窗口
         * window.ShowNotification(GUIContent);        通知

         * window.Repaint();                           重新绘制窗口
        */

        /* 静态方法
         * EditorWindow.FocusWindowIfItsOpen<类型>();                 如果指定类型的第一个找到的编辑器窗口是打开的，则集中该编辑器窗口。
         * EditorWindow.CreateInstance<类型>();                       创建可编写脚本的对象的实例。
         * EditorWindow.GetWindow<>()                                 创建一个窗口 可以随意放大缩小
         * EditorWindow.GetWindowWithRect<>(Rect,utility,title,focus);创建一个窗口 不可以随意放大缩小 固定窗口大小
        */

        #endregion
    }
}