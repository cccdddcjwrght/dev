using Unity.Entities;
using System;

namespace SGame.UI
{
    /// <summary>
    /// UI 脚本接口
    /// </summary>
    public interface IUIScript
    {
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <param name="context">ui上下文, 包括更新事件等</param>
        public void OnInit(UIContext context);
    }
}