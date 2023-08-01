using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    public struct DiceAnimation : IComponentData
    {
        // 动画播放时间
        public float       playSecond;

        // 旋转速度
        public float        rotationSpeed;
        
        // 旋转目标
        public quaternion   target;
    }
    
    // 骰子动画系统
    [DisableAutoCreation]
    public partial class DiceAnimationSystem : SystemBase
    {
        // 骰子的显示对象
        private ResourceManager          m_resourceManager ;
        private GameWorld                m_world;
        private Unity.Mathematics.Random m_random;
        
        public void Initalize(GameWorld world, ResourceManager resourceManager)
        {
            m_world           = world;
            m_resourceManager = resourceManager;
            m_random = new Unity.Mathematics.Random((uint)Time.ElapsedTime);
        }
        
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities.WithAll<DiceSpawnSystem.Initalized>().ForEach((Entity e, ref Rotation rot, ref DiceAnimation anim, ref DiceData dice) =>
            {
                if (anim.playSecond <= 0)
                    return;

                anim.playSecond -= deltaTime;
                if (anim.playSecond <= 0)
                {
                    anim.playSecond = 3;
                    rot.Value = DiceData.GetQuation(dice.Value);
                    dice.Value = (dice.Value + 1) % 6;
                }
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}