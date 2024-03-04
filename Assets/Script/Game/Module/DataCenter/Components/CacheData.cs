using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameConfigs;
using UnityEngine;

namespace SGame
{
	partial class DataCenter
	{
		public CacheData cacheData = new CacheData();

		#region DataUtil

		static public string GetStrValue(string key , string def = default) 
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var index = data.datas.FindIndex(v => v.key == key);
				if(index >= 0)
				{
					return data.datas[index].str;
				}
			}
			return def;
		}

		static public int GetIntValue(string key, int def = default)
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var index = data.datas.FindIndex(v => v.key == key);
				if (index >= 0)
				{
					return data.datas[index].val;
				}
			}
			return def;
		}

		static public void SetStrValue(string key, string val) {
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var item = data.datas.Find(v => v.key == key);
				if (item == null)
					data.datas.Add(new CacheItem() { key = key, str = val });
				else
					item.str = val;
			}
		}

		static public void SetIntValue(string key, int val)
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var item = data.datas.Find(v => v.key == key);
				if (item == null)
					data.datas.Add(new CacheItem() { key = key, val = val });
				else
					item.val = val;
			}
		}

		#endregion

	}

	[Serializable]
	public class CacheData 
	{
		public List<CacheItem> datas = new List<CacheItem>();

		public void Serialize(out string json)
		{
			var old = datas;
			this.datas = this.datas.FindAll(s => s.save || s.key.StartsWith("@"));
			json = JsonUtility.ToJson(this);
			this.datas = old;
		}

	}

	[Serializable]
	public class CacheItem
	{
		public string key;
		public string str;
		public int val;

		[System.NonSerialized]
		public bool save;

		public bool Equals(CacheItem other)
		{
			return other.key == key;
		}
	}

}
