
using GameConfigs;

namespace SGame 
{
    /// <summary>
    /// 战斗系数
    /// </summary>
    public class BattleConst
    {
        //暴击伤害倍率
        public static readonly float criticalhit_ratio = GlobalDesginConfig.GetFloat("criticalhit_ratio");
        //连击后降低系数
        public static readonly float doublehit_ratio = GlobalDesginConfig.GetFloat("doublehit_ratio");
        //眩晕回合数
        public static readonly int dizziness_inning = GlobalDesginConfig.GetInt("dizziness_inning");

        public static readonly int max_turncount = 15;          //最大回合数

        public const float move_distance = 100f;
        public const float move_time = 0.5f;
    }
}


