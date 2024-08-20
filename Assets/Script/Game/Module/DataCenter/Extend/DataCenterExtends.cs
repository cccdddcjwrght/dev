using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Unity.Entities;
using UnityEngine;

namespace SGame
{
	[Serializable]
	partial class DataCenter
	{
		[System.NonSerialized]
		public int offlinetime;

		public void SetData<T>(T data) where T : unmanaged, IComponentData
		{
			if (EntityManager.HasComponent<T>(m_data))
				EntityManager.SetComponentData<T>(m_data, data);
			else
				EntityManager.AddComponentData<T>(m_data, data);
		}

		public T GetData<T>() where T : unmanaged, IComponentData
		{
			return EntityManager.GetComponentData<T>(m_data);
		}

		public void UpdateData<T>(DataExcute<T> excute) where T : unmanaged, IComponentData
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
#if !SVR_RELEASE
			log.Warn("save offline point:" + accountData.lasttime); 
#endif
		}

	}

	public static partial class DataCenterExtension
	{
		private const string __DKey = "__Data";
		static int __state = 0;

		/// <summary>
		/// 从磁盘加载
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetStrFromDisk(string key = null)
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
			return str;
		}

		/// <summary>
		/// 写入磁盘
		/// </summary>
		/// <param name="str"></param>
		/// <param name="key"></param>
		public static void SaveStrToDisk(string str, string key = null)
		{
			PlayerPrefs.SetString(key ?? __DKey, str);
			PlayerPrefs.Save();
#if !SVR_RELEASE
			var path = Application.persistentDataPath + "/data_" + key;
			System.IO.File.WriteAllText(path, str);
#endif			
		}

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

		public static bool IS_AUTO_SAVE = true;
		
		static void SetTimer()
		{
			var flag = true;
			new Action(() => {
				flag = false;
				SaveData(DataCenter.Instance);
			}).CallWhenQuit();

			ThreadPool.QueueUserWorkItem((a) => {
				Thread.CurrentThread.Name = "SaveData";
				while (flag)
				{
					Thread.Sleep(10000);
					if (IS_AUTO_SAVE)
					{
						SaveData(DataCenter.Instance);
#if DATA_SYNC
						DataSyncModule.SendDataToServer();
#endif
					}

				}

			});

		}
	}
}

