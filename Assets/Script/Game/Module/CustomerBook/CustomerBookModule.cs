using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;

namespace SGame
{
    // 图鉴系统
    public class CustomerBookModule : Singleton<CustomerBookModule>
    {
        private CustomerBookReward              m_rewardDatas;
        private List<CustomerBookData>          m_customerBookData;
        public List<CustomerBookData>           Datas => m_customerBookData;

        // 初始化图鉴数据
        public void Initalize()
        {
            // 保存图鉴
            m_rewardDatas = DataCenter.Instance.m_customerBookReward;

            // 初始化图鉴数据
            m_customerBookData = new List<CustomerBookData>();
            var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.RoleData>();
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var conf = configs.Datalist(i);
                if (conf.Value.Book != 0)
                {
                    // 保存图鉴数据
                    m_customerBookData.Add(new CustomerBookData(conf.Value, IsRewarded(conf.Value.Id)));
                }
            }
        }

        /// <summary>
        /// 是否有奖励
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        bool IsRewarded(int roleID)
        {
            return m_rewardDatas.Values.Contains(roleID);
        }

        /// <summary>
        /// 添加奖励
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool AddReward(int roleID)
        {
            m_rewardDatas.Values.Add(roleID);
            return true;
        }
    }
}