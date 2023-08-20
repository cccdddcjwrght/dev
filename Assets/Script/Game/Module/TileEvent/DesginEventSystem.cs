using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    public struct DesginEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public int event_id;
        
        /// <summary>
        /// 整形事件值
        /// </summary>
        public int iValue;
        public int3 i3Value;

        /// <summary>
        /// 浮点数事件值
        /// </summary>
        public float fValue;
        public float3 f3Value;
    }
    
    /// <summary>
    /// 游戏事件系统
    /// </summary>
    public class DesginEventSystem
    {
        /// <summary>
        /// 事件对象
        /// </summary>
        private List<IDesginEvent> m_datas;
        
        /// <summary>
        /// 游戏事件工厂
        /// </summary>
        private DesginEventFactory m_eventFactory;

        /// <summary>
        /// 事件地块事件
        /// </summary>
        /// <param name="tileId"></param>
        public void Do(int tileId)
        {
            
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="desginEvent"></param>
        /// <returns></returns>
        public bool Add(DesginEvent desginEvent)
        {
            //m_datas.add
            //IDesginEvent e = m_eventFactory.Create(desginEvent);
            ///m_datas.Add(e);
            return true;
        }

        /// <summary>
        /// 清除事件
        /// </summary>
        public void Clear()
        {
            
        }
    }
}