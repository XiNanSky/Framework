/***************************************************
* Copyright(C) 2021 by DefaultCompany              *
* All Rights Reserved By Author lihongliu.         *
* Author:            XiNan                         *
* Email:             1398581458@qq.com             *
* Version:           0.1                           *
* UnityVersion:      2018.4.36f1                   *
* Date:              2021-11-10                    *
* Nowtime:           18:06:10                      *
* Description:                                     *
* History:                                         *
***************************************************/

namespace Framework.Console
{
    using FairyGUI;
    using UnityEngine;

    public class ConsolePanel : MonoBehaviour, IConsoleDisplay
    {
        #region component
        // 背景组件
        private GComponent m_bgCom;
        // 日志列表
        private GList m_list;
        // 功能按钮
        private GButton m_clearBtn;
        private GButton m_collapsedBtn;
        private GButton m_infoBtn;
        private GButton m_warnBtn;
        private GButton m_errorBtn;
        private GButton m_hideBtn;
        private GButton m_settingBtn;

        // 菜单操作按钮
        private GButton m_menuShowBtn;
        private GButton m_menuHideBtn;

        // 过滤组件
        private GTextInput m_filterInput;
        // 命令组件
        private GTextInput m_cmdInput;
        // 命令按钮
        private GButton m_cmdSendBtn;

        // 最小化组件
        private GComponent m_minCom;
        private GTextField m_minInfoCountText;
        private GTextField m_minWarnCountText;
        private GTextField m_minErrorCountText;
        private GTextField m_minFPSText;
        private Controller m_minSystemController;
        private Controller m_windowController;

        // 透明控制器
        private Controller m_tpController;
        private Controller m_tpFliterController;
        private Controller m_tpCmdController;
        private Controller m_tpScrllBarController;
        private Controller m_minTransparentController;

        // 菜单控制器
        private Controller m_menuController;

        #endregion

        /// <summary>
        /// 是否列表保持在最下面
        /// </summary>
        private bool m_listKeepDown = true;

        /// <summary>
        /// 选中列表条目数据
        /// </summary>
        private ConsoleLogEntry selectedData;

        /// <summary>
        /// 最大化
        /// </summary>
        private bool m_maximizeShow;

        /// <summary>
        /// 显示
        /// </summary>
        private bool m_isShow = false;

        private UIPanel uiPanel;
        private GComponent mRoot = null;

        private void Awake()
        {
            Console.Create();
            Console.SetDisplay(this);
        }

        private void Start()
        {
            uiPanel = GetComponent<UIPanel>();
            if (uiPanel == null) return;
            mRoot = uiPanel.ui;
            //mRoot.MakeFullScreen();

            m_isShow = true;
            m_bgCom = mRoot.GetChild("bg").asCom;
            m_windowController = mRoot.GetController("window");
            //if (FastConsole.options.openState == OpenState.Minimize)
            //{
            //    m_maximizeShow = false;
            //    m_windowController.SetSelectedIndex(1);
            //}
            //else if (FastConsole.options.openState == OpenState.Normal)
            //{
            m_maximizeShow = true;
            m_windowController.SetSelectedIndex(0);
            //}

            m_menuController = mRoot.GetController("menu");
            m_menuController.SetSelectedIndex(0);

            m_list = mRoot.GetChild("list").asList;
            m_list.RemoveChildrenToPool();
            m_list.SetVirtual();
            m_list.itemRenderer = ItemRenderer;
            m_list.onClickItem.Set(OckClickItem);
            m_list.onTouchBegin.Set(OnListTouchBegin);
            m_list.scrollPane.onPullUpRelease.Set(OnListPullUpRelease);

            var bw = GRoot.inst.width / 7;

            m_clearBtn = mRoot.GetChild("clear_btn").asButton;
            m_clearBtn.onClick.Set(OckCleanup);
            SetBtnSize(m_clearBtn, bw, 1);

            m_collapsedBtn = mRoot.GetChild("collapse_btn").asButton;
            m_collapsedBtn.onClick.Set(OckCollapse);
            SetBtnSize(m_collapsedBtn, bw, 2);

            m_infoBtn = mRoot.GetChild("info_btn").asButton;
            m_infoBtn.onClick.Set(OckInfo);
            m_infoBtn.selected = true;
            SetBtnSize(m_infoBtn, bw, 3);

            m_warnBtn = mRoot.GetChild("warn_btn").asButton;
            m_warnBtn.onClick.Set(OckWarn);
            m_warnBtn.selected = true;
            SetBtnSize(m_warnBtn, bw, 4);

            m_errorBtn = mRoot.GetChild("error_btn").asButton;
            m_errorBtn.onClick.Set(OckError);
            m_errorBtn.selected = true;
            SetBtnSize(m_errorBtn, bw, 5);

            m_settingBtn = mRoot.GetChild("setting_btn").asButton;
            m_settingBtn.onClick.Set(OckSetting);
            SetBtnSize(m_settingBtn, bw, 6);

            m_hideBtn = mRoot.GetChild("hide_btn").asButton;
            m_hideBtn.onClick.Set(OckClose);
            SetBtnSize(m_hideBtn, bw, 7);

            // show menu
            m_menuShowBtn = mRoot.GetChild("menu_show_btn").asButton;
            m_menuShowBtn.onClick.Set(OckMenuShow);

            // hide menu
            m_menuHideBtn = mRoot.GetChild("menu_hide_btn").asButton;
            m_menuHideBtn.onClick.Set(OckMenuHide);

            // fliter
            var filterCom = mRoot.GetChild("fliter_com").asCom;
            m_filterInput = filterCom.GetChild("input_text").asTextInput;
            m_filterInput.onChanged.Set(OnFilterChange);

            // cmd
            var cmdCom = mRoot.GetChild("cmd_com").asCom;
            m_cmdInput = cmdCom.GetChild("input_text").asTextInput;
            m_cmdInput.onChanged.Set(OnCmdChange);
            m_cmdInput.onKeyDown.Set(OnEnterCmdSend);
            m_cmdSendBtn = cmdCom.GetChild("send_btn").asButton;
            m_cmdSendBtn.onClick.Set(OckCmdSend);
            m_cmdSendBtn.visible = false;

            // 最小化
            m_minCom = mRoot.GetChild("minimize_com").asCom;
            m_minCom.draggable = true;
            m_minCom.dragBounds = new Rect(0, 0, GRoot.inst.width, GRoot.inst.height);
            m_minCom.onClick.Set(OckMinimizeCom);

            m_minTransparentController = m_minCom.GetController("transparent");
            m_minSystemController = m_minCom.GetController("systemInfo");

            m_minInfoCountText = m_minCom.GetChild("info_text").asTextField;
            m_minWarnCountText = m_minCom.GetChild("warn_text").asTextField;
            m_minErrorCountText = m_minCom.GetChild("error_text").asTextField;
            m_minFPSText = m_minCom.GetChild("fps_text").asTextField;

            //if (FastConsole.options.miniLayout == MiniLayout.LEFT_TOP)
            //{
            //    m_minCom.x = 0;
            //    m_minCom.y = 0;
            //}
            //else if (FastConsole.options.miniLayout == MiniLayout.LEFT_BOTTOM)
            //{
            //    m_minCom.x = 0;
            //    m_minCom.y = GRoot.inst.height - m_minCom.height;
            //}
            //else if (FastConsole.options.miniLayout == MiniLayout.RIGHT_TOP)
            //{
            //    m_minCom.x = GRoot.inst.width - m_minCom.width;
            //    m_minCom.y = 0;
            //}
            //else if (FastConsole.options.miniLayout == MiniLayout.RIGHT_BOTTOM)
            //{
            //    m_minCom.x = GRoot.inst.width - m_minCom.width;
            //    m_minCom.y = GRoot.inst.height - m_minCom.height;
            //}


            // 透明模式控制
            m_tpController = mRoot.GetController("transparent");
            m_tpFliterController = filterCom.GetController("transparent");
            m_tpCmdController = cmdCom.GetController("transparent");
            m_tpScrllBarController = m_list.scrollPane.vtScrollBar.GetController("transparent");

            OckClose();
        }

        //protected override void OnRelease()
        //{
        //    Console.SetDisplay(null);
        //}

        public void Refresh()
        {
            if (!m_isShow)
                return;

            // count
            var infoCount = Console.InfoCount.ToString();
            var warnCount = Console.WarnCount.ToString();
            var errorCount = Console.ErrorCount.ToString();

            // 最大化的时候才更新列表显示
            if (m_maximizeShow)
            {
                m_list.numItems = Console.ViewLogs.Count;
                if (m_listKeepDown)
                {
                    m_list.scrollPane.ScrollBottom();
                }

                m_infoBtn.title = infoCount;
                m_warnBtn.title = warnCount;
                m_errorBtn.title = errorCount;
            }

            m_minInfoCountText.text = infoCount;
            m_minWarnCountText.text = warnCount;
            m_minErrorCountText.text = errorCount;
        }

        public void RefreshSetting()
        {
            if (!m_isShow)
                return;

            //// 透明设置
            //if (FastConsole.options.transparent)
            //{
            //    m_tpController.SetSelectedIndex(1);
            //    m_tpFliterController.SetSelectedIndex(1);
            //    m_tpCmdController.SetSelectedIndex(1);
            //    m_tpScrllBarController.SetSelectedIndex(1);
            //    m_minTransparentController.SetSelectedIndex(1);
            //}
            //else
            {
                m_tpController.SetSelectedIndex(0);
                m_tpFliterController.SetSelectedIndex(0);
                m_tpCmdController.SetSelectedIndex(0);
                m_tpScrllBarController.SetSelectedIndex(0);
                m_minTransparentController.SetSelectedIndex(0);
            }

            // 穿透设置
            var touchable = true;// !FastConsole.options.touchEnable;
            m_list.touchable = touchable;
            m_bgCom.touchable = touchable;

            Refresh();
        }

        /// <summary>
        /// 设置功能按钮Size
        /// </summary>
        /// <param name="button"></param>
        /// <param name="width"></param>
        /// <param name="index"></param>
        private void SetBtnSize(GButton button, float width, int index)
        {
            button.width = width;
            button.x = width * (index - 1);
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            selectedData = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="obj"></param>
        private void ItemRenderer(int index, GObject obj)
        {
            var item = obj.asCom;
            var data = Console.ViewLogs[index];

            var colorController = item.GetController("color");
            var typeController = item.GetController("type");
            var groupController = item.GetController("group");
            var selectedController = item.GetController("selected");
            var transparentController = item.GetController("transparent");

            var contentText = item.GetChild("content_text").asRichTextField;
            var countText = item.GetChild("count_text").asTextField;

            var copyBtn = item.GetChild("btn_copy").asButton;

            // type
            if (data.LogType == LogType.Log)
                typeController.SetSelectedIndex(0);
            else if (data.LogType == LogType.Warning)
                typeController.SetSelectedIndex(1);
            else
                typeController.SetSelectedIndex(2);

            // color
            if (index % 2 == 0)
                colorController.SetSelectedIndex(0);
            else
                colorController.SetSelectedIndex(1);

            // group
            if (data.LogCount > 1)
                groupController.SetSelectedIndex(0);
            else
                groupController.SetSelectedIndex(1);

            // selected
            if (data.Selected)
                selectedController.SetSelectedIndex(1);
            else
                selectedController.SetSelectedIndex(0);

            // transparent
            //if (FastConsole.options.transparent)
            //    transparentController.SetSelectedIndex(1);
            //else
            transparentController.SetSelectedIndex(0);

            // count
            if (data.LogCount > 999) countText.text = "999+";
            else countText.text = data.LogCount.ToString();

            if (/*!FastConsole.options.detailEnable &&*/ data.Selected)
            {
                contentText.autoSize = AutoSizeType.Height;
                contentText.text = data.ToString();

                item.height = contentText.textHeight;

                copyBtn.visible = true;
                copyBtn.data = data;
                copyBtn.onClick.Set(OckClickItemCopy);
            }
            else
            {
                contentText.autoSize = AutoSizeType.None;
                contentText.text = data.LogContent;
                contentText.height = contentText.initHeight;

                copyBtn.visible = false;

                item.height = item.initHeight;
            }

            // 字体颜色设置
            //contentText.color = FastConsole.options.GetColor();

            item.data = data;
        }

        private void OckClickItem(EventContext context)
        {
            GComponent item = context.data as GComponent;
            ConsoleLogEntry data = item.data as ConsoleLogEntry;

            //// 显示日志详情界面
            //if (FastConsole.options.detailEnable)
            //{
            //    FastConsole.inst.detailUI.Show();
            //    FastConsole.inst.detailUI.Refresh(data);
            //}

            // 选中状态设置
            if (selectedData != null)
            {
                if (selectedData == data) selectedData.Selected = !selectedData.Selected;
                else
                {
                    selectedData.Selected = false;
                    data.Selected = true;
                    selectedData = data;
                }
            }
            else
            {
                selectedData = data;
                selectedData.Selected = true;
            }

            Refresh();

            m_listKeepDown = false;
        }

        private void OckClickItemCopy(EventContext context)
        {
            // GComponent item = context.sender as GComponent;
            // LogEntry data = item.data as LogEntry;
            // TODO
        }

        private void OnListTouchBegin() { m_listKeepDown = false; }
        private void OnListPullUpRelease() { m_listKeepDown = true; }

        private void OnFilterChange()
        {
            Reset();
            //FastConsole.inst.searchTarget = m_filterInput.text;
        }

        private void OckCleanup()
        {
            Reset();
            Console.Cleanup();
        }

        private void OckCollapse()
        {
            Reset();
            //FastConsole.inst.collapsed = m_collapsedBtn.selected;
        }

        private void OckInfo()
        {
            Reset();
            Console.InfoSelected = m_infoBtn.selected;
        }

        private void OckWarn()
        {
            Reset();
            Console.WarnSelected = m_warnBtn.selected;
        }

        private void OckError()
        {
            Reset();
            Console.ErrorSelected = m_errorBtn.selected;
        }

        private void OckSetting()
        {
            //FastConsole.inst.settingUI.Show();
        }

        private void OckMenuShow() { m_menuController.SetSelectedIndex(0); }
        private void OckMenuHide() { m_menuController.SetSelectedIndex(1); }

        private void OckMinimizeCom()
        {
            m_windowController.SetSelectedIndex(1);
            m_maximizeShow = true;
            Refresh();
        }

        private void OnCmdChange()
        {
            string cmd = m_cmdInput.text;
            if (string.IsNullOrEmpty(cmd)) m_cmdSendBtn.visible = false;
            else m_cmdSendBtn.visible = true;
        }

        private void OnEnterCmdSend()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                OckCmdSend();
        }

        private void OckCmdSend()
        {
            Console.RunCommand(m_cmdInput.text);
            m_cmdInput.text = "";
            m_cmdSendBtn.visible = false;
        }

        private void OckClose()
        {
            m_maximizeShow = false;
            m_windowController.SetSelectedIndex(0);
        }

    }
}