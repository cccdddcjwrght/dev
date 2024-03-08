using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGame;
using UnityEngine;

namespace SGame
{
	public partial class DataCenter
	{
		public AdData addatas = new AdData();

		public class AdUtil
		{
			static AdData _data = Instance.addatas;

			public static int GetAdLimit(string id)
			{
				if (!string.IsNullOrEmpty(id))
				{
					_data.Refresh();
					if (ConfigSystem.Instance.TryGet(id, out GameConfigs.ADConfigRowData cfg))
					{
						if (cfg.LimitType == 0 || cfg.Limit == 0) return -1;
						var ad = _data.items.Find(a => a.id == id);
						return Math.Max(0, cfg.Limit - ad.count);

					}
				}
				return 0;

			}

			public static int GetAdMaxCount(string id)
			{
				if (ConfigSystem.Instance.TryGet(id, out GameConfigs.ADConfigRowData cfg))
					return cfg.Limit;
				return 0;
			}

			public static int GetAdInterval(string id)
			{
				if (!string.IsNullOrEmpty(id))
				{
					var datas = DataCenter.Instance.addatas;
					datas.Refresh();
					if (ConfigSystem.Instance.TryGet(id, out GameConfigs.ADConfigRowData cfg))
					{
						if (cfg.Interval > 0)
						{
							var ad = datas.items.Find(a => a.id == id);
							return ad.time - GameServerTime.Instance.serverTime;
						}
					}
				}
				return 0;

			}

			public static void RecordPlayAD(string id)
			{
				if (!string.IsNullOrEmpty(id))
				{
					_data.Refresh();
					if (ConfigSystem.Instance.TryGet(id, out GameConfigs.ADConfigRowData cfg))
					{
						var index = _data.items.FindIndex(a => a.id == id);
						if (index < 0)
						{
							_data.items.Add(new AdItem() { id = id });
							index = _data.items.Count - 1;
						}
						var ad = _data.items[index];
						ad.count += 1;
						if (cfg.Interval > 0)
							ad.time = GameServerTime.Instance.serverTime + cfg.Interval;
						_data.items[index] = ad;
						DataCenter.SetStrValue("@adinfo", JsonUtility.ToJson(_data));
					}
				}
			}

			public static bool IsAdEnable(string id)
			{
				if ( ConstDefine.C_AD_OPEN && ConfigSystem.Instance.TryGet(id, out GameConfigs.ADConfigRowData cfg))
				{
					return cfg.Disable != 1 && (cfg.UnlockNum == 0 /*|| DataCenter.Instance.account.passLevel > cfg.UnlockNum*/);
				}
				return false;
			}

			public static bool IsAdCanPlay(string id)
			{
				return IsAdEnable(id) && GetAdLimit(id) != 0 && GetAdInterval(id) <= 0;
			}

		}

	}

	[Serializable]
	public class AdData
	{
		public int day;
		public List<AdItem> items;

		public void Refresh()
		{
			var sd = GameServerTime.Instance.serverDay;
			if (day != sd)
			{
				day = sd;
				var info = JsonUtility.FromJson(DataCenter.GetStrValue("@adinfo"), GetType()) as AdData;
				if (info != null && info.day == day) items = info.items;
				else (items = items ?? new List<AdItem>())?.Clear();
			}
		}

	}

	[Serializable]
	public struct AdItem
	{
		public string id;
		public int count;
		public int time;
	}

}