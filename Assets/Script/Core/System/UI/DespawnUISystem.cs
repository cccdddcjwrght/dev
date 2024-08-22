using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
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
        private static ILog log = LogManager.GetLogger("xl.ui");
        
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
                if (window.BaseValue == null)
                {
                    log.Error("base value is null=" + e);
                    commandBuffer.DestroyEntity(e);
                    return;
                }

                if (despawn.isDispose)
                {
                    // 直接销毁
                    UIFactory.Instance.Dispose(window.BaseValue);
                }
                else
                {
                    // 缓存回收
                    UIFactory.Instance.Free(window.BaseValue);
                }
                
                // 删除UI对象
                window.Dispose();
                commandBuffer.DestroyEntity(e);
            }).WithStructuralChanges().WithoutBurst().Run();
            
            // 检测来自UI内部的关闭设置
            Entities.WithAll<UIInitalized>().WithNone<DespawningUI>().ForEach((Entity e, UIWindow window) =>
            {
                if (window.BaseValue != null &&  window.BaseValue.isClosed) {
                    commandBuffer.AddComponent<DespawningUI>(e);
                    commandBuffer.SetComponent(e, new DespawningUI(){isDispose = false});
                }
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}