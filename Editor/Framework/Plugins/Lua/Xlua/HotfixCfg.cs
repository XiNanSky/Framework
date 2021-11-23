/* * * * * * * * * * * * * * * * * * * * * * * *
* Copyright(C) 2021 by Tianyou Games 
* All rights reserved. 
* FileName:         Xlua 
* Author:           XiNan 
* Version:          0.4 
* UnityVersion:     2019.4.10f1 
* Date:             2021-07-06
* Time:             20:00:58
* E-Mail:           1398581458@qq.com
* Description:        
* History:          
* * * * * * * * * * * * * * * * * * * * * * * * */

namespace Framework.XLua
{
#if XLua
    using global::XLua;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.Networking;

    public static class HotfixCfg
    {
        [Hotfix]
        public static List<Type> by_property
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Framework");
                list.Add("Framework.Kit");
                list.Add("Framework.UI");
                list.Add("Framework.Net");
                list.Add("Framework.Manager");
                list.Add("Framework.Lua");
                list.Add("Framework.Extend");
                list.Add("Framework.Util");
                list.Add("Framework.XML");
                list.Add("Framework.WX");

                list.Add("Function");
                list.Add("Function.ResUpdate");
                List<Type> types = new List<Type>(Assembly.Load("Assembly-CSharp").GetTypes());
                types.RemoveAll((t) => { return !list.Contains(t.Namespace); });
                return types.ToList();
            }

        }



        //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>()
        {
            typeof (Action),
            typeof (Func<double, double, double>),
            typeof (Action<string>),
            typeof (Action<string[]>),
            typeof (Action<double>),
            typeof (Action<object>),
            typeof (Action<bool>),
            typeof (Action<int>),
            typeof (Action<object, object>),
            typeof (Action<byte[]>),
            typeof (Action<UnityEngine.EventSystems.PointerEventData>),
            typeof (UnityEngine.Events.UnityAction),
            typeof (System.Collections.IEnumerator),
        };

        //黑名单
        [BlackList]
        public static List<List<string>> BlackList = new List<List<string>>()
        {
            new List<string>() {"UnityEngine.WWW", "movie"},
#if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
#endif

            new List<string>() {"UnityEngine.Texture2D", "alphaIsTransparency"},
            new List<string>() {"UnityEngine.Security", "GetChainOfTrustValue"},
            new List<string>() {"UnityEngine.CanvasRenderer", "onRequestRebuild"},
            new List<string>() {"UnityEngine.Light", "areaSize"},
            new List<string>() {"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},

#if !UNITY_WEBPLAYER
            new List<string>() {"UnityEngine.Application", "ExternalEval"},
#endif

            new List<string>() {"UnityEngine.Light", "lightmapBakeType"},
            new List<string>() {"UnityEngine.Light", "shadowRadius"},
            new List<string>() {"UnityEngine.Light", "SetLightDirty"},
            new List<string>() {"UnityEngine.Light", "shadowAngle"},
            new List<string>() {"UnityEngine.Light", "shadowAngle"},
            new List<string>() {"UnityEngine.WWW", "MovieTexture"},
            new List<string>() {"UnityEngine.WWW", "GetMovieTexture"},

            new List<string>() {"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
            new List<string>() {"UnityEngine.Component", "networkView"}, //4.6.2 not support
            new List<string>()
            {
                "System.IO.FileInfo",
                "GetAccessControl",
                "System.Security.AccessControl.AccessControlSections"
            },
            new List<string>()
            {
                "System.IO.FileInfo",
                "SetAccessControl",
                "System.Security.AccessControl.FileSecurity"
            },
            new List<string>()
            {
                "System.IO.DirectoryInfo",
                "GetAccessControl",
                "System.Security.AccessControl.AccessControlSections"
            },
            new List<string>()
            {
                "System.IO.DirectoryInfo",
                "SetAccessControl",
                "System.Security.AccessControl.DirectorySecurity"
            },
            new List<string>()
            {
                "System.IO.DirectoryInfo",
                "CreateSubdirectory",
                "System.String",
                "System.Security.AccessControl.DirectorySecurity"
            },
            new List<string>()
            {
                "System.IO.DirectoryInfo",
                "Create",
                "System.Security.AccessControl.DirectorySecurity"
            },
            new List<string>()
            {
                "UnityEngine.MonoBehaviour",
                "runInEditMode"
            }
        };

        //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>()
        {
            typeof (System.Object),
            typeof (UnityEngine.Object),
            typeof (Vector2),
            typeof (Vector3),
            typeof (Vector4),
            typeof (Quaternion),
            typeof (Color),
            typeof (Ray),
            typeof (Bounds),
            typeof (Ray2D),
            typeof (Time),
            typeof (GameObject),
            typeof (Component),
            typeof (Behaviour),
            typeof (UnityEngine.Transform),
            typeof (Resources),
            typeof (TextAsset),
            typeof (Keyframe),
            typeof (AnimationCurve),
            typeof (AnimationClip),
            typeof (MonoBehaviour),
            typeof (ParticleSystem),
            typeof (SkinnedMeshRenderer),
            typeof (Renderer),
            //typeof (WWW),
            typeof (UnityWebRequest),
            typeof (WaitForSeconds),
            // typeof(Light),
            typeof (Mathf),
            typeof (List<int>),
            typeof (Action<string>),
            typeof (Action<string[]>),
            typeof (Debug),

            typeof(DG.Tweening.AutoPlay),
            typeof(DG.Tweening.AxisConstraint),
            typeof(DG.Tweening.Ease),
            typeof(DG.Tweening.LogBehaviour),
            typeof(DG.Tweening.LoopType),
            typeof(DG.Tweening.PathMode),
            typeof(DG.Tweening.PathType),
            typeof(DG.Tweening.RotateMode),
            typeof(DG.Tweening.ScrambleMode),
            typeof(DG.Tweening.TweenType),
            typeof(DG.Tweening.UpdateType),

            //typeof(DG.Tweening.DOTween),
            //typeof(DG.Tweening.DOVirtual),
            //typeof(DG.Tweening.EaseFactory),
            //typeof(DG.Tweening.Tweener),
            //typeof(DG.Tweening.Tween),
            //typeof(DG.Tweening.Sequence),
            //typeof(DG.Tweening.TweenParams),
            //typeof(DG.Tweening.Core.ABSSequentiable),

            //typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),

            //typeof(DG.Tweening.TweenCallback),
            //typeof(DG.Tweening.TweenExtensions),
            //typeof(DG.Tweening.TweenSettingsExtensions),
            //typeof(DG.Tweening.ShortcutExtensions),
        };

    }
#endif
}