using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using GameConfigs;

namespace SGame
{
	public delegate void DataExcute<T>(ref T data);

	public partial class DataCenter : IModule
	{
		[SerializeField]
		private int loadtime;
		
		// 用户数据
		public Entity m_data;
		public AbilityData abilityData = new AbilityData();

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
			Initalize();
		}

		public void Initalize()
		{
			if(loadtime == 0)
			{
				PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.DIAMOND, GlobalDesginConfig.GetInt("initial_gems"));
				OnFirstInit();
			}
			GameServerTime.Instance.Update((int)DateTimeOffset.Now.ToUnixTimeSeconds(), 0);
			DoInit();
			IsInitAll = true;
			loadtime = GameServerTime.Instance.serverTime;
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
	}
}
