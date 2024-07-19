using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using SGame.VS;
using UnityEngine;

namespace SGame
{
    // 图鉴系统
    public class CustomerBookModule : Singleton<CustomerBookModule>
    {
        private CustomerBookReward              m_recordData;
        private List<CustomerBookData>          m_customerBookData;
        public List<CustomerBookData>           Datas => m_customerBookData;

        // 初始化图鉴数据
        public void Initalize()
        {
            // 保存图鉴
            m_recordData = DataCenter.Instance.m_customerBookReward;
            
            // m_recordData.Clear();

            // 初始化图鉴数据
            m_customerBookData = new List<CustomerBookData>();
            var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.RoleData>();
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var conf = configs.Datalist(i);
                if (conf.Value.Book != 0)
                {
                    // 保存图鉴数据
                    m_customerBookData.Add(new CustomerBookData(conf.Value, m_recordData.GetOrCreate(conf.Value.Id)));
                }
            }

            EventManager.Instance.Reg<int,int,int>((int)GameEvent.CHARACTER_CREATE, OnCharacterCreate);
        }

        CustomerBookData GetBookData(int roleID)
        {
            foreach (var item in m_customerBookData)
            {
                if (item.ID == roleID)
                    return item;
            }

            return null;
        }
        
        /// <summary>
        /// 排序
        /// </summary>
        public void ReSort()
        {
            m_customerBookData.Sort((a, b) =>
            {
                // 先排除未解锁的
                if (!a.IsUnlock)
                    return 1;
                if (!b.IsUnlock)
                    return -1;

                return a.ID - b.ID;
            });
        }

        void OnCharacterCreate(int id, int roleID, int roleType)
        {
            if (roleType == (int)EnumRole.Customer)
            {
                if (ConfigSystem.Instance.TryGet(roleID, out RoleDataRowData config))
                {
                    if (config.Book != 0)
                    {
                        // 记录图鉴
                        var record = m_recordData.GetOrCreate(roleID);
                        if (record.isUnlock == false)
                        {
                            record.isUnlock = true;
                            EventManager.Instance.Trigger((int)GameEvent.CUSTOMER_BOOK_UPDATE);
                        }
                        //m_recordData.GetOrCreate(roleID).isUnlock = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// 获取奖励
        /// </summary>
        /// <param name="data"></param>
        public void TakeReward(CustomerBookData data)
        {
            int[] item = data.Config.GetUnlockRewardArray();
            List<int[]> items = new List<int[]>() { item };
            //PropertyManager.Instance.Update(item[0], item[1], item[2]);

            Utils.ShowRewards(items);

            //m_recordData.Rewarded.Add(data.ID);
            data.SetRewared();
            
            EventManager.Instance.Trigger((int)GameEvent.CUSTOMER_BOOK_UPDATE);
        }

        public bool HasNew
        {
            get
            {
                foreach (var d in m_customerBookData)
                {
                    if (d.IsUnlock && d.isOpened == false)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 首次打开需要标记
        /// </summary>
        /// <param name="data"></param>
        public void MarkOpened(CustomerBookData data)
        {
            data.SetOpened();
            EventManager.Instance.Trigger((int)GameEvent.CUSTOMER_BOOK_UPDATE);
        }
    }
}