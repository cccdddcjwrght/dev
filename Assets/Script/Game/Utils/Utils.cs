using System.Collections.Generic;
using System;
using System.Linq;
using GameConfigs;
using Unity.Entities;
using UnityEngine;
using System.Globalization;
using Unity.Transforms;
using System.Text;
using log4net;
using Unity.Mathematics;
using FairyGUI;
using GameTools;
using SGame.UI;
using System.Collections;

namespace SGame
{
	public partial class Utils
	{
		private static ILog log = LogManager.GetLogger("game.utils");

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

		public static string ConvertNumberStrLimit3(double number) => ConvertNumberStr(number, 3);

		//把数值转成专用字符串表示
		public static string ConvertNumberStr(double number, int limit = 0)
		{
			var unit = "";
			if (number >= 1000)
			{
				int a, b;
				a = b = 0;
				while (number >= 1000)
				{
					b++;
					if (b > 9) { a++; b = 9; } //999P--->1KP
					number = (number * 0.001d).Round();
				}
				unit = string.Format("{0}{1}", a > 0 ? c_price[a] : "", c_price[b]);//单位
			}
			if (limit > 0)
				return number.Round().ToString($"G{limit}") + unit;
			else
				return number.Round().ToString() + unit;
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
			EnumRole r = (EnumRole)roleType;
			switch (roleType)
			{
				case (int)EnumRole.Cook:
					return EnumTarget.Cook;
				case (int)EnumRole.Waiter:
					return EnumTarget.Waiter;
				case (int)EnumRole.Customer:
				case (int)EnumRole.Car:
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
				case (int)EnumRole.Cook:
				case (int)EnumRole.Player:
					return "born_0";
				case (int)EnumRole.Waiter:
					return "born_1";
				case (int)EnumRole.Customer:
					return "born_3";
			}

			return "";
		}

		/// <summary>
		/// 通过角色类型获得目标类型
		/// </summary>
		/// <param name="roleType"></param>
		/// <returns></returns>
		public static EnumTarget GetTargetFromRoleTypeEnum(EnumRole roleType)
		{
			return GetTargetFromRoleType((int)roleType);
		}

		/// <summary>
		/// 通过角色类型获得位置标签
		/// </summary>
		/// <param name="roleType"></param>
		/// <returns></returns>
		public static string GetMapTagFromRoleTypeEnum(EnumRole roleType)
		{
			return GetMapTagFromRoleType((int)roleType);
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


		/// <summary>
		/// 添加子节点
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		public static void AddEntityChild(Entity parent, Entity child)
		{
			var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

			// 父节点设置
			DynamicBuffer<Child> childBuffer;
			if (!EntityManager.HasComponent<Child>(parent))
			{
				childBuffer = EntityManager.AddBuffer<Child>(parent);
			}
			else
			{
				childBuffer = EntityManager.GetBuffer<Child>(parent);
			}
			childBuffer.Add(new Child() { Value = child });

			// 关联子节点, 必须的又LocalToParent
			if (!EntityManager.HasComponent<Parent>(child))
			{
				EntityManager.AddComponent<Parent>(child);
			}
			EntityManager.SetComponentData(child, new Parent() { Value = parent });
			if (EntityManager.HasComponent<LocalToWorld>(child) && !EntityManager.HasComponent<LocalToParent>(child))
			{
				EntityManager.AddComponent<LocalToParent>(child);
			}
		}

		/// <summary>
		/// 删除子节点
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="Child"></param>
		public static void RemoveEntityChild(Entity parent, Entity child)
		{
			var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

			// 删除父节点中的子节点
			if (EntityManager.HasComponent<Child>(parent))
			{
				var childBuffer = EntityManager.AddBuffer<Child>(parent);
				for (int i = 0; i < childBuffer.Length; i++)
				{
					if (childBuffer[i].Value == child)
					{
						childBuffer.RemoveAtSwapBack(i);
						break;
					}
				}
			}

			if (EntityManager.HasComponent<Parent>(child))
			{
				// 防止经常删除添加, 这里直接使用赋值
				EntityManager.RemoveComponent<Parent>(child);
			}

			if (EntityManager.HasComponent<LocalToParent>(child))
			{
				EntityManager.RemoveComponent<LocalToParent>(child);
			}
		}

		/// <summary>
		/// 获取旋转角度, 以二维里的Y轴为起点
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>0-360 角度</returns>
		public static float GetRotation(float x, float y)
		{
			Vector2 dir = new Vector2(x, y);
			dir = dir.normalized;
			float cosAngle = Mathf.Acos(dir.y); // 通过反余弦计算角度
			if (x >= 0)
				return cosAngle * Mathf.Rad2Deg;

			return 360 - cosAngle * Mathf.Rad2Deg;
		}

		public static bool SwitchRemove(List<int> value, int index)
		{
			if (index >= value.Count || index < 0)
				return false;

			if (value.Count - 1 == index)
				return false;

			var temp = value[index];
			value[index] = value[value.Count - 1];
			value[value.Count - 1] = temp;
			return true;
		}

		#region 广告

		public static void PlayAd(string ad, Action<bool, string> complete = null)
		{
			var state = false;
			log.Info("Play AD : " + ad);
			DoPlayAd(ad, complete, ref state);
			if (!state)
			{
				log.Info("no ad sdk");
				complete?.Invoke(true, null);
			}
		}

		static partial void DoPlayAd(string ad, Action<bool, string> complete, ref bool state);

		#endregion

		#region Recharge

		public static void Pay(string id, Action<bool, string> complete = null)
		{
			var state = false;
			DoPay(id, complete, ref state);
			if (!state)
			{
				log.Info("no pay sdk");
				complete?.Invoke(true, null);
			}
		}

		static partial void DoPay(string id, Action<bool, string> complete, ref bool state);

		#endregion

		#region TimeFormat

		static List<string[]> tfs = new List<string[]>(){
			new string[]{ "{0:D2}:{1:D2}", "{0}:{1:D2}", "{0}:{1:D2}:{2:D2}", "{0}Day {1:D2}", "{0}Day" },
			new string[]{ "{0:D2}分{1:D2}秒", "{0}小时{1:D2}分", "{0}小时{1:D2}分{2:D2}秒", "{1}天{1:D2}小时", "{0}天" },
			new string[]{ "{0}M{1:d2}S", "{0}H{1:D2}M", "{0}H{1:D2}M{2:D2}S", "{0}D{1:D2}H", "{0}D" },
			new string[]{ "{0:D2}m {1:D2}s", "{0}h {1:D2}m", "{0}h {1:D2}m {2:D2}s", "{0}d {1:D2}h", "{0}d" }
		};

		/// <summary>
		/// 时间格式化
		/// </summary>
		/// <param name="time">总时间（秒）</param>
		/// <param name="locType">格式索引</param>
		/// <param name="needsec">是否需要秒</param>
		/// <param name="daylimit">天数分割上限</param>
		/// <param name="formats">自定义格式 { 分：秒  | 时：分 | 时：分：秒 | 天：时 | 天 }</param>
		/// <returns></returns>
		static public string FormatTime(int time, int locType = 2, bool needsec = true, int daylimit = 3, string[] formats = null)
		{
			var hour = (int)math.floor(time / 3600);
			var min = (int)math.floor(math.fmod(time, 3600) / 60);
			var sec = (int)math.fmod(math.fmod(time, 3600), 60);
			formats = formats ?? tfs[math.clamp(locType, 0, tfs.Count - 1)];

			if (hour <= 0) return string.Format(formats[0], min, sec);
			if (hour > 24 && daylimit > 0)
			{
				var day = math.floor(hour / 24);
				if (day >= daylimit) return string.Format(formats[4], day);
			}
			if (needsec)
				return string.Format(formats[2], hour, min, sec);
			return string.Format(formats[1], hour, min);
		}

		/// <summary>
		/// 定时器
		/// </summary>
		/// <param name="time">秒</param>
		/// <param name="update">帧回调</param>
		/// <param name="target">挂载定时器的目标</param>
		/// <param name="delay">延迟</param>
		/// <param name="start">开始回调</param>
		/// <param name="completed">完成回调</param>
		/// <returns></returns>
		static public Action<bool> Timer(float time, Action update, object target = null, float delay = 0, Action start = null, Action completed = null)
		{
			if (time > 0)
			{
				var tween = GTween.To(0, 1, time).SetDelay(delay).SetTarget(target);
				if (tween != null)
				{
					if (update != null) tween.OnUpdate(() => update());
					if (completed != null) tween.OnComplete(() => completed());
					if (start != null) tween.OnComplete(() => start());
					return new Action<bool>(s => tween.Kill(s));
				}
			}
			return default;
		}

		#endregion

		static public string GetItemIcon(int type, int id)
		{
			var icon = string.Empty;
			switch (type)
			{
				case 1:
					if (ConfigSystem.Instance.TryGet<ItemRowData>(id, out var item))
						icon = item.Icon;
					break;
			}
			return icon;
		}

		/// <summary>
		/// 获得设置界面语言名字
		/// </summary>
		/// <param name="lang_setting"></param>
		/// <returns></returns>
		static public string GetLangName(int lang_setting)
		{
			if (!ConfigSystem.Instance.TryGet(lang_setting, out Language_settingRowData config))
			{
				log.Error("lang not found=" + lang_setting);
				return "en";
			}

			return config.Label;
		}

		/// <summary>
		/// 获取地图格子的对象
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		static public Transform GetGirdObject(Vector2Int pos)
		{
			string objectName = string.Format("({0}x0x{1})", pos.x, pos.y);
			Transform root = MapAgent.agent.transform.FindRecursive(objectName);
			if (root == null)
			{
				return null;
			}

			Transform body = root.GetChild(0);
			if (body == null)
			{
				return null;
			}

			return body.GetChild(0);
		}

		/// <summary>
		/// 判断角色加载完成
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static bool CharacterReadly(Entity e)
		{
			bool ret = CharacterModule.Instance.IsReadly(e);
			log.Info("CheckReadly=" + e);
			return ret;
		}

		/// <summary>
		/// 通过Entity获取Character
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static Character GetCharacterFromEntity(Entity e)
		{
			return CharacterModule.Instance.FindCharacter(e);
		}

		/// <summary>
		/// 判断节点是否有某个tag
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static bool MapHasTag(string tag, Vector3 pos)
		{
			var map_pos = MapAgent.VectorToGrid(pos);
			var tags = MapAgent.GetTagsByPos(map_pos.x, map_pos.y);

			if (tags == null || tags.Count == 0)
				return false;
			bool ret = tags.Contains(tag);
			
			//log.Info(string.Format("check tag pos {0}, map_pos={1}, ret={2}", pos, map_pos, ret));
			return ret;
		}

		/// <summary>
		/// 提取角色字符串, 并返回提取后的字符串
		/// </summary>
		/// <param name="part"></param>
		/// <param name="la"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string PickCharacterPart(string part, string key, out string ret)
		{
			part = part.ToLower();
			string[] settings	= part.Split('|');
			var currentCharacter = settings[0];
			var keyvalue		= new Dictionary<string, string>();
			ret = currentCharacter;
			string value = "";
			
			for (int i = 1; i < settings.Length; )
			{
				string categoryName = settings[i++];
				string elementName = settings[i++];

				if (categoryName == key)
				{
					value = elementName;
				}
				else
				{
					ret += "|" + categoryName + "|";
					ret += elementName;
				}
			}

			return value;
		}

		/// <summary>
		/// 创建角色接口
		/// </summary>
		/// <param name="part"></param>
		/// <returns></returns>
		public static IEnumerator GenCharacter(string part)
		{
			var weaponStr = Utils.PickCharacterPart(part, "weapon", out string newPart);
			var gen = CharacterGenerator.CreateWithConfig(newPart);
			while (gen.ConfigReady == false)
				yield return null;

			var ani = gen.Generate();
			if (!string.IsNullOrEmpty(weaponStr))
			{
				if (!int.TryParse(weaponStr, out int weaponID))
				{
					log.Error("parse weapon id fail=" + weaponStr);
				}
				else
				{
					Equipments equip = ani.AddComponent<Equipments>();
					yield return null;
					equip.SetWeapon(weaponID);
				}
			}

			yield return ani;
		}
		
	}
}