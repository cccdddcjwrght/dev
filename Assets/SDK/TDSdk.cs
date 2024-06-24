#if !TD_OFF || TD_ON
using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SGame;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SDK.TDSDK
{
	public enum TDEvent
	{
		new_device,
		register,
		login,
		update,
		enter_game,
		guide_step,
		open_show_begin,
		open_show_end,
		item_get,
		item_cost,
		shop_buy,
		level_finish,
		machine_level,
		equipment_upgrade,
		equipment_merge,
		equipment_reset,
		equipment_decompose,
		ability_update,
		investor_appear,
		investor_click,
		investor_disappear,
		ad_click,
		ad_failed,
		ad_show,
		ad_close,
		ad_reward,
		level_complete,
		level_open,
		new_area,
		pet_born,
		pet_grow,
		cook_unlock,
		cook_upgrade,
		task_reward,
		likes_spin,
	}

	public enum TableType
	{
		item = 1,
		equip = 2,
		pet = 3,
	}

	public class PItem : IEquatable<PItem>
	{
		public string name;
		public List<string> events;
		public Func<string, object> get;
		public object val;
		public bool once;
		public bool upload;
		private bool saved;


		public object GetVal()
		{
			var ret = get == null ? val : get(name);
			return ret;
		}

		public bool Check(out object ret, Dictionary<string, object> properties = null)
		{
			ret = GetVal();
			if (get == null || !System.Object.Equals(ret, val))
			{
				val = ret;
				if (properties != null && upload && (!once || !saved))
				{
					saved = true;
					properties[name] = val;
				}
				return true;
			}
			return false;
		}

		public void Set(object val)
		{
			this.val = val;
			saved = false;
		}

		public void Clear()
		{
			events?.Clear();
			get = null;
			name = null;
			val = null;
		}

		public bool Equals(PItem other)
		{
			return other.name == name;
		}

		public PItem LinkEvent(params string[] events)
		{
			if (events != null && events.Length > 0)
			{
				this.events = this.events ?? new List<string>();
				this.events.AddRange(events);
			}
			return this;
		}

		public PItem LinkEvent(params TDEvent[] events)
		{
			if (events != null && events.Length > 0)
			{
				this.events = this.events ?? new List<string>();
				this.events.AddRange(events.Select(e => e.ToString()));
			}
			return this;
		}

	}

	public class ItemCache
	{

		static public ItemCache Item = new ItemCache();

		public int type;
		public int id;
		public int num;
		public int after;
		public string reason;

	}

	[Serializable]
	public class ItemUseInfo
	{
		static public ItemUseInfo Info = new ItemUseInfo();

		public List<ItemInfo> items = new List<ItemInfo>();
	}

	[Serializable]
	public struct ItemInfo
	{
		public int id;
		public int count;
	}

	partial class ThinkDataSDK
	{

		#region Member
		static ILog log = LogManager.GetLogger("TD.Common");

		private DataCenter appData { get { return DataCenter.Instance; } }


		private Dictionary<string, PItem> _properties = new Dictionary<string, PItem>();
		private Dictionary<string, object> _caches = new Dictionary<string, object>();
		private Dictionary<string, object> _onceCaches = new Dictionary<string, object>();

		private string netType;

		#endregion

		#region Partial

		partial void DoAwake()
		{
			netType = Application.internetReachability.ToString();
			RegisterProperties();
			RegisterEvent();
			if (IsFirstStartApp())
				TrackFirst("new_device", GetProperty("device_id"));
		}

		partial void DoGetCommonSuperProperties(ref Dictionary<string, object> properties)
		{
			properties = properties ?? new Dictionary<string, object>();
		}

		partial void DoGetDynamicSuperProperties(ref Dictionary<string, object> properties)
		{
			properties = properties ?? new Dictionary<string, object>();
			properties["network_type"] = netType;
			if (DataCenter.Instance != null)
			{
				properties["current_level"] = DataCenter.Instance.roomData.roomID;
				properties["current_diamond"] = PropertyManager.Instance.GetItem(((int)ItemID.DIAMOND)).num;
			}
		}

		partial void DoFillEventProperties(string eventName, ref Dictionary<string, object> properties)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				var ps = properties ?? new Dictionary<string, object>();
				_properties.Values.Where(p => p.events != null && p.events.Contains(eventName)).All((p) =>
				{
					ps[p.name] = p.GetVal();
					return true;
				});
				properties = ps;
			}
		}

		#endregion

		#region Method

		private void RegisterEvent()
		{
			//通用打点接口
			EventManager.Instance.Reg<string, object[]>(-1, TrackNormal);
			EventManager.Instance.Reg(((int)GameEvent.LOGIN_COMPLETE), () =>
			{
				OnLogined(DataCenter.Instance.accountData.playerID.ToString());
			});

			EventManager.Instance.Reg(((int)GameEvent.ENTER_GAME), () => TrackNormal(TDEvent.enter_game.ToString()));
			EventManager.Instance.Reg<int>(((int)GameEvent.GUIDE_STEP), (a) => TrackNormal(TDEvent.guide_step.ToString(), "guide_id", a));

			//开场动画埋点，开始和结束
			EventManager.Instance.Reg((int)GameEvent.GAME_ENTER_VIEW_STATR, () => TrackNormal(TDEvent.open_show_begin.ToString()));
			EventManager.Instance.Reg((int)GameEvent.GAME_ENTER_VIEW_END, () => TrackNormal(TDEvent.open_show_end.ToString()));

			EventManager.Instance.Reg<int, int, int, int>((int)GameEvent.ITEM_CHANGE_BURYINGPOINT, (c1, c2, c3, c4) => OnChangeItemed(c1, c2, c3, c4));
			EventManager.Instance.Reg<int, int>((int)GameEvent.EQUIP_NUM_UPDATE, (cfgId, cNum) =>
			{
				OnChangeItemed((int)TableType.equip, cfgId, cNum, DataCenter.EquipUtil.GetEquipNum(cfgId));
			});

			EventManager.Instance.Reg<int>((int)GameEvent.SHOP_GOODS_BUY_RESULT, (cfgId) =>
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.ShopRowData>(cfgId, out var data))
					TrackNormal(TDEvent.shop_buy.ToString(), "goods_id", cfgId, "purchase_type", data.PurchaseType, "purchase_price", data.Price);
			});

			//点击下一关卡埋点
			EventManager.Instance.Reg((int)GameEvent.PREPARE_LEVEL_ROOM, () =>
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.RoomRowData>(DataCenter.Instance.roomData.roomID, out var data)) { TrackNormal(TDEvent.level_finish.ToString(), "level_sub_id", data.SubId, "level_region_id", data.RegionId); }
			});

			//工作台升级埋点
			EventManager.Instance.Reg<int, int>((int)GameEvent.WORK_TABLE_UPLEVEL, (id, lv) => {
				var w = DataCenter.MachineUtil.GetWorktable(id);
				TrackNormal(TDEvent.machine_level.ToString(),
					 "machine_id", id,
					 "machine_level", lv,
					 "machine_star", w.star);
				});

			//装备升级，品质提升，重置，分解埋点
			EventManager.Instance.Reg<string, int, int, int, int>((int)GameEvent.EQUIP_BURYINGPOINT, (id, e1, e2, e3, e4) => OnEquiped(id, e1, e2, e3, e4));

			//全局科技埋点
			EventManager.Instance.Reg<int, int>((int)GameEvent.TECH_LEVEL, (techId, techLevel) =>
			TrackNormal(TDEvent.ability_update.ToString(),
				"ability_id", techId,
				"ability_level", techLevel));

			//投资人埋点-出现-点击-消失
			EventManager.Instance.Reg((int)GameEvent.INVEST_APPEAR, () => TrackNormal(TDEvent.investor_appear.ToString()));
			EventManager.Instance.Reg<int>((int)GameEvent.INVEST_CLICK, (t) => TrackNormal(TDEvent.investor_click.ToString(), "appear_time", t));
			EventManager.Instance.Reg((int)GameEvent.INVEST_DISAPPEAR, ()=> TrackNormal(TDEvent.investor_disappear.ToString()));

			//广告埋点
			EventManager.Instance.Reg<string>((int)GameEvent.AD_CLICK, (id) => OnAded(TDEvent.ad_click.ToString(), id));
			EventManager.Instance.Reg<string>((int)GameEvent.AD_FAILED, (id) => OnAded(TDEvent.ad_failed.ToString(), id));
			EventManager.Instance.Reg<string>((int)GameEvent.AD_SHOW, (id) => OnAded(TDEvent.ad_show.ToString(), id));
			EventManager.Instance.Reg<string>((int)GameEvent.AD_REWARD, (id) => OnAded(TDEvent.ad_reward.ToString(), id));


			//关卡开始结束埋点
			EventManager.Instance.Reg<string>((int)GameEvent.UI_SHOW, (uiname) =>
			{
				if (uiname == "welcomenewlevel") TrackNormal(TDEvent.level_open.ToString());
				else if (uiname == "levelcomplete") TrackNormal(TDEvent.level_complete.ToString());
			});

			EventManager.Instance.Reg<int, int, int, int>((int)GameEvent.PET_BORN, (id, born_type, level, qualty) => TrackNormal(
				 TDEvent.pet_born.ToString(), "pet_id", id, "pet_born_type", born_type, "pet_quality", qualty, "grow_num", level));

			//区域解锁埋点
			EventManager.Instance.Reg<int>((int)GameEvent.WORK_AREA_UNLOCK, (area) => TrackNormal(TDEvent.new_area.ToString(), "area_id", area));
			//菜品升级埋点
			EventManager.Instance.Reg<int, int>((int)GameEvent.COOKBOOK_UP_LV, (id, lv) => TrackNormal(TDEvent.cook_unlock.ToString(), "cook_id", id, "cook_level", lv));
			//主线任务埋点
			EventManager.Instance.Reg<int>((int)GameEvent.MAIN_TASK_UPDATE, (id) => TrackNormal(TDEvent.task_reward.ToString(), "task_id", id));
			//好评抽奖埋点
			EventManager.Instance.Reg<int>((int)(GameEvent.LIKE_SPIN), (id) => TrackNormal(TDEvent.likes_spin.ToString(), "likes_spin", id));

		}

		private void RegisterProperties()
		{
			RegisterProperty("device_id", SystemInfo.deviceUniqueIdentifier, once: true);
			RegisterProperty("instancing", get: (k) => SystemInfo.supportsInstancing ? 1 : 0, upload: false).LinkEvent(TDEvent.login);
		}

		#endregion

		#region Event


		public void OnLogined(string id)
		{
			var time = System.DateTime.Now;
			Login(id);
			if (DataCenter.IsFirstLogin)
			{
				TrackNormal(TDEvent.register.ToString());
				UpdateData("register_time", time, true);
				UpdateData("first_login_time", time, true);
				UpdateData("role_name", DataCenter.Instance.accountData.playerName, true);
			}
			UpdateData("last_login_time", time);
			UpdateData("current_level", DataCenter.Instance.roomData.roomID);
			TrackNormal("login");
		}

		public void OnDataRefresh()
		{
			netType = Application.internetReachability.ToString();
			if (_properties != null && _properties.Count > 0)
			{
				_caches = _caches ?? new Dictionary<string, object>();
				_onceCaches = _onceCaches ?? new Dictionary<string, object>();

				_caches.Clear();
				_onceCaches.Clear();

				foreach (var item in _properties)
				{
					var v = item.Value;
					if (!v.once)
						v.Check(out _, _caches);
					else
						v.Check(out _, _onceCaches);
				}
				if (_caches.Count > 0)
					UpdateDatas(_caches, false);
				if (_onceCaches.Count > 0)
					UpdateDatas(_onceCaches, true);
			}
		}

		public void OnChangeItemed(int type, int id, int changeNum, int num)
		{
			if (changeNum > 0) TrackNormal(TDEvent.item_get.ToString(), "item_type", type, "item_id", id, "item_get_num", changeNum, "item_get_after", num);
			else if (changeNum < 0) TrackNormal(TDEvent.item_cost.ToString(), "item_type", type, "item_id", id, "item_cost_num", changeNum, "item_cost_after", num);
		}

		public void OnEquiped(string id, int equipCfgId, int equipLevel, int equipQuality, int equipPos)
		{
			TrackNormal(id.ToString(), "equipment_id", equipCfgId,
				"equipment_level", equipLevel,
				"equipment_quality", equipQuality,
				"equipment_type", equipPos);
		}

		public void OnAded(string type, string id) 
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.ADConfigRowData>(id, out var data))
			{
				TrackNormal(type, "ad_type", data.Type,
					"ad_id", data.Ad,
					"ad_fnish", DataCenter.IsIgnoreAd() ? 1 : 0,
					"ad_key", data.ID);
			}
		}

		#endregion

		#region Private

		private PItem RegisterProperty(string name, object val = null, System.Func<string, object> get = null, bool once = false, bool upload = true)
		{
			if (string.IsNullOrEmpty(name)) return default;
			if (!_properties.ContainsKey(name))
				_properties[name] = new PItem() { name = name, val = val, get = get, once = once, upload = upload };
			return _properties[name];
		}

		private object GetPropertyVal(string name)
		{
			if (!string.IsNullOrEmpty(name) && _properties.TryGetValue(name, out var p))
			{
				return p.GetVal();
			}
			return default;
		}

		private object[] GetProperty(string name)
		{
			return new object[] { name, GetPropertyVal(name) };
		}

		public static bool IsFirstStartApp()
		{
			const string k = "__install__";
			var flag = System.Reflection.Assembly.GetCallingAssembly().GetHashCode();
			if (!PlayerPrefs.HasKey(k))
				PlayerPrefs.SetInt(k, flag);
			var v = PlayerPrefs.GetInt(k, flag);
			return v == flag;
		}

		#endregion

	}
}

#endif