using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using Unity.Entities;

namespace SGame.UI
{
    public enum UI_TYPE
    {
        WINDOW      = 0, // 普通的FGUI WINDOW
        COM_WINDOW  = 1, // 只有一个GCponent的组件 
    }
    
    /// <summary>
    /// 包含FGUI里的WINDOW, 与没有WINDOW的HUD(HUD 必须有个父节点, 而
    /// </summary>
    public interface IBaseWindow
    {
        UI_TYPE type { get; }
        
        /// <summary>
        /// UI名称
        /// </summary>
        string uiname { get; set; }
        
        /// <summary>
        /// 配置ID
        /// </summary>
        public int configID { get; }
        
        public PoolID poolID { get; }

        /// <summary>
        /// 显示
        /// </summary>
        void Show();
        
        /// <summary>
        /// 隐藏
        /// </summary>
        void Hide();

        /// <summary>
        /// 正在关闭或准备关闭
        /// </summary>
        bool isClosing { get; }
        
        /// <summary>
        /// 已经关闭
        /// </summary>
        bool isClosed { get; }
        
        /// <summary>
        /// 已经释放资源
        /// </summary>
        bool isDisposed { get; }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="nocache"></param>
        void Close(bool nocache = false);
        
        /// <summary>
        /// 已经显示完毕
        /// </summary>
        bool isReadyShowed { get; }

        /// <summary>
        /// 正在隐藏
        /// </summary>
        bool isHiding { get; }
        
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool isShowing { get; }

        public UIContext context { get; }
        
        public GComponent content { get; }

        public void OnFrameUpdate(double deltaTime);

        public bool Initalize(IUIScript scirpt, UIContext context);

        public void Reopen();
    }
}