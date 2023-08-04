using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

namespace SGame
{
    [DisableAutoCreation]
    public partial class CharacterMoveSystem : SystemBase
    {
        // 初始化完成标签
        public struct InitalizedTag : IComponentData
        {
        }
        
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
            Entities.WithAll<CharacterSpawnSystem.InitalizedTag>().ForEach((Entity e, CharacterMover mover, in SpeedData speed) =>
            {
                if (mover.m_paths.Count == 0)
                {
                    return;
                }

                CharacterController controller = mover.m_controller;
                if (mover.m_movedDistance <= 0.00001f)
                {
                    controller.transform.position = mover.m_paths[0];
                }
                
                float delta = speed.Value * dt;
                mover.Movement(delta);
                Animator anim = controller.GetComponent<Animator>();

                if (mover.isFinish)
                {
                    controller.transform.position = mover.LastPosition();
                    anim.SetFloat("Speed", 0);
                    mover.Clear();
                }
                else
                {
                    mover.GetPositionFromDistance(mover.m_movedDistance, out float3 f3target, out quaternion look_at);
                    Vector3 target = f3target;       
                    Vector3 deltaMovement = target - controller.transform.position;
                    controller.Move(deltaMovement);
                    controller.transform.rotation = look_at; //Quaternion.LookRotation(deltaMovement);
                    anim.SetFloat("Speed", 1);
                }
            }).WithoutBurst().Run();
           
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }

        protected override void OnDestroy()
        {
        }
    }
}