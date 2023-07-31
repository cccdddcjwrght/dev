using Unity.Entities;
using Unity.Mathematics;

namespace SGame
{
    [GenerateAuthoringComponent]
    public struct Snake : IComponentData
    {
        // 最小每步移動距離
        public float m_step;

        // 貪吃蛇軀體數量
        public int m_bodyNum;

        // 每個身體占用多少個路徑點
        public int m_boneNum;

        // 貪吃蛇開始位置 
        public float2 m_startPositon;

        // 貪吃蛇移動路徑數量
        public int bonesCount
        {
            get
            {
                return m_boneNum * m_bodyNum;
            }
        }
    }
}