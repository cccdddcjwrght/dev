using log4net;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇對象數據的移動
    /// </summary>
    [DisableAutoCreation]
    public partial class SnakeMoveSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.snake"); 
        

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            
            // 貪吃蛇的移動
            Entities.WithAll<SnakeSpawnSystem.Initalized>().ForEach((Entity e, SnakeData snakeData, ref SnakeMovement snakeMovement, in Snake snake,in SpeedData speedData) =>
            {
                // 當前需要移動的距離
                float movment = deltaTime * speedData.Value + snakeMovement.movement;

                // 需要移動的方向
                float2 dir = snakeMovement.m_dir;
                
                // 獲得每次移動的步長
                float step = snake.m_step;
                float2 move_step = math.normalize(dir) * step;
                
                // 按步長移動
                while (movment >= step)
                {
                    snakeData.Move(move_step);
                    movment -= step;
                }

                snakeMovement.movement = math.max(0, movment);
            }).WithoutBurst().Run();
        }
    }
}