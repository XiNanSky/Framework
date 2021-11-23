/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-22                    *
* Nowtime:           17:35:13                      *
* Description:       UI接口                         *
* History:                                         *
***************************************************/

namespace Framework.UI
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;


    /// <summary>
    /// UI代理器
    /// </summary>
    public interface IUIView
    {
        UIConfig CacheUIConfig { get; }
        bool Visible { get; set; }
        bool Touchable { get; set; }
        float CloseTimestamp { get; set; }
        GameObject CacheGameObject { get; }
        void Initialize();
        void Release();
        void Update();
        void Open();
        void Close();
    }

    /// <summary>
    /// UI代理器
    /// </summary>
    public interface IUIProxy
    {
        void OnCreateUILayerNode(UILayer layer, UILayerNode node);
        Task<int> HandleInitialize();
        void HandleChangeScreenSize(in Dictionary<UILayer, UILayerNode> layers);
        void HandleChangeScreenSize(in Dictionary<string, IUIView> data);
        IUIView CreateUIView(UIConfig uiConfig);
        IUIView HandleAttachUIView(IUIView rootUIView, string node, UIConfig uiConfig);
        IUIView HandleAddChild(IUIView root, object node, UIConfig uiConfig);
        void HandleUILayerNodeTouchable(UILayerNode uiLayerNode, bool state);
        void HandleAddToUILayerNode(UILayerNode uILayerNode, IUIView uiView);
        void HandleOpen(IUIView uiView);
        void HandleClose(IUIView uiView);
        void HandleDestroy(UILayerNode layer, IUIView uiView);
        bool HandleIsNull(object obj);
    }

}
