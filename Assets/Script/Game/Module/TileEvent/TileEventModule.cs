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

        public TileEventModule(GameWorld gameWorld)
        {
            m_eventSystem = gameWorld.GetECSWorld().CreateSystem<TileEventSystem>();
            m_processSystem = gameWorld.GetECSWorld().CreateSystem<TileEventProcessSystem>();
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
            m_processSystem.Update();
        }
    }
}