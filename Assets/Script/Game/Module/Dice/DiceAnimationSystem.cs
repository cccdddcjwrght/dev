using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    // 骰子动画播放
    [GenerateAuthoringComponent]
    public struct DiceAnimation : IComponentData
    {
        public enum AnimState : uint
        {
            STOP    = 0,  // 没有播放
            PLAYING = 1,  // 播放中
            RESULT  = 2,  // 显示结果中
        }
        
        // 动画是否正在播放
        public AnimState          state;
        
        // 动画播放时间
        public float        time;

        // 旋转速度
        public float        speed;
        
        // 旋转目标
        public quaternion   rotation;

        public bool isPlaying
        {
            get
            {
                return state != AnimState.STOP;
            }
        }
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
            Entities.WithAll<DiceSpawnSystem.Initalized>().ForEach((Entity e, ref Rotation rot, ref DiceAnimation anim, in DiceData dice) =>
            {
                if (anim.isPlaying == false)
                    return;
                
                float r    = deltaTime * anim.speed;
                rot.Value  = math.slerp(rot.Value, anim.rotation, r);
                float dt   = math.dot(rot.Value, anim.rotation);
                anim.time -= deltaTime;

                if (anim.time <= 0 && anim.state == DiceAnimation.AnimState.PLAYING)
                {
                    // 筛子目标设为最后一个
                    int face          = m_random.NextInt(0, 4);
                    anim.rotation     = DiceData.GetQuation(dice.Value, face);
                    anim.state        = DiceAnimation.AnimState.RESULT;
                    anim.time         = 0;
                }

                // 结束骰子动画动画
                if ((1 - math.abs(dt)) <= 0.0001)
                {
                    if (anim.state == DiceAnimation.AnimState.PLAYING)
                    {
                        int dice_value = m_random.NextInt(1, 7);
                        int face = m_random.NextInt(0, 4);
                        anim.rotation = DiceData.GetQuation(dice_value, face);
                    }
                    else
                    {
                        rot.Value = anim.rotation;
                        anim.state = DiceAnimation.AnimState.STOP;
                    }
                }
            }).WithoutBurst().Run();
        }
    }
}