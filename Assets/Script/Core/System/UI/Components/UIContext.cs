using UnityEngine;
using System;
using FairyGUI;
using Unity.Entities;
namespace SGame.UI
{
    /// <summary>
    /// UI对象上下文
    /// </summary>
    public class UIContext
    {
        /// <summary>
        /// 游戏世界
        /// </summary>
        public GameWorld         gameWorld;
        
        /// <summary>
        /// UI模块
        /// </summary>
        public UIModule          uiModule;

        /// <summary>
        /// UI节点
        /// </summary>
        public GComponent        content;

        /// <summary>
        /// Window 对象
        /// </summary>
        public FairyWindow       window;

        /// <summary>
        /// UI对象本身
        /// </summary>
        public Entity            entity;

        /// <summary>
        /// 配置表ID
        /// </summary>
        public int               configID;

        /// <summary>
        /// onShown事件
        /// </summary>
        public Action<UIContext> onShown;

        /// <summary>
        /// ON Hide事件
        /// </summary>
        public Action<UIContext> onHide;

        /// <summary>
        /// UI关闭注销
        /// </summary>
        public Action<UIContext> onUninit;

		/// <summary>
		/// UI关闭
		/// </summary>
		public Action<UIContext> onClose;

        /// <summary>
        /// UI打开事件
        /// </summary>
        public Action<UIContext> onOpen;

        /// <summary>
        /// UI的更新事件
        /// </summary>
        public Action<UIContext> onUpdate;

        /// <summary>
        /// UI 显示动画 
        /// </summary>
        public Action<UIContext> doShowAnimation;

        /// <summary>
        /// 开始显示
        /// </summary>
        public Action<UIContext> beginShown;

		/// <summary>
		/// 开始隐藏
		/// </summary>
		public Action<UIContext> beginHide;

		/// <summary>
		/// UI 隐藏动画
		/// </summary>
		public Action<UIContext> doHideAnimation;
    }
}
