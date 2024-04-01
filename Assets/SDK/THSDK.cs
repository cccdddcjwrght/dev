#if USE_THIRD_SDK
using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEngine;
using System;
using System.Linq;

namespace SDK.THSDK
{

	[Serializable]
	public struct RecordData
	{
		public List<TimeRecord> list;
	}

	[System.Serializable]
	public class TimeRecord
	{
		public string px;
		public string key;
		public float time;
	}

	public class THSdk : SGame.MonoSingleton<THSdk>
	{
		private List<TimeRecord> _records = new List<TimeRecord>();
		private List<string> _resetKeyPx = new List<string> { "scene", "wt", null, };
		private EventManager _emgr;
		private bool _isInited;

		private Action _command;

		private int scene { get { return DataCenter.Instance.roomData.roomID; } }


		void Start()
		{
			_emgr = EventManager.Instance;
			_records = LoadRecord();

			Action call = () => SaveRecord(_records);
			this.Loop(call, () => transform, 10000, 10000);
			call.CallWhenQuit();

			AddRecord(0, "app");
			InitEvent();
			try
			{
				ThirdSdk.ThirdSDK.inst.AddListener(ThirdSdk.THIRD_EVENT_TYPE.TET_THIRD_SDK_INIT_COMPLETE, (a) =>
				{
					_isInited = true;
					Debug.Log($"::Init:{a}");
				});
				ThirdSdk.ThirdSDK.inst.Init(gameObject);
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
			}

		}

		void Update()
		{
			if (_records.Count > 0)
			{
				foreach (var item in _records)
					item.time += Time.deltaTime;
			}
			var c = _command;
			_command = null;
			try
			{
				c?.Invoke();
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
			}
		}

		private void OnApplicationPause(bool pause)
		{
			if (!pause)
				ThirdSdk.ThirdSDK.inst.OnActive();
		}

		public IEnumerator WaitInitCompleted()
		{
			while (!_isInited) yield return null;
		}

		private void InitEvent()
		{
			_emgr.Reg(((int)GameEvent.ENTER_NEW_ROOM), EnterNewScene);//记录新场景
			_emgr.Reg(((int)GameEvent.PREPARE_LEVEL_ROOM), LevelRoom);//记录离开场景

			_emgr.Reg<int>(((int)GameEvent.WORK_TABLE_ENABLE), (id) => AddRecord(id, "wt"));//记录工作台

#region Record

			//界面打开
			_emgr.Reg<string>(((int)GameEvent.UI_SHOW), (name) => Trigger("ui_show", "type", name));

			//工作台升级
			_emgr.Reg<int, int>(((int)GameEvent.WORK_TABLE_UPLEVEL), (id, lv) =>
				Trigger("level_up",
				 "game_stage", scene,
				 "item_id", id,
				 "to_level", lv)
			);
			//工作台升星
			_emgr.Reg<int, int>(((int)GameEvent.WORK_TABLE_UP_STAR), (id, star) =>
				Trigger("star_up",
				"game_stage", scene,
				"item_id", id,
				"to_star", star,
				"game_time", GetRecordTime(id, "wt"))
			);
			//关卡科技
			_emgr.Reg<int>(((int)GameEvent.TECH_ADD_REWARD), id =>
				Trigger("ft_upgrade",
				  "game_stage", scene,
				  "upgrade_id", id,
				  "game_time", GetRecordTime(-1))
			);

			//商品
			_emgr.Reg<int>(((int)GameEvent.SHOP_GOODS_BUY_RESULT), id => Trigger("shop_buy", "goods_id", id));
			//穿
			_emgr.Reg<int>(((int)GameEvent.ROLE_EQUIP_PUTON), id => Trigger("equip_item", "equip_id", id));
			//加成道具
			_emgr.Reg<int, int>(((int)GameEvent.SHOP_BOOST_BUY), (id, lv) => Trigger("buy_boost", "bst_id", id, "bst_lv", id));

#endregion
		}

		public void Trigger(string eventID, params object[] args)
		{
			if (string.IsNullOrEmpty(eventID)) return;
			if (args.Length % 2 == 0)
			{
				var dic = new Dictionary<string, object>();
				for (int i = 0; i < args.Length; i += 2)
				{
					if (args[i] != null)
						dic[args[i].ToString()] = args[i + 1];
				}
				TriggerWithDic(eventID, dic);
			}
		}

		private void TriggerWithDic(string eventID, Dictionary<string, object> args)
		{
			Debug.Log($"event:{eventID}");
#if !UNITY_EDITOR
			ThirdSdk.ThirdSDK.inst.SendAnalyticsEvent(eventID, args);
#endif

		}

		private void LevelRoom()
		{
			Trigger("game_end", "game_name", 1, "game_stage", scene, "game_time", GetRecordTime(-1), "heart_time", GetRecordTime(0, "app"));
		}

		private void EnterNewScene()
		{
			_records.RemoveAll(r => _resetKeyPx.Contains(r.px));
			AddRecord(-1);
			Trigger("game_start", "game_name", 1, "game_stage", scene, "heart_time", GetRecordTime(0, "app"));
		}

		private void AddRecord(int id, string px = null)
		{
			var f = GetRecordTime(id, px, false);
			if (f > 0)
				return;
			_records.Add(new TimeRecord() { px = px, key = px + id, time = 0.01f });
		}

		private float GetRecordTime(int id, string px = null, bool add = true)
		{

			var s = px + id;
			var r = _records?.Find(v => v.key == s);
			if (r != null) return r.time;
			else if (add) AddRecord(id, px);
			return 0;
		}

		private List<TimeRecord> LoadRecord()
		{
			var v = PlayerPrefs.GetString("thsdk", "");
			var r = string.IsNullOrEmpty(v) ? default : JsonUtility.FromJson<RecordData>(v);
			return r.list ?? new List<TimeRecord>();
		}

		private void SaveRecord(List<TimeRecord> records)
		{
			if (records != null && records.Count > 0)
				PlayerPrefs.SetString("thsdk", JsonUtility.ToJson(new RecordData() { list = records }));
			PlayerPrefs.Save();
		}
	}
}

#endif