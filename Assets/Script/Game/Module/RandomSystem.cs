using Unity.Mathematics;

namespace SGame
{
    /// <summary>
    /// 随机系统
    /// </summary>
    public class RandomSystem : Singleton<RandomSystem>
    {
        Unity.Mathematics.Random m_rand;

        public void Initalize(uint seed)
        {
            m_rand = new Random(seed);
        }

        /// <summary>
        /// next int
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int NextInt(int min, int max)
        {
            return m_rand.NextInt(min, max);
        }
    }
}