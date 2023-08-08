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
        /// UI对象本身
        /// </summary>
        public Entity            entity;

        /// <summary>
        /// onShown事件
        /// </summary>
        public Action<UIContext> onShown;

        /// <summary>
        /// ON Hide事件
        /// </summary>
        public Action<UIContext> onHide;

        /// <summary>
        /// UI关闭事件
        /// </summary>
        public Action<UIContext> onClose;

        /// <summary>
        /// UI的更新事件
        /// </summary>
        public Action<UIContext> onUpdate;
    }
}
