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

        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var commandBuffer = m_commandBufferSystem.CreateCommandBuffer();
            Entities.ForEach((Entity e, UIWindow window, in DespawningUI despawn) =>
            {
                if (window.Value is FairyWindow)
                {
                    if (despawn.isDispose)
                    {
                        // 直接销毁
                        UIFactory.Instance.Dispose(window.Value);
                    }
                    else
                    {
                        // 缓存回收
                        UIFactory.Instance.Free(window.Value);
                    }
                }
                

                // 删除UI对象
                window.Dispose();
                commandBuffer.DestroyEntity(e);
            }).WithStructuralChanges().WithoutBurst().Run();
            
            // 检测来自UI内部的关闭设置
            Entities.WithAll<UIInitalized>().WithNone<DespawningUI>().ForEach((Entity e, UIWindow window) =>
            {
                if (window.Value != null &&  window.Value.isClosed) {
                    commandBuffer.AddComponent<DespawningUI>(e);
                    commandBuffer.SetComponent(e, new DespawningUI(){isDispose = false});
                }
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}