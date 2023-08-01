using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇對象創建
    /// </summary>
    [DisableAutoCreation]
    public partial class SnakeSpawnSystem : SystemBase
    {
        // 初始化標記
        public struct Initalized : IComponentData
        {
        }

        private static ILog log = LogManager.GetLogger("xl.snake"); 

        public const string SNAKE_BODY_PREFAB = "Assets/BuildAsset/Prefabs/Snake/SnakeBody.prefab"; 

        private Entity          m_snakeBodyPrefab;
        
        public void Initalize(ResourceManager resourceMnaager)
        {
            m_snakeBodyPrefab = resourceMnaager.GetEntityPrefab(SNAKE_BODY_PREFAB);
        }

        protected override void OnUpdate()
        {
            // 創建CommandBuffer
            EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.Temp);
            
            Entities.WithNone<Initalized>().ForEach((Entity e, SnakeData bones, in Snake snake) =>
            {
                commandBuffer.AddComponent<Initalized>(e);

                // 初始化貪吃蛇路徑數據
                bones.Initalize(snake.m_startPositon, snake.bonesCount);
                
                // 創建貪吃蛇軀體顯示部分
                for (int i = 0; i < snake.m_bodyNum; i++)
                {
                    // 實例化貪吃蛇的身體
                    Entity body = commandBuffer.Instantiate(m_snakeBodyPrefab);
                 
                    commandBuffer.SetComponent(body, new SnakeBoneData()
                    {
                        Value = i * snake.m_boneNum,     // 身體索引
                        snake = e,                       // 蛇的本體
                    });
                    
                    // 關聯軀體的Entity
                    commandBuffer.AppendToBuffer(e, new LinkedEntityGroup { Value = body });
                }
            }).WithoutBurst().Run();
            
            // 執行CommandBuffer命令, 這樣會效率高一些
            commandBuffer.Playback(EntityManager);
            commandBuffer.Dispose();
        }
    }
}