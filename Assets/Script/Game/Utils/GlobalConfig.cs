using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SGame;
using UnityEngine;

//全局配置
namespace GameConfigs
{
	public class GlobalConfig
	{
		//枚举类型
		public enum VType
		{
			VInt = 0,
			VStr = 1,
			VFloat = 2
		}

		private static Dictionary<string, string> _iniCfgs = null;

		//获取判断第一行第一个值的类型，是否在服务器上已经存在
		//类型是Int或Bool时返回一整行数据
		//返回空值
		public static int GetInt(string name)
		{
			//查找是否有替代值
			if (TryGetIniCfg("@" + name, out var v) && int.TryParse(v, out var fv))
			{
				return fv;
			}

			//找到表里面的数值
			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.game_globalRowData val))
			{
				if (val.Type == (int)VType.VInt)
				{
					if (int.TryParse(val.Value, out int result))
					{
						return result;
					}
				}
			}

			//查找是否有临时配置
			if (TryGetIniCfg(name, out v) && int.TryParse(v, out fv))
			{
				return fv;
			}

			GameDebug.LogWarning("Parse Fail=" + name);
			return 0;
		}

		//类型是float时输出数据
		public static float GetFloat(string name)
		{
			if (TryGetIniCfg("@" + name, out var v) && float.TryParse(v, out var fv))
			{
				return fv;
			}

			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.game_globalRowData val))
			{
				if (val.Type == (int)VType.VFloat)
				{
					if (float.TryParse(val.Value, out float result))
					{
						return result;
					}
				}
			}

			if (TryGetIniCfg(name, out v) && float.TryParse(v, out fv))
			{
				return fv;
			}

			GameDebug.LogWarning("Parse Fail=" + name);
			return 0;
		}


		public static string GetStr(string name)
		{
			return GetStr(name, false);
		}

		//类型是string时输出数据
		public static string GetStr(string name, bool checkempty)
		{
			if (TryGetIniCfg("@" + name, out var v) && (!checkempty || !string.IsNullOrEmpty(v)))
			{
				return v;
			}

			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.game_globalRowData val))
			{
				if (val.Type == (int)VType.VStr && (!checkempty || !string.IsNullOrEmpty(v)))
				{
					return val.Value;
				}
			}

			if (TryGetIniCfg(name, out v))
			{
				return v;
			}

			GameDebug.LogWarning("Parse Fail=" + name);
			return null;

		}

		public static bool TryGetIniCfg(string key, out string val)
		{
			val = default;
			if (_iniCfgs == null)
			{
				//工程配置
				var asset = Resources.Load<TextAsset>("app");
				if (asset != null)
					_iniCfgs = Utils.IniParserBySplit(asset.text, Environment.NewLine);
				else
					_iniCfgs = new Dictionary<string, string>();

				///本地配置
				asset = Resources.Load<TextAsset>("local");
				if (asset)
				{
					var local = Utils.IniParserBySplit(asset.text, Environment.NewLine);
					if (local != null && local.Count > 0)
					{
						foreach (var item in local)
							_iniCfgs[item.Key] = item.Value;
					}
				}

			}
			if (_iniCfgs.Count == 0) return false;
			return _iniCfgs.TryGetValue(key, out val);
		}

	}
}
