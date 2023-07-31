using log4net;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 貪吃顯示對象的移動
    /// </summary>
    [DisableAutoCreation]
    public partial class SnakeBodyMoveSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.snake"); 
        

        protected override void OnUpdate()
        {
            // 創建CommandBuffer
            EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.Temp);

            float deltaTime = Time.DeltaTime;

            // 貪吃蛇的移動
            Entities.ForEach((Entity e, ref Translation trans, in SnakeBoneData body) =>
            {
                // 獲得蛇對象數據
                SnakeData snakeData = EntityManager.GetComponentData<SnakeData>(body.snake);
                float2          pos = snakeData.GetBonePosition(body.Value);
                trans.Value         = new float3(pos.x, pos.y, 0);
            }).WithoutBurst().Run();
        }
    }
}