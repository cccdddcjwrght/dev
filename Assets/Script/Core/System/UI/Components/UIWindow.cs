using Unity.Entities;
using FairyGUI;
using System;
using log4net;
using UnityEngine;
using System.Collections.Generic;

namespace SGame.UI
{
    // UI组件, 使用UI组件好处在于 支持对象在没创建完成的时候进行销毁
    public class UIWindow : IComponentData
    {
        public string name
        {
            get;
            set;
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
        
        public bool isReadyShowed           { get { return m_isReadyShowed && isShowing; } }
        
        public bool isHiding                { get { return m_isHiding; } }
        
        // 显示版本
        public int screenSizeVer = 0;

        public FairyGUI.FitScreen fitScreen;

        // 显示结束事件
        public Action<FairyWindow>  onShownFinish;

        private static ILog log = LogManager.GetLogger("UI");

        public void OnFrameUpdate(double deltaTime)
        {
            if (screenSizeVer != StageCamera.screenSizeVer)
                HandleScreenSizeChanged();
            
            //if (_luaUpdate != null)
            //    _luaUpdate.Action(deltaTime);//.Call(deltaTime);
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
            base.DoShowAnimation();
        }

        /// <summary>
        /// 隐藏UI时调用动画
        /// </summary>
        protected override void DoHideAnimation()
        {
            m_isHiding = true;
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
            


        }


        override protected void OnShown()
        {
            log.Debug("UI OnShow=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
            m_isReadyShowed = true;
            m_isDelayClose = false;
            
            base.OnShown();

            onShownFinish?.Invoke(this);
            onShownFinish = null;
        }

        protected override void OnHide()
        {
            log.Debug("UI OnHide=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
            m_isReadyShowed = false;
            m_isHiding = false;
            base.OnHide();
            
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

        public bool Initalize()
        {
            return true;
        }

        public bool isClosed()
        {
            return World.DefaultGameObjectInjectionWorld.EntityManager.HasComponent<UIClosed>(uiEntity);
        }

        public void Close()
        {
            CleanTween();
            m_isDelayClose = true;
            Hide();
        }

        public void CloseImmediately()
        {
            World.DefaultGameObjectInjectionWorld.GetExistingSystem<UISystem>().CloseUI(this.uiEntity);
        }

        public void OnClose()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            
            /*
            if (_luaUpdate != null)
            {
                _luaUpdate.Dispose();
                _luaUpdate = null;
            }

            if (_luaLogic != null)
            {
                _luaLogic.Dispose();
                _luaLogic = null;
            }
            
            if (_luaDoShowAnimation != null)
            {
                _luaDoShowAnimation.Dispose();
                _luaDoShowAnimation = null;
            }

            if (_luaDoHideAnimation != null)
            {
                _luaDoHideAnimation.Dispose();
                _luaDoHideAnimation = null;
            }
            */
        }
        
        public void HandleScreenSizeChanged()
        {
           // if (!Application.isPlaying)
           //     DisplayObject.hideFlags = HideFlags.DontSaveInEditor;

            screenSizeVer = StageCamera.screenSizeVer;
            float width = GRoot.inst.width;
            float height = GRoot.inst.height;

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

    // UI销毁
    public struct UIDestroy : IComponentData
    {
    }

    // UI已被关闭
    public struct UIClosed : IComponentData
    {
    }
    
    // UI 关闭事件
    public struct UICloseEvent : IComponentData
    {
        
    }
}