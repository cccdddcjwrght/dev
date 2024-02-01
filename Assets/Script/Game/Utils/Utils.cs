using System.Collections.Generic;
using System;
using System.Linq;
using GameConfigs;
using Unity.Entities;
using UnityEngine;
using System.Globalization;
using System.Text;

namespace SGame
{
	public partial class Utils
	{
		static public Dictionary<string, string> IniParser(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				if (System.IO.File.Exists(path))
				{
					return IniParser(System.IO.File.ReadAllLines(path));
				}
			}
			return null;
		}

		public static Entity Vector2Entity(Vector2Int v)
		{
			return new Entity { Index = v.x, Version = v.y };
		}

		public static Vector2Int Entity2Vector(Entity e)
		{
			return new Vector2Int(e.Index, e.Version);
		}

		static public Dictionary<string, string> IniParser(params string[] lines)
		{
			if (lines != null && lines.Length > 0)
			{
				var datas = lines.Where(Line => !string.IsNullOrEmpty(Line) && !Line.StartsWith("#") && Line.IndexOf('=') > 0)
					.Select(Line => Line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
					.Where(ls => ls.Length > 1)
					.ToDictionary(Parts => Parts[0].Trim(), Parts => Parts.Length > 1 ? Parts[1].Trim() : null);
				return datas;
			}
			return null;
		}

		static public Dictionary<string, string> IniParserBySplit(string text, string splitChar)
		{
			if (!string.IsNullOrEmpty(text))
			{
				return IniParser(text.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries));
			}
			return null;
		}

		public static long Clamp(long value, long min, long max)
		{
			if (value >= max)
				value = max;

			if (value <= min)
				value = min;

			return value;
		}

		/// <summary>
		/// 时间倒计时
		/// </summary>
		/// <param name="time"></param>
		/// <returns></returns>
		public static string TimeFormat(float time)
		{
			if (time <= 0)
				time = 0;
			double t = time;
			t *= TimeSpan.TicksPerSecond;
			DateTime dt = new DateTime((long)t);
			if (dt.Hour > 0)
				return dt.ToString("HH:mm:ss");
			return dt.ToString("mm:ss");
		}

		/// <summary>
		/// 获得场景中数据单列
		/// </summary>
		/// <param name="entityManager"></param>
		/// <param name="data"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool TryGetSingletonData<T>(EntityManager entityManager, out T data) where T : struct, IComponentData
		{
			data = default;
			EntityQuery query = entityManager.CreateEntityQuery(typeof(T));
			if (query.CalculateEntityCount() == 0)
				return false;

			data = query.GetSingleton<T>();
			return true;
		}

		/// <summary>
		/// 获得场景中数据单列
		/// </summary>
		/// <param name="entityManager"></param>
		/// <param name="data"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool TryGetSingletonObject<T>(EntityManager entityManager, out T data) where T : class, IComponentData
		{
			data = default;
			EntityQuery query = entityManager.CreateEntityQuery(typeof(T));
			if (query.CalculateEntityCount() == 0)
				return false;

			data = query.GetSingleton<T>();
			return true;
		}


		/// <summary>
		/// 激活或禁止ENTITY
		/// </summary>
		/// <param name="mgr"></param>
		/// <param name="e"></param>
		/// <param name="enable"></param>
		/// <returns></returns>
		public static bool EnableEntity(EntityManager mgr, Entity e, bool enable)
		{
			if (!mgr.Exists(e))
			{
				return false;
			}

			if (enable && mgr.HasComponent<Disabled>(e))
			{
				mgr.RemoveComponent<Disabled>(e);
				return true;
			}

			if (!enable && !mgr.HasComponent<Disabled>(e))
			{
				mgr.AddComponent<Disabled>(e);
				return true;
			}

			return false;
		}


		static readonly char[] c_price = new char[] { default, 'K', 'M', 'G', 'T', 'P', 'E', 'Z', 'Y', 'B' };
		//把数值转成专用字符串表示
		public static string ConvertNumberStr(double number)
		{
			if (number > 1000)
			{
				int a, b;
				a = b = 0;
				while (number > 1000)
				{
					if (b++ > 9) { a++; b = 1; }
					number *= 0.001d;
				}
				return string.Format("{0}{1}{2}", number.Round(), a > 0 ? c_price[a] : "", c_price[b]);
			}
			return number.ToString();
		}

		/// <summary>
		/// 两位小数
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		static public double Round(double number, int digits = 2)
		{
			return number.Round(digits);
		}

		/// <summary>
		/// 取整
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		static public double ToInt(double number)
		{
			return number.ToInt();
		}

		/// <summary>
		/// 通过角色类型获得目标类型
		/// </summary>
		/// <param name="roleType"></param>
		/// <returns></returns>
		public static EnumTarget GetTargetFromRoleType(int roleType)
		{
			RoleType r = (RoleType)roleType;
			switch (roleType)
			{
				case (int)RoleType.CHEF:
					return EnumTarget.Cook;
				case (int)RoleType.WAITER:
					return EnumTarget.Waiter;
				case (int)RoleType.CUSTOMER:
				case (int)RoleType.CAR:
					return EnumTarget.Customer;
			}

			return EnumTarget.Player;
		}

		/// <summary>
		/// 通过角色类型获得位置标签
		/// </summary>
		/// <param name="roleType"></param>
		/// <returns></returns>
		public static string GetMapTagFromRoleType(int roleType)
		{
			switch (roleType)
			{
				case (int)RoleType.CHEF:
				case (int)RoleType.PLAYER:
					return "born_0";
				case (int)RoleType.WAITER:
					return "born_1";
				case (int)RoleType.CUSTOMER:
					return "born_3";
			}

			return "";
		}


		#region 语言

		//本机语言
		public static string MachineLanguage()
		{
			switch (Application.systemLanguage)
			{
				case SystemLanguage.Afrikaans:
					return "af";
				case SystemLanguage.Arabic:
					return "ar";
				case SystemLanguage.Basque:
					return "eu";
				case SystemLanguage.Belarusian:
					return "be";
				case SystemLanguage.Bulgarian:
					return "bg";
				case SystemLanguage.Catalan:
					return "ca";
				case SystemLanguage.Chinese:
					return "chs";
				case SystemLanguage.Czech:
					return "cs";
				case SystemLanguage.Danish:
					return "da";
				case SystemLanguage.Dutch:
					return "nl";
				case SystemLanguage.English:
					return "en";
				case SystemLanguage.Estonian:
					return "et";
				case SystemLanguage.Faroese:
					return "fo";
				case SystemLanguage.Finnish:
					return "fu";
				case SystemLanguage.French:
					return "fr";
				case SystemLanguage.German:
					return "de";
				case SystemLanguage.Greek:
					return "el";
				case SystemLanguage.Hebrew:
					return "he";
				case SystemLanguage.Icelandic:
					return "is";
				case SystemLanguage.Indonesian:
					return "id";
				case SystemLanguage.Italian:
					return "it";
				case SystemLanguage.Japanese:
					return "ja";
				case SystemLanguage.Korean:
					return "ko";
				case SystemLanguage.Latvian:
					return "lv";
				case SystemLanguage.Lithuanian:
					return "lt";
				case SystemLanguage.Norwegian:
					return "nn";
				case SystemLanguage.Polish:
					return "pl";
				case SystemLanguage.Portuguese:
					return "pt";
				case SystemLanguage.Romanian:
					return "ro";
				case SystemLanguage.Russian:
					return "ru";
				case SystemLanguage.SerboCroatian:
					return "sr";
				case SystemLanguage.Slovak:
					return "sk";
				case SystemLanguage.Slovenian:
					return "sl";
				case SystemLanguage.Spanish:
					return "es";
				case SystemLanguage.Swedish:
					return "sv";
				case SystemLanguage.Thai:
					return "th";
				case SystemLanguage.Turkish:
					return "tr";
				case SystemLanguage.Ukrainian:
					return "uk";
				case SystemLanguage.Vietnamese:
					return "vi";
				case SystemLanguage.ChineseSimplified:
					return "chs";
				case SystemLanguage.ChineseTraditional:
					return "zh";
				case SystemLanguage.Hungarian:
					return "hu";
				case SystemLanguage.Unknown:
					return "unknown";

			}
			;
			return default;
		}

		public static string GetLanguage()
		{
			var info = CultureInfo.CurrentCulture;
			if (info != null)
			{
				var lan = info.ToString().ToLower();
				if (lan.StartsWith("zh"))
				{
					switch (lan)
					{
						case "zh":
						case "zh-cn":
						case "zh-sg":
							return "chs";
						case "zh-tw":
						case "zh-hk":
						case "zh-mo":
						case "zh-hant":
							return "zh";
					}
				}
				return lan.Split('-')[0];
			}
			return MachineLanguage();
		}
		#endregion

	}
}