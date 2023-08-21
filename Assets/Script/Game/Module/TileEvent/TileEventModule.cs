using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public RoundEvent roundEvent;
    }
    
    
    /// <summary>
    /// 游戏事件系统, 目前是挂在场景事件中
    /// </summary>
    public class TileEventModule
    {


        public TileEventModule(GameWorld gameWorld, RandomSystem randomSystem)
        {
            m_gameWorld     = gameWorld;
            m_randomSystem  = randomSystem;
            m_eventSystem   = gameWorld.GetECSWorld().CreateSystem<TileEventSystem>();
            m_processSystem = gameWorld.GetECSWorld().CreateSystem<TileEventProcessSystem>();
            m_conditionFactory  = new DesginConditionFactory();
            m_actionFactory     = new DesginActionFactory();
            //m_desginEvent       = new List<DesginEvent>();
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
        /// 计算路过的事件
        /// </summary>
        /// <param name="startTileId"></param>
        /// <param name="endTileId"></param>
        /// <returns></returns>
        List<TileEventProcess> GetPassEventProcess(int startTileId, int endTileId)
        {
            List<TileEventProcess> ret = new List<TileEventProcess>();
            for (int i = startTileId; i < endTileId; i++)
            {
                // 随机获取金币数量
                int addGold = m_randomSystem.NextInt(10, 20);
                TileEventProcess v  = new TileEventProcess();
                v.condition         = m_conditionFactory.CreateTileCondition(0, i, TileEventTrigger.State.PASSOVER);
                v.action            = m_actionFactory.CreateGold(addGold);
                ret.Add(v);
            }

            return ret;
        }
        
        /// <summary>
        /// 解析事件数据, 并运行下一回合的事件处理
        /// </summary>
        public DiceRollData NextRound(int startTileId)
        {
            // 获得第一个event
            RoundData data = m_roundDatas.First();
            m_roundDatas.RemoveAt(0);
            
            DiceRollData v = new DiceRollData();
            
            // 1. 解析事件, 生成下一个骰子应该播放多少
            v.Value1 = data.dice1; //m_randomSystem.NextInt(1, 7);
            v.Value2 = data.dice2; //m_randomSystem.NextInt(1, 7);
            int endTileID = startTileId + v.Value1 + v.Value2;

            // 添加路过事件
            List<TileEventProcess> passoverEvent = GetPassEventProcess(startTileId, endTileID);
            foreach (var e in passoverEvent)
            {
                m_processSystem.AddTileEvent(e.condition, e.action);
            }
            
            // 添加结束事件
            var cond = m_conditionFactory.CreateTileCondition(0, endTileID, TileEventTrigger.State.FINISH);
            var act = m_actionFactory.Create(data.roundEvent);//m_actionFactory.CreateGold(m_randomSystem.NextInt(100, 200));
            if (act != null)
            {
                m_processSystem.AddTileEvent(cond, act);
            }
            return v;
        }

        public void AddEventGroup(RoundData diceEvent)
        {
            m_roundDatas.Add(diceEvent);
        }
        
        /// <summary>
        /// 数据
        /// </summary>
        private TileEventSystem        m_eventSystem;
        private TileEventProcessSystem m_processSystem;
        private GameWorld              m_gameWorld;
        //private List<DesginEvent>      m_desginEvent;
        private DesginConditionFactory m_conditionFactory;
        private DesginActionFactory    m_actionFactory;
        private RandomSystem           m_randomSystem;
        

        private List<RoundData> m_roundDatas = new List<RoundData>();
    }
}