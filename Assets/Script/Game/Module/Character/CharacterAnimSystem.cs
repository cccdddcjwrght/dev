using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 角色动画状态更改
    /// </summary>
    [UpdateInGroup(typeof(GameLogicGroup))]
    public class CharacterAnimSystem : ComponentSystem
    {
        private int m_walkName = 0;
        protected override void OnCreate()
        {
            m_walkName = Animator.StringToHash("walking");
        }
        protected override void OnUpdate()
        {
            // 同步移动状态
            Entities.WithAll<CharacterSpawnSystem.CharacterInitalized>().ForEach((Entity e, Character character) =>
            {
                if (character.modelAnimator != null)
                {
                    character.modelAnimator.SetBool(m_walkName, character.isMoving);
                }
            });
        }
    }
}