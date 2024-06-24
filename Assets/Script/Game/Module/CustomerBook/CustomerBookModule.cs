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
        /// 获取奖励
        /// </summary>
        /// <param name="data"></param>
        public void TakeReward(CustomerBookData data)
        {
            int[] item = data.Config.GetUnlockRewardArray();
            List<int[]> items = new List<int[]>() { item };
            PropertyManager.Instance.Update(item[0], item[1], item[2]);

            Utils.ShowRewards(items);

            m_rewardDatas.Values.Add(data.ID);
            data.SetRewared();
            
            EventManager.Instance.Trigger((int)GameEvent.CUSTOMER_BOOK_UPDATE);
        }

        /// <summary>
        /// 首次打开需要标记
        /// </summary>
        /// <param name="data"></param>
        public void MarkOpened(CustomerBookData data)
        {
            
        }
    }
}