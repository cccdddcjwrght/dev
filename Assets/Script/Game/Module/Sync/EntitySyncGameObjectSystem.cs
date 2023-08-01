using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

namespace SGame
{
    /// <summary>
    /// 同步组件
    /// </summary>
    public struct EntitySyncGameObject : IComponentData
    {
        // 需要同步的对象
        public Entity Value;
    } 
    
    // 对象同步系统
    [DisableAutoCreation]
    public partial class EntitySyncGameObjectSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity e, in LocalToWorld localToWorld,in EntitySyncGameObject sync) =>
            {
                if (EntityManager.Exists(sync.Value) && EntityManager.HasComponent<Transform>(sync.Value))
                {
                    // 同步对象
                    Transform sycnTransform = EntityManager.GetComponentObject<Transform>(sync.Value);
                    sycnTransform.position = localToWorld.Position;
                    sycnTransform.rotation = localToWorld.Rotation;
                }
            }).WithoutBurst().Run();
        }
    }
}
