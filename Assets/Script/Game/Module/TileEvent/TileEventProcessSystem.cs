using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Entities;
using UnityEngine;
namespace SGame
{
    class TileEventProcess
    {
        public IDesginCondition condition; // 执行功能的条件
        public IDesginAction    action;    // 执行功能的事件
    }
    
    /// <summary>
    /// 处理地块产生的事件功能
    /// </summary>
    [DisableAutoCreation]
    public partial class TileEventProcessSystem : SystemBase
    {
        private static ILog                         log         = LogManager.GetLogger("xl.game")     ;
        private ItemGroup                           m_itemGroup                                            ;

        /// <summary>
        /// 当前待处理事件
        /// </summary>
        private List<TileEventProcess>              m_currentProcess;
        
        /// <summary>
        /// 检测成功后的事件
        /// </summary>
        private List<TileEventProcess>              m_sucessProcess;
        
        /// <summary>
        /// 检测不通过的事件
        /// </summary>
        private List<TileEventProcess>              m_failProcess;


        /// <summary>
        /// 添加地砖相关事件
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="action"></param>
        public void AddTileEvent(IDesginCondition cond, IDesginAction action)
        {
            TileEventProcess val = new TileEventProcess()
            {
                condition = cond,
                action = action,
            };

            m_currentProcess.Add(val);
        }
        
        
        /// <summary>
        /// 处理完后交换处理
        /// </summary>
        void SwitchProcess()
        {
            if (m_sucessProcess.Count > 0)
            {
                m_currentProcess.Clear();
                
                // 和失败部分交换
                var tmp = m_currentProcess;
                m_currentProcess          = m_failProcess;
                m_failProcess             = tmp;

                // 清空执行过的部分
                m_sucessProcess.Clear();
            }
        }

        protected override void OnCreate()
        {
            m_itemGroup = PropertyManager.Instance.GetGroup(ItemType.USER);
            m_currentProcess = new List<TileEventProcess>(32);
            m_sucessProcess = new List<TileEventProcess>(32);
            m_failProcess = new List<TileEventProcess>(32);
        }
        
        protected override void OnUpdate()
        {
            UserSetting setting = DataCenter.Instance.GetUserSetting();
            // 找到需要触发的事件
            Entities.ForEach((Entity e, Character Character, in DynamicBuffer<TileEventTrigger> triggers) =>
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    TileEventTrigger t = triggers[i];
                    foreach (var eventData in m_currentProcess)
                    {
                        if (eventData.condition.CheckTile(Character.characterId, t.titleId, t.state))
                        {
                            // 符合触发事件
                            m_sucessProcess.Add(eventData);
                        }
                        else
                        {
                            // 不符合触发条件
                            m_failProcess.Add(eventData);
                        }
                    }
                }
            }).WithoutBurst().Run();

            // 执行需要触发的事件, 防止在ECS循环中执行代码, 可能会照成一些用法被禁止
            if (m_sucessProcess.Count > 0)
            {
                foreach (var eventData in m_sucessProcess)
                {
                    eventData.action.Do();
                }
                SwitchProcess();
            }
        }
    }
}