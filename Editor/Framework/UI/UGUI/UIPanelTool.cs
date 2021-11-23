///** 
// *Elephant 
// *All rights reserved. 
// *FileName:         Framework.UI
// *Author:           XiNan
// *Version:          1.0
// *UnityVersion:     2019.4.13f1c1
// *Date:             2021-06-08
// *Description:        
// *History:          
//*/

//namespace Framework.UI
//{
//    using System.Collections;
//    using System.Collections.Generic;
//    using UnityEditor;
//    using UnityEngine;
//    using UnityEngine.UI;

//    public class UIPanelTool
//    {
//        [MenuItem("ETool/UIFramework/Create UIPanel", false, 0)]
//        //创建UIPanel初始物体
//        public static void CreateUIPanel()
//        {
//            GameObject Panel = new GameObject("UIPanel");
//            GameObject Canvas = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(CanvasGroup), typeof(GraphicRaycaster));
//            Canvas.transform.SetParent(Panel.transform);
//            var canvas = Canvas.GetComponent<Canvas>();
//            canvas.renderMode = RenderMode.ScreenSpaceCamera;
//            canvas.pixelPerfect = true;
//            canvas.planeDistance = 0.1f;

//            var canvasScaler = Canvas.GetComponent<CanvasScaler>();
//            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
//            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
//            canvasScaler.referenceResolution = new Vector2(1920, 1080);
//        }


//        [MenuItem("ETool/UIFramework/Tag/Add UIPanel")]
//        public static void AddUIPanelTag()
//        {
//            //打开标签管理器
//            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
//            //找到标签属性
//            var tagsProp = tagManager.FindProperty("tags");
//            var tags = System.Enum.GetNames(typeof(UIBaseLayer));
//            for (int i = 0; i < tags.Length; i++)
//            {
//                //在标签属性数组中指定的索引处插入一个空元素。
//                tagsProp.InsertArrayElementAtIndex(i);
//                //给标签属性数组中刚才插入的空元素赋值
//                tagsProp.GetArrayElementAtIndex(i).stringValue = tags[i];
//            }
//            //添加完之后应用一下
//            tagManager.ApplyModifiedProperties();
//            Debug.Log($"Now TagsPorp Size:{tagsProp.arraySize}");
//        }

//        [MenuItem("ETool/UIFramework/Tag/Clear All")]
//        public static void ClearAllTag()
//        {
//            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
//            tagManager.FindProperty("tags").ClearArray();
//            tagManager.ApplyModifiedProperties();
//            Debug.Log($"Finish Clear Tag All");
//        }
//    }
//}