using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

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
                Vector3 target = mover.GetPositionFromDistance(mover.m_movedDistance);

                Vector3 deltaMovement = target - controller.transform.position;
                controller.Move(deltaMovement);
                controller.transform.rotation = Quaternion.LookRotation(deltaMovement);
                
                Animator anim = controller.GetComponent<Animator>();
                if (mover.isFinish)
                {
                    //anim.SetBool("Move", false);
                    anim.SetFloat("Speed", 0);
                    mover.Clear();
                }
                else
                {
                    //anim.SetBool("Move", true);
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