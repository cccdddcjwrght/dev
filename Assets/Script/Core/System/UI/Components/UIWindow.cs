using Unity.Entities;
using FairyGUI;
using System;
using log4net;
using UnityEngine;

namespace SGame.UI
{
    // UI组件, 使用UI组件好处在于 支持对象在没创建完成的时候进行销毁
    public class UIWindow : IComponentData
    {
        public string name
        {
            get { return Value != null ? Value.uiname : null; }
        }

        // 创建的Windoww
        public FairyWindow Value;

        // 自己的Entity
        public Entity entity;
        
        // Fairygui 的包引用
        public UIPackageRequest       uiPackage;
        
        // 创建的原始对象
        public GObject                gObject;

        public void Dispose()
        {
            entity = Entity.Null;
            if (Value != null)
            {
                if (Value.isDisposed == false)
                {
                    Value.Dispose();
                }

                Value = null;
            }

            if (uiPackage != null)
            {
                uiPackage.Release();
                uiPackage = null;
            }
        }

        // 是否已经准备好显示
        public bool isReadlyShow
        {
            get
            {
                if (Value == null)
                    return false;

                return Value.isReadyShowed;
            }
        }
    }

    public class FairyWindow : FairyGUI.Window
    {
        private const string SHOW_ANIM_NAME     = "doshow";
        private const string HIDE_ANIM_NAME     = "dohide";
        
        public Entity uiEntity;

        public string uiname;//                 { get { return m_uiConfig.IsValid() ? m_uiConfig.Name : null; } }

        // UI 是否已经初始化完毕
        public  bool    isReadly             {  get { return _isReadly; } }
        private bool    _isReadly            = false;

        // 是否延迟关闭
        private bool                        m_isDelayClose = false;

        // 是否已经调用过ONSHOWN
        private bool                        m_isReadyShowed = false;

        // 是否正在隐藏
        private bool                        m_isHiding = false;

        private bool m_isClosed = false;
        
        public bool isReadyShowed           { get { return m_isReadyShowed && isShowing; } }
        
        public bool isHiding                { get { return m_isHiding; } }
        
        public bool isClosed { get { return m_isClosed; } }
        
        // 显示版本
        public int screenSizeVer = 0;

        public FairyGUI.FitScreen fitScreen;

        // 显示结束事件
        public Action<FairyWindow>  onShownFinish;

        private IUIScript m_uiScript;
        private UIContext m_context;

        private static ILog log = LogManager.GetLogger("UI");

        public void OnFrameUpdate(double deltaTime)
        {
            if (screenSizeVer != StageCamera.screenSizeVer)
                HandleScreenSizeChanged();
            
            m_context.onUpdate?.Invoke(m_context);
        }

        // 调用函数结束通知
        public void FinishAnimation(bool isShow)
        {
            if (isShow)
            {
                OnShown();
            }
            else
            {
                HideImmediately();
            }
        }

        /// <summary>
        /// 显示UI时调用动画
        /// </summary>
        protected override void DoShowAnimation()
        {
            if (m_context.doShowAnimation != null)
                m_context.doShowAnimation(m_context);
            else
                base.DoShowAnimation();
        }

        /// <summary>
        /// 隐藏UI时调用动画
        /// </summary>
        protected override void DoHideAnimation()
        {
            m_isHiding = true;

            if (m_context.doHideAnimation != null)
                m_context.doHideAnimation(m_context);
            else
                base.DoHideAnimation();
        }
        
        
        //protected override void Do

        protected override void OnInit()
        {
            if (isDisposed)
                return;

            base.OnInit();

            m_isReadyShowed         = false;
            m_isDelayClose          = false;
            _isReadly               = true;
            m_isHiding              = false;
            container.renderMode    = RenderMode.ScreenSpaceOverlay;

            if (true)//m_uiConfig.Fullscreen != 0)
            {
                fitScreen = FitScreen.FitSize;
                MakeFullScreen();
            }
            else
            {
                fitScreen = FitScreen.None;
                x = (GRoot.inst.width - width) / 2;
                y = (GRoot.inst.height - height) / 2;
            }
            

            if (m_uiScript != null)
                m_uiScript.OnInit(m_context);

        }

        override protected void OnShown()
        {
            log.Debug("UI OnShow=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
            m_isReadyShowed = true;
            m_isDelayClose = false;
            
            base.OnShown();

            m_context.onShown?.Invoke(m_context);
        }

        protected override void OnHide()
        {
            log.Debug("UI OnHide=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
            m_isReadyShowed = false;
            m_isHiding = false;
            base.OnHide();
            m_context.onHide?.Invoke(m_context);
            
            if (m_isDelayClose == true)
            {
                m_isDelayClose = false;
                CloseImmediately();
            }
        }

        public void Reopen()
        {
            // 清空之前的动画
            log.Debug("Reopen=" + uiname);
            CleanTween();
            if (isShowing == true)
                HideImmediately();
            this.visible = true;
            this.contentPane.visible = true;
        }

        void CleanTween()
        {
            foreach (var trans in contentPane.Transitions)
            {
                trans.Stop(true, false);
            }
            GTween.Kill(contentPane, false);
        }

        public bool Initalize(IUIScript scirpt, UIContext context)
        {
            m_uiScript = scirpt;
            m_context = context;
            return true;
        }

        public void Close()
        {
            CleanTween();
            m_isDelayClose = true;
            Hide();
        }

        public void CloseImmediately()
        {
            m_isClosed = true;
            m_context.onClose?.Invoke(m_context);
        }

        public void OnClose()
        {
            
        }

        public void HandleScreenSizeChanged()
        {
            screenSizeVer = StageCamera.screenSizeVer;
            float width   = GRoot.inst.width;
            float height  = GRoot.inst.height;

            var ui = contentPane;
            if (ui != null)
            {
                switch (fitScreen)
                {
                    case FitScreen.FitSize:
                        MakeFullScreen();
                        SetXY(0,0,true);
                        break;

                    case FitScreen.FitWidthAndSetMiddle:
                        SetSize(width, ui.sourceHeight);
                        SetXY(0, (int)((height - ui.sourceHeight) / 2), true);
                        break;

                    case FitScreen.FitHeightAndSetCenter:
                        SetSize(ui.sourceWidth, height);
                        SetXY((int)((width - ui.sourceWidth) / 2), 0, true);
                        break;
                }

            }
        }
    }

}