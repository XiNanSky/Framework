/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Editors.Tool 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.3.13f1 
* Date:             2020-06-05
* Time:             06:59:00
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Tool
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary> 图片直接赋值 </summary>
    public static class SceneImageSelectEditor
    {
        //private static Object LastSelectObj = null;//用来记录上次选中的GameObject，只有它带有Image组件时才把图片赋值给它
        //private static Object CurSelectObj = null;

        //[InitializeOnLoadMethod]
        //private static void Init()
        //{
        //    Selection.selectionChanged += OnSelectChange;
        //}

        //private static void OnSelectChange()
        //{
        //    LastSelectObj = CurSelectObj;
        //    CurSelectObj = Selection.activeObject;
        //    //如果要遍历目录，修改为SelectionMode.DeepAssets
        //    Object[] arr = Selection.GetFiltered(typeof(Object), SelectionMode.TopLevel);
        //    if (arr != null && arr.Length > 0)
        //    {
        //        GameObject selectObj = LastSelectObj as GameObject;
        //        if (selectObj != null && (arr[0] is Sprite || arr[0] is Texture2D))
        //        {
        //            string assetPath = AssetDatabase.GetAssetPath(arr[0]);//拿到该资源的路径
        //            Image image = selectObj.GetComponent<Image>();
        //            bool isImgWidget = false;
        //            if (image != null)
        //            {
        //                isImgWidget = true;
        //                Object newImg = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite));
        //                Undo.RecordObject(image, "Change Image");//有了这句才可以用 ctrl + z 撤消此赋值操作
        //                image.sprite = newImg as Sprite;
        //                //image.SetNativeSize();        //是否使用图片原始大小
        //                EditorUtility.SetDirty(image);
        //            }
        //            if (isImgWidget)
        //            {   //赋完图后把焦点还给Image节点
        //                EditorApplication.delayCall = delegate { Selection.activeGameObject = LastSelectObj as GameObject; };
        //            }
        //        }
        //    }
        //}
    }
}