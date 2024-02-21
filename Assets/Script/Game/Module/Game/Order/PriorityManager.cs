using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
    //优先级加权
    public enum PriorityMode
    {
        role=1000,
        distance=10,
        sort=1,
    }
    public class PriorityManager : Singleton<PriorityManager>
    {
        private static ILog log = LogManager.GetLogger("game.order");
        
        private Dictionary<int, Character>  m_datas  = new Dictionary<int, Character>();
        private Dictionary<int, float> m_priorityDatas = new Dictionary<int, float>();//（id,权重值)

        public void Initalize()
        {
            m_datas.Clear();
            m_priorityDatas.Clear();
        }

        /// <summary>
        /// 每初始化一个人物，需要留存数据  
        /// </summary>
        /// <param name="data"></param>
        public void AddRoleData(Character data)
        {
            if (!m_datas.ContainsKey(data.CharacterID))
            {
                m_datas.Add(data.CharacterID,data);
            }
        }

        
        public void SetPriority(int id,int sort,float distance)
        {
            if (m_priorityDatas.ContainsKey(id)&&m_datas.ContainsKey(id))
            {
                m_priorityDatas[id] = 
                    m_datas[id].roleType*(int)PriorityMode.role
                    - distance * (int)PriorityMode.distance;
            }
            else if(!m_priorityDatas.ContainsKey(id))
            {
                m_priorityDatas.Add(id, m_datas[id].roleType*(int)PriorityMode.role
                                        - distance * (int)PriorityMode.distance);
            }
            else
            {
                Debug.LogWarning("人物初始化错误");
            }
        }

        /// <summary>
        /// 输出优先级最大的人物id
        /// 若没有，则输出0
        /// </summary>
        /// <returns></returns>
        public int GetPriorityID()
        {
            // 判断m_datas中所有存在的key值中，m_priorityDatas字典的内容值不为空也不为0
            bool allPrioritiesNonEmpty = m_datas.Keys.All(key => m_priorityDatas.ContainsKey(key) && m_priorityDatas[key] != 0f);
            if (allPrioritiesNonEmpty)
            {
                int key = m_priorityDatas.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                return key;
            }
            return 0;
        }

    }
}