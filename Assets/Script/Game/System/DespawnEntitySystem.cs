using System.Collections.Generic;
using Unity.Entities;

// 销毁entity的系统
namespace SGame
{
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class DespawnEntitySystem : SystemBase
    {
        private List<Entity>                            m_entitys;
        private EndSimulationEntityCommandBufferSystem  m_commanderBuffer;
        
        protected override void OnCreate()
        {
            m_entitys = new List<Entity>(10);
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
            m_entitys.Clear();
        }

        public void DespawnEntity(Entity e)
        {
            if (!m_entitys.Contains(e))
            {
                m_entitys.Add(e);
            }
        }
    }
}
