using System.Collections.Generic;
using System.ComponentModel;
using Unity.Entities;

// 销毁entity的系统
namespace SGame
{
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public class DespawnEntitySystem : ComponentSystem
    {
        private List<Entity>                            m_entitys;
        private List<Entity>                            m_destoryEntitys;
        private EndSimulationEntityCommandBufferSystem  m_commanderBuffer;
        
        protected override void OnCreate()
        {
            m_entitys = new List<Entity>(10);
            m_destoryEntitys = new List<Entity>(10);

            m_commanderBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            if (m_entitys.Count == 0)
                return;

            var commandBuffer = m_commanderBuffer.CreateCommandBuffer();
            foreach (var e in m_entitys)
            {
                commandBuffer.AddComponent<DespawningEntity>(e);
            }
            
            foreach (var e in m_destoryEntitys)
            {
                commandBuffer.DestroyEntity(e);
            }
            m_entitys.Clear();
            m_destoryEntitys.Clear();
        }

        public void DespawnEntity(Entity e)
        {
            if (!m_entitys.Contains(e))
            {
                m_entitys.Add(e);
            }
        }
        
        public void DestoryEntity(Entity e)
        {
            if (!m_destoryEntitys.Contains(e))
            {
                m_destoryEntitys.Add(e);
            }
        }
    }
}
