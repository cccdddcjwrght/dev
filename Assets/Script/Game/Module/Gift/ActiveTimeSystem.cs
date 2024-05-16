using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Cinemachine;
using log4net;
using SGame;
using SGame.VS;
using Unity.VisualScripting;
using UnityEngine;

namespace SGame
{
	/// <summary>
	/// 活动时间系统, 用于判断时间区间内活动是否开启
	/// </summary>
	public class ActiveTimeSystem : MonoSingleton<ActiveTimeSystem>
	{
		private static ILog log = LogManager.GetLogger("game.activetime");

		private const float UPDATE_TICK = 1.0f; // 每秒更新

		/// <summary>
		/// 时间结构提
		/// </summary>
		public struct TimeRange
		{
			public int tMin; // 最小时间
			public int tMax; // 最大时间

			public static TimeRange Zero { get { return new TimeRange() { tMin = 0, tMax = 0 }; } }

			public bool Equals(TimeRange other)
			{
				return this.tMin == other.tMin && this.tMax == other.tMax;

			}

			public static bool operator ==(TimeRange first, TimeRange other) => first.Equals(other);
			public static bool operator !=(TimeRange lhs, TimeRange rhs) => !(lhs == rhs);
		}

		// 激活数据
		public struct ActiveData
		{
			public int configID;
			public int actSubID;
			public bool isActive;  // 当前是否激活
			public TimeRange timeRange; // 时间区域
		}


		private Dictionary<int, TimeRange> m_datas = new Dictionary<int, TimeRange>();   // 用于判定是否在活动中
		private List<ActiveData> m_active = new List<ActiveData>();             // 用于触发活动事件
		private float m_timeInterval = 0;


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
					AddTimeRange(config.Value.Id, config.Value.BeginTime, config.Value.EndTime, config.Value.Value);
				}
			}
			m_timeInterval = 0;
		}

		void Update()
		{
			m_timeInterval -= Time.deltaTime;
			if (m_timeInterval > 0)
			{
				return;
			}
			m_timeInterval = UPDATE_TICK;

			// 统计事件
			var currentTime = GameServerTime.Instance.serverTime;
			if (currentTime == 0)
				return;
			var flag = false;
			for (int i = 0; i < m_active.Count; i++)
			{
				var value = m_active[i];
				var isActive = currentTime >= value.timeRange.tMin && currentTime <= value.timeRange.tMax;
				if (isActive != value.isActive)
				{
					value.isActive = isActive;
					m_active[i] = value;
					flag = true;

					if (isActive == true)
						EventManager.Instance.Trigger((int)GameEvent.ACTIVITY_OPEN, value.configID);
					else
						EventManager.Instance.Trigger((int)GameEvent.ACTIVITY_CLOSE, value.configID);
				}
			}
			if (flag)
				EventManager.Instance.AsyncTrigger((int)GameEvent.GAME_MAIN_REFRESH);
		}


		/// <summary>
		/// 添加活动时间
		/// </summary>
		/// <param name="id">活动ID</param>
		/// <param name="time1">开始时间</param>
		/// <param name="time2">结束时间</param>
		/// <returns>是否成功返回</returns>
		public bool AddTimeRange(int id, string time1, string time2, int actSubID = 0)
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

			var timeOffset1 = new DateTimeOffset(date1);
			var timeOffset2 = new DateTimeOffset(date2);
			int offset1 = (int)timeOffset1.ToUnixTimeSeconds();
			int offset2 = (int)timeOffset2.ToUnixTimeSeconds();
			log.Info(string.Format("add time id={0}, time1={1}, time2={2} value1={3} value2={4}", id, date1.ToString(), date2.ToString(), offset1, offset2));

			if (offset1 >= offset2)
			{
				log.Error(string.Format("time1={0} lagre than time2={1}", offset1, offset2));
				return false;
			}
			var timeRange = new TimeRange() { tMin = offset1, tMax = offset2 };
			m_datas.Add(id, timeRange);
			m_active.Add(new ActiveData() { configID = id, isActive = false, timeRange = timeRange, actSubID = actSubID });
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

		public bool IsActiveBySubID(int actSubID, int time, out ActiveData data)
		{
			data = m_active.Find(a => a.configID > 0 && a.actSubID == actSubID && IsActive(a.configID, time));
			return data.configID > 0;
		}

		/// <summary>
		/// 获得活动时间
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public TimeRange GetTimeRange(int id)
		{
			if (m_datas.TryGetValue(id, out TimeRange value))
			{
				return value;
			}

			log.Error("active id not found=" + id.ToString());
			return TimeRange.Zero;
		}

		/// <summary>
		/// 获取活动剩余时间
		/// </summary>
		/// <param name="id"></param>
		/// <param name="currentTime">当前时间</param>
		/// <returns></returns>
		public int GetLeftTime(int id, int currentTime)
		{
			if (!IsActive(id, currentTime))
				return 0;

			var timeRange = GetTimeRange(id);
			return timeRange.tMax - currentTime;
		}
	}
}