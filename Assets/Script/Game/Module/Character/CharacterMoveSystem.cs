using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Transforms;

namespace SGame
{
    [DisableAutoCreation]
    public partial class CharacterMoveSystem : SystemBase
    {
        private static ILog                             log                     = LogManager.GetLogger("xl.Character");
        private GameWorld                               m_world;
        private ResourceManager                         m_resourceManager;
            
        public void Initalize(GameWorld world, ResourceManager resourceManager)
        {
            m_world             = world;
            m_resourceManager   = resourceManager;
        }
        
        protected override void OnCreate()
        {
        }

        protected override void OnUpdate()
        {
            float dt = Time.DeltaTime;
            EntityCommandBuffer cmdBuffer = new EntityCommandBuffer(Allocator.Temp);
            Entities.WithAll<CharacterSpawnSystem.InitalizedTag>().ForEach((Entity e,
                CharacterMover mover, 
                ref Translation translation, 
                ref Rotation  rotation,
                    in JumpTimeData jumpTime,
                    in JumpHighData  jumpHigh) =>
            {
                if (mover.isFinish)
                    return;

                if (mover.m_Intervaltime > 0)
                {
                    mover.m_Intervaltime -= dt;
                    return;
                }

                CharacterController controller = mover.m_controller;
                if (mover.m_movedDistance <= 0.00001f)
                {
                    controller.transform.position = mover.m_paths[0];
                }
                
                // 速度等于距离除以时间
                float nodeDistance = mover.nodeDistance;
                if (nodeDistance <= 0.000000001f)
                {
                    log.Error("node distance check fail!!=" + nodeDistance.ToString());
                    return;
                }
                if (jumpTime.Value <= 0.000001f)
                {
                    log.Error("speed is too small!!" + jumpTime.Value.ToString());
                }
                // 计算得到移动速度
                float speed = nodeDistance / jumpTime.Value;
                
                // 通过速度计算每帧移动距离
                float delta     = speed * dt;
                int oldIndex    = mover.currentIndex;
                mover.Movement(delta);
                int index       = mover.currentIndex;

                Animator anim = controller.GetComponent<Animator>();

                if (mover.isFinish)
                {
                    controller.transform.position = mover.LastPosition();
                    anim.SetFloat("Speed", 0);
                    mover.Finish();
                }
                else
                {
                    if (oldIndex != index)
                    {
                        // 计时开始
                        mover.ResetTimer();
                    }
                    float3 f3target = mover.GetPosition();
                    float3 startPos = mover.GetStartPosition();
                    float3 nextPos = mover.GetNextPosition();
                    float  progress = mover.GetMoveProgress();
                    if (progress <= 0.5)
                        progress *= 2.0f;
                    else
                        progress = 2 * (1 - progress);
   
                    float  high = jumpHigh.Value * progress;       // 计算跳跃高点
                    f3target.y = math.max(startPos.y + high, nextPos.y);
                    quaternion look_at = mover.GetRotation();
                    Vector3 target = f3target;       
                    Vector3 deltaMovement = target - controller.transform.position;
                    controller.Move(deltaMovement);
                    controller.transform.rotation = look_at; 
                    anim.SetFloat("Speed", 1);
                }
                
                // 将移动的位置同步回来
                translation.Value = controller.transform.position;
                rotation.Value    = controller.transform.rotation;

                // 将移动后的位置同步回去
            }).WithoutBurst().Run();
           
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }

        protected override void OnDestroy()
        {
        }
    }
}