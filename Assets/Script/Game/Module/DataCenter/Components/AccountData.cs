using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Unity.Entities;
using UnityEngine;
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
			if (head == 0 && ConfigSystem.Instance.TryGet(item => (item).DefaultIcon == 1 && (item).Type == 1,
					out AvatarRowData Config))
			{
				return Config.AvatarId;
			}

			return head;
		}

		public int GetFrame()
		{
			if (frame == 0 && ConfigSystem.Instance.TryGet(item => (item).DefaultIcon == 1 && (item).Type == 2,
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

		/// <summary>
		/// 离线收益
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static double CaluOfflineReward(int time)
		{
			var min = GlobalDesginConfig.GetInt("min_offline_Value");
			var max = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.OfflineTime);
			var rate = (int)AttributeSystem.Instance.GetValue(EnumTarget.Game, EnumAttribute.OfflineAddition);
			var gold = (double)min;

			time = Mathf.Clamp(time, 0, max);
			var ws = MachineUtil.GetWorktables((w) => !w.isTable && w.level > 0);
			if (ws?.Count > 0) ws.ForEach(w => gold += w.GetPrice() / w.GetWorkTime());
			return (ConstDefine.C_PER_SCALE * gold * time * rate).ToInt();
		}

	}

}
