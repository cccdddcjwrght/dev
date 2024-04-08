using log4net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame 
{
    public class ExclusiveModule : Singleton<ExclusiveModule>
    {
        private static ILog log = LogManager.GetLogger("game.exclusive");
        private ExclusiveData m_data;
        private EventHandleContainer m_handles = new EventHandleContainer();

        public void Initalize() 
        {
            m_data = DataCenter.Instance.exclusiveData;
            m_handles += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLevelRoom);
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.AFTER_ENTER_ROOM, OnAfterEnterRoom);
        }

        void OnLevelRoom() 
        {
            DataCenter.ExclusiveUtils.Clear();
        }

        void OnAfterEnterRoom(int levelID) 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(m_data.cfgId, out var data)) 
            {
                bool flag = true;
                if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(m_data.cfgId, out var buffData))
                {
                    if (buffData.Attribute == (int)EnumAttribute.LevelGold)
                        flag = false;
                }

                if (flag) 
                {
                    double endTime = m_data.time + data.BuffDuration;
                    bool isAdd = endTime > GameServerTime.Instance.serverTime;
                    if ((isAdd || data.BuffDuration <= 0))
                    {
                        EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData(data.BuffId, data.BuffValue, 0, (int)(endTime - GameServerTime.Instance.serverTime))
                        { from = DataCenter.ExclusiveUtils.EXCLUSIVE_FORM });
                    }
                }
                EventManager.Instance.Trigger((int)GameEvent.ROOM_START_BUFF);
            } 
            
            if (CheckOpen())
                DelayExcuter.Instance.DelayOpen("exclusiveui", "mainui");

        }

        bool CheckOpen() 
        {
            //return false;
            return m_data.cfgId == 0 && "exclusiveui".IsOpend(false);
        }
    }
}


