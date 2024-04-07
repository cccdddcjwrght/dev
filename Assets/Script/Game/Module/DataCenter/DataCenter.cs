using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using GameConfigs;
using log4net;
using System.Linq;

namespace SGame
{
	public delegate void DataExcute<T>(ref T data);

	public partial class DataCenter : IModule
	{
		private static ILog log = LogManager.GetLogger("game.datacetner");

		public static bool IsFirstLogin { get; private set; }

		public static bool IsNew { get; set; }

		public int registertime;

		[SerializeField]
		private int loadtime;

		// 用户数据
		public Entity m_data;
		[SerializeField]
		public AccountData accountData = new AccountData();

		[SerializeField]
		public AbilityData abilityData = new AbilityData();

		[SerializeField]
		public SetData setData = new SetData();

		[SerializeField]
		public GuideData guideData = new GuideData();

		[SerializeField]
		public GameRecordData m_gameRecord = new GameRecordData(); // 游戏统计数据包括 顾客数量, 厨师数量, 服务员数量


		[SerializeField]
		private ItemData itemData = new ItemData();
		[SerializeField]
		private ItemData cacheItem = new ItemData();

		[SerializeField]
		public double m_foodTipsGold;

		private GameWorld m_world;
		static DataCenter s_instance;

		public bool IsInitAll { get; private set; } = false;

		public static DataCenter Instance
		{
			get
			{
				return s_instance;
			}
		}

		// 获取用户信息
		public UserData GetUserData()
		{
			return m_world.GetEntityManager().GetComponentData<UserData>(m_data);
		}

		public void SetUserData(UserData data)
		{
			m_world.GetEntityManager().SetComponentData(m_data, data);
		}

		public UserSetting GetUserSetting()
		{
			return m_world.GetEntityManager().GetComponentData<UserSetting>(m_data);
		}

		public void SetUserSetting(UserSetting data)
		{
			m_world.GetEntityManager().SetComponentData(m_data, data);
		}


		public EntityManager EntityManager
		{
			get { return m_world.GetEntityManager(); }
		}

		public DataCenter(GameWorld world)
		{
			s_instance = this;
			m_world = world;
			m_data = EntityManager.CreateEntity(
				typeof(UserData),
				typeof(UserSetting)
			);

			EntityManager.SetComponentData(m_data, UserSetting.GetDefault());
			EntityManager.SetComponentData(m_data, UserData.GetDefault());
		}

		public void Load()
		{
			DoLoad();
			AfterLoad();
		}

		public void Save()
		{
			BeforeSave();
			DoSave();
		}

		public void Initalize()
		{
			GameServerTime.Instance.Update((int)DateTimeOffset.Now.ToUnixTimeSeconds(), -1);

			PropertyManager.Instance.InitCache(cacheItem);
			PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).Initalize(itemData);
			PropertyManager.Instance.CombineCache2Items();
			if (loadtime == 0)
			{
				IsNew = true;
				registertime = GameServerTime.Instance.serverTime;
				accountData.playerID = System.DateTime.Now.Ticks;
				InitItem();
				OnFirstInit();
				IsFirstLogin = true;
			}
			else
			{
				// 小费恢复
				if (m_foodTipsGold > 0)
				{
					log.Info("FoodTip=" + m_foodTipsGold + " before=" + PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).GetNum((int)ItemID.GOLD));
					PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.GOLD, m_foodTipsGold);
					m_foodTipsGold = 0;
					log.Info("FoodTip=" + m_foodTipsGold + " after=" + PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).GetNum((int)ItemID.GOLD));
				}
			}

			var scene = roomData.roomID <= 0 ? 1 : roomData.roomID;
			var ud = GetUserData();
			ud.scene = scene;
			SetUserData(ud);

			DoInit();
			m_gameRecord.Initalize();

			IsInitAll = true;
			loadtime = GameServerTime.Instance.serverTime;

			EventManager.Instance.Trigger(((int)GameEvent.DATA_INIT_COMPLETE));
		}

		private void InitItem()
		{
			PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.DIAMOND, GlobalDesginConfig.GetInt("initial_gems"));

			var cfgstr = GlobalDesginConfig.GetStr("initial_items") ?? GlobalConfig.GetStr("initial_items");
			if (!string.IsNullOrEmpty(cfgstr))
			{
				var group = PropertyManager.Instance.GetGroup(PropertyGroup.ITEM);
				var dic = cfgstr
					.Split('|', StringSplitOptions.RemoveEmptyEntries)
					.Select(s => s.Split('_', StringSplitOptions.RemoveEmptyEntries))
					.Where(s => s.Length > 1)
					.ToDictionary(s => int.Parse(s[0]), s => int.Parse(s[1]));
				foreach (var item in dic)
					group.SetNum(item.Key, item.Value);
			}
		}

		public void Update()
		{

		}


		public void Shutdown()
		{

		}

		partial void OnFirstInit();
		partial void DoInit();
		partial void DoLoad();
		partial void AfterLoad();
		partial void DoSave();
		partial void BeforeSave();

	}
}
