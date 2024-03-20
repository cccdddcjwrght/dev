using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
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
		public int head;
		public int frame;
		
		/// <summary>
		/// 记录事件
		/// </summary>
		public int lasttime;

		public Account To()
		{
			return new Account() { pid = playerID };
		}

		public int GetHead()
		{
			if (head==0&&ConfigSystem.Instance.TryGet(item => (item).DefaultIcon == 1 &&(item).Type==1,
				    out AvatarRowData Config))
			{
				return Config.AvatarId;
			}

			return head;
		}
		
		public int GetFrame()
		{
			if (frame==0&&ConfigSystem.Instance.TryGet(item => (item).DefaultIcon == 1&&(item).Type==2,
				    out AvatarRowData Config))
			{
				return Config.AvatarId;
			}

			return frame;
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
			return offlinetime > 0 ? GameServerTime.Instance.serverTime - offlinetime : 0;
		}
	}

}
