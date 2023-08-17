using System.Collections;
using System.Collections.Generic;
using log4net;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    [DisableAutoCreation]
    public class CharacterSpawnSystem : ComponentSystem
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

        void CreateCharacter(Entity e, Character c)
        {
            // 1. 创建显示界面
            Entity renderEntity = m_resourceManager.LoadCharacterRender(c.characterId);
            c.render = renderEntity;
            
            // 2. 初始化移动速度
            if (EntityManager.HasComponent<SpeedData>(e) == false)
            {
                log.Error("Speed Component Not Found!");
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
        }

        protected override void OnUpdate()
        {
            EntityCommandBuffer cmdBuffer = new EntityCommandBuffer(Allocator.Temp);
            Entities.WithNone<InitalizedTag>().ForEach((Entity e, Character c) =>
            {
                CreateCharacter(e, c);
                cmdBuffer.AddComponent<InitalizedTag>(e);
                log.Info("character initalize finish=" + c.characterName);
            });
            
            cmdBuffer.Playback(EntityManager);
            cmdBuffer.Dispose();
        }

        protected override void OnDestroy()
        {
        }
    }
}