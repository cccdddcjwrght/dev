
using Unity.Entities;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace SGame.UI
{
    public delegate void UISTATE_CHANGE(int ui);

    /// <summary>
    /// UI组, 用于判断UI之间关系 
    /// </summary>
    public interface IUIGroup
    {
        //UISTATE_CHANGE onUIEvent { get; set; }
        
        /// <summary>
        /// 判断UI是否显示
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns></returns>
        bool IsShow(int ui_id);
        
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="ui_id"></param>
        /// <param name="priority"></param>
        /// <returns>成功打开UI</returns>
        bool OpenUI(int ui_id, int priority);
        
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns>成功关闭UI</returns>
        bool CloseUI(int ui_id);
    }
}
