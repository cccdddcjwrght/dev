using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace SGame
{
    // 骰子恢复系统
    [DisableAutoCreation]
    public partial class DiceRecoverSystem : SystemBase
    {
        // 骰子的显示对象
        private ItemGroup         m_userData;
        private Entity            m_counter;
        private const int DICE_ID = (int)UserType.DICE_POWER;
        private const int MAXDICE_ID = (int)UserType.DICE_MAXPOWER;
        
        public void Initalize(ItemGroup userData)
        {
            m_userData = userData;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((Entity e, ref TimeoutData timeCounter, in DiceRecover recover) =>
            {
                if (timeCounter.Value <= 0)
                {
                    // 时间恢复
                    timeCounter.Value = recover.duration;

                    long diceNum = m_userData.GetNum(DICE_ID);
                    long diceMax = m_userData.GetNum(MAXDICE_ID);
                    long num = Utils.Clamp(diceNum + recover.recoverNum, 0, diceMax);
                    m_userData.SetNum(DICE_ID, num);
                }
            }).WithoutBurst().Run();
        }
    }
}