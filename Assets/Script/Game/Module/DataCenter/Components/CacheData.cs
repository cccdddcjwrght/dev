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
				var index = FindDataIndex(data.datas, key);
				if(index >= 0)
				{
					return data.datas[index].str;
				}
			}
			return def;
		}

		static int FindDataIndex(List<CacheItem> datas, string key)
		{
			for (int i = 0; i < datas.Count; i++)
			{
				if (datas[i].key == key)
					return i;
			}
			
			return -1;
		}
		
		static CacheItem FindData(List<CacheItem> datas, string key)
		{
			var index = FindDataIndex(datas, key);
			if (index < 0)
				return null;

			return datas[index];
		}

		static public int GetIntValue(string key, int def = default)
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var index = FindDataIndex(data.datas, key);//.FindIndex(v => v.key == key);
				if (index >= 0)
				{
					return data.datas[index].val;
				}
			}
			return def;
		}

		static public double GetDoubleValue(string key, double def = default)
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var index = FindDataIndex(data.datas, key);
				if (index >= 0)
				{
					return data.datas[index].dval;
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
				var item = FindData(data.datas, key);//data.datas.Find(v => v.key == key);
				if (item == null)
					data.datas.Add(new CacheItem() { key = key, val = val });
				else
					item.val = val;
			}
		}

		static public void SetDoubleValue(string key, double val)
		{
			var data = Instance.cacheData;
			if (!string.IsNullOrEmpty(key))
			{
				var item = FindData(data.datas, key); //
				if (item == null)
					data.datas.Add(new CacheItem() { key = key, dval = val });
				else
					item.dval = val;
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
		public double dval;

		[System.NonSerialized]
		public bool save;

		public bool Equals(CacheItem other)
		{
			return other.key == key;
		}
	}

}
