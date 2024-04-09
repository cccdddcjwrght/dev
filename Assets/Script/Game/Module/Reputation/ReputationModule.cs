using log4net;
using SGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGame
{
    public enum RoomBuffEnum
    {
        Exclusive,      //开局buff
        RoomLike,       //好评buff
    }

    public class ReputationModule : Singleton<ReputationModule>
    {
        private static ILog log = LogManager.GetLogger("game.reputation");
        private ReputationData m_data;

        private List<TotalItem> m_RoomTypeList;
        private List<TotalItem> m_TotalList = new List<TotalItem>();
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

        public string icon 
        {
            get 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(m_data.cfgId, out var data))
                    return data.BuffIcon;
                return "ui_like_praise";
            }
        }


        public void Initalize() 
        {
            m_data = DataCenter.Instance.reputationData;
            m_handles += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLevelRoom);
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, OnEnterRoom);

            m_RoomTypeList = new List<TotalItem>()
            {
                new TotalItem(){ roomBuffEnum = RoomBuffEnum.Exclusive },
                new TotalItem(){ roomBuffEnum = RoomBuffEnum.RoomLike},
            };
        }

        void OnLevelRoom() 
        {
            DataCenter.ReputationUtils.Clear();
        }

        void OnEnterRoom(int scene) 
        {
            int validTime = DataCenter.ReputationUtils.GetBuffValidTime();
            if (validTime > 0)
                DataCenter.ReputationUtils.AddBuff(m_data.cfgId);

            if (validTime <= 0 && m_data.progress >= maxLikeNum)
                DataCenter.ReputationUtils.Reset();
        }

        public void AddLikeNum(int characterID) 
        {
            if (m_data.progress < maxLikeNum)
            {
                //int likeNum = 1;
                int likeNum = (int)AttributeSystem.Instance.GetValueByRoleID(characterID, EnumAttribute.LikeNum);
                m_data.progress += likeNum;
                if (m_data.progress >= maxLikeNum) DataCenter.ReputationUtils.RandomSelect();

                EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, likeNum);
            }    
        }

        public List<TotalItem> GetVailedBuffList()
        {
            m_TotalList.Clear();

            var t1 = m_RoomTypeList.Find((t) => t.roomBuffEnum == RoomBuffEnum.Exclusive);
            var exclusiveData = DataCenter.Instance.exclusiveData;
            if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(exclusiveData.cfgId, out var data1))
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(data1.BuffId, out var buffRowData)
                    && buffRowData.Attribute == (int)EnumAttribute.Price)
                {
                    t1.name = data1.BuffName;
                    t1.multiple = data1.BuffValue;
                    t1.time = exclusiveData.endTime;
                    t1.isEver = data1.BuffDuration <= 0;
                }
            }

            var t2 = m_RoomTypeList.Find((t) => t.roomBuffEnum == RoomBuffEnum.RoomLike);
            var reputationData = DataCenter.Instance.reputationData;
            if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(reputationData.cfgId, out var data2))
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(data2.BuffId, out var buffRowData)
                    && buffRowData.Attribute == (int)EnumAttribute.Price)
                {
                    t2.name = data2.BuffName;
                    t2.multiple = data2.BuffValue;
                    t2.time = reputationData.endTime;
                    t2.isEver = data2.BuffDuration <= 0;
                }
            }

            for (int i = 0; i < m_RoomTypeList.Count; i++)
            {
                if (m_RoomTypeList[i].time > GameServerTime.Instance.serverTime ||
                    m_RoomTypeList[i].isEver)
                    m_TotalList.Add(m_RoomTypeList[i]);
            }

            return m_TotalList;
        }

        public float GetTotalValue() 
        {
            float value = 0;
            for (int i = 0; i < m_TotalList.Count; i++)
            {
                value += m_TotalList[i].multiple;
            }
            return value;
        }
    }


    public class TotalItem
    {
        public string name;
        /// <summary>
        /// 倍数
        /// </summary>
        public float multiple;

        /// <summary>
        /// 剩余时间
        /// </summary>
        public int time;

        /// <summary>
        /// 是否永久
        /// </summary>
        public bool isEver;

        /// <summary>
        /// 类型
        /// </summary>
        public RoomBuffEnum roomBuffEnum;
    }
}
