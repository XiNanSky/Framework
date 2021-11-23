///* * * * * * * * * * * * * * * * * * * * * * * *
//* DefaultCompany
//* FileName:         Framework.UICompnoent
//* Author:           XiNan
//* Version:          0.1
//* UnityVersion:     2020.3.5f1c1
//* Date:             2021-05-07
//* Time:             11:23:53
//* E-Mail:           1398581458@qq.com
//* Description:        
//* History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.Editor.UICompnoent
//{
//    using Framework.UI;
//    using UnityEditor;
//    using UnityEditor.UI;
//    using UnityEngine;

//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(UIButton), true)]
//    public class UIButtonEditor : ButtonEditor
//    {
//        protected static bool showImage;

//        protected SerializedObject serObjs;

//        protected string UndoNmae;

//        protected override void OnEnable()
//        {
//            base.OnEnable();
//            serObjs = new SerializedObject(targets);
//            UndoNmae = "UIButton";
//            init();
//        }

//        public override void OnInspectorGUI()
//        {
//            // 更新序列化对象的表示，仅当对象自上次调用Update后被修改或它是一个脚本时。
//            serObjs.UpdateIfRequiredOrScript();
//            // 显示并修改自定义面板 
//            inspector();
//            // 应用属性修改而不注册撤消操作。
//            serObjs.ApplyModifiedPropertiesWithoutUndo();
//            // 在下一次调用Update()时更新hasMultipleDifferentValues缓存。
//            serObjs.SetIsDifferentCacheDirty();
//            // 执行自定义面板操作 
//            if (GUI.changed) { change(); foreach (var t in serObjs.targetObjects) EditorUtility.SetDirty(t); }
//            foreach (var t in serObjs.targetObjects) Undo.RecordObject(t, string.Concat("Undo", UndoNmae));
//            Repaint();
//        }

//        protected virtual void init() { }

//        protected virtual void inspector() { base.OnInspectorGUI(); }

//        protected virtual void change() { }

//        private void OnDestroy() { serObjs.Dispose(); }

//        protected override void OnDisable() { base.OnDisable(); }
//    }
//}