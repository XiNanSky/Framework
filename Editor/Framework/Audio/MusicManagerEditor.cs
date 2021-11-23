/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XN 
* All rights reserved. 
* FileName:         Framework.Audio 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.4.0f1 
* Date:             2020-07-07
* Time:             12:25:05
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Editor.Audio
{
    using Framework;
    using UnityEditor;
    using UnityEngine;         

    //[CustomEditor(typeof(MusicManager))]
    public class MusicManagerEditor : BaseEditor
    {
        //private MusicManager obj;

        //protected override void awake()
        //{
        //    obj = target as MusicManager;
        //}

        //protected override void init()
        //{
        //    if (obj == null) obj = target as MusicManager;
        //}

        //protected override void inspector()
        //{
        //    vector = Tool.BE.ScrollView(() =>
        //    {
        //        if (obj.attribute != null)
        //        {
        //            obj.attribute.WaitTime = Tool.AC.FieldFloat("间隙时间", obj.attribute.WaitTime);
        //            obj.attribute.Volume = Tool.AC.Slider("音乐音量", obj.attribute.Volume, 0, 1);
        //            obj.attribute.Loop = Tool.AC.Toggle("单曲循环", obj.attribute.Loop);
        //            obj.attribute.Shuffle = Tool.AC.Toggle("随机播放", obj.attribute.Shuffle);

        //            Tool.EA.Separator();
        //            Tool.AC.Toggle("暂停播放", obj.attribute.Pause);
        //            Tool.AC.Toggle("音乐静音", obj.attribute.Mute);

        //            if (Tool.AC.Button("切换音乐")) obj.CutMusic();
        //            if (Tool.AC.Button("暂停播放")) obj.PauseMusic();
        //            if (Tool.AC.Button("恢复播放")) obj.ResumeMusic();
        //            if (Tool.AC.Button("音乐静音")) obj.MuteMusic();

        //            Tool.EA.Separator();
        //            KeysList = Tool.BE.FoldoutHeaderGroup(keysList, KeysList, "播放列表");
        //        }
        //    }, vector, GStyleTool.scrollView);
        //    Undo.RecordObject(target, "Undo MusicManager");
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