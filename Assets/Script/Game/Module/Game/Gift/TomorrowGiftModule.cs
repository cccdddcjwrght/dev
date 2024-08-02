using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using SGame;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 明日礼包数据
    /// </summary>
    [System.Serializable]
    public class TomorrowGiftData
    {
        public enum STATE : uint
        {
            UNINIT = 0,    // 未初始化
            WAIT_TAKE   = 1,    // 等待获取
            TAKED       = 2,   // 已经获取
        }
        
        public int   state;  // 状态
        public int   time;   // 服务器, 秒
    }
    
    public class TomorrowGiftModule : Singleton<TomorrowGiftModule>
    {
        public const int GOOD_ITEM_ID = 103; // 商品ID
        private TomorrowGiftData m_data;
        private static ILog log = LogManager.GetLogger("game.tomorrowGift");

        public const int OPEN_ID = 18;

        public void Initalize()
        {
            m_data = DataCenter.Instance.m_gameRecord.tomorrowGift;
            UpdateState();
        }

        public void UpdateState()
        {
            // 时间未初始化
            if (m_data.state == (int)TomorrowGiftData.STATE.UNINIT && 
                OPEN_ID.IsOpend(false))
            {
                // 明天时间
                m_data.state = (int)TomorrowGiftData.STATE.WAIT_TAKE;
                m_data.time = GameServerTime.Instance.nextDayTime;
            }
        }

        public bool CanShow()
        {
            return m_data.state == (int)TomorrowGiftData.STATE.WAIT_TAKE;
        }

        /// <summary>
        /// 判断是否可领取
        /// </summary>
        public bool CanTake => m_data.state == (int)TomorrowGiftData.STATE.WAIT_TAKE && time <= 0;


        /// <summary>
        /// 里可领取的剩余时间(秒)
        /// </summary>
        public int time
        {
            get { 
                var stime = GameServerTime.Instance.serverTime;
                return stime < m_data.time ? m_data.time - stime : 0; 
            }
        }

        /// <summary>
        /// 获取礼物
        /// </summary>
        public bool TakeGift()
        {
            if (m_data.state != (int)TomorrowGiftData.STATE.WAIT_TAKE)
            {
                log.Warn("state not match=" + m_data.state);
                return false;
            }
            
            if (time > 0)
            {
                log.Warn("time not zero");
                return false;
            }
            
            RequestExcuteSystem.BuyGoods(GOOD_ITEM_ID);
            m_data.state = (int)TomorrowGiftData.STATE.TAKED;
            
            log.Info("Tomorrow Git Add Success !");
            return true;
        }
    }
}
