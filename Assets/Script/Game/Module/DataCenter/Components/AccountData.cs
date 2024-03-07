﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine.Serialization;

namespace SGame
{
	public struct Account : IComponentData
	{
		public long pid;
	}

	[Serializable]
	public class AccountData 
	{
		[FormerlySerializedAs("user_id")]
		public long playerID;
		public int platform;
		public string token;

		public string account;
		public string mail;

		public string playerName;

		/// <summary>
		/// 记录事件
		/// </summary>
		public int lasttime;

		public Account To()
		{
			return new Account() { pid = playerID };
		}

	}

	partial class DataCenter
	{
		/// <summary>
		/// 间隔时常
		/// 一般在登录时计算离线时常
		/// </summary>
		/// <returns></returns>
		public int GetOfflineTime()
		{
			return accountData.lasttime > 0 ? GameServerTime.Instance.serverTime - accountData.lasttime : 0;
		}
	}

}
