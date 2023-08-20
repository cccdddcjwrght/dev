using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public class TileEventModule
    {
        private TileEventSystem        m_eventSystem;
        private TileEventProcessSystem m_processSystem;
        private GameWorld              m_gameWorld;
        private List<DesginEvent>      m_desginEvent;

        public TileEventModule(GameWorld gameWorld)
        {
            m_gameWorld     = gameWorld;
            m_eventSystem   = gameWorld.GetECSWorld().CreateSystem<TileEventSystem>();
            m_processSystem = gameWorld.GetECSWorld().CreateSystem<TileEventProcessSystem>();
            m_desginEvent   = new List<DesginEvent>();
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
        /// 运行下一回合的事件处理
        /// </summary>
        public void NextRound()
        {
            // m_desginEvent.get
            // 1. 解析事件, 生成下一个骰子应该播放多少
            // 2. 生成对应的游戏内事件集
        }
    }
}