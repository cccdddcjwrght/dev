using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace SGame
{
    [DisableAutoCreation]
    public partial class CharacterSpawnSystem : SystemBase
    {
        // 初始化完成标签
        public struct InitalizedTag : IComponentData
        {
        }
        
        private static ILog                             log                     = LogManager.GetLogger("xl.Character");
        private GameWorld                               m_world;
        private ResourceManager                         m_resourceManager;
        private EndSimulationEntityCommandBufferSystem  m_commandBuffer;
            
        public void Initalize(GameWorld world, ResourceManager resourceManager)
        {
            m_world             = world;
            m_resourceManager   = resourceManager;
            m_commandBuffer     = world.GetECSWorld().GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnCreate()
        {
        }

        void CreateCharacter(Entity e)
        {
            Character c = EntityManager.GetComponentObject<Character>(e);
            
            // 1. 创建显示界面
            Entity renderEntity = m_resourceManager.LoadCharacterRender(c.characterId);
            c.render = renderEntity;
            
            if (EntityManager.HasComponent<JumpTimeData>(e) == false)
            {
                log.Error("JumpTime Component Not Found!");
                return;
            }
            
            if (EntityManager.HasComponent<JumpHighData>(e) == false)
            {
                log.Error("JumpHighData Component Not Found!");
                return;
            }

            // 3. 初始化角色控制器
            if (EntityManager.HasComponent<CharacterMover>(e) == false)
            {
                log.Error("ChracterMover Not Attach!");
                return;
            }
            CharacterMover mover = EntityManager.GetComponentObject<CharacterMover>(e);
            if (EntityManager.HasComponent<CharacterController>(renderEntity) == false)
            {
                log.Error("Character Not found Controller !");
                return;
            }
            CharacterController characterController = EntityManager.GetComponentObject<CharacterController>(renderEntity);
            mover.m_controller = characterController;

            Translation trans = EntityManager.GetComponentData<Translation>(e);
            characterController.transform.position = trans.Value;
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer cmdBuffer = new EntityCommandBuffer(Allocator.Temp);
            NativeList<Entity> entityList   = new NativeList<Entity>(Allocator.Temp);
            
            Entities.WithNone<InitalizedTag>().WithAll<Character,Translation>().ForEach((Entity e) =>
            {
                entityList.Add(e);
                cmdBuffer.AddComponent<InitalizedTag>(e);
            }).WithoutBurst().Run();

            foreach (Entity e in entityList)
            {
                CreateCharacter(e);
            }
            entityList.Dispose();
            
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }

        protected override void OnDestroy()
        {
        }
    }
}