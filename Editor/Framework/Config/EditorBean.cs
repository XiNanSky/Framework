///* * * * * * * * * * * * * * * * * * * * * * * *
//* Copyright(C) 2020 by XN 
//* All rights reserved. 
//* FileName:         Framework.EditorsMnager 
//* Author:           XiNan 
//* Version:          0.1 
//* UnityVersion:     2019.2.18f1 
//* Date:             2020-05-16
//* Time:             20:26:20
//* E-Mail:           1398581458@qq.com
//* Description:        
//* History:          
//* * * * * * * * * * * * * * * * * * * * * * * * */

//namespace Framework.EditorMnager
//{
//    using UnityEngine;

//    /// <summary> 分辨率宽度 </summary>  
//    public enum ScreenWidth
//    {
//        [InspectorName("默认")] Default = 1024,
//        [InspectorName("1920")] One = 1920,
//        [InspectorName("1600")] TWO = 1600,
//    }

//    /// <summary> 分辨率高度 </summary>  
//    public enum ScreenHeight
//    {
//        [InspectorName("默认")] Default = 768,
//        [InspectorName("1080")] One = 1080,
//        [InspectorName("900")] TWO = 900,
//    }

//    /// <summary> 屏幕模式 </summary>   
//    public enum FullScreenMode
//    {
//        [InspectorName("锁定模式")] ExclusiveFullScreen = 0,    /* Exclusive  Mode.  */
//        [InspectorName("全屏窗口")] FullScreenWindow = 1,       /* Fullscreen window.*/
//        [InspectorName("窗口最大化")] MaximizedWindow = 2,      /* Maximized  window.*/
//        [InspectorName("窗口模式")] Windowed = 3                /* Windowed.         */
//    }

//    /// <summary> 刷新率 </summary>      
//    public enum RefreshRate
//    {
//        [InspectorName("默认")] Default = 0,
//        [InspectorName("60Hz")] Sixty = 60,
//        [InspectorName("75Hz")] Seventy_Five = 75,
//        [InspectorName("90Hz")] Ninety = 90,
//        [InspectorName("120Hz")] OneHundred_Twenty = 120
//    }

//    /// <summary> 默认的移动设备方向 </summary>  
//    public enum PlayerSettingOrientation
//    {
//        [InspectorName("纵向,默认")] Portrait = 0,
//        [InspectorName("纵向,上下颠倒")] PortraitUpsideDown = 1,
//        [InspectorName("横向,从纵向逆时针方向")] LandscapeRight = 2,
//        [InspectorName("横向,从纵向顺时针方向")] LandscapeLeft = 3,
//        [InspectorName("自动旋转屏幕")] AutoRotation = 4
//    }

//    /// <summary> 睡眠超时 </summary>  
//    public enum SleepTimeoutTime
//    {
//        [InspectorName("防止屏幕变暗")] NeverSleep = -1,
//        [InspectorName("将睡眠超时设置为用户在系统设置中指定的任何值")] SystemSetting = -2,
//    }

//    /// <summary> 报错信息 </summary>  
//    public enum EActionOnDotNetUnhandledException
//    {
//        [InspectorName("在未处理.net异常(没有生成崩溃报告)的情况下,静默退出.")] SilentExit = 0,
//        [InspectorName("在未处理.net异常的情况下崩溃(将生成崩溃报告)")] Crash = 1
//    }

//    [SerializeField]
//    public class EditorBean 
//    {
//        //Screen.resolutions 监视器支持的所有分辨率

//        /// <summary> 屏幕模式 </summary>   
//        public FullScreenMode ScreenFullMode;

//        /// <summary> 刷新率 </summary>   
//        public RefreshRate RefreshRate;

//        /// <summary> 默认的移动设备方向 </summary>  
//        public PlayerSettingOrientation PlayerSettingOrientation;

//        /// <summary> 崩溃报告 </summary>
//        public bool enableCrashReportAPI;

//        /// <summary> 定义全屏游戏是否应使辅助显示变暗 </summary>
//        public bool captureSingleScreen;

//        /// <summary> 允许用户在全屏模式和窗口模式之间切换 </summary>
//        public bool ScreenFull;

//        /// <summary> 分辨率宽高 </summary>
//        public int ScreenWidth, ScreenHeight;

//        /// <summary> 屏幕亮度 </summary>
//        public int Brightness;

//        /// <summary> 开发者模式 </summary>
//        public bool DeveloperMode;

//        /// <summary> 睡眠超时模式 </summary>
//        public SleepTimeoutTime NeverSleep;

//        public bool AutorotateToLandscapeLeft, AutorotateToLandscapeRight, AutorotateToPortrait, AutorotateToPortraitUpsideDown;

//        /// <summary> 加速度更新 </summary>
//        public int AccelerometerFrequency;

//        public EActionOnDotNetUnhandledException ActionOnDotNetUnhandledException;

//        /// <summary> 构建目标的应用程序标识符 </summary>
//        public string applicationIdentifier;//

//        /// <summary> 在程序打包时 预制碰撞网格 </summary>
//        public bool bakeCollisionMeshes;

//        /// <summary> 预定义程序集编译"不安全的"c#代码 </summary>
//        public bool allowUnsafeCode;

//        /// <summary> 开发者姓名 </summary>
//        public string Author;

//        public void byteRead(ByteBuffer data)
//        {
//            if (data == null || data.ToBytes() == null)
//            {
//                NeverSleep = SleepTimeoutTime.NeverSleep;    //特殊处理 值为负数
//                return;
//            }

//            PlayerSettingOrientation = data.readEnum(PlayerSettingOrientation);
//            ScreenFullMode = data.readEnum(ScreenFullMode);
//            RefreshRate = data.readEnum(RefreshRate);
//            NeverSleep = data.readEnum(NeverSleep);

//            ScreenWidth = data.readInt();
//            ScreenHeight = data.readInt();
//            Brightness = data.readInt();
//            AccelerometerFrequency = data.readInt();

//            enableCrashReportAPI = data.readBool();
//            captureSingleScreen = data.readBool();
//            ScreenFull = data.readBool();
//            DeveloperMode = data.readBool();
//            AutorotateToLandscapeLeft = data.readBool();
//            AutorotateToLandscapeRight = data.readBool();
//            AutorotateToPortrait = data.readBool();
//            AutorotateToPortraitUpsideDown = data.readBool();
//            bakeCollisionMeshes = data.readBool();
//            allowUnsafeCode = data.readBool();

//            Author = data.readUTF();
//            applicationIdentifier = data.readUTF();
//        }

//        public ByteBuffer byteWrite(ByteBuffer data)
//        {
//            //enum              
//            data.writeEnum(PlayerSettingOrientation);
//            data.writeEnum(ScreenFullMode);
//            data.writeEnum(RefreshRate);
//            data.writeEnum(NeverSleep);

//            //int
//            data.writeInt(ScreenWidth);
//            data.writeInt(ScreenHeight);
//            data.writeInt(Brightness);
//            data.writeInt(AccelerometerFrequency);

//            //bool
//            data.writeBool(enableCrashReportAPI);
//            data.writeBool(captureSingleScreen);
//            data.writeBool(ScreenFull);
//            data.writeBool(DeveloperMode);
//            data.writeBool(AutorotateToLandscapeLeft);
//            data.writeBool(AutorotateToLandscapeRight);
//            data.writeBool(AutorotateToPortrait);
//            data.writeBool(AutorotateToPortraitUpsideDown);
//            data.writeBool(bakeCollisionMeshes);
//            data.writeBool(allowUnsafeCode);

//            //string
//            data.writeUTF(Author);
//            data.writeUTF(applicationIdentifier);
//            return data;
//        }
//    }
//}