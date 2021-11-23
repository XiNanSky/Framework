/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-05-20
* Time:             22:16:29
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary> 自定义窗口基类 </summary>
    public class BaseWindowEditor : EditorWindow
    {
        protected GUILayoutOption contentOp;      //不建议使用 显示风格里包含格式 GUIStyle

        protected GUILayoutOption titleOp;

        protected GUILayoutOption lalbeOp;

        protected EditorWindow window;

        protected Vector2 vector;

        protected object beanObj;

        /*添加菜单栏用于打开窗口
        [MenuItem("Tool/Bug Reporter")]
        protected static void ShowWindow<T>()
        {
            GetWindow(typeof(T));
        }  
        */

        /// <summary> 绘制 </summary>
        protected virtual void onGUI()
        {

        }

        /// <summary> 改变 </summary>
        protected virtual void change()
        {

        }

        /// <summary> 清除 </summary>
        protected virtual void close()
        {

        }

        /// <summary> 重置 </summary>
        protected virtual void reset()
        {

        }

        /// <summary> 保存 </summary>
        protected virtual void save()
        {

        }

        /// <summary> 加载 </summary>
        protected virtual void load()
        {

        }

        protected virtual void onDisable()
        {

        }

        protected virtual void onEnable()
        {

        }

        #region 周期函数

        private void OnEnable()
        {
            onEnable();
        }

        private void OnGUI()
        {
            onGUI();

            Repaint();//刷新

            if (GUI.changed) change();
        }

        private void OnDisable()
        {
            save();
            onDisable();
        }

        private void OnDestroy()
        {
            close();
        }

        #endregion


    }
}