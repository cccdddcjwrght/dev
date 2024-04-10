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
        public const int        GOOD_ITEM_ID = 104;             // 商品ID
        private const string    KEY_NAME = "newibiegift.taked"; // 存储名字
        private static ILog     log = LogManager.GetLogger("game.tomorrowGift");
        private EventHanle m_enterGameEvent;

        private const int OPEN_ID = 18;
        
        
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
            RequestExcuteSystem.BuyGoods(GOOD_ITEM_ID);
            
            // 设置已获取标记
            DataCenter.SetIntValue(KEY_NAME, 1);
            return true;
        }
    }
}
