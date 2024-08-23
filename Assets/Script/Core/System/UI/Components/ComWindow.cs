using System.Collections;
using System.Collections.Generic;
using SGame.UI;
using UnityEngine;
using FairyGUI;
namespace SGame.UI
{
    /// <summary>
    /// 简单UI, 用于处理HUD等简单的UI, 但必须要有父节点
    /// </summary>
    public class ComWindow : IBaseWindow
    {
        private int         m_configID;
        private GComponent  m_gcomponet;
        private GComponent  m_parent;
        private UIContext   m_context;
        private bool        m_closed;
        private PoolID      m_poolID;
        private IUIScript   m_scirpt;
        private bool        m_isInit = false;
        private bool        m_isDisposed = false;

        public ComWindow(int configID, GComponent component)
        {
            m_gcomponet = component;
            m_configID = configID;
        }

        public void SetParent(GComponent parent)
        {
            m_parent = parent;
        }

        public void SetPoolID(PoolID poolID)
        {
            m_poolID = poolID;
        }

        public UI_TYPE type => UI_TYPE.COM_WINDOW;

        public GComponent content => m_gcomponet;

        public void SetComponent(GComponent component) => m_gcomponet = component;

        public PoolID poolID => m_poolID;
        
        /// <summary>
        /// UI名称
        /// </summary>
        public string uiname { get; set; }

        /// <summary>
        /// 配置ID
        /// </summary>
        public int configID => m_configID;

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            if (!isShowing)
            {
                m_parent.AddChild(m_gcomponet);
                m_gcomponet.visible = true;

                if (!m_isInit)
                    OnInit();
                
                m_context.onShown?.Invoke(m_context);
            }
        }

        void OnInit()
        {
            m_isInit = true;
            m_scirpt?.OnInit(m_context);
            m_context.onOpen?.Invoke(m_context);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            if (isShowing)
            {
                HideImmediately();
                m_context.onHide?.Invoke(m_context);
            }
        }

        public void HideImmediately()
        {
            if (isShowing)
            {
                m_gcomponet.parent.RemoveChild(m_gcomponet);
                m_gcomponet.visible = false;
            }
        }

        /// <summary>
        /// 正在关闭或准备关闭
        /// </summary>
        public bool isClosing => isClosed;

        /// <summary>
        /// 已经关闭
        /// </summary>
        public bool isClosed => m_closed || isDisposed;

        /// <summary>
        /// 已经释放资源
        /// </summary>
        public bool isDisposed => m_gcomponet.isDisposed;

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="nocache"></param>
        public void Close(bool nocache = false)
        {
            Hide();
            if (m_closed == false)
            {
                m_closed = true;
                m_context.onClose?.Invoke(m_context);
            }
        }

        /// <summary>
        /// 已经显示完毕
        /// </summary>
        public bool isReadyShowed => isShowing;

        /// <summary>
        /// 正在隐藏
        /// </summary>
        public bool isHiding => !isShowing;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool isShowing => m_gcomponet.parent != null;

        public UIContext context => m_context;

        public void OnFrameUpdate(double deltaTime)
        {
            context.onUpdate?.Invoke(context);
        }

        public void Dispose()
        {
            if (!m_gcomponet.isDisposed)
            {
                context?.onUninit?.Invoke(context);
                m_gcomponet.Dispose();
                m_isDisposed = true;
            }
            else if (!m_isDisposed)
            {
                context?.onUninit?.Invoke(context);
                m_isDisposed = true;
            }
        }

        public bool Initalize(IUIScript scirpt, UIContext context)
        {
            m_scirpt = scirpt;
            m_context = context;
            return true;
        }

        public void Reopen()
        {
            m_closed = false;
            m_context.onOpen?.Invoke(m_context);
            Show();
        }
    }
}
