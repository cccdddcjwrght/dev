using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using libx;
using log4net;
using UnityEngine;

namespace SGame.Firend
{

    /// <summary>
    /// 好友模块 
    /// </summary>
    public class FriendModule : MonoSingleton<FriendModule>
    {
        private static ILog log = LogManager.GetLogger("game.friend");
        private static int HIRE_TIME_INTERVAL = 300; // 下次时间间隔 单位秒
        private static int HIRING_TIME = 100;
        private const int OPEN_ID = 14;

        /// <summary>
        /// 好友数据
        /// </summary>
        private FriendData m_friendData { get { return SGame.DataCenter.Instance.m_friendData; } }
        private Dictionary<long, FriendItemData> m_hirstory = new Dictionary<long, FriendItemData>();

        /// <summary>
        /// 判断是否开启
        /// </summary>
        /// <returns></returns>
        public static bool IsOpened()
        {
            return OPEN_ID.IsOpend(false);
        }

        /// <summary>
        /// 判断好友客人是否开启
        /// </summary>
        /// <returns></returns>
        public static bool IsCustomerFriendOpened()
        {
            return IsOpened() && ((int)FunctionID.FRIEND_CUSTOMER).IsOpend(false);
        }

        void TestJsonData()
        {
            if (m_friendData == null || (m_friendData.RecommendFriends.Count == 0 && m_friendData.Friends.Count == 0))
            {
                const string fileName = "Assets/BuildAsset/Json/TestFriendData.txt.bytes";
                var req = Assets.LoadAsset(fileName, typeof(TextAsset));
                var data = (req.asset as TextAsset).text;

                DataCenter.Instance.m_friendData = JsonUtility.FromJson<FriendData>(data);
            }
        }

        /// <summary>
        /// 整体剩余倒计时
        /// </summary>
        public int coldTime
        {
            get
            {
                int current = GameServerTime.Instance.serverTime;
                return current >= m_friendData.nextHireTime ? 0 : m_friendData.nextHireTime - current;
            }
        }

        /// <summary>
        /// 获取雇佣剩余时间
        /// </summary>
        public int hiringTime
        {
            get
            {
                int current = GameServerTime.Instance.serverTime;
                return current >= m_friendData.hiringTime ? 0 : m_friendData.hiringTime - current;
            }
        }

        /// <summary>
        /// 初始话
        /// </summary>
        public void Initalize()
        {
            HIRE_TIME_INTERVAL = GameConfigs.GlobalDesginConfig.GetInt("friend_hire_time", 30);
            HIRING_TIME = GameConfigs.GlobalDesginConfig.GetInt("friend_hiring_time", 10);
            TestJsonData();
            
            // 添加好友历史记录
            foreach (var f in m_friendData.RecommendFriends)
                m_hirstory.Add(f.player_id, f);
            foreach (var f in m_friendData.Friends)
                m_hirstory.Add(f.player_id, f);

            EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, OnEventBeforeEnterRoom);
        }

        void OnEventBeforeEnterRoom(int roomID)
        {
            /// 重新发送好友雇佣信息
            ReSendFriendHiring();
        }

        /// <summary>
        /// 重新发送好友雇佣信息
        /// </summary>
        void ReSendFriendHiring()
        {
            int currentTime = GetCurrentTime();
            if (currentTime >= m_friendData.hiringTime)
                return;
            
            foreach (var friend in m_friendData.Friends)
            {
                if (currentTime < friend.hiringTime)
                {
                    /// 只有一个满足
                    EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_HIRING, friend.player_id, GetLevelRoleID(), GetRoleData(friend.player_id));
                    log.Info("Friend Resend Hiring ID = " + friend.player_id);
                    return;
                }
            }
        }
        
        public void SetData(FriendData data)
        {
            DataCenter.Instance.m_friendData = data;
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
        public FriendItemData GetFriendItem(long player_id)
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
        static int FindFirend(List<FriendItemData> datas, long player_id)
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
        public void AddFriend(long player_id)
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
        /// 可添加好友数量
        /// </summary>
        /// <returns></returns>
        public int GetRecommendNum()
        {
            return m_friendData.RecommendFriends.Count;
        }

        /// <summary>
        /// 是否拥有可雇佣好友
        /// </summary>
        /// <returns></returns>
        public bool HasCanHirePlayer()
        {
            foreach (var f in m_friendData.Friends)
            {
                if (CanHire(f.player_id))
                    return true;
            }

            return false;
        }
        
        
        /// <summary>
        /// 判断是否能雇佣
        /// </summary>
        /// <param name="player_id"></param>
        /// <returns></returns>
        public bool CanHire(long player_id)
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
        /// 获得当前关卡的角色ID
        /// </summary>
        /// <returns></returns>
        static int GetLevelRoleID()
        {
            int levelID = DataCenter.Instance.GetUserData().scene;
            if (!ConfigSystem.Instance.TryGet(levelID, out LevelRowData config))
            {
                log.Error("not found level ID=" + levelID);
                return 0;
            }

            return config.PlayerId;
        }

        /// <summary>
        /// 雇佣好友
        /// </summary>
        /// <param name="player_id"></param>
        public void HireFriend(long player_id)
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
            m_friendData.hiringPlayerID = player_id;
            EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_DATE_UPDATE);
            EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_HIRING, player_id, GetLevelRoleID(), GetRoleData(player_id));
            EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_MAIN_REFRESH);
        }

        /// <summary>
        /// 删除好友, 包含推荐好友 
        /// </summary>
        /// <param name="player_id"></param>
        public void RemoveFriend(long player_id)
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
        
        /// <summary>
        /// 好友结构转角色结构
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public RoleData GetRoleData(long playerID)
        {
            var item = FriendModule.Instance.GetFriendInHirstory(playerID);

            List<BaseEquip> equips = new List<BaseEquip>();

            foreach (var e in item.equips)
            {
                BaseEquip equip = new BaseEquip()
                {
                    cfgID = e.id,
                    level = e.level,
                    quality = e.quality,
                };
                equip.Refresh();
                equips.Add(equip);
            }

            RoleData roleData = new RoleData()
            {
                roleTypeID = item.roleID,
                isEmployee = true,
                equips = equips
            };
            return roleData;
        }

        /// <summary>
        /// 获取正在雇佣的好友信息
        /// </summary>
        /// <returns></returns>
        public FriendItemData GetHiringFriend()
        {
            if (m_hirstory.TryGetValue(m_friendData.nextHireTime, out FriendItemData ret))
                return ret;
            
            var currentTime = GetCurrentTime();
            foreach (var friend in m_friendData.Friends)
            {
                if (friend.hiringTime > currentTime)
                {
                    return friend;
                }
            }

            return null;
        }

        private void Update()
        {
            if (m_friendData != null)
            {
                if (m_friendData.hiringTime > 0 && m_friendData.hiringTime <= GetCurrentTime())
                {
                    m_friendData.hiringTime = 0;
                    m_friendData.hiringPlayerID = 0;
                    EventManager.Instance.AsyncTrigger((int)GameEvent.FRIEND_HIRING_TIMEOUT);
                }
            }
        }

        /// <summary>
        /// 获取历史出现过的好友纪律
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public FriendItemData GetFriendInHirstory(long playerID)
        {
            if (m_hirstory.TryGetValue(playerID, out FriendItemData friendData))
            {
                return friendData;
            }

            return null;
        }

        /// <summary>
        /// 是否拥有好友顾客
        /// </summary>
        /// <returns></returns>
        public bool HasFriend()
        {
            return m_friendData.Friends.Count > 0;
        }

        /// <summary>
        /// 是否有新的推荐好友
        /// </summary>
        /// <returns></returns>
        public bool HasNewRecommend()
        {
            var datas = m_friendData;
            foreach (var item in datas.RecommendFriends)
            {
                if (item.isNew)
                    return true;
            }
            
            return false;
        }

        // 刷新推荐
        public void RefreshRecommend()
        {
            var datas = m_friendData;
            foreach (var item in datas.RecommendFriends)
            {
                item.isNew = false;
            }
        }

        /// <summary>
        /// 红点条件判定
        /// </summary>
        /// <returns></returns>
        public bool RedDot()
        {
            if (HasNewRecommend())
            {
                return true;
            }
            if (HasCanHirePlayer())
                return true;

            return false;
        }
    }
}
