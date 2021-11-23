/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN
* All rights reserved. 
* FileName:         Editors
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-04-21
* Time:             20:01:08
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary> Editor 基类 无预览窗口 数据类 </summary> 
    //tips:[CustomEditor(typeof(EditorManager))]
    public class BaseEditor : Editor
    {
        #region 属性

        /// <summary> 开启预览窗口 </summary>
        public bool preview = false;

        /// <summary> scorll pos </summary>
        protected Vector2 vector;
        protected string UndoNmae;

        protected SerializedObject serObj;
        protected SerializedObject serObjs;

        #endregion

        #region 重写方法

        /// <summary> 一次初始化 </summary>
        protected virtual void awake() { }

        /// <summary> 多次初始化 </summary>
        protected virtual void init() { }

        /// <summary> 显示面板 修改数据 </summary>
        protected virtual void inspector() { }

        /// <summary> 面板中数据变动 修改属性 </summary>
        protected virtual void change() { }

        /// <summary> 销毁时调用 </summary>
        protected virtual void destroy() { }

        /// <summary> 脚本或对象禁用时调用 </summary>
        protected virtual void disable() { }

        #endregion

        #region 周期方法

        /// <summary> 鼠标点击进入调用 </summary> 最先调用 修改脚本后 该方法不会重新调用 
        private void Awake()
        {
            awake();
        }

        /// <summary> 每次开启调用 </summary> 在awake之后调用 修改脚本后 该方法会自动重新调用
        private void OnEnable()
        {
            serObjs = new SerializedObject(targets);
            init();
        }

        /// <summary> 脚本或对象禁用时调用 </summary>
        private void OnDisable()
        {
            disable();
            foreach (var t in serObjs.targetObjects)
            {
                if (t == null) continue;
                EditorUtility.SetDirty(t);
                Undo.RecordObject(t, string.Concat("Undo", UndoNmae));
            }
        }

        /// <summary> 销毁时调用 </summary> 并且鼠标点击其他实例 且也会调用该方法
        private void OnDestroy()
        {
            destroy();
        }

        /// <summary> 在场景编辑器中处理事件 </summary>
        private void OnSceneGUI()
        {

        }

        /// <summary> 执行这一个函数来一个自定义检视面板 </summary> 首次进入 执行7次  相当于updata
        public override void OnInspectorGUI()
        {
            // 更新序列化对象的表示，仅当对象自上次调用Update后被修改或它是一个脚本时。
            serObjs.UpdateIfRequiredOrScript();
            // 显示并修改自定义面板 
            inspector();
            // 应用属性修改而不注册撤消操作。
            serObjs.ApplyModifiedPropertiesWithoutUndo();
            // 在下一次调用Update()时更新hasMultipleDifferentValues缓存。
            serObjs.SetIsDifferentCacheDirty();
            // 执行自定义面板操作 
            if (GUI.changed) { change(); foreach (var t in serObjs.targetObjects) EditorUtility.SetDirty(t); }
            foreach (var t in serObjs.targetObjects) Undo.RecordObject(t, string.Concat("Undo", UndoNmae));
            Repaint();
        }

        /// <summary> 实现这个方法来创建一个自定义uielement检查器 </summary>  2019.2.18f
        //public override VisualElement CreateInspectorGUI()
        //{
        //    return null;
        //}

        #endregion
    }
}