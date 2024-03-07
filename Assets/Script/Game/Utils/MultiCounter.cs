using System.Collections.Generic;

namespace SGame
{
    /// <summary>
    /// 多个计数器模块, 0 就删除KEY
    /// </summary>
    public class MultiCounter<TKey>
    {
        /// <summary>
        /// 计数器
        /// </summary>
        public Dictionary<TKey, int> m_counters;

        public MultiCounter(int cap = 32)
        {
            m_counters = new Dictionary<TKey, int>(cap);
        }

        /// <summary>
        /// 获得计数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default_value"></param>
        /// <returns></returns>
        public int Get(TKey key)
        {
            if (m_counters.TryGetValue(key, out int value))
                return value;

            return 0;
        }

        /// <summary>
        /// 添加数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int Add(TKey key, int val)
        {
            if (!m_counters.TryGetValue(key, out int value))
            {
				if (val <= 0) return 0;
                m_counters.Add(key, val);
                return val;
            }

            value += val;
            if (value <= 0)
            {
                m_counters.Remove(key);
                return 0;
            }

            m_counters[key] = value;
            return value;
        }

        /// <summary>
        /// 删除所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return m_counters.Remove(key);
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(TKey key, int value)
        {
            m_counters[key] = value;
        }

        /// <summary>
        /// 统计所有的值
        /// </summary>
        /// <returns></returns>
        public int GetAllValue()
        {
            int count = 0;
            foreach (int v in m_counters.Values)
            {
                count += v;
            }

            return count;
        }

        /// <summary>
        /// 判断是否有值
        /// </summary>
        /// <returns></returns>
        public bool HasValue()
        {
            return m_counters.Count > 0;
        }

        /// <summary>
        /// 清除所有
        /// </summary>
        public void Clear()
        {
            m_counters.Clear();
        }
    }
}