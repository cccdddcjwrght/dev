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
    public struct EntitySyncGameObjectTag : IComponentData
    {
    } 
    
    // 对象同步系统
    [UpdateInGroup(typeof(GameLogicAfterGroup))]
    public partial class EntitySyncGameObjectSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<EntitySyncGameObjectTag>().ForEach((Entity e, Transform sycnTransform, in LocalTransform trans) =>
            {
                // 同步对象
                if (sycnTransform != null)
                {
                    sycnTransform.position = trans.Position;
                    sycnTransform.rotation = trans.Rotation;
                }
            }).WithoutBurst().Run();
        }
    }
}
