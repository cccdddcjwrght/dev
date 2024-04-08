using log4net;
using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public class ReputationModule : Singleton<ReputationModule>
    {
        private static ILog log = LogManager.GetLogger("game.reputation");
        private ReputationData m_data;

        private EventHandleContainer m_handles = new EventHandleContainer();

        public int maxLikeNum 
        {
            get 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.LevelRowData>(DataCenter.Instance.roomData.roomID, out var data))
                    return data.LikeNum;
                return 0;
            }
        }

        public void Initalize() 
        {
            m_data = DataCenter.Instance.reputationData;
            m_handles += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLevelRoom);

            if (DataCenter.ReputationUtils.GetBuffValidTime() > 0)
                DataCenter.ReputationUtils.AddBuff(m_data.cfgId);
        }

        void OnLevelRoom() 
        {
            DataCenter.ReputationUtils.Reset();
        }

        public void AddLikeNum(int characterID) 
        {
            int roomID = DataCenter.Instance.roomData.roomID;
            if (ConfigSystem.Instance.TryGet<GameConfigs.LevelRowData>(roomID, out var levelData)) 
            {
                if (m_data.progress < levelData.LikeNum)
                {
                    //int likeNum = (int)AttributeSystem.Instance.GetValueByRoleID(characterID, EnumAttribute.LikeNum);
                    //m_data.progress += likeNum;
                    m_data.progress += 1;
                    if (m_data.progress >= levelData.LikeNum) DataCenter.ReputationUtils.RandomSelect();

                    EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD);
                }    
            }
        }

    }
}
