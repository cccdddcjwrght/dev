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
    public class FriendModule : Singleton<FriendModule>
    {
        private static ILog log = LogManager.GetLogger("game.friend");
        private static int HIRE_TIME_INTERVAL = 300; // 下次时间间隔 单位秒
        private static int HIRING_TIME = 100;
        
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
            HIRE_TIME_INTERVAL = GameConfigs.GlobalDesginConfig.GetInt("friend_hire_time", 30);
            HIRING_TIME = GameConfigs.GlobalDesginConfig.GetInt("friend_hiring_time", 10);
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
            int currentTime = GetCurrentTime();
            // 处理好友数据
            bool isFree = currentTime >= m_friendData.nextHireTime;
            foreach (var item in m_friendData.Friends)
            {
                // 好友数据 
                item.state = (int)GetHireState(isFree, item.GetActiveTime(currentTime), item.GetDisableTime(currentTime));
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
        public int GetCurrentTime()
        {
            return GameServerTime.Instance.serverTime;
        }
        
        /// <summary>
        /// 判断是否能雇佣
        /// </summary>
        /// <param name="player_id"></param>
        /// <returns></returns>
        public bool CanHire(int player_id)
        {
            var friend = GetFriendItem(player_id);
            if (friend == null)
            {
                log.Error("player id not found=" + player_id);
                return false;
            }
            
            if (friend.state == (int)FIREND_STATE.RECOMMEND)
                return false;

            int currentTime = GetCurrentTime();
            if (currentTime < m_friendData.nextHireTime)
                return false;

            if (friend.GetDisableTime(currentTime) > 0)
                return false;

            return true;
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
            var serverTime = GetCurrentTime();
            item.hireTime = GameServerTime.Instance.nextDayTime;
            item.hiringTime = serverTime + HIRING_TIME;
            m_friendData.nextHireTime = serverTime + HIRE_TIME_INTERVAL;
            m_friendData.hiringTime = serverTime + HIRING_TIME;
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
                m_friendData.RecommendFriends.RemoveAt(index);
                EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
                return;
            }
            
            log.Error("Remove Friend Fail playerid=" + player_id);
        }

        /// <summary>
        /// 获得雇佣状态
        /// </summary>
        /// <param name="isFree">是否能使用</param>
        /// <param name="hiringTime">雇佣中的时间</param>
        /// <param name="disableTime">禁用时间</param>
        /// <returns></returns>
        public FIREND_STATE GetHireState(bool isFree, int hiringTime, int disableTime)
        {
            if (hiringTime > 0)
                return FIREND_STATE.HIRING;

            if (disableTime > 0)
                return FIREND_STATE.HIRED;

            if (!isFree)
            {
                return FIREND_STATE.HIRE_CD;
            }

            return FIREND_STATE.CAN_HIRE;
        }
    }
}
