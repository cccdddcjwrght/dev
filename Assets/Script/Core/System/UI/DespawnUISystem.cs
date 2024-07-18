using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

// UI销毁系统
namespace SGame.UI
{
    [UpdateAfter(typeof(UISystem))]
    [UpdateInGroup(typeof(UIGroup))]
    public partial class DespawnUISystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_commandBufferSystem;
        public List<Entity> m_DestoryEntity = new List<Entity>();

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }

        public void RemoveEntity(Entity e)
        {
            if (!m_DestoryEntity.Contains(e))
                m_DestoryEntity.Add(e);
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBufferSystem.CreateCommandBuffer();

            foreach (var e in m_DestoryEntity)
            {
                if (EntityManager.Exists(e) && !EntityManager.HasComponent<DespawningEntity>(e))
                {                
                    commandBuffer.AddComponent<DespawningEntity>(e);
                }
            }
            m_DestoryEntity.Clear();

            Entities.WithAll<DespawningEntity>().ForEach((Entity e, UIWindow window) =>
            {
                window.Dispose();
            }).WithoutBurst().Run();
            
            // 检测来自UI内部的关闭设置
            Entities.WithAll<UIInitalized>().WithNone<DespawningEntity>().ForEach((Entity e, UIWindow window) =>
            {
                if (window.Value != null &&  window.Value.isClosed) {
                    commandBuffer.AddComponent<DespawningEntity>(e);
                }
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}