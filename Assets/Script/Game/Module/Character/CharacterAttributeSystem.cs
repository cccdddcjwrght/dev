using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace SGame
{
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class CharacterAttributeSystem : SystemBase
    {
        private EndInitializationEntityCommandBufferSystem m_commandBuffer;
        private double m_baseSpeed = 0;
        protected override void OnCreate()
        {
            base.OnCreate();
            m_commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        }

        double baseSpeed
        {
            get
            {
                if (m_baseSpeed == 0)
                {
                    m_baseSpeed = 5;
                }

                return m_baseSpeed;
            }
        }

        protected override void OnUpdate()
        {
            // 计算速度属性
            double global_speed = AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.Speed);
            //var commandBuffer = m_commandBuffer.CreateCommandBuffer();
            Entities.WithAll<CharacterSpawnSystem.CharacterInitalized>().ForEach((Entity e, ref Speed speed, in CharacterAttribue attr) =>
            {
                var target = Utils.GetTargetFromRoleType(attr.roleType);
                double character_speed = baseSpeed;
                if (target != EnumTarget.Customer)
                     character_speed *= (1 + AttributeSystem.Instance.GetValue(target, EnumAttribute.Speed, 0) + global_speed);
                else
                    character_speed *= (1 + AttributeSystem.Instance.GetValue(target, EnumAttribute.Speed, attr.roleID) + global_speed);

                speed.Value = (float)(character_speed);
            }).WithoutBurst().Run();
            
        }
    }
}