using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using log4net;
using SGame;
using UnityEngine;

namespace SGame
{
    public class NewbieGiftModule : Singleton<NewbieGiftModule>
    {
        private static int _GOOD_ITEM_ID = -1;
        private static int _OPEN_ID = -1;
        
        /// <summary>
        /// 奖励商品ID
        /// </summary>
        public static int GOOD_ITEM_ID
        {
            get
            {
                if (_GOOD_ITEM_ID < 0)
                {
                    _GOOD_ITEM_ID = GlobalDesginConfig.GetInt("newbie_gift_goods");
                }

                return _GOOD_ITEM_ID;
            }
        }

        /// <summary>
        /// 功能定义ID
        /// </summary>
        public static int OPEN_ID
        {
            get
            {
                if (_OPEN_ID < 0)
                {
                    _OPEN_ID = GlobalDesginConfig.GetInt("newbie_gift_func");
                }

                return _OPEN_ID;
            }
        }

        
        private const string    KEY_NAME = "newibiegift.taked"; // 存储名字
        private static ILog     log = LogManager.GetLogger("game.tomorrowGift");
        private EventHanle m_enterGameEvent;

        
        
        public void Initalize()
        {
            m_enterGameEvent = EventManager.Instance.Reg<int>((int)GameEvent.AFTER_ENTER_ROOM, OnFirstEnterRoom);
        }

        void OnFirstEnterRoom(int levelID)
        {
            m_enterGameEvent.Close();
            m_enterGameEvent = null;

            if (CanTake())
            {
                // 自动打开明日礼包
                log.Info("Open newbiegift");
                DelayExcuter.Instance.DelayOpen("newbiegift", "mainui");
            }
        }

        public bool CanTake()
        {
            return OPEN_ID.IsOpend(false) && DataCenter.GetIntValue(KEY_NAME, 0) == 0;
        }
        
        /// <summary>
        /// 获取礼物
        /// </summary>
        public bool TakeGift()
        {
            if (!CanTake())
                return false;
            
            // 获取礼物
            RequestExcuteSystem.BuyGoods(GOOD_ITEM_ID, (ret) =>
            {
                if (ret)
                {
                    DataCenter.SetIntValue(KEY_NAME, 1);
                    EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_MAIN_REFRESH);
                }
            });
            
            // 设置已获取标记
            return true;
        }
    }
}
