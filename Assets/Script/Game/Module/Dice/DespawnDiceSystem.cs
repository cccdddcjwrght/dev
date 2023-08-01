using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    // 骰子销毁系统
    [DisableAutoCreation]
    public partial class DespawnDiceSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.Temp);
            Entities.WithAll<DespawningEntity>().ForEach((Entity e, ClientDice dice) =>
            {
                // Entity 对象会在后续的GameWorld 里面销毁掉, 这里只需要处理GameObject 相关的资源回收即可, 目前是检点的直接Destory删除销毁
                GameObject.Destroy(dice.gameObject);
            }).WithoutBurst().Run();
        }
    }
}