using System;
using System.Collections;
using System.Collections.Generic;
using libx;
using log4net;
using UnityEngine;

namespace SGame.Firend
{

    /// <summary>
    /// 好友模块 
    /// </summary>
    public class FirendModule : Singleton<FirendModule>
    {
        private static ILog log = LogManager.GetLogger("game.friend");
        private const int HIRE_TIME_INTERVAL = 300; // 下次时间间隔 单位秒
        
        /// <summary>
        /// 好友数据
        /// </summary>
        private FriendData m_friendData;
        
        void TestJsonData()
        {
            const string fileName = "Assets/BuildAsset/Json/TestFriendData.txt.bytes";
            var req = Assets.LoadAsset(fileName, typeof(TextAsset));
            var data = (req.asset as TextAsset).text;

            m_friendData = JsonUtility.FromJson<FriendData>(data);
        }

        /// <summary>
        /// 初始话
        /// </summary>
        public void Initalize()
        {
            TestJsonData();
        }
        
        public void SetData(FriendData data)
        {
            m_friendData = data;
        }
        

        /// <summary>
        /// 更新好友状态
        /// </summary>
        public void UpdateFriends()
        {
            // 处理好友数据
            foreach (var item in m_friendData.Friends)
            {
                // 好友数据 
                item.state = (int)GetHireState(item.hireTime);
            }
            
            // 对好友排序, 先按雇佣状态 再按关卡通关数
            m_friendData.Friends.Sort((item1, item2) =>
            {
                if (item1.state == item2.state)
                {
                    return item1.passLevel >= item2.passLevel ? -1 : 1;
                }

                return item1.state < item2.state ? -1 : 1;
            });

            // 处理邀请数据
            foreach (var item in m_friendData.RecommendFriends)
            {
                item.state = (int)FIREND_STATE.RECOMMEND;
                item.hireTime = 0;
            }
        }

        /// <summary>
        /// 获得好友数据
        /// </summary>
        /// <returns></returns>
        public FriendData GetDatas()
        {
            return m_friendData;
        }

        /// <summary>
        /// 查找好友信息
        /// </summary>
        /// <param name="player_id"></param>
        /// <returns></returns>
        public FirendItemData GetFriendItem(int player_id)
        {
            var index = FindFirend(m_friendData.Friends, player_id);
            if (index >= 0)
            {
                return m_friendData.Friends[index];
            }

            index = FindFirend(m_friendData.RecommendFriends, player_id);
            if (index >= 0)
                return m_friendData.RecommendFriends[index];

            return null;
        }

        /// <summary>
        /// 通关ID查找好友
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="player_id"></param>
        /// <returns></returns>
        static int FindFirend(List<FirendItemData> datas, int player_id)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                var item = datas[i];
                if (item.player_id == player_id)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 同意好友邀请
        /// </summary>
        /// <param name="player_id"></param>
        public void AddFriend(int player_id)
        {
            var index = FindFirend(m_friendData.RecommendFriends, player_id);
            if (index < 0)
            {
                log.Error("player_id not found=" + player_id);
                return;
            }
            
            m_friendData.Friends.Add(m_friendData.RecommendFriends[index]);
            m_friendData.RecommendFriends.RemoveAt(index);
            EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public long GetCurrentTime()
        {
            return DateTime.Now.Ticks;
        }

        /// <summary>
        /// 获得下次雇佣时间
        /// </summary>
        /// <param name="hireTime"></param>
        /// <returns></returns>
        public long GetNextHireTime(long hireTime)
        {
            return hireTime + new TimeSpan(0, 0, HIRE_TIME_INTERVAL).Ticks;
        }

        /// <summary>
        /// 雇佣好友
        /// </summary>
        /// <param name="player_id"></param>
        public void HireFriend(int player_id)
        {
            var index = FindFirend(m_friendData.Friends, player_id);
            if (index < 0)
            {
                log.Error("player id not found=" + player_id);
                return;
            }

            var item = m_friendData.Friends[index];
            item.hireTime = GetCurrentTime();
            m_friendData.nextHireTime = GetNextHireTime(item.hireTime);// + new TimeSpan()
            EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
        }

        /// <summary>
        /// 删除好友, 包含推荐好友 
        /// </summary>
        /// <param name="player_id"></param>
        public void RemoveFriend(int player_id)
        {
            var index = FindFirend(m_friendData.Friends, player_id);
            if (index >= 0)
            {
                m_friendData.Friends.RemoveAt(index);
                EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
                return;
            }
            
            index = FindFirend(m_friendData.RecommendFriends, player_id);
            if (index >= 0)
            {
                m_friendData.RecommendFriends.RemoveAt(player_id);
                EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
                return;
            }
            
            log.Error("Remove Friend Fail playerid=" + player_id);
        }

        /// <summary>
        /// 24小时刷新雇佣时间
        /// </summary>
        /// <param name="lastHireTiem"></param>
        /// <returns></returns>
        public bool CanHire24Hour(long lastHireTime)
        {
            long serverTime = GetCurrentTime();
            var d1 = new DateTime(serverTime);
            //d1.DayOfYear
            var d2 = new DateTime(lastHireTime);
            if (d1.Date == d2.Date)
            {
                // 日期相同, 不能雇佣
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获得雇佣状态
        /// </summary>
        /// <param name="hireTime"></param>
        /// <returns></returns>
        public FIREND_STATE GetHireState(long hireTime)
        {
            // 24小时内已经雇佣了
            if (!CanHire24Hour(hireTime))
            {
                // 判断是否在雇佣CD中 
                if (hireTime < m_friendData.nextHireTime)
                {
                    // 正在雇佣
                    return FIREND_STATE.HIRING;
                }
                
                // 已经雇佣过了
                return FIREND_STATE.HIRED;
            }
            

            // 雇佣在CD中
            if (hireTime < m_friendData.nextHireTime)
            {
                return FIREND_STATE.HIRE_CD;
            }
            
            // 可以雇佣
            return FIREND_STATE.CAN_HIRE;
        }
    }
}
