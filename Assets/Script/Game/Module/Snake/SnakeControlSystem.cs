using log4net;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace SGame
{
    /// <summary>
    /// 貪吃蛇控制
    /// </summary>
    [DisableAutoCreation]
    public partial class SnakeControlSystem : SystemBase
    {
        private static ILog log = LogManager.GetLogger("xl.snake"); 
        

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            
            // 貪吃蛇的移動
            Entities.WithAll<SnakeSpawnSystem.Initalized>().ForEach((Entity e, SnakeData snakeData, ref SnakeMovement snakeMovement) =>
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                float2 movement = snakeMovement.m_dir;
                if (math.abs(h) > 0.001 || math.abs(v) > 0.001)
                {
                    movement.x = 0;
                    movement.y = 0;
                    if (math.abs(h) > 0.001)
                        movement.x = math.sign(h);
                    
                    if (math.abs(v) > 0.001)
                        movement.y = math.sign(v);

                    snakeMovement.m_dir = math.normalize(movement);;
                }

            }).WithoutBurst().Run();
        }
    }
}