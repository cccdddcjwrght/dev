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
using SGame.Firend;
using UnityEngine.SceneManagement;

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
		public static bool TryGetSingletonData<T>(EntityManager entityManager, out T data) where T : unmanaged, IComponentData
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


		static readonly string[] c_price = new string[] { default, "K", "M", "B", "T" };

		public static string ConvertNumberStrLimit3(double number) => ConvertNumberStr(number, 3);

		//把数值转成专用字符串表示
		public static string ConvertNumberStr(double number, int limit = 3)
		{
			var unit = "";
			var sign = number >= 0 ? unit : "-";
			number = Math.Abs(number);
			if (number >= 1000)
			{
				int a, b, c;
				a = -1;
				b = c = 0;
				while (number >= 1000)
				{
					b++;
					if (b > 4)
					{
						a++;
						if (a >= 26)
						{
							a = 0;
							c++;
						}
					}
					number = (number * 0.001d).Round();
				}
				if (b > 4)
					unit = string.Format("{0}{1}", (char)(c + 97), (char)(a + 97));
				else
					unit = c_price[b];
			}
			number = number.Round();
			if (limit > 0)
			{
				if (number < Math.Pow(10, limit - 1))
					return sign + number.ToString($"G{limit}") + unit;
				else
					return sign + ((int)number).ToString($"G{limit}") + unit;

			}
			else
				return sign + number.ToString() + unit;
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
					return "born_0";
				case (int)EnumRole.Player:
					return "born_2";
				case (int)EnumRole.Waiter:
					return "born_1";
				case (int)EnumRole.Customer:
					return SGame.Randoms.Random._R.NextItem(StaticDefine.CUSTOMER_TAG_BORN);
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

		/// <summary>
		/// 获取玩家外观字符串
		/// </summary>
		/// <param name="isEmployee"></param>
		/// <returns></returns>
		public static string GetPlayerOutlookString(bool isEmployee)
		{
			/// 玩家自己外观
			if (!isEmployee)
			{
				return DataCenter.EquipUtil.GetRoleEquipString();
			}

			// 邀请好友外观
			var friend = FriendModule.Instance.GetHiringFriend();
			if (friend == null)
			{
				log.Error("NOT FOUND HIRING FRIEND！");
				return DataCenter.EquipUtil.GetRoleEquipString();
			}

			// 获得好友装备信息
			var roleData = FriendModule.Instance.GetRoleData(friend.player_id);
			return DataCenter.EquipUtil.GetRoleEquipString(friend.roleID, roleData.equips);
		}

		/// <summary>
		/// 通过好友ID获得角色外观
		/// </summary>
		/// <param name="playerID"></param>
		/// <returns></returns>
		public static string GetOutlookStringFromPlayerID(int playerID)
		{
			// 玩家外观
			if (playerID == 0)
				return GetPlayerOutlookString(false);

			var friend = FriendModule.Instance.GetFriendInHirstory(playerID);
			if (friend == null)
			{
				log.Error("friend not found=" + playerID);
				return null;
			}

			var roleData = FriendModule.Instance.GetRoleData(playerID);
			return DataCenter.EquipUtil.GetRoleEquipString(friend.roleID, roleData.equips);
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

		/*
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
		*/
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
			if (dir.sqrMagnitude < 0.0001) // 距离太短
				return 0;

			float cosAngle = Mathf.Acos(dir.y); // 通过反余弦计算角度
			if (x >= 0)
				return cosAngle * Mathf.Rad2Deg;

			return 360 - cosAngle * Mathf.Rad2Deg;
		}

		/// <summary>
		/// 获得2维平面的
		/// </summary>
		/// <param name="rot"></param>
		/// <returns></returns>
		public static float GetRotationAngle(Quaternion rot)
		{
			Vector3 ret = rot * Vector3.forward;
			return GetRotation(ret.x, ret.z);
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


		public static void PlayAd(string ad, Action<bool, string> complete = null, bool perload = false)
		{
			var state = false;
			if (perload) ad = "@" + ad;
			//log.Info("<color=green>[AD]</color>Play AD : " + ad);
			DoPlayAd(ad, complete, ref state);
			if (!state)
			{
#if !SVR_RELEASE
				log.Info("no ad sdk"); 
#endif
				complete?.Invoke(true, null);
			}
		}

		public static bool IsCanPlayAd(string id)
		{
			var state = true;
			DoCheckPlayAd(id, ref state);
			return state;
		}

		static partial void DoPlayAd(string ad, Action<bool, string> complete, ref bool state);

		static partial void DoCheckPlayAd(string ad, ref bool state);

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
			new string[]{ "{0:D2}:{1:D2}", "{0}:{1:D2}", "{0}:{1:D2}:{2:D2}", "{0}Day {1:D2}", "{0}Day","{0:d2}S" },
			new string[]{ "{0:D2}分{1:D2}秒", "{0}小时{1:D2}分", "{0}小时{1:D2}分{2:D2}秒", "{1}天{1:D2}小时", "{0}天","{0:d2}秒" },
			new string[]{ "{0}M{1:d2}S", "{0}H{1:D2}M", "{0}H{1:D2}M{2:D2}S", "{0}D{1:D2}H", "{0}D","{0:d2}S" },
			new string[]{ "{0:D2}m {1:D2}s", "{0}h {1:D2}m", "{0}h {1:D2}m {2:D2}s", "{0}d {1:D2}h", "{0}d", "{0:d2}s" }
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
		static public string FormatTime(int time, int locType = 2, bool needsec = false, int daylimit = 3, string[] formats = null)
		{
			var hour = (int)math.floor(time / 3600);
			var min = (int)math.floor(math.fmod(time, 3600) / 60);
			var sec = (int)math.fmod(math.fmod(time, 3600), 60);
			formats = formats ?? tfs[math.clamp(locType, 0, tfs.Count - 1)];

			if (hour <= 0)
			{
				if (min <= 0)
					return string.Format(formats[5], sec);
				return string.Format(formats[0], min, sec);
			}
			if (hour > 24 && daylimit > 0)
			{
				var day = math.floor(hour / 24);
				hour = (int)math.floor(hour - day * 24);//(int)math.floor((time - (day * 24 * 3600)) / 3600);
				if (day >= daylimit) return string.Format(formats[3], day, hour);
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

		static public string GetItemName(int type, int id)
		{
			var icon = string.Empty;
			switch (type)
			{
				case 1:
					if (ConfigSystem.Instance.TryGet<ItemRowData>(id, out var item))
						icon = item.Name;
					break;
			}
			return icon;
		}

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
			string[] settings = part.Split('|');
			var currentCharacter = settings[0];
			var keyvalue = new Dictionary<string, string>();
			ret = currentCharacter;
			string value = "";

			for (int i = 1; i < settings.Length;)
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
			var data = CharacterPartGen.ParseString(part);
			List<string> weapons = data.GetValues("weapon");
			List<string> effects = data.GetValues("effect");
			data.RemoveDatas("weapon");
			data.RemoveDatas("effect");
			data.RemoveData("pet");
			var newPart = data.ToPartString();
			var gen = CharacterGenerator.CreateWithConfig(newPart);
			while (gen.ConfigReady == false)
				yield return null;


			var ani = gen.Generate();
			Equipments equip = ani.GetComponent<Equipments>();
			if (equip == null)
				equip = ani.AddComponent<Equipments>();
			yield return null;

			// 设置武器
			foreach (var weaponStr in weapons)
			{
				if (!int.TryParse(weaponStr, out int weaponID))
				{
					log.Error("parse weapon id fail=" + weaponStr);
				}
				else
				{
					yield return null;
					equip.SetWeapon(weaponID);
				}
			}

			// 设置特效
			foreach (var effectStr in effects)
			{
				if (!int.TryParse(effectStr, out int effectID))
				{
					log.Error("parse weapon id fail=" + effectStr);
				}
				else
				{
					equip.SetEffect(effectID);
				}
			}

			yield return ani;
		}

		public static string GetRoleEqString(int roleType)
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.RoleDataRowData>(roleType, out var role))
			{
				if (ConfigSystem.Instance.TryGet<GameConfigs.roleRowData>(role.Model, out var model))
					return model.Part;
			}
			return default;
		}

		#region List

		static private bool _getlist_remove_null = false;

		public static List<int[]> GetArrayList(params Func<int[]>[] calls)
		{

			if (calls?.Length > 0)
			{
				var list = new List<int[]>();
				for (int i = 0; i < calls.Length; i++)
				{
					var call = calls[i];
					if (call != null)
					{
						try
						{
							var v = call();
							if (!_getlist_remove_null || v != null)
								list.Add(v);
						}
						catch (Exception e)
						{
							log.Info("error:" + e.Message);
						}
					}
				}
				return list;
			}
			return default;

		}

		public static List<int[]> GetArrayList(bool removenull, params Func<int[]>[] calls)
		{
			_getlist_remove_null = true;
			var ret = GetArrayList(calls);
			_getlist_remove_null = false;
			return ret;
		}

		#endregion

		/// <summary>
		/// 判断某个模块是否每日首次登录
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool IsFirstLoginInDay(string key)
		{
			// 当前时间
			int currentTime = GameServerTime.Instance.serverTime;
			int value = DataCenter.GetIntValue(key, 0);
			if (value <= 0 || currentTime >= value)
			{
				// 首次
				DataCenter.SetIntValue(key, GameServerTime.Instance.nextDayTime);
				return true;
			}

			// 非首次, 数据不用刷新
			return false;
		}

		static public List<int[]> ConvertId2Effects(ulong effectID, List<int[]> effects)
		{
			var list = new List<int[]>();
			for (int i = effects.Count - 1; i >= 0 && effectID > 0; i--)
			{
				if (effects[i] == null) continue;
				var index = effectID;
				effectID = effectID / 100;
				index = index - effectID * 100 - 1;
				if (index >= 0)
					list.Insert(0, new int[] { effects[i][index * 2], effects[i][index * 2 + 1] });
			}
			return list;
		}

		static public ulong RandomEffectID(List<int[]> effects, List<int[]> weights, List<int[]> rets = null)
		{
			if (effects == null || weights == null || effects.Count != weights.Count) return 0;
			ulong id = 0;
			for (int i = 0; i < effects.Count; i++)
			{
				var bs = effects[i];
				if (bs == null || bs.Length == 0) break;
				var w = weights[i];
				var index = SGame.Randoms.Random._R.NextWeight(w);
				if (bs.Length > index * 2)
				{
					id = (id * 100) + ((ulong)index + 1);
					if (rets != null)
						rets.Add(new int[] { bs[index * 2], bs[index * 2 + 1] });
				}
			}
			return id;
		}

		static public List<int[]> CombineBuffInfo(List<int[]> list)
		{
			return list
				.GroupBy(v => v[0])
				.ToDictionary(v => v.Key, v => v.Sum(i => i[1]))
				.Select(v => new int[] { v.Key, v.Value })
				.ToList();
		}

		static public List<BuffData> ToBuffDatas(List<int[]> list, int from = 0)
		{

			if (list != null)
			{
				return CombineBuffInfo(list).Select(kv => new BuffData()
				{
					id = kv[0],
					val = kv[1],
					from = from,
				}).ToList();
			}
			return default;

		}

		static public bool GotoTips(object target, string tips, Action<int> call = null, bool ignoreConfirm = false)
		{
			if (target == null) return false;
			tips = tips + "\n" + "ui_goto_get".Local();
			call = call ?? new Action<int>((index) =>
			{
				if (index == 0)
				{
					if (target is int id) id.Goto();
					else if (target is string s) s.Goto();
					else if (target is bool b && b) "shop".Goto();//默认跳转商城
				}

			});
			if (!ignoreConfirm)
				SGame.UIUtils.Confirm("@ui_goto_title", tips, call, new string[] { "@ui_common_ok", "@ui_common_cancel" });
			else call(0);
			return true;
		}

		static public bool CheckItemCount(int id, double need, object tips = null, object go = null, Action<int> call = null, bool ignorConfirm = false)
		{
			if (id != 0 && need > 0)
			{
				var item = PropertyManager.Instance.GetItem(id);
				var num = item.num;
				if (need > num)
				{
					var s = false.Equals(tips);
					var t = s ? null : tips != null ? tips.ToString() : "item_not_enough".Local(null, GetItemName(1, id).Local());
					if (go == null || !GotoTips(go, t, call, ignorConfirm))
					{
						if (!s)
							t.Tips();
					}
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 获取半径碰撞点
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		static public Vector3 GetCircleHitPoint(Vector3 start, Vector3 end, float radius)
		{
			Vector3 dir = end - start;
			float len = dir.magnitude;

			// 半径内
			if (len <= radius)
			{
				return start;
			}

			dir = dir.normalized * (len - radius);
			return start + dir;
		}

		/// <summary>
		/// 通过MACHINE ID 获得权重
		/// </summary>
		/// <param name="levelID"></param>
		/// <param name="machineID"></param>
		/// <returns></returns>
		public static int GetLevelWeight(int levelID, int machineID)
		{
			if (!ConfigSystem.Instance.TryGet(levelID, out LevelRowData config))
			{
				log.Error("machineID id not found=" + machineID);
				return 0;
			}

			for (int i = 0; i < config.MachineIdLength; i++)
			{
				if (config.MachineId(i) == machineID)
				{
					return config.OrderWeight(i);
				}
			}

			log.Error("machine id not found=" + machineID);
			return 0;
		}

		/// <summary>
		/// 获得最大顾客上限
		/// </summary>
		/// <returns></returns>
		public static int GetMaxCustomer()
		{
			int value = Unity.VisualScripting.SceneVariables.Instance(SceneManager.GetActiveScene()).variables.declarations.Get<int>("MaxCustomer");
			return value;
		}

		/// <summary>
		/// 包含汽车里面的顾客
		/// </summary>
		/// <returns></returns>
		public static int GetAllMaxCustomer()
		{
			int v1 = GetMaxCustomer();
			int currentLevelID = DataCenter.Instance.roomData.roomID;
			int v2 = 0;

			GameConfigs.LevelPath paths = ConfigSystem.Instance.LoadConfig<LevelPath>();
			for (int i = 0; i < paths.DatalistLength; i++)
			{
				var item = paths.Datalist(i);
				if (item.Value.Id == currentLevelID)
				{
					var path = CarQueueManager.Instance.GetOrCreate(item.Value.PathTag);
					if (path.IsValid)
					{
						v2 += item.Value.CountNum;
					}
				}
			}

			return v1 + v2;
		}

		public static int FindCompareIndex<T>(IList<T> items, Comparison<T> compare, Func<T, bool> condition = null, int def = -1)
		{
			if (items?.Count > 0 && compare != null)
			{
				var index = 0;
				var val = items[index];
				var flag = condition == null;
				for (int i = 1; i < items.Count; i++)
				{
					var c = items[i];
					if (condition != null)
					{
						if (!condition(val))
						{
							index = i;
							val = c;
							continue;
						}
						else if (!condition(c)) continue;
						flag = true;
					}
					if (compare(c, val) == 1)
					{
						index = i;
						val = c;
					}
				}
				return flag ? index : def;
			}
			return def;
		}

		/// <summary>
		/// 快速获取两点间距离
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static int GetInt2DistanceFast(int2 p1, int2 p2)
		{
			int2 ret = p1 - p2;
			ret.x = ret.x > 0 ? ret.x : -ret.x;
			ret.y = ret.y > 0 ? ret.y : -ret.y;
			return ret.x + ret.y;
		}

		public static int GetCurrentLevelID()
		{
			int currentLevelID = DataCenter.Instance.roomData.roomID;
			return currentLevelID;
		}

		// 调用AStar寻路查询
		public static int2 GetAStarPosFromMapPos(Vector2Int mapPos)
		{
			var ret = MapAgent.GridToIndex(mapPos);
			return new int2(ret.x, ret.y);
		}

		/// <summary>
		/// 加载AI脚本
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetAIPath(string name)
		{
			string path = "Assets/BuildAsset/VisualScript/Prefabs/AI/" + name + ".prefab";
			return path;
		}

		/// <summary>
		/// 通过角色ID 获得身体部件
		/// </summary>
		/// <param name="roleID"></param>
		/// <returns></returns>
		public static string GetRolePartFromID(int roleID)
		{
			string configAI = "";
			if (!ConfigSystem.Instance.TryGet(roleID, out RoleDataRowData roleData))
			{
				throw new Exception("not found roleID=" + roleID);
			}
			if (!ConfigSystem.Instance.TryGet(roleData.Model, out roleRowData config))
			{
				throw new Exception("not found roleModleID=" + roleData.Model);
			}

			return config.Part;
		}

		/// <summary>
		/// 获得工作人员显示皮肤
		/// </summary>
		/// <param name="roleType"></param>
		/// <returns></returns>
		public static int GetWorkerRoleID(int roleType)
		{
			switch (roleType)
			{
				case (int)EnumRole.Cook:
					return DataCenter.Instance.cookerModel;

				case (int)EnumRole.Waiter:
					return DataCenter.Instance.waiterModel;
			}

			return 0;
		}

		/// <summary>
		/// 查找网格属性
		/// </summary>
		/// <param name="levelID"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static GameConfigs.LevelGridPropertyRowData GetLevelGridProperty(int levelID, Vector2Int pos)
		{
			var cfg = ConfigSystem.Instance.Find((GameConfigs.LevelGridPropertyRowData cfg) =>
				cfg.Id == levelID
				&& pos.x >= cfg.MinX
				&& pos.x <= cfg.MaxX
				&& pos.y >= cfg.MinY
				&& pos.y <= cfg.MaxY);
			return cfg;
		}

		/// <summary>
		/// 获得区域ID
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static int GetLevelGridAreaID(Vector2Int pos)
		{
			var levelID = DataCenter.Instance.GetUserData().scene;
			var cfg = GetLevelGridProperty(levelID, pos);
			if (cfg.IsValid())
			{
#if !SVR_RELEASE
				log.Debug("area valid area=" + cfg.Area + " pos=" + pos); 
#endif
				return cfg.Area;
			}

			log.Error("area not found " + " pos=" + pos);
			return 0;
		}

		/// <summary>
		/// 获得当前任务进度
		/// </summary>
		/// <param name="value"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static bool GetCurrentTaskProgress(out int value, out int max)
		{
			var CurTaskId = DataCenter.TaskMainUtil.GetCurTaskCfgId();
			var info = DataCenter.TaskMainUtil.CurrentTaskInfo;
			value = 0;
			max = 0;
			if (info.cfg.IsValid())
			{
				var cfg = info.cfg;
				max = info.values[1];
				value = DataCenter.TaskMainUtil.GetTaskProgress(cfg.TaskType, info.values);
				if (cfg.ProgressType == 1)
				{
					value = value >= max ? 1 : 0;
					max = 1;
				}
				return true;
			}

			return false;
		}
		
		/// <summary>
		/// 通过3D坐标获得调试信息坐标
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static Vector2 Cover3DToOverlayPos(Vector3 pos)
		{
			int width = DebugOverlay.Width;
			int height = DebugOverlay.Height;
			//int sreenWidth = Screen.width;
			//int screenHeight = Screen.height;
            
			Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
			screenPos.y = Camera.main.pixelHeight - screenPos.y;
            
			float y = screenPos.y / Camera.main.pixelHeight;
			float x = screenPos.x / Camera.main.pixelWidth;
			Vector2 ret = new Vector2();
			ret.x = x * width;
			ret.y = y * height;
			return ret;
		}

		/// <summary>
		/// 获得好友触发时长
		/// </summary>
		/// <returns></returns>
		public static float GetFriendTriggerTime() => FriendModule.GetFriendTriggerTime();
	}
}