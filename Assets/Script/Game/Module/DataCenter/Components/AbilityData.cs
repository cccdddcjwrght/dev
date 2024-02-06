using System;
using System.Collections;
using System.Collections.Generic;
using GameConfigs;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGame
{
    public class AbilityData
    {
        public int len;
        [Serializable]
        public struct AbilitLevelRenderer
        {
            public int level;
            public int CurLevelValue;
            public int NextLevelValue;
            public int BuffType;//buff类型
            public int[] BuyData;
        }

        [Serializable]
        public class AbilityList
        {
            public int    ID;
            public string VaultIcon;
            public string VaultDes;
            public int    LevelIndex=0;//等级
            public bool   IsLock=false;
            public int[]  LockData;
            public List<AbilitLevelRenderer> abilitLevelList = new List<AbilitLevelRenderer>();
        }

        private List<AbilityList> abilityList = new List<AbilityList>();
        
        //科技列表初始化
        public void InitAbilityList()
        {
            var abilityListConfig = ConfigSystem.Instance.LoadConfig<GameConfigs.AbilityList>(); //科技列表
            len = abilityListConfig.DatalistLength;
            for (int i = 0; i < len; i++)
            {
                var rowData = abilityListConfig.Datalist(i);
                AddAbilityListRenderer(rowData.Value.Id,
                    rowData.Value.VaultIcon,
                    rowData.Value.VaultDes,
                    rowData.Value.GetLockCostArray(),
                    rowData.Value.LevelId(0),
                    rowData.Value.LevelId(1)
                    
                );
            }
        }

        //获取科技列表
        public List<AbilityList> GetAbilityList()
        {
            return abilityList;
        }

        /// <summary>
        /// 表格注入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vaultIcon"></param>
        /// <param name="firstId"></param>
        /// <param name="lastId"></param>
        public void AddAbilityListRenderer(int id, string vaultIcon,string valutDes, int[] lockData,int firstId, int lastId)
        {
            var abilityLevelList = SetAbilityLevel(firstId, lastId);
            AbilityList abilityItemData = new AbilityList
            {
                ID = id,
                VaultIcon = vaultIcon,
                VaultDes  = valutDes,
                LockData = lockData,
                
                abilitLevelList = abilityLevelList
            };

            abilityList.Add(abilityItemData);
        }


        /// <summary>
        /// 获取到科技升级数据
        /// </summary>
        /// <param name="firstId"></param>
        /// <param name="lastId"></param>
        public List<AbilitLevelRenderer> SetAbilityLevel(int firstId, int lastId)
        {
            List<AbilitLevelRenderer> abilitLevelList = new List<AbilitLevelRenderer>();
            if (ConfigSystem.Instance.TryGets<AbilityLevelRowData>((c) => (
                        (AbilityLevelRowData)c).Id >= firstId && ((AbilityLevelRowData)c).Id <= lastId,
                    out var list))
            {
                var len = list.Count;
                for (int i = 0; i < len; i++)
                {
                    var rowData = list[i];
                    var nextlevel = i + 1 == len ? 0 : list[i + 1].Value;
                    int[] buyData = i + 1 == len ? new []{0,0}:new [] { list[i].Cost(1), list[i].Cost(2) };
                    AbilitLevelRenderer levelItemData = new AbilitLevelRenderer()
                    {
                        level          = rowData.VaultLevel,
                        CurLevelValue  = rowData.Value,
                        NextLevelValue = nextlevel,
                        BuffType       = rowData.Type,
                        BuyData        = buyData
                    };

                    abilitLevelList.Add(levelItemData);
                }
            }

            return abilitLevelList;
        }

        /// <summary>
        /// 等级升级
        /// </summary>
        /// <param name="index"></param>
        public void UpgradeLevel(int index)
        {
            if (abilityList[index].LevelIndex < abilityList[index].abilitLevelList.Count-1)
            {
                abilityList[index].LevelIndex++;
            }
        }

        public int GetValueType(int buffID)
        {
            if (ConfigSystem.Instance.TryGet<BuffRowData>((c) => (
                        (BuffRowData)c).ID == buffID,
                    out var cfg))
            {
                return cfg.AddType;
            }

            return 0;
        }

        public void UnLockAbility(int index)
        {
            abilityList[index].IsLock = true;
        }
    }
}

