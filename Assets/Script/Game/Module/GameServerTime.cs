using System;
using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;

public interface ICrossDay
{
	void OnDayChange(int day);
}

// 游戏服务器时间
public class GameServerTime : Singleton<GameServerTime>
{
	private const int delaySecond = 5;
	private const int daySecond = 24 * 60 * 60;

	private int m_serverTime;  // 服务器时间
	private double m_localTime;   // 对应客户端时间
	private long m_nextServerDayTime;

	private int m_lastDay;

	public ICrossDay crossDay;

	public int serverDay
	{
		get
		{
			if (nextDayInterval <= 0)
			{
				var t = serverTime;
				if (m_nextServerDayTime != 0)
				{
					m_nextServerDayTime += daySecond;
					t = (int)m_nextServerDayTime - 2;
				}
				m_lastDay =  DateTimeOffset.FromUnixTimeSeconds(t).DateTime.DayOfYear;
				crossDay?.OnDayChange(m_lastDay);
			}
			return m_lastDay;
		}
	}

	public int nextDayInterval
	{
		get
		{
			return (int)(m_nextServerDayTime - serverTime);
		}
	}

	// 更新服务器时间
	public void Update(int serverTime, int nextDayTime = 0)
	{
		if (nextDayTime > 0)
			m_nextServerDayTime = nextDayTime + delaySecond;
		m_localTime = GlobalTime.passTime;
		m_serverTime = serverTime;
		m_lastDay = serverDay;

	}

	// 服务器时间
	public int serverTime
	{
		get
		{
			if (m_localTime == 0 || m_serverTime == 0)
				return 0;

			double diffTime = GlobalTime.passTime - m_localTime;
			int ret = m_serverTime + (int)diffTime;
			return ret;
		}
	}
}
