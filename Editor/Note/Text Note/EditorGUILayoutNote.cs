/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2020 by XiNan Indie Developer 
* All rights reserved. 
* FileName:         Editors.Note 
* Author:           XiNan 
* Version:          0.1 
* UnityVersion:     2019.2.18f1 
* Date:             2020-04-22
* Time:             15:28:09
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.Note
{
    public class EditorGUILayoutNote
    {
        #region 连续性绘制方法

        /*
         * bool    EditorGUILayout.BeginFadeGroup(float) : 传入参数控制绘制是否隐藏或显示 0为完全隐藏 1为显示 不等于1则不可以鼠标点击输入参数
         * as :    bool = EditorGUILayout.BeginFadeGroup(float);
         *         EditorGUILayout.EndFadeGroup();

         * Rect    EditorGUILayout.BeginHorizontal(GUIStyle,GUILayoutOption); 传入风格 自定义参数 横向绘制
         * as :    EditorGUILayout.BeginHorizontal();
         *         EditorGUILayout.EndHorizontal();

         * Vector2 EditorGUILayout.BeginScrollView(Vector2,bool,bool,GUIStyle,GUIStyle,GUIStyle,GUILayoutOption) : 控制内容上下滚动 左右滚动
         * as :    Vector2 = EditorGUILayout.BeginScrollView(Vector2,true,true,GUILayout.Height(100),GUILayout.Width(400)); GUIStyle如果不特殊设置最好不用none
         *         EditorGUILayout.EndScrollView(); trips:GUILayoutOption的宽高最好使用Screen.height,Screen.Width 
        */

        #endregion

        #region 一次性绘制方法

        /* 
         * EditorGUILayout.Slider : 滑动条, ("名字",中间变量,最小值,最大值)
         * as : float = EditorGUILayout.Slider("Slider",float,0,200)

         * EditorGUILayout.Vector2Field <== 二维坐标 ==> EditorGUILayout.Vector2IntField 
         * EditorGUILayout.Vector3Field <== 三维坐标 ==> EditorGUILayout.Vector3IntField
         * EditorGUILayout.Vector4Field <== 四维坐标 ==> ("名字",中间变量,风格,自定义参数)
         * as : Vector2 = EditorGUILayout.Vector2Field("Vector2Field",Vector2,GUIStyle,GUILayoutOption);

         * EditorGUILayout.ColorField : 颜色选择,("内容",颜色,开启吸管,开启透明度,开启HDR,风格,自定义参数)
         * as : Color = EditorGUILayout.ColorField(GUIContent,Color,吸管,透明度,HDR,GUIStyle,GUILayoutOption);
        */

        #endregion

        #region 横轴绘制
        /* GUILayout.BeginHorizontal : 开始横轴绘制 (GUI风格,细节参数 如；高度宽度)
         * as : GUILayout.BeginHorizontal(GUIStyle,GUILayoutOption)  

         * GUILayout.EndHorizontal : 结束横轴绘制
         * as : GUILayout.EndHorizontal() */
        #endregion

        #region 区块绘制
        /* GUILayout.BeginArea : 开始区块绘制 (Rect,GUIContent,GUI风格,)
         * as : GUILayout.BeginArea(Rect,GUIContent,GUIStyle);

         * GUILayout.EndArea : 结束区块绘制
         * as : GUILayout.EndArea(); */
        #endregion

        #region 滚轴绘制
        /* GUILayout.BeginScrollView : 开始滚轴绘制
         * as :GUILayout.BeginScrollView(new Vector2(200,200),true,true,GUIStyle.none,GUIStyle.none,gUILayoutOption);

         * GUILayout.EndScrollView : 结束绘制滚轴
         * as :GUILayout.EndScrollView() */
        #endregion

        /*方法
         * GUILayout.Window : 游戏中绘制窗口方法 需要在OnGUI使用
         * as GUILayout.Window(windowID,Rect,GUI.WindowFunction(int),"Content");(ID,显示位置,绘制窗口内容方法,标题)

         * GUILayout.Box : 绘制box形状边框风格
         * as GUILayout.Box(Content,GuiStyle,GUILayoutOption);("内容，图片",风格,自定义参数)

         * GUILayout.Button : 绘制按钮风格 返回值为boolean 按钮每点击一次则改变一次状态
         * as GUILayout.Button(Content,GUIStyle,GUILayoutOption);("内容，图片",风格,自定义参数)
         * GUILayout.RepeatButton : 绘制按钮风格 返回值为boolean 按住不放，按钮就会返回true

         * GUILayout.PasswordField : 绘制一个输入框 返回值为string
         * as string = GUILayout.PasswordField(string,'*'(char),MaxLength,GUIStyle,GUILayoutOption);("输入内容","遮罩字符",输入长度,风格,自定义参数)

         * GUILayout.FlexibleSpace : 插入一行空白元素
         * GUILayout.Space : 在当前布局插入指定高度空白

         * GUILayout.SelectionGrid : 绘制可以选择的格子按钮 格子里有多个内容可点击 返回int 是玩家点击的格子下标
         * as int = GUILayout.SelectionGrid(int,GUIContent[](),xCount(int),GUIStyle,GUILayoutOption);(下标,内容集合,每横排显示N个格子,风格,自定义参数)

         * GUILayout.HorizontalScrollbar : 绘制横滚轴 返回值float
         * as float = GUILayout.HorizontalScrollbar(float(value),size,left,right,GUILayoutOption);(位置,滚轴大小,滚轴最左边,滚轴最右边,风格,自定义参数)
         * GUILayout.VerticalScrollbar : 绘制纵滚轴 返回值float 同上

         * GUILayout.HorizontalSlider : 横滑动条 返回值float
         * asfloat = GUILayout.HorizontalSlider(float(value),left,right,gUILayoutOption);(位置,滑动最左边,滑动最右边,风格,自定义参数)
         * GUILayout.VerticalSlider  : 纵滑动条 返回值float 同上

         * GUILayout.Label : 标题 处于内容最上方
         * as GUILayout.Label("Content",GUIStyle,GUILayoutOption);("内容",风格,自定义参数)

         * GUILayout.TextArea <<= 字符输入块 返回值为string 用户输入的字符 =>> GUILayout.TextField
         * as string = GUILayout.TextArea(string,maxLenght,GUIStyle,GUILayoutOption);("内容",输入最大长度,风格,自定义参数)
         * as string = GUILayout.TextField(string,maxLenght,GUIStyle,GUILayoutOption);同上 效果微乎其微

         * GUILayout.Toggle : 绘制一个按钮 返回值 boolean 用户每点击一次则改变一次状态
         * as bool = GUILayout.Toggle(bool,GUIContent,GUIStyle,GUILayoutOption);(状态bool,内容,风格,自定义参数)

         * GUILayout.Toolbar : 绘制一排连续bar按钮 返回值 int 返回用户选择的bar下标
         * as int = GUILayout.Toolbar(int,GUIContent[],GUIStyle,GUILayoutOption);(选中的下标,内容数组(图片),风格,自定义参数)

        */

        /* 返回类型为 GUILayoutOption
         * GUILayout.MaxHeight : 传递给控件最大高度限制
         * GUILayout.MinHeight : 传递给控件最小高度限制
         * GUILayout.MaxWidth  : 传递给控件最大宽度限制
         * GUILayout.MinWidth  : 传递给控件最小宽度限制
         * GUILayout.ExpandHeight : 传递给空间允许或不允许垂直扩展控件 
         * GUILayout.ExpandWidth  : 传递给空间允许或不允许横轴扩展控件
         * GUILayout.Width  : 传递给空间宽度
         * GUILayout.Height : 传递给空间高度
        */
    }
}