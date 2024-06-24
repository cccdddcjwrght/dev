using log4net;
using SGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame
{
    public enum RoomBuffEnum
    {
        Exclusive,      //开局buff
        RoomLike,       //好评buff
        AdBuff,         //广告buff
    }

    public class ReputationModule : Singleton<ReputationModule>
    {
        private const float PERCENTAGE_VALUE = 0.01f;
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

        public GameConfigs.RoomLikeRowData roomLikeData 
        {
            get 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(m_data.cfgId, out var data))
                    return data;
                return default;
            }
        }

        public string icon 
        {
            get 
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(m_data.cfgId, out var data))
                {
                    if(data.BuffIcon != string.Empty) return data.BuffIcon;
                    if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(data.BuffId, out var buffData))
                        return buffData.Icon;
                }
                return "ui_like_praise";
            }
        }


        public void Initalize() 
        {
            m_data = DataCenter.Instance.reputationData;
            m_handles += EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLevelRoom);
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, OnEnterRoom);
            //m_handles += EventManager.Instance.Reg<bool>((int)GameEvent.APP_PAUSE, (pause) =>
            //{
            //    int validTime = DataCenter.ReputationUtils.GetBuffValidTime();
            //    if (validTime <= 0 && m_data.progress >= maxLikeNum)
            //        DataCenter.ReputationUtils.Reset();
            //    EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, 0);
            //});

            m_RoomTypeList = new List<TotalItem>()
            {
                new TotalItem(){ roomBuffEnum = RoomBuffEnum.Exclusive },
                new TotalItem(){ roomBuffEnum = RoomBuffEnum.RoomLike},
                new TotalItem(){ roomBuffEnum = RoomBuffEnum.AdBuff },
            };
        }

        void OnLevelRoom() 
        {
            DataCenter.ReputationUtils.Clear();
        }

        void OnEnterRoom(int scene) 
        {
            var list = DataCenter.Instance.likeData.likeRewardDatas;
            if (list.Count > 0)
            {
                list.ForEach((i) =>
                {
                    if (i.itemType == (int)EnumItemType.ChestKey)
                    {
                        var reward = DataCenter.LikeUtil.GetItemDrop(i.typeId, i.num);
                        reward.ForEach((r) => PropertyManager.Instance.Update(r.type, r.id, r.num));
                    }
                    else 
                    {
                        PropertyManager.Instance.Update(1, i.id, i.num);
                    }
                });
                list.Clear();
            }
        }

        public void AddLikeNum(int characterID) 
        {
            //if (m_data.progress < maxLikeNum)
            //{
                //int likeNum = 1;
                int roleId = CharacterModule.Instance.FindCharacter(characterID).roleID;
                int likeNum = (int)AttributeSystem.Instance.GetValueByRoleID(roleId, EnumAttribute.LikeNum);
                DataCenter.Instance.likeData.likeNum += likeNum;
                //m_data.progress += likeNum;
                //if (m_data.progress >= maxLikeNum) DataCenter.ReputationUtils.RandomSelect();

                EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, likeNum);
            //}    
        }

        public List<TotalItem> GetVailedBuffList()
        {
            m_TotalList.Clear();
            m_RoomTypeList.ForEach((r) => r.Reset());

            var t1 = m_RoomTypeList.Find((t) => t.roomBuffEnum == RoomBuffEnum.Exclusive);
            var exclusiveData = DataCenter.Instance.exclusiveData;
            if (ConfigSystem.Instance.TryGet<GameConfigs.RoomExclusiveRowData>(exclusiveData.cfgId, out var data1))
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(data1.BuffId, out var buffRowData)
                    && buffRowData.Attribute == (int)EnumAttribute.Price)
                {
                    t1.name = data1.BuffName;
                    t1.multiple = 1 + data1.BuffValue * PERCENTAGE_VALUE;
                    t1.time = exclusiveData.endTime - GameServerTime.Instance.serverTime;
                    t1.isEver = data1.BuffDuration <= 0;
                }
            }

            var t2 = m_RoomTypeList.Find((t) => t.roomBuffEnum == RoomBuffEnum.RoomLike);
            var reputationData = DataCenter.Instance.reputationData;
            if (ConfigSystem.Instance.TryGet<GameConfigs.RoomLikeRowData>(reputationData.cfgId, out var data2))
            {
                if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(data2.BuffId, out var buffRowData)
                    && buffRowData.Attribute == (int)EnumAttribute.Price)
                {
                    t2.name = data2.BuffName;
                    t2.multiple = 1 + data2.BuffValue * PERCENTAGE_VALUE;
                    t2.time = reputationData.endTime - GameServerTime.Instance.serverTime;
                    t2.isEver = data2.BuffDuration <= 0;
                }
            }

            var t3 = m_RoomTypeList.Find((t) => t.roomBuffEnum == RoomBuffEnum.AdBuff);
            if (ConfigSystem.Instance.TryGet<GameConfigs.BuffRowData>(AdModule.AD_BUFF_ID, out var data3)) 
            {
                t3.name = UIListener.Local("ui_boosts_name_1");
                t3.multiple = AdModule.Instance.GetAdRatio();
                t3.time = AdModule.Instance.GetBuffTime();
                t3.isEver = false;
            }

            for (int i = 0; i < m_RoomTypeList.Count; i++)
            {
                if (m_RoomTypeList[i].time > 0 || m_RoomTypeList[i].isEver)
                    m_TotalList.Add(m_RoomTypeList[i]);
            }

            return m_TotalList;
        }

        public float GetTotalValue() 
        {
            float value = 1;
            for (int i = 0; i < m_TotalList.Count; i++)
            {
                value *= m_TotalList[i].multiple;
            }
            return value;
        }

        public bool GetLike(OrderData orderData, int characterId) 
        {
            int rate = Random.Range(0, 100);
            var character = CharacterModule.Instance.FindCharacter(characterId);
            ConfigSystem.Instance.TryGet<GameConfigs.RoleDataRowData>(character.roleID, out var cfg);
            var isLikeFood = cfg.GetLikeGoodsArray().ToList().Contains(orderData.foodType);
            if (isLikeFood)
                return rate < cfg.LikeRatio(1);
            else
                return rate < cfg.LikeRatio(0);
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

        public void Reset() 
        {
            name        = string.Empty;
            multiple    = 0;
            time        = 0;
            isEver      = false;

        }
    }
}
