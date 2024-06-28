using Unity.Mathematics;
using System.Collections.Generic;
using log4net;
using Sirenix.Utilities;
using Unity.VisualScripting;

namespace SGame
{
    /// <summary>
    /// 随机系统
    /// </summary>
    public class RandomSystem : Singleton<RandomSystem>
    {
	    private static ILog log = LogManager.GetLogger("game.random");
	    
        Unity.Mathematics.Random m_rand;
        private List<int> m_cache1 = new List<int>();
        private List<int> m_cache2 = new List<int>();

        
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

        public double NextDouble(double min, double max)
        {
            return m_rand.NextDouble(min, max);
        }

        /// <summary>
        /// 获得不可重复的随机 ID 数据
        /// </summary>
        /// <param name="inputIDs">随机的ID对象</param>
        /// <param name="widgets">权重</param>
        /// <param name="num">随机出的数量</param>
        /// <param name="outputIDs"></param>
        /// <returns></returns>
        public bool GetRandomIDs(List<int> inputIDs, List<int> widgets, int num, List<int> outputIDs)
        {
	        outputIDs.Clear();
	        if (num <= 0)
		        return false;
	        
	        // 数量太多， 直接返回
	        if (num >= inputIDs.Count)
		        outputIDs.AddRange(inputIDs);
	        
	        // 放置数据被修改了
	        m_cache1.Clear();
	        m_cache2.Clear();
	        m_cache1.AddRange(inputIDs);
	        m_cache2.AddRange(widgets);
	        inputIDs = m_cache1;
	        widgets = m_cache2;
	        
	        for (int i = 0; i < num; i++)
	        {
		        // 不可重复
		        int index = GetRandomIndex(inputIDs, widgets, inputIDs.Count - i);
		        if (index < 0)
			        return false;

		        // 交换最后一个
		        outputIDs.Add(inputIDs[index]);
		        
		        // 交换到最后一个
		        Utils.SwitchRemove(inputIDs, index);
	        }

	        return true;
        }
        
        /// <summary>
        /// 获得一个随机ID 的索引
        /// </summary>
        /// <param name="inputIDs">ID</param>
        /// <param name="widgets">权重</param>
        /// <returns></returns>
        public int GetRandomIndex(List<int> inputIDs, List<int> weights, int len)
        {
	        if (inputIDs == null || inputIDs.Count < len)
	        {
		        log.Error("input role ids is null or zero");
		        return -1;
	        }

	        if (weights != null && weights.Count != inputIDs.Count)
	        {
		        log.Error("weight not match");
		        return -1;
	        }
                
	        // 计算随机数
	        if (weights == null || weights.Count < len || weights.Count == 0)
	        {
		        int randIndex = RandomSystem.Instance.NextInt(0, len);
		        return randIndex;
	        } 
                
	        // 计算
	        int allCount = 0;
	        for (int i = 0; i < len; i++)
		        allCount += weights[i];

	        int randValue = NextInt(0, allCount);
	        int count = 0;
	        for (int i = 0; i < len; i++)
	        {
		        count += weights[i];
		        if (randValue < count)
		        {
			        return i;
		        }
	        }

	        return len - 1;
        }
        
        /// <summary>
        /// 获得一个随机ID
        /// </summary>
        /// <param name="inputIDs">ID</param>
        /// <param name="widgets">权重</param>
        /// <returns></returns>
        public int GetRandomID(List<int> inputIDs, List<int> weights)
        {
	        int index = GetRandomIndex(inputIDs, weights, inputIDs.Count);
	        if (index >= 0)
		        return inputIDs[index];

	        return inputIDs[0];
        }

    }
}