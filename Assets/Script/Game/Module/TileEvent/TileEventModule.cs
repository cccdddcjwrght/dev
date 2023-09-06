using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

namespace SGame
{
    public struct DiceRollData
    {
        // 第一个骰子的值
        public int Value1;
        
        // 第二个骰子的值
        public int Value2;

        // 服务器ID识别
        public int eventId;
    }

    public struct RoundEvent
    {
        public long playerId;
        public int eventType;
        public int gold;
        public int card_id;
    }
    
    public struct RoundData
    {
        public int eventId;
        public int dice1;
        public int dice2;
        public int pos;
        public int serverEventId;

        public RoundEvent roundEvent;
    }
    
    
    /// <summary>
    /// 游戏事件系统, 目前是挂在场景事件中
    /// </summary>
    public class TileEventModule
    {
        public TileEventModule(GameWorld gameWorld, 
            RandomSystem randomSystem, 
            TileModule tileModule)
        {
            m_gameWorld     = gameWorld;
            m_randomSystem  = randomSystem;
            m_tileModule    = tileModule;
            m_eventSystem   = gameWorld.GetECSWorld().CreateSystem<TileEventSystem>();
            m_processSystem = gameWorld.GetECSWorld().CreateSystem<TileEventProcessSystem>();
            m_conditionFactory  = new DesginConditionFactory();
            m_actionFactory     = new DesginActionFactory(tileModule);
        }

        public void Update()
        {
            m_eventSystem.Update();
            m_processSystem.Update();
        }

        public void Shutdown()
        {
            m_gameWorld.GetECSWorld().DestroySystem(m_eventSystem);
            m_gameWorld.GetECSWorld().DestroySystem(m_processSystem);
        }

        /// <summary>
        /// 清空所有游戏事件
        /// </summary>
        public void ClearAllEvents()
        {
            // 清空运行中的事件
            m_processSystem.Clear();
            
            // 清空队列中的事件
            m_roundDatas.Clear();
        }

        /// <summary>
        /// 计算路过的事件
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        List<TileEventProcess> GetPassEventProcess(int startPos, int endPos, int power)
        {
            List<TileEventProcess> ret = new List<TileEventProcess>();
            int tileCount = m_tileModule.tileCount;

            for (int i = startPos; i < endPos; i++)
            {
                int pos         = i % tileCount;
                int tileId      = m_tileModule.GetTileIdByPos(pos);
                
                // 获得配置
                if (!ConfigSystem.Instance.TryGet(tileId, out GameConfigs.GridRowData girdData))
                {
                    log.Error("tile not found=" + tileId + " pos=" + i);
                    continue;
                }
                
                // 条件判定
                var cond    = m_conditionFactory.CreateTileCondition(0, i, TileEventTrigger.State.PASSOVER);
                
                /// 建筑路过
                IDesginAction act = m_actionFactory.CreatePass(pos);
                if (act != null)
                {
                    ret.Add(new TileEventProcess()
                    {
                        condition = cond,
                        action    = act
                    });
                }

                // 金币路过
                if (girdData.BaseRewardLength != 2)
                {
                    continue;
                }

                // 添加自动金币事件
                int addGold = girdData.BaseReward(1);
                if (addGold != 0)
                {
                    TileEventProcess v = new TileEventProcess();
                    v.condition        = cond;
                    v.action           = m_actionFactory.CreateGold(addGold * power);
                    ret.Add(v);
                }
            }

            return ret;
        }
        
        /// <summary>
        /// 解析事件数据, 并运行下一回合的事件处理
        /// </summary>
        public bool NextRound(int startPos, out DiceRollData v)
        {
            v = default;
            if (m_roundDatas.Count == 0)
                return false;
            
            // 获得第一个event
            RoundData data = m_roundDatas.First();
            m_roundDatas.RemoveAt(0);
            
            // 1. 解析事件, 生成下一个骰子应该播放多少
            v.Value1 = data.dice1;                          
            v.Value2 = data.dice2;                         
            v.eventId = data.serverEventId;
            int endPos = startPos + v.Value1 + v.Value2;
            
            var userProperty =  PropertyManager.Instance.GetUserGroup(0);
            var power = (int)userProperty.GetNum((int)UserType.DICE_POWER);
            if (power <= 0)
            {
                log.Error("power is zero!!");
                return false;
            }

            // 添加路过事件
            List<TileEventProcess> passoverEvent = GetPassEventProcess(startPos, endPos, power);
            foreach (var e in passoverEvent)
            {
                m_processSystem.AddTileEvent(e.condition, e.action);
            }
            
            // 添加结束事件
            int pos = endPos % m_tileModule.tileCount;
            var cond = m_conditionFactory.CreateTileCondition(0, endPos, TileEventTrigger.State.FINISH);
            var act = m_actionFactory.Create(data.roundEvent, pos, power);//m_actionFactory.CreateGold(m_randomSystem.NextInt(100, 200));
            if (act != null)
            {
                m_processSystem.AddTileEvent(cond, act);
            }
            return true;
        }

        /// <summary>
        /// 判断事件池是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return m_roundDatas.Count == 0;
        }

        public void AddEventGroup(RoundData diceEvent)
        {
            m_roundDatas.Add(diceEvent);
        }
        
        /// <summary>
        /// 数据
        /// </summary>
        private TileEventSystem         m_eventSystem;
        private TileEventProcessSystem  m_processSystem;
        private GameWorld               m_gameWorld;
        //private List<DesginEvent>     m_desginEvent;
        private DesginConditionFactory  m_conditionFactory;
        private DesginActionFactory     m_actionFactory;
        private RandomSystem            m_randomSystem;
        private TileModule              m_tileModule;
        

        private List<RoundData>     m_roundDatas    = new List<RoundData>();
        private static ILog         log             = LogManager.GetLogger("xl.Game.tileevent");
    }
}