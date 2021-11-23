/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Audio 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.4.0f1 
* Date:             2020-07-07
* Time:             13:14:54
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor.Audio
{
    using Framework;
    using UnityEditor;
    using UnityEngine;

    //[CustomEditor(typeof(UISoundManager))]
    /// <summary> </summary>
    public class UISoundManagerEditor : BaseEditor
    {
        //private UISoundManager obj;

        //protected override void awake()
        //{
        //    obj = target as UISoundManager;
        //}

        //protected override void init()
        //{
        //    if (obj == null) obj = target as UISoundManager;
        //}

        //protected override void inspector()
        //{
        //    vector = Tool.BE.ScrollView(() =>
        //    {
        //        if (obj.attribute != null)
        //        {
        //            obj.attribute.Mute = Tool.AC.Toggle("音乐静音", obj.attribute.Mute);
        //            obj.attribute.Volume = Tool.AC.Slider("音乐音量", obj.attribute.Volume, 0, 1);

        //            Tool.EA.Separator();
        //            KeysList = Tool.BE.FoldoutHeaderGroup(keysList, KeysList, "播放列表");
        //        }
        //    }, vector, GStyleTool.scrollView);
        //    Undo.RecordObject(target, "Undo UISoundManager");
        //}


        //private bool KeysList;
        //private void keysList()
        //{
        //    Tool.BE.Vertical(() =>
        //    {
        //        for (int i = 0; i < obj.keys.Count; i++)
        //        {
        //            Tool.BE.Horizontal(() =>
        //            {
        //                Tool.AC.LabelPrefix(string.Concat("NO:", (i + 1)));
        //                Tool.AC.LabelSelectable(obj.keys[i], GUILayout.Height(15));
        //            }, GStyleTool.helpBox);
        //        }
        //    });
        //}

        //protected override void change()
        //{
        //    EditorUtility.SetDirty(target);
        //}
    }
}