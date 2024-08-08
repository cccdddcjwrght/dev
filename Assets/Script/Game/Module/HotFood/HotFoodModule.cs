using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameConfigs;
using System.Linq;
using FairyGUI;

namespace SGame 
{
    public class HotFoodModule : Singleton<HotFoodModule>
    {
        private HotFoodData m_HotFoodData = DataCenter.Instance.hotFoodData;
        private int m_HotFoodWeight = GlobalDesginConfig.GetInt("hot_weight", 1000);
        public int HotDuration = GlobalDesginConfig.GetInt("hot_duration", 300);
        public int HotCD = GlobalDesginConfig.GetInt("hot_cd", 10);

        public void Initalize() 
        {
            //EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, OnLevelRoom);
            EventManager.Instance.Reg<int>((int)GameEvent.ENTER_ROOM, OnEnterRoom);
        }

        public void StartFood(int foodID) 
        {
            m_HotFoodData.Start(foodID);
            //CdFinish();
            EventManager.Instance.Trigger((int)GameEvent.HOTFOOD_REFRESH);
        }

        public void StopFood() 
        {
            m_HotFoodData.Stop();
            //CdFinish();
            EventManager.Instance.Trigger((int)GameEvent.HOTFOOD_REFRESH);
        }

        public void CdFinish() 
        {
            var cdTime = m_HotFoodData.GetCdTime();
            if (cdTime > 0) 
            {
                GTween.To(0, 1, m_HotFoodData.GetCdTime()).OnComplete(() =>
                {
                    EventManager.Instance.Trigger((int)GameEvent.HOTFOOD_REFRESH);
                });
            }
        }

        public void OnEnterRoom(int scene) 
        {
            CdFinish();
        }
        public void OnLevelRoom() 
        {
            m_HotFoodData.Reset();
        }

        /// <summary>
        /// 获取热卖食物额外刷出权重
        /// </summary>
        public int GetHotFoodWeight(int foodID) 
        {
            if (m_HotFoodData.foodID == foodID && m_HotFoodData.IsForce())
                return m_HotFoodWeight;
            return 0;
        }
    }

    partial class DataCenter 
    {
        public HotFoodData hotFoodData = new HotFoodData();
    }


    [System.Serializable]
    public class HotFoodData 
    {
        public int foodID;
        public int time;
        public int cdTime;

        /// <summary>
        /// 开始热卖
        /// </summary>
        /// <param name="foodID"></param>
        public void Start(int foodID) 
        {
            this.foodID = foodID;
            time = GameServerTime.Instance.serverTime + HotFoodModule.Instance.HotDuration;
            cdTime = time + HotFoodModule.Instance.HotCD;
        }

        /// <summary>
        /// 结束热卖
        /// </summary>
        public void Stop() 
        {
            foodID  = 0;
            time    = 0;
            cdTime  = GameServerTime.Instance.serverTime + HotFoodModule.Instance.HotCD;
        }

        public void Reset() 
        {
            foodID  = 0;
            time    = 0;
            cdTime  = 0;
        }

        /// <summary>
        /// 是否热卖中
        /// </summary>
        /// <returns></returns>
        public bool IsForce() 
        {
            return foodID > 0 && GetTime() > 0;
        }

        /// <summary>
        /// 获取持续时间
        /// </summary>
        /// <returns></returns>
        public int GetTime() 
        {
            return time - GameServerTime.Instance.serverTime;
        }

        /// <summary>
        /// 获取cd冷却时间
        /// </summary>
        /// <returns></returns>
        public int GetCdTime() 
        {
            return cdTime - GameServerTime.Instance.serverTime;
        }
    }
}


