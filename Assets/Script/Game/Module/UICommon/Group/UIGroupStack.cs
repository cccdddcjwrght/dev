
using Unity.Entities;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace SGame.UI
{
    /// <summary>
    /// UI 组, 通过堆栈逻辑显示UI。 遵循先入后出算法 
    /// </summary>
    public class UIGroupStack : IUIGroup
    {
        //public UISTATE_CHANGE onUIEvent { get; set; }
        
        public UIGroupStack()
        {
            m_datas = new List<int>();
        }

        /// <summary>
        /// 只有最后一个UI是显示的
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns></returns>
        public bool IsShow(int ui_id)
        {
            return m_datas.Count > 0 && m_datas[m_datas.Count - 1] == ui_id;
        }

        public bool OpenUI(int ui_id, int priority)
        {
            int index = m_datas.LastIndexOf(ui_id);
            if (index >= 0 && index == m_datas.Count - 1)
                return false;
            
            if (index >= 0)
            {
                // 删除列表
                List<int> removeList = m_datas.GetRange(index + 1, m_datas.Count - index);

                // 删除后面的UI
                m_datas.RemoveRange(index + 1, m_datas.Count - index);
                
                // 通知更改
                //for (int i = removeList.Count - 1; i >= 0; i--)
                //    onUIEvent(removeList[i]);
            }
            else
            {
                // 还没打开, 进行打开
                m_datas.Add(ui_id);
            }
            
            //onUIEvent(ui_id);
            return true;
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="ui_id"></param>
        /// <returns></returns>
        public bool CloseUI(int ui_id)
        {
            if (m_datas.Count == 0)
                return false;

            if (m_datas[m_datas.Count - 1] == ui_id)
            {
                m_datas.RemoveAt(m_datas.Count - 1);
                //onUIEvent(ui_id);

                //if (m_datas.Count > 0)
                //{
                //    onUIEvent(m_datas[m_datas.Count - 1]);
                //}
                return true;
            }
            
            return m_datas.Remove(ui_id);
        }
        
        /// <summary>
        /// UI数据
        /// </summary>
        public List<int> m_datas;

    }
}
