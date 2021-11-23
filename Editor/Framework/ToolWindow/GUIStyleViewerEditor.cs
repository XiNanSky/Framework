/* * * * * * * * * * * * * * * * * * * * * * * * 
*Copyright(C) 2021 by xinansky 
*All rights reserved. 
*FileName:         Framework.EToolWindow 
*Author:           XiNan 
*Version:          0.1 
*UnityVersion:     2020.3.5f1c1 
*Date:             2021-07-04 
*NOWTIME:          14:11:19 
*Description:        
*History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.EToolWindow
{
    using Framework.Editor;
    using UnityEditor;
    using UnityEngine;

    public class GUIStyleViewer : BaseWindowEditor
    {
        private static new EditorWindow window;
        [MenuItem("ETool/Windows/GUIStyle Viewer")]
        public static void Open()
        {
            window = window ?? GetWindow<GUIStyleViewer>(true, "GUIStyle Viewer", true);//创建窗口
            window.wantsMouseMove = true;
            window.Show(true);//展示         
        }

        public GUIStyleViewer()
        {
            minSize = new Vector2(600, 800);
        }

        private string search = "";

        private void OnGUI()
        {
            ETool.BE.Horizontal(() =>
            {
                GUILayout.Space(30);
                search = EditorGUILayout.TextField("", search, "SearchTextField", GUILayout.MaxWidth(position.x / 3));
                ETool.AC.Label("", "SearchCancelButtonEmpty");
            }, "HelpBox");
            vector = ETool.BE.ScrollView(() =>
            {
                foreach (GUIStyle style in GUI.skin.customStyles)
                {
                    if (style.name.ToLower().Contains(search.ToLower()))
                    {
                        DrawStyleItem(style);
                    }
                }
            }, vector);
        }

        private void DrawStyleItem(GUIStyle style)
        {
            ETool.BE.Horizontal(() =>
            {
                GUILayout.Space(40);
                ETool.AC.LabelSelectable(style.name, GUILayout.Height(40));
                GUILayout.FlexibleSpace();
                ETool.AC.LabelSelectable(style.name, style, GUILayout.Height(40));
                GUILayout.Space(40);
                ETool.AC.LabelSelectable("", style, GUILayout.Height(40), GUILayout.Width(40));
                GUILayout.Space(50);
                if (ETool.AC.Button("Copy"))
                {
                    TextEditor textEditor = new TextEditor();
                    textEditor.text = style.name;
                    textEditor.OnFocus();
                    textEditor.Copy();
                }
            }, GStyleTool.helpBox, GUILayout.Height(40));
            GUILayout.Space(10);
        }

        protected override void close()
        {
            window = null;
        }
    }
}