using Unity.Entities;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 角色动画状态更改
    /// </summary>
    [UpdateInGroup(typeof(GameLogicGroup))]
    public partial class CharacterAnimSystem : SystemBase
    {
        private int m_walkName = 0;
        private int m_weightName = 0;
        
        protected override void OnCreate()
        {
            m_walkName = Animator.StringToHash("walking");
            m_weightName = Animator.StringToHash("weight");
        }
        
        protected override void OnUpdate()
        {
            // 同步移动状态
            Entities.WithAll<CharacterSpawnSystem.CharacterInitalized>().ForEach((Entity e, Character character) =>
            {
                if (character.modelAnimator != null)
                {
                    character.modelAnimator.SetBool(m_walkName, character.isMoving);
                    character.modelAnimator.SetFloat(m_weightName, character.m_food == Entity.Null ? 0 : 1);
                }
            }).WithoutBurst().Run();
        }
    }
}