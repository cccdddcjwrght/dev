using System;
using System.Collections;
using System.Collections.Generic;
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
            UNINITALIZE = 0,    // 未初始化
            WAIT_TAKE   = 1,    // 等待获取
            TAKED       = 2,   // 已经获取
        }
        
        public int   state;  // 状态
        public int   time;   // 服务器, 秒
    }
    
    public class TomorrowGiftModule : Singleton<TomorrowGiftModule>
    {
        private TomorrowGiftData m_data;
        private static ILog log = LogManager.GetLogger("game.tomorrowGift");
        
        public void Initalize()
        {
            m_data = DataCenter.Instance.m_gameRecord.tomorrowGift;
            if (m_data.state == (int)TomorrowGiftData.STATE.UNINITALIZE)
            {
                // 明天时间
                m_data.state = (int)TomorrowGiftData.STATE.WAIT_TAKE;
                m_data.time = GameServerTime.Instance.nextDayTime;
            }
        }

        /// <summary>
        /// 里可领取的剩余时间(秒)
        /// </summary>
        public int time
        {
            get { 
                var stime = GameServerTime.Instance.serverTime;
                return stime >= m_data.time ? stime - m_data.time : 0; 
            }
        }

        /// <summary>
        /// 获取礼物
        /// </summary>
        public bool TakeGift()
        {
            if (m_data.state != (int)TomorrowGiftData.STATE.WAIT_TAKE)
            {
                log.Error("state not match=" + m_data.state);
                return false;
            }
            
            m_data.state = (int)TomorrowGiftData.STATE.TAKED;
            log.Info("Take TomorrowGift Finish!");
            return true;
        }
    }
}
