using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

// UI销毁系统
namespace SGame.UI
{
    public struct UICloseEvent : IComponentData
    {
        
    }
    public partial class DespaenUISystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem m_commandBufferSystem;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBufferSystem.CreateCommandBuffer();
            Entities.WithAll<UICloseEvent>().WithNone<DespawningEntity>().ForEach((Entity e, UIWindow window) =>
            {
                commandBuffer.AddComponent<DespawningEntity>(e);
            }).WithStructuralChanges().WithoutBurst().Run();;
            
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