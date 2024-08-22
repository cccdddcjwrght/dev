
using GameConfigs;

namespace SGame 
{
    /// <summary>
    /// ս��ϵ��
    /// </summary>
    public class BattleConst
    {
        //�����˺�����
        public static readonly float criticalhit_ratio = GlobalDesginConfig.GetFloat("criticalhit_ratio");
        //�����󽵵�ϵ��
        public static readonly float doublehit_ratio = GlobalDesginConfig.GetFloat("doublehit_ratio");
        //ѣ�λغ���
        public static readonly int dizziness_inning = GlobalDesginConfig.GetInt("dizziness_inning");

        public static readonly int max_turncount = 15;          //���غ���

        public const float move_distance = 100f;
        public const float move_time = 0.5f;
    }
}


