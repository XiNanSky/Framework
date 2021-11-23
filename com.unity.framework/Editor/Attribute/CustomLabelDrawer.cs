/***************************************************
* Copyright(C) 2019 by xinansky                    *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2019.3.13f1                   *
* Date:              2019-05-16                    *
* Nowtime:           19:38:52                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
    /// </summary>
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelDrawer : PropertyDrawer
    {
        private GUIContent _label = null;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_label == null)
            {
                string name = (attribute as LabelAttribute).name;
                _label = new GUIContent(name);
            }

            EditorGUI.PropertyField(position, property, _label);
        }
    }
}