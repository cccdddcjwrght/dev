using System.Collections;
using System.Collections.Generic;
using log4net;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public class CharacterAttributeSystem : ComponentSystem
    {
        private static ILog log = LogManager.GetLogger("game.character");
        private EndInitializationEntityCommandBufferSystem m_commandBuffer;
        private double m_baseSpeed = 0;
        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            // 计算速度属性
            Entities.WithNone<DisableAttributeTag, DespawningEntity>().WithAll<CharacterSpawnSystem.CharacterInitalized>().ForEach((
                Entity e, 
                Character character,
                ref Speed speed, 
                ref CharacterAttribue attr
            ) =>
            {
                var v = AttributeSystem.Instance.GetValueByRoleID(attr.roleID, EnumAttribute.Speed, character.isEmployee);
                if (v <= 0)
                {
                    // 速度异常表示没有读取成功
                    log.Warn("Role Speed Is Zero RoleID=" + attr.roleID + " employee=" + character.isEmployee);
                    v = 10;
                }
                speed.Value = (float)(v) + character.externSpeed;
            });
        }
    }
}