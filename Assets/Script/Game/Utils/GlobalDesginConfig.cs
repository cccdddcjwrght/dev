using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SGame;
using UnityEngine;

// 策划全局配置
namespace GameConfigs
{
	public class GlobalDesginConfig
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
			//找到表里面的数值
			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.desgin_globalRowData val))
			{
				if (val.Type == (int)VType.VInt)
				{
					if (int.TryParse(val.Value, out int result))
					{
						return result;
					}
				}
			}

			GameDebug.LogWarning("Parse Fail=" + name);
			return 0;
		}

		//类型是float时输出数据
		public static float GetFloat(string name)
		{
			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.desgin_globalRowData val))
			{
				if (val.Type == (int)VType.VFloat)
				{
					if (float.TryParse(val.Value, out float result))
					{
						return result;
					}
				}
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
			if (SGame.ConfigSystem.Instance.TryGet(name, out GameConfigs.desgin_globalRowData val))
			{
				if (val.Type == (int)VType.VStr && (!checkempty))
				{
					return val.Value;
				}
			}

			
			GameDebug.LogWarning("Parse Fail=" + name);
			return null;
		}

		public static desgin_global GetCfg(int index = 0)
		{
			ConfigSystem.Instance.TryGetByIndex<desgin_global>(index, out var data);
			return data;
		}
	}
}
