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

        /// <summary>
        /// 删除该人物的优先级
        /// </summary>
        /// <param name="id"></param>
        public void RemovePriorityData(int id)
        {
            if(m_priorityDatas.ContainsKey(id))
            {
                m_priorityDatas.Remove(id);
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
            else if(!m_priorityDatas.ContainsKey(id)&&m_datas.ContainsKey(id))
            {
                m_priorityDatas.Add(id, m_datas[id].roleType*(int)PriorityMode.role
                                        - distance * (int)PriorityMode.distance);
            }
           
        }

        /// <summary>
        /// 输出优先级最大的人物id
        /// 若没有，则输出0
        /// </summary>
        /// <returns></returns>
        public int GetPriorityID()
        {
            if (m_priorityDatas.Count == 0 || m_datas.Count == 0)
            {
                return 0;
            }
            int key = m_priorityDatas.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            return key;
        }

    }
}