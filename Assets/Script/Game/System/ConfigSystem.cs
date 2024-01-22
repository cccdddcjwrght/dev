using Unity.Entities;
using FlatBuffers;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SGame
{
	[ExecuteAlways]
	public class ConfigSystem //: ComponentSystem
	{

		// 配置表小數縮放 一千倍
		const string ROW_NAME = "RowData";           // 行数据名称
		const string CONFIG_PATH = "Assets/BuildAsset/configs/";
		const string CONFIG_EXT = ".bytes";

		Dictionary<Type, ByteBuffer> _buffers;                          // type 到类型到buffer, 用于快速找到对于的数据
		Dictionary<string, ByteBuffer> _bufferFromFileName;               // 配置文件名到buffer
		Dictionary<Type, Dictionary<int, int>> _intKeys;                          // 以int为key
		Dictionary<Type, Dictionary<string, int>> _strKeys;                          // 以str为key

		protected void OnCreate()
		{
			// 判断资源伤是否已经加载
#if UNITY_EDITOR
			if (Application.isPlaying == false)
			{

			}
#endif

			_buffers = new Dictionary<Type, ByteBuffer>();
			_bufferFromFileName = new Dictionary<string, ByteBuffer>();
			_intKeys = new Dictionary<Type, Dictionary<int, int>>();
			_strKeys = new Dictionary<Type, Dictionary<string, int>>();
		}

		private static ConfigSystem _instance;

		public static ConfigSystem Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ConfigSystem();
					_instance.OnCreate();
				}
				return _instance;
			}
		}

		#region private

		// 通过类型获得表名字
		static string GetTableName(Type t)
		{
			string tableName = t.Name;
			if (tableName.EndsWith(ROW_NAME) == true)
			{
				// 表的行名
				tableName = tableName.Substring(0, tableName.Length - ROW_NAME.Length);
			}

			return tableName;
		}

		// isRow 是行类型, 还是表类型
		static string GetConfigPath(string tableName)
		{
			string configPath = CONFIG_PATH + tableName + CONFIG_EXT;
			return configPath;
		}

		// 获得配置数量
		public int GetConfigCount(Type config)
		{
			ByteBuffer bb = GetBuffer(config);
			int index = bb.GetInt(bb.Position) + bb.Position;
			Table p = new Table(index, bb);
			return DatalistLength(in p);
		}

		public T LoadConfigFromIndex<T>(int idx, ByteBuffer bb) where T : IFlatbufferObject
		{
			int index = bb.GetInt(bb.Position) + bb.Position;

			Table p = new Table(index, bb);
			int count = DatalistLength(in p);

			T ret = default(T);
			if (count <= idx)
			{
				Debug.LogError("out of index=" + idx.ToString());
				return ret;
			}

			// 定位
			int o = p.__offset(4);          // 获取数量
			ret.__init(p.__indirect(p.__vector(o) + idx * 4), p.bb);
			return ret;
		}

		public static int DatalistLength(in Table t)
		{
			int o = t.__offset(4);
			return o != 0 ? t.__vector_len(o) : 0;
		}

		// 获得字符串
		static string GetFirstItemStr(in Table p)
		{
			int o = p.__offset(4);
			return o != 0 ? p.__string(o + p.bb_pos) : null;
		}

		static private void initStrKeys(Dictionary<string, int> keys, ByteBuffer bb)
		{
			int index = bb.GetInt(bb.Position) + bb.Position;
			Table p = new Table(index, bb);
			int count = DatalistLength(in p);

			// 便利数据, 获取第一个字段
			int o = p.__offset(4);
			for (int i = 0; i < count; i++)
			{
				Table item = new Table(p.__indirect(p.__vector(o) + i * 4), bb);

				string key = GetFirstItemStr(in item);
				keys.Add(key, i);
			}
		}

		static private void initIntKeys(Dictionary<int, int> keys, ByteBuffer bb)
		{
			int index = bb.GetInt(bb.Position) + bb.Position;
			Table p = new Table(index, bb);
			int count = DatalistLength(in p);

			// 便利数据, 获取第一个字段
			int o = p.__offset(4);
			for (int i = 0; i < count; i++)
			{
				Table item = new Table(p.__indirect(p.__vector(o) + i * 4), bb);

				int key = GetFirstItemInt(in item);
				keys.Add(key, i);
			}
		}

		// 获得Int
		static int GetFirstItemInt(in Table p)
		{
			int o = p.__offset(4);
			return o != 0 ? p.bb.GetInt(o + p.bb_pos) : 0;
		}

		private ByteBuffer GetBuffer(string tableName)
		{
			ByteBuffer bb = null;

			// 通过名字找
			if (_bufferFromFileName.TryGetValue(tableName, out bb) == false)
			{
				string tablePath = GetConfigPath(tableName);
				var asset = libx.Assets.LoadAsset(tablePath, typeof(TextAsset));
				if (asset.isDone == false || string.IsNullOrEmpty(asset.error) == false)
				{
					if (!string.IsNullOrEmpty(asset.error))
					{
						GameDebug.LogError("Load Asset Fail=" + asset.error);
					}
					asset.Release();
					return null;
				}

				TextAsset text_asset = asset.asset as TextAsset;
				Byte[] data = text_asset.bytes;
				bb = new ByteBuffer(data);
				_bufferFromFileName.Add(tableName, bb);
				asset.Release();
			}

			return bb;
		}

		private ByteBuffer GetBuffer(Type t)
		{
			ByteBuffer bb = null;
			// 直接通过类型找不到 (不能直接通过名字查找, 因为 获取名字等方法 会产生gc)
			if (_buffers.TryGetValue(t, out bb) == false)
			{
				// 通过名字找
				string tableName = GetTableName(t);
				if (_bufferFromFileName.TryGetValue(tableName, out bb) == false)
				{
					string tablePath = GetConfigPath(tableName);
					var asset = libx.Assets.LoadAsset(tablePath, typeof(TextAsset));
					if (asset.isDone == false || string.IsNullOrEmpty(asset.error) == false)
					{
						if (!string.IsNullOrEmpty(asset.error))
						{
							GameDebug.LogError("Load Asset Fail=" + asset.error);
						}
						asset.Release();
						return null;
					}

					TextAsset text_asset = asset.asset as TextAsset;
					Byte[] data = text_asset.bytes;
					bb = new ByteBuffer(data);
					_buffers.Add(t, bb);
					_bufferFromFileName.Add(tableName, bb);
					asset.Release();
				}
			}

			return bb;
		}
		#endregion

		#region public function

		// 通过第一个字段为key
		public bool TryGet<T>(int key, out T ret) //where T : IFlatbufferObject 
									   where T : IFlatbufferObject
		{
			Type t = typeof(T);
			ret = default(T);
			ByteBuffer bb = GetBuffer(t);
			if (bb == null)
			{
				return false;
			}

			// 没有就创建
			Dictionary<int, int> keys = null;
			if (_intKeys.TryGetValue(t, out keys) == false)
			{
				keys = new Dictionary<int, int>();
				initIntKeys(keys, bb);
				_intKeys.Add(t, keys);
			}

			// 查找key
			int index = 0;
			if (keys.TryGetValue(key, out index) == false)
			{
				//log.Error("key not found = " + key.ToString());
				return false;
			}

			ret = LoadConfigFromIndex<T>(index, bb);
			return true;
		}

		// 通过第一个字段为string
		public bool TryGet<T>(string key, out T ret) //where T : IFlatbufferObject
										   where T : IFlatbufferObject
		{
			Type t = typeof(T);
			ByteBuffer bb = GetBuffer(t);
			ret = default(T);
			if (bb == null)
			{
				return false;
			}

			// 没有就创建
			Dictionary<string, int> keys = null;
			if (_strKeys.TryGetValue(t, out keys) == false)
			{
				keys = new Dictionary<string, int>();
				initStrKeys(keys, bb);
				_strKeys.Add(t, keys);
			}

			// 查找key
			int index = 0;
			if (keys.TryGetValue(key, out index) == false)
			{
				//log.Error("key not found = " + key.ToString());
				return false;
			}

			ret = LoadConfigFromIndex<T>(index, bb);
			return true;
		}

		public bool TryGetByIndex<T>(int index, out T ret) where T : IFlatbufferObject
		{
			Type t = typeof(T);
			ByteBuffer bb = GetBuffer(t);
			ret = default(T);
			if (bb == null)
				return false;

			int pos = bb.GetInt(bb.Position) + bb.Position;
			Table p = new Table(pos, bb);
			int count = DatalistLength(in p);
			if (count > index)
			{
				ret = LoadConfigFromIndex<T>(index, bb);
				return true;
			}

			return true;
		}

		// 获取整个表, 主要是用于遍历
		public T LoadConfig<T>() where T : IFlatbufferObject
		{
			Type t = typeof(T);
			ByteBuffer bb = GetBuffer(t);
			if (bb != null)
			{
				T ret = default(T);
				ret.__init(bb.GetInt(bb.Position) + bb.Position, bb);
				return ret;
			}

			return default(T);
		}
		
		// 指定表名获取表
		public T LoadConfigFromTableName<T>(string tableName) where T : IFlatbufferObject
		{
			Type t = typeof(T);
			ByteBuffer bb = GetBuffer(tableName);
			if (bb != null)
			{
				T ret = default(T);
				ret.__init(bb.GetInt(bb.Position) + bb.Position, bb);
				return ret;
			}

			return default(T);
		}

		public bool TryGet<T>(Func<T, bool> condition, out T data) where T : IFlatbufferObject
		{
			data = default;
			if (condition != null)
			{
				Type t = typeof(T);
				ByteBuffer bb = GetBuffer(t);
				if (bb == null) return false;
				int index = bb.GetInt(bb.Position) + bb.Position;
				Table p = new Table(index, bb);
				int count = DatalistLength(in p);
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						var item = LoadConfigFromIndex<T>(i, bb);
						if (item.ByteBuffer != null && condition(item))
						{
							data = item;
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool TryGets<T>(Func<T, bool> condition, out List<T> datas) where T : IFlatbufferObject
		{
			datas = null;
			if (condition != null)
			{
				Type t = typeof(T);
				ByteBuffer bb = GetBuffer(t);
				if (bb == null) return false;
				int index = bb.GetInt(bb.Position) + bb.Position;
				Table p = new Table(index, bb);
				int count = DatalistLength(in p);
				if (count > 0)
				{
					datas = new List<T>();
					for (int i = 0; i < count; i++)
					{
						var item = LoadConfigFromIndex<T>(i, bb);
						if (condition(item))
						{
							datas?.Add(item);
						}
					}
					return datas.Count > 0;
				}

			}
			return false;
		}

		public T Find<T>(Func<T, bool> condition) where T : IFlatbufferObject
		{
			TryGet<T>(condition, out var v);
			return v;
		}

		public List<T> Finds<T>(Func<T, bool> condition) where T : IFlatbufferObject
		{
			TryGets<T>(condition, out var v);
			return v;
		}

		public IFlatbufferObject LoadConfigFromIndex(IFlatbufferObject ret, int idx, ByteBuffer bb)
		{
			int index = bb.GetInt(bb.Position) + bb.Position;

			Table p = new Table(index, bb);
			int count = DatalistLength(in p);

			if (count <= idx)
			{
				Debug.LogError("out of index=" + idx.ToString());
				return ret;
			}

			// 定位
			int o = p.__offset(4);          // 获取数量
			ret.__init(p.__indirect(p.__vector(o) + idx * 4), p.bb);
			return ret;
		}

		public IFlatbufferObject GetConfigByID(Type type, int id)
		{
			if (type != null && typeof(IFlatbufferObject).IsAssignableFrom(type))
			{
				IFlatbufferObject ret = New(type);
				ByteBuffer bb = GetBuffer(type);
				if (bb == null)
					return default;

				Dictionary<int, int> keys = null;
				if (_intKeys.TryGetValue(type, out keys) == false)
				{
					keys = new Dictionary<int, int>();
					initIntKeys(keys, bb);
					_intKeys.Add(type, keys);
				}

				int index = 0;
				if (keys.TryGetValue(id, out index) == false)
					return default;
				return LoadConfigFromIndex(ret, index, bb);
			}
			return null;
		}

		public IFlatbufferObject GetConfigByID(Type type, string id)
		{
			if (type != null && typeof(IFlatbufferObject).IsAssignableFrom(type))
			{
				IFlatbufferObject ret = New(type);
				ByteBuffer bb = GetBuffer(type);
				if (bb == null)
					return default;

				Dictionary<string, int> keys = null;
				if (_strKeys.TryGetValue(type, out keys) == false)
				{
					keys = new Dictionary<string, int>();
					initStrKeys(keys, bb);
					_strKeys.Add(type, keys);
				}

				int index = 0;
				if (keys.TryGetValue(id, out index) == false)
					return default;
				return LoadConfigFromIndex(ret, index, bb);
			}
			return null;
		}

		public IFlatbufferObject GetConfig(Type type, Func<IFlatbufferObject, bool> condition)
		{
			if (condition != null && type != null && typeof(IFlatbufferObject).IsAssignableFrom(type))
			{
				Type t = type;
				ByteBuffer bb = GetBuffer(t);
				if (bb == null) return default;
				int index = bb.GetInt(bb.Position) + bb.Position;
				Table p = new Table(index, bb);
				int count = DatalistLength(in p);
				if (count > 0)
				{
					IFlatbufferObject ret = New(type);
					for (int i = 0; i < count; i++)
					{
						var item = LoadConfigFromIndex(ret, i, bb);
						if (item != null && item.ByteBuffer != null && condition(item))
							return item;
					}
				}
			}
			return default;
		}

		public IFlatbufferObject GetConfig(string type, Func<IFlatbufferObject, bool> condition) {

			Type t = GetCfgItemType(type);
			if (t != null)
				return GetConfig(t, condition);
			return null;
		}

		public List<IFlatbufferObject> GetConfigs(Type type, Func<IFlatbufferObject, bool> condition)
		{
			if (condition != null && type != null && typeof(IFlatbufferObject).IsAssignableFrom(type))
			{
				Type t = type;
				ByteBuffer bb = GetBuffer(t);
				if (bb == null) return null;
				int index = bb.GetInt(bb.Position) + bb.Position;
				Table p = new Table(index, bb);
				int count = DatalistLength(in p);
				if (count > 0)
				{
					var configs = new List<IFlatbufferObject>();
					for (int i = 0; i < count; i++)
					{
						IFlatbufferObject ret = New(type);
						var item = LoadConfigFromIndex(ret, i, bb);
						if (item != null && item.ByteBuffer != null && condition(item))
							configs.Add(item);
					}
					return configs;
				}
			}
			return null;
		}

		public List<IFlatbufferObject> GetConfigs(string type, Func<IFlatbufferObject, bool> condition)
		{
			Type t = GetCfgItemType(type);
			if (t != null)
				return GetConfigs(t, condition);
			return null;
		}

		public IFlatbufferObject GetTable(string type)
		{
			Type t = Type.GetType("GameConfigs." + type);
			return GetTable(t);
		}

		public IFlatbufferObject GetConfigByID(string type, int id)
		{
			Type t = GetCfgItemType(type);
			return GetConfigByID(t, id);
		}

		public IFlatbufferObject GetConfigByID(string type, string id)
		{
			Type t = GetCfgItemType(type);
			return GetConfigByID(t, id);
		}


		public IFlatbufferObject GetTable(Type type)
		{
			Type t = type;
			if (t == null) return default;
			ByteBuffer bb = GetBuffer(t);
			if (bb != null)
			{
				IFlatbufferObject ret = New(type);
				ret.__init(bb.GetInt(bb.Position) + bb.Position, bb);
				return ret;
			}

			return default;
		}

		private Type GetCfgItemType(string type)
		{

			if (!string.IsNullOrEmpty(type))
			{
				if (type.EndsWith("RowData"))
					return Type.GetType("GameConfigs." + type, false, true);
				else
					return Type.GetType("GameConfigs." + type + "RowData", false, true);

			}

			return null;

		}

		#endregion


		void ClearInter()
		{
			_buffers = null;
			_bufferFromFileName = null;
			_intKeys = null;
			_strKeys = null;
		}


		public static void Cleanup()
		{
			if (_instance != null)
			{
				_instance.ClearInter();
				_instance = null;
			}
		}

		public void Reload()
		{
			_buffers.Clear();
			_bufferFromFileName.Clear();
			_intKeys.Clear();
			_strKeys.Clear();
		}


		public static IFlatbufferObject New(Type type)
		{
			return (IFlatbufferObject)System.Activator.CreateInstance(type);
			//return (IFlatbufferObject)(type.GetConstructor(Array.Empty<Type>())?.Invoke(Array.Empty<object>()));
		}
	}
}