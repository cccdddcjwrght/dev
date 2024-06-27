using GameConfigs;
using log4net;
using SGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SGame
{
    //店铺显示buff类型
    public enum ShopBuffEnum
    {
        //Exclusive,      //开局buff
        //RoomLike,       //好评buff
        AD_BUFF,           //广告buff
        NO_AD_BUFF,       //去广告buff
        PET_BUFF,         //当前跟随宠物buff
    }

    public class ReputationModule : Singleton<ReputationModule>
    {
        private const float PERCENTAGE_VALUE = 0.01f;
        private static ILog log = LogManager.GetLogger("game.reputation");
        private ReputationData m_data;

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
            m_handles += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, OnEnterRoom);
            m_handles += EventManager.Instance.Reg<BuffData>((int)GameEvent.BUFF_TRIGGER, (b) => RefreshVailedBuffList());

            var buff = GlobalDesginConfig.GetIntArray("no_ads_buff");
            m_TotalList = new List<TotalItem>()
            {
                new TotalItem(){ name = UIListener.Local("ui_boosts_name_1"), type = ShopBuffEnum.AD_BUFF },
                new TotalItem(){ name = UIListener.Local("ui_boosts_name_3"), type = ShopBuffEnum.NO_AD_BUFF, multiple = 1 + buff[1] * PERCENTAGE_VALUE, isEver = true },
                new TotalItem(){ name = UIListener.Local("ui_boosts_name_2"), type = ShopBuffEnum.PET_BUFF, isEver = true },
            };
        }

        void OnEnterRoom(int scene) 
        {
            AddLikeRewardData();
        }

        public void AddLikeRewardData() 
        {
            //好评储存的奖励 进入场景加入数据
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
                    else PropertyManager.Instance.Update(1, i.id, i.num);
                });
                list.Clear();
            }
        }

        //增加好评数量
        public void AddLikeNum(int characterID) 
        {
            int roleId = CharacterModule.Instance.FindCharacter(characterID).roleID;
            int likeNum = (int)AttributeSystem.Instance.GetValueByRoleID(roleId, EnumAttribute.LikeNum);
            DataCenter.Instance.likeData.likeNum += likeNum;
            EventManager.Instance.Trigger((int)GameEvent.ROOM_LIKE_ADD, likeNum);
        }

        public void RefreshVailedBuffList()
        {
            //免广告buff
            m_TotalList.Find((t) => t.type == ShopBuffEnum.NO_AD_BUFF).isForce = DataCenter.ShopUtil.IsIgnoreAd();
            //宠物buff
            var pet_buff = m_TotalList.Find((t) => t.type == ShopBuffEnum.PET_BUFF);
            var pet_item = DataCenter.Instance.petData.pet;
            if (pet_item != null) 
            {
                var list = pet_item.GetEffects(true, false, true);
                float multiple = 0;
                list.ForEach((i) =>
                {
                    //对应buff配置id
                    if (i[0] == 7) multiple = 1 + i[1] * PERCENTAGE_VALUE;
                });
                pet_buff.multiple = multiple;
                pet_buff.isForce = multiple > 0;
            }

            //广告buff
            var buff_time = AdModule.Instance.GetBuffTime();
            var ad_buff = m_TotalList.Find((t) => t.type == ShopBuffEnum.AD_BUFF);
            ad_buff.time = buff_time;
            ad_buff.isForce = buff_time > 0;
            ad_buff.multiple = AdModule.Instance.GetAdRatio();

            EventManager.Instance.Trigger((int)GameEvent.TOTAL_REFRESH);
        }

        public List<TotalItem> GetVailedBuffList() 
        {
            return m_TotalList;
        }

        public float GetTotalValue() 
        {
            float value = 1;
            for (int i = 0; i < m_TotalList.Count; i++)
            {
                if(m_TotalList[i].isForce) value *= m_TotalList[i].multiple;
            }
            return value;
        }

        //获取好评
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
        /// 是否生效
        /// </summary>
        public bool isForce;

        /// <summary>
        /// 类型
        /// </summary>
        public ShopBuffEnum type;

        public void Reset() 
        {
            name        = string.Empty;
            multiple    = 0;
            time        = 0;
            isEver      = false;
        }
    }
}
