using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace SGame 
{

    [Serializable]
    public class BoostShopData 
    {
        public List<BuffShopData> buffList = new List<BuffShopData>();

        [NonSerialized]
        public Dictionary<int, BuffShopData> buffDict;
    }

    [Serializable]
    public class BuffShopData 
    {
        public int cfgId;
        public int buffID;
        public int value;
        public int type;
        public int shopId;
        public int endTime;

        public int GetTime() 
        {
            var time = endTime - GameServerTime.Instance.serverTime;
            if (time > 0) return time;
            return 0;
        }
    }

    public partial class DataCenter 
    {
        public BoostShopData boostShopData = new BoostShopData();
    }


    public class BuffShopModule : Singleton<BuffShopModule>
    {
        private BoostShopData m_Data { get { return DataCenter.Instance.boostShopData; } }

        private EventHandleContainer m_Event = new EventHandleContainer();

        //���buffid
        private List<int> randomBuffs = new List<int>();
        //�̶�buffid
        private List<int> fixedBuffs = new List<int>();

        public void Initalize() 
        {
            InitBuffShopData();
            InitConfigData();

            m_Event += EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, (s) => LoadShopBuffData());
        }

        public void InitBuffShopData() 
        {
            m_Data.buffDict = m_Data.buffList.ToDictionary(v => v.cfgId);
        }

        public void InitConfigData() 
        {
            var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.BoostShop>();
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                var config = configs.Datalist(i).Value;
                if (config.Type == 1) randomBuffs.Add(config.Id);
                else fixedBuffs.Add(config.Id);
            }
        }

        public void LoadShopBuffData()
        {
            m_Data.buffList.ForEach((v) =>
            {
                if (v.GetTime() > 0) EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData()
                {
                    id = v.buffID,
                    val = v.value,
                    time = v.GetTime(),
                    from = 100000 + v.cfgId
                });
            });
        }

        public int GetRandomShopId() 
        {
            return GetConfig(1).ShopId;
        }

        public BuffShopData GetForceRandomBuffData() 
        {
            return m_Data.buffList.Find((v) => v.type == 1 && v.GetTime() > 0);
        }

        public int GetRandomBuff() 
        {
            List<int> weights = new List<int>();
            randomBuffs.Foreach((v) => weights.Add(GetConfig(v).Weight));
            var index = SGame.Randoms.Random._R.NextWeight(weights) + 1;
            return index;
        }


        public void AddBoostShopBuff(int cfgId) 
        {
            var buff = m_Data.buffList.Find((v) => v.cfgId == cfgId);
            var config = GetConfig(cfgId);
            if (buff == null)
            {
                buff = new BuffShopData()
                {
                    cfgId = cfgId,
                    buffID = config.BuffId(0),
                    value = config.BuffId(1),
                    type = config.Type,
                    shopId = config.ShopId,
                    endTime = GameServerTime.Instance.serverTime + config.BuffTime,
                };
                m_Data.buffList.Add(buff);
            }
            else 
            {
                if (buff.GetTime() > 0) buff.endTime += config.BuffTime;
                else buff.endTime = GameServerTime.Instance.serverTime + config.BuffTime;
            }
            InitBuffShopData();

            int from = 100000 + buff.cfgId;
            EventManager.Instance.Trigger(((int)GameEvent.BUFF_TRIGGER), new BuffData() { id = 0, from = from });
            EventManager.Instance.Trigger((int)GameEvent.BUFF_TRIGGER, new BuffData()
            {
                id = buff.buffID,
                val = buff.value,
                time = buff.GetTime(),
                from = from
            });
            EventManager.Instance.Trigger((int)GameEvent.BUFFSHOP_REFRESH);

        }

        //type:1 ��� 2:�̶�
        public List<int> GetBuffList(int type) 
        {
            if (type == 1) return randomBuffs;
            else return fixedBuffs;
        }


        public GameConfigs.BoostShopRowData GetConfig(int id) 
        {
            if (ConfigSystem.Instance.TryGet<GameConfigs.BoostShopRowData>(id, out var config))
                return config;
            return default;
        }
    }
}


