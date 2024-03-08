using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Entities;
using UnityEngine;

namespace SGame
{

	partial class DataCenter
	{
		[System.NonSerialized]
		public int offlinetime;

		public void SetData<T>(T data) where T : struct, IComponentData
		{
			if (EntityManager.HasComponent<T>(m_data))
				EntityManager.SetComponentData<T>(m_data, data);
			else
				EntityManager.AddComponentData<T>(m_data, data);
		}

		public T GetData<T>() where T : struct, IComponentData
		{
			return EntityManager.GetComponentData<T>(m_data);
		}

		public void UpdateData<T>(DataExcute<T> excute) where T : struct, IComponentData
		{
			if (excute != null)
			{
				var d = EntityManager.GetComponentData<T>(m_data);
				excute(ref d);
				EntityManager.SetComponentData<T>(m_data, d);
			}
		}

		partial void DoLoad()
		{
			this.LoadData();
			this.offlinetime = accountData.lasttime;
		}

		partial void BeforeSave()
		{
			accountData.lasttime = GameServerTime.Instance.serverTime;
		}

	}

	public static partial class DataCenterExtension
	{
		const string __DKey = "__Data";
		static int __state = 0;

		public static bool LoadData(this DataCenter data, string key = null)
		{
			var str = "";
#if !SVR_RELEASE

#if LOCAL_DATA
		var p = Application.persistentDataPath + "/data_" + key;
		if (!string.IsNullOrEmpty(key) && File.Exists(p))
			str = File.ReadAllText(p);
		else 
#endif
			str = PlayerPrefs.GetString(key ?? __DKey, null);
#else
		str = PlayerPrefs.GetString(key ?? __DKey, null);
#endif
			var ret = !string.IsNullOrEmpty(str);
			if (ret)
				JsonUtility.FromJsonOverwrite(str, data);
			if (0 == __state++)
				SetTimer();

			return ret;
		}

		public static void SaveData(this DataCenter data, string key = null)
		{
			if (data != null && data.IsInitAll)
			{
				data.Save();
				var str = JsonUtility.ToJson(data);
				PlayerPrefs.SetString(key ?? __DKey, str);
				PlayerPrefs.Save();
#if !SVR_RELEASE
				var path = Application.persistentDataPath + "/data_" + key;
				System.IO.File.WriteAllText(path, str);
#endif
			}
		}

		public static void SavePlayerData(this DataCenter data)
		{
			if (data != null)
				SaveData(data);
		}

		static void SetTimer()
		{
			new Action(() => SaveData(DataCenter.Instance)).CallWhenQuit();
			0.Loop(() => SaveData(DataCenter.Instance), () => true, 10000, 10000);
		}
	}
}

