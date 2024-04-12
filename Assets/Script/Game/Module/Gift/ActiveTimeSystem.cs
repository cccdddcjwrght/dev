using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SGame;
using UnityEngine;

namespace SGame
{
    /// <summary>
    /// 活动时间系统, 用于判断时间区间内活动是否开启
    /// </summary>
    public class ActiveTimeSystem : Singleton<ActiveTimeSystem>
    {
        private static ILog log = LogManager.GetLogger("game.activetime");
        
        /// <summary>
        /// 时间结构提
        /// </summary>
        public struct TimeRange
        {
            public int tMin; // 最小时间
            public int tMax; // 最大时间
        }

        private Dictionary<int, TimeRange> m_datas = new Dictionary<int, TimeRange>();

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initalize()
        {
            var configs = ConfigSystem.Instance.LoadConfig<GameConfigs.ActivityTime>();
            for (int i = 0; i < configs.DatalistLength; i++)
            {
                GameConfigs.ActivityTimeRowData? config = configs.Datalist(i);
                if (config != null)
                {
                    AddTimeRange(config.Value.Id, config.Value.BeginTime, config.Value.EndTime);
                }
            }
        }

        /// <summary>
        /// 添加活动时间
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <param name="time1">开始时间</param>
        /// <param name="time2">结束时间</param>
        /// <returns>是否成功返回</returns>
        public bool AddTimeRange(int id, string time1, string time2)
        {
            if (m_datas.ContainsKey(id))
            {
                log.Error("id repeate=" + id);
                return false;
            }
            
            if (!DateTime.TryParse(time1, out DateTime date1))
            {
                log.Error(string.Format("pase time fail id={0}, timeformat={1}", id, time1));
                return false;
            }
            
            if (!DateTime.TryParse(time2, out DateTime date2))
            {
                log.Error(string.Format("pase time fail id={0}, timeformat={1}", id, time2));
                return false;
            }
            
            log.Info(string.Format("add time id={0}, time1={1}, time2={2}", id, date1.ToString(), date2.ToString()));
            
            var timeOffset1 = new DateTimeOffset(date1);
            var timeOffset2 = new DateTimeOffset(date2);
            int offset1 = (int)timeOffset1.ToUnixTimeSeconds();
            int offset2 = (int)timeOffset2.ToUnixTimeSeconds();
            if (offset1 >= offset2)
            {
                log.Error(string.Format("time1={0} lagre than time2={1}", offset1, offset2));
                return false;
            }

            m_datas.Add(id, new TimeRange(){tMin = offset1, tMax = offset2});
            return true;
        }
        
        /// <summary>
        /// 判断某个时间是否在活动内
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsActive(int id, int time)
        {
            if (m_datas.TryGetValue(id, out TimeRange value))
            {
                return time >= value.tMin && time <= value.tMax;
            }
            
            log.Error("active id not found=" + id.ToString());
            return false;
        }
    }
}