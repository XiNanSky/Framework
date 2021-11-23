/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-22                    *
* Nowtime:           17:27:14                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Threading.Tasks;
    using Framework.UI;
    using System.Text;

    [AttributeUsage(AttributeTargets.Field)]
    public class UILayerAttribute : Attribute
    {
        /// <summary>
        /// 是否需要在Root下创建layer层级
        /// </summary>
        public bool CreateLayerFlag;
        public UILayerAttribute(bool flag)
        {
            CreateLayerFlag = flag;
        }
    }

    public enum UILayer
    {
        [UILayer(false)]
        None,

        /// <summary>
        /// 场景UI，如：点击建筑查看建筑信息---一般置于场景之上，界面UI之下
        /// </summary>
        [UILayer(true)]
        Scene,

        /// <summary>
        /// 背景UI，如：主界面---一般情况下用户不能主动关闭，永远处于其它UI的最底层
        /// </summary>
        [UILayer(true)]
        Backgroud,

        /// <summary>
        /// 普通UI，一级、二级、三级等窗口---一般由用户点击打开的多级窗口
        /// </summary>
        [UILayer(true)]
        Normal,

        /// <summary>
        /// 弹出UI,道具获取、战力变化，GoodsTips这类是例外，不在这层，属于最上层
        /// </summary>
        [UILayer(true)]
        Popup,

        /// <summary>
        /// 提示UI，如：错误弹窗，网络连接弹窗等
        /// </summary>
        [UILayer(true)]
        Tip,

        /// <summary>
        /// 顶层UI，如：场景加载
        /// </summary>
        [UILayer(true)]
        Top,

        /// <summary>
        /// 信息UI---如：跑马灯、广播等---一般永远置于用户打开窗口顶层
        /// </summary>
        [UILayer(true)]
        Info,
    }

    /// <summary>
    /// UI层级Node链表？
    /// </summary>
    public class UILayerNode
    {
        public object View;
        public GameObject CacheGameObject;
        public int PlaneDistance;
        public int OrderInLayer;
    }

    /// <summary>
    /// UI管理系统
    /// </summary>
    public class UISystem
    {
        public static string ConfigPath = "{0}";
        private static IUIProxy mGUIProxy = null;
        private static readonly Dictionary<UILayer, UILayerNode> mLayers = new Dictionary<UILayer, UILayerNode>();
        private static readonly Dictionary<string, UIConfig> mCacheLoadedUIConfigs = new Dictionary<string, UIConfig>();
        private static readonly Dictionary<string, IUIView> mCacheOpenedUIViews = new Dictionary<string, IUIView>();
        private static IUIView mTopNormalLayerUIView = null;
        private static readonly List<IUIView> mClosedNormalLayerUIViews = new List<IUIView>();
        private static Action mUpdateHandle = delegate { };
        private static float lastSW, lastSH;

        private static Action<string> mOpenAction = delegate { };
        private static Action<string> mCloseAction = delegate { };

        public static async Task<int> Initialize<T>(params object[] args) where T : class, IUIProxy, new()
        {
            mGUIProxy = Activator.CreateInstance(typeof(T), args) as IUIProxy;
            await mGUIProxy.HandleInitialize();

            int planeDistance = 1000;
            int orderInLayer = 0;
            int index = 0;
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                UILayerAttribute ret = null;//AttributeUtils.GetToEnum<UILayerAttribute>(layer)
                if (ret == null)
                    continue;
                if (!ret.CreateLayerFlag)
                    continue;
                planeDistance -= index * 100;
                orderInLayer += index * 1000;
                var node = new UILayerNode
                {
                    PlaneDistance = planeDistance,
                    OrderInLayer = orderInLayer,
                };
                mGUIProxy.OnCreateUILayerNode(layer, node);
                mLayers.Add(layer, node);
                index++;
            }
            lastSW = Screen.width;
            lastSH = Screen.height;
            return 1;
        }

        public static void Update()
        {
            ChangeScreenSize();
            mUpdateHandle.Invoke();
        }

        public static void Release()
        {
            mUpdateHandle = delegate { };
        }

        public static void AddOpenAction(Action<string> action)
        {
            mOpenAction += action;
        }

        public static void AddCloseAction(Action<string> action)
        {
            mCloseAction += action;
        }

        private static void AddToLayer(IUIView uiView)
        {
            mLayers.TryGetValue(uiView.CacheUIConfig.UILayer, out UILayerNode uILayerNode);
            mGUIProxy.HandleAddToUILayerNode(uILayerNode, uiView);
        }

        public static UIConfig LoadUIConfig(string name)
        {
            if (!mCacheLoadedUIConfigs.TryGetValue(name, out UIConfig value))
            {
                var path = ObjPoolKit.New<StringBuilder>();
                path.Clear();
                //value = AssetManager.LoadAsset<UIConfig>(path.AppendFormat(ConfigPath, name).ToString());
                // value = ResourceManager.LoadAsset<UIConfig>(path.AppendFormat(ConfigPath, name).ToString(), true).Result;
                // if (value != null)
                // {
                //     if (mCacheLoadedUIConfigs.ContainsKey(name))
                //         ResourceManager.ReleaseAsset(value);
                //     else
                //         mCacheLoadedUIConfigs.Add(name, value);
                // }
                ObjPoolKit.Release(path);
            }
            return value;
        }

        private static void ChangeScreenSize()
        {
            if (lastSW != Screen.width || lastSH != Screen.height)
            {
                lastSW = Screen.width;
                lastSH = Screen.height;
                if (mGUIProxy == null)
                    return;
                mGUIProxy.HandleChangeScreenSize(mLayers);
                mGUIProxy.HandleChangeScreenSize(mCacheOpenedUIViews);
            }
        }

        //private static void OnOpen(IUIView uiView)
        //{
        //    if (uiView == null)
        //        return;
        //    switch (uiView.CacheUIConfig.UILayer)
        //    {
        //        case UILayer.NormalLayer:
        //            {
        //                var current = mTopNormalLayerUIView;
        //                mTopNormalLayerUIView = uiView;
        //                if (current != null)
        //                {
        //                    Close(current.CacheUIConfig.name, false);
        //                }
        //            }
        //            break;
        //    }

        //    uiView.CacheGameObject.SetLayer(LayerMask.NameToLayer("UI"), 1);
        //    uiView.Touchable = true;
        //    uiView.Visible = true;
        //    mGUIProxy.HandleOpen(uiView);
        //    uiView.Open();

        //    if (mLayers.TryGetValue(UILayer.BackgroudLayer, out UILayerNode uiLayerNode))
        //    {
        //        if (mTopNormalLayerUIView != null)
        //        {
        //            uiLayerNode.CacheGameObject.SetLayer(LayerMask.NameToLayer("RenderOff"), 1);
        //            mGUIProxy.HandleUILayerNodeTouchable(uiLayerNode, false);
        //        }
        //        else
        //        {
        //            uiLayerNode.CacheGameObject.SetLayer(LayerMask.NameToLayer("UI"), 1);
        //            mGUIProxy.HandleUILayerNodeTouchable(uiLayerNode, true);
        //        }
        //    }
        //}

        public static IUIView CreateUI(string name)
        {
            var uiConfig = LoadUIConfig(name);
            if (uiConfig == null)
                return null;
            return mGUIProxy.CreateUIView(uiConfig);
        }

        public static IUIView Open(string name)
        {
            var uiConfig = LoadUIConfig(name);
            if (uiConfig == null)
                return null;

            if (!mCacheOpenedUIViews.TryGetValue(name, out IUIView uiView))
            {
                uiView = mGUIProxy.CreateUIView(uiConfig);
                if (uiView != null)
                {
                    mUpdateHandle += uiView.Update;
                    mCacheOpenedUIViews.Add(name, uiView);
                }
            }
            else
            {
                if (uiView.Visible)
                    return uiView;
            }

            AddToLayer(uiView);
            //OnOpen(uiView);

            switch (uiView.CacheUIConfig.UILayer)
            {
                case UILayer.Normal:
                    {
                        var current = mTopNormalLayerUIView;
                        mTopNormalLayerUIView = uiView;
                        if (current != null)
                        {
                            Close(current.CacheUIConfig.name, false, true);
                        }
                    }
                    break;
            }
            uiView.CacheGameObject.SetLayerAll(LayerMask.NameToLayer("UI"));
            uiView.Touchable = true;
            uiView.Visible = true;
            mGUIProxy.HandleOpen(uiView);
            uiView.Open();
            mOpenAction.Invoke(name);
            if (mLayers.TryGetValue(UILayer.Backgroud, out UILayerNode uiLayerNode))
            {
                if (mTopNormalLayerUIView != null)
                {
                    uiLayerNode.CacheGameObject.SetLayerAll(LayerMask.NameToLayer("RenderOff"));
                    mGUIProxy.HandleUILayerNodeTouchable(uiLayerNode, false);
                }
                else
                {
                    uiLayerNode.CacheGameObject.SetLayerAll(LayerMask.NameToLayer("UI"));
                    mGUIProxy.HandleUILayerNodeTouchable(uiLayerNode, true);
                }
            }
            return uiView;
        }

        public static void Close(string name, bool destroy, bool flag = false)
        {
            var config = LoadUIConfig(name);
            if (config == null)
                return;
            if (!mCacheOpenedUIViews.TryGetValue(name, out IUIView uiView))
            {
                return;
            }
            if (uiView.Visible)
            {
                //OnClose(uiView);

                uiView.CacheGameObject.SetLayer(LayerMask.NameToLayer("RenderOff"));
                uiView.Touchable = false;
                uiView.CloseTimestamp = Time.realtimeSinceStartup;
                uiView.Visible = false;
                uiView.Close();
                mCloseAction.Invoke(name);
                switch (uiView.CacheUIConfig.UILayer)
                {
                    case UILayer.Normal:
                        {
                            if (mTopNormalLayerUIView == uiView)
                            {
                                mTopNormalLayerUIView = null;
                                if (mClosedNormalLayerUIViews.Count > 0)
                                {
                                    Open(mClosedNormalLayerUIViews[mClosedNormalLayerUIViews.Count - 1].CacheUIConfig.name);
                                    mClosedNormalLayerUIViews.RemoveAt(mClosedNormalLayerUIViews.Count - 1);
                                }
                            }
                            else
                            {
                                bool ret = true;
                                for (int i = 0; i < mClosedNormalLayerUIViews.Count; ++i)
                                {
                                    if (mClosedNormalLayerUIViews[i] == uiView)
                                    {
                                        ret = false;
                                        break;
                                    }
                                }
                                if (ret && flag)
                                    mClosedNormalLayerUIViews.Add(uiView);
                            }
                        }
                        break;
                }

                if (mLayers.TryGetValue(UILayer.Backgroud, out UILayerNode layer))
                {
                    if (mTopNormalLayerUIView != null)
                    {
                        layer.CacheGameObject.SetLayerAll(LayerMask.NameToLayer("RenderOff"));
                        mGUIProxy.HandleUILayerNodeTouchable(layer, false);
                    }
                    else
                    {
                        layer.CacheGameObject.SetLayerAll(LayerMask.NameToLayer("UI"));
                        mGUIProxy.HandleUILayerNodeTouchable(layer, true);
                    }
                }
            }

            mGUIProxy.HandleClose(uiView);
            if (destroy)
            {
                mClosedNormalLayerUIViews.Remove(uiView);
                mUpdateHandle -= uiView.Update;
                mLayers.TryGetValue(config.UILayer, out UILayerNode layer);
                mGUIProxy.HandleDestroy(layer, uiView);
                mCacheOpenedUIViews.Remove(name);
            }
        }

        public static IUIView Find(string name)
        {
            mCacheOpenedUIViews.TryGetValue(name, out IUIView uiView);
            return uiView;
        }

        public static IUIView Attach(IUIView root, string node, string name)
        {
            var config = LoadUIConfig(name);
            if (config == null)
                return null;
            return mGUIProxy.HandleAttachUIView(root, node, config);
        }

        public static IUIView AddChild(IUIView root, object node, string name)
        {
            var config = LoadUIConfig(name);
            if (config == null)
                return null;
            return mGUIProxy.HandleAddChild(root, node, config);
        }

        public static void CloseAll(bool destroy)
        {
            foreach (var item in mCacheOpenedUIViews)
            {
                Close(item.Key, destroy);
            }
        }

        public static void Clear()
        {
            CloseAll(true);
            mCacheOpenedUIViews.Clear();
        }

        public static void CloseByCondition(bool destroy, Func<string, bool> func)
        {
            if (func == null)
                return;
            foreach (var item in mCacheOpenedUIViews)
            {
                if (!func.Invoke(item.Key))
                    continue;
                Close(item.Key, destroy);
            }
        }

        public static void CloseLayeUI(UILayer layer)
        {

        }
    }
}
