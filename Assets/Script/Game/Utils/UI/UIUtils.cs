using FairyGUI;
using SGame.UI;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using Unity.VisualScripting;
using System.Xml.Linq;
using GameConfigs;
using Unity.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGame
{
	public static class UIUtils
	{
		public static int GetUI(string name)
		{
			if (ConfigSystem.Instance.TryGet<GameConfigs.ui_resRowData>((GameConfigs.ui_resRowData conf) =>
			{
				return conf.Name == name;
			}, out GameConfigs.ui_resRowData conf))
			{
				return conf.Id;
			}

			// 找不到ID
			return 0;
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		/// <param name="mgr"></param>
		/// <param name="ui">UI 的Entity对象</param>
		public static bool CloseUI(Entity ui)
		{
			if (UIModule.Instance != null)
				return UIModule.Instance.CloseUI(ui);
			return true;
		}

		/// <summary>
		/// 通过UI名字关闭UI
		/// </summary>
		/// <param name="uiName"></param>
		/// <returns></returns>
		public static bool CloseUIByName(string uiName)
		{
			var e = UIModule.Instance.GetUI(uiName);
			if (e == Entity.Null)
				return false;

			return UIModule.Instance.CloseUI(e);
		}

		public static bool CloseUIByID(int id)
		{
			if (ConfigSystem.Instance.TryGet<ui_resRowData>(id, out var cfg))
			{
				var e = UIModule.Instance.GetUI(cfg.Name);
				if (e == Entity.Null)
					return false;

				return UIModule.Instance.CloseUI(e);
			}
			return false;
		}


		/// <summary>
		/// 重新加载可见UI
		/// </summary>
		public static void ReloadVisibleUI()
		{
			List<UI.UIWindow> allUI = UIModule.Instance.GetVisibleUI();
			List<int> uiID = new List<int>();

			// 除了HUD 的UI, 所有UI都重新开一遍
			foreach (var ui in allUI)
			{
				var configID = ui.Value.configID;
				if (ConfigSystem.Instance.TryGet(configID, out GameConfigs.ui_resRowData uiconfig))
				{
					if (HasUIGroup(configID, 11))
					{
						continue;
					}
					if (uiconfig.Type == (int)UIType.UI && !uiID.Contains(configID))
					{
						uiID.Add(configID);
						ui.Value.Close();
					}
				}
			}

			var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			foreach (var id in uiID)
			{
				UIRequest.Create(entityManager, id);
			}
		}

		/// <summary>
		/// 通过世界坐标转换成控件坐标
		/// </summary>
		/// <param name="ui"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public static Vector2 WorldPosToUI(GComponent ui, Vector3 pos)
		{
			Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
			screenPoint.y = Screen.height - screenPoint.y;
			return ui.GlobalToLocal(screenPoint);
		}

		public static Vector2 WorldPosToUI(Vector3 pos)
		{
			Vector3 screenPoint = GameCamera.camera.WorldToScreenPoint(pos);
			screenPoint.y = Screen.height - screenPoint.y;
			Vector2 uipos = GRoot.inst.GlobalToLocal(screenPoint);
			return uipos;
		}




		/// <summary>
		/// 更加坐标类型获得UI位置 
		/// </summary>
		/// <param name="ui"></param>
		/// <param name="pos"></param>
		/// <param name="posType"></param>
		/// <returns></returns>
		public static Vector2 GetUIPosition(GComponent ui, Vector3 pos, PositionType posType)
		{
			Vector2 ret = Vector2.zero;

			switch (posType)
			{
				case PositionType.POS3D:
					ret = UIUtils.WorldPosToUI(ui, pos);
					break;

				case PositionType.POS2D_CENTER:
					{

						Vector2 center = new Vector2(ui.width / 2, ui.height / 2);
						ret = center + new Vector2(pos.x, pos.y);
					}
					break;

				case PositionType.POS2D:
					ret = new Vector2(pos.x, pos.y);
					break;
			}

			return ret;
		}

		//根据ui名称获取到相应的坐标位置
		public static Vector2 GetUIPosition(string uiName, string uiPath, bool isCenter = false)
		{
			Entity e = GetUIEntity(uiName);
			var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<UIWindow>(e);
			var item = ui.Value.contentPane.GetChildByPath(uiPath);
			if (item == null)
			{
				Debug.Log("ui path not found={0}, {1}" + uiName + uiPath);
				return Vector2.zero;
			}

			Vector2 ret = item.LocalToGlobal(Vector2.zero);
			ret = GRoot.inst.GlobalToLocal(ret);
			if (isCenter)
			{
				ret.x += item.width * 0.5f;
				ret.y += item.height * 0.5f;
			}

			return ret;
		}

		public static Entity OpenUI(string name, params object[] args)
		{
			if (!string.IsNullOrEmpty(name))
				return OpenUI(name, args?.Length > 0 ? new UIParam() { Value = args } : default);
			return default;
		}

		public static Entity OpenUI(string name, IComponentData data)
		{
			if (!string.IsNullOrEmpty(name))
			{
				var mgr = UIModule.Instance.GetEntityManager();
				var uid = GetUI(name);
				var e = UIModule.Instance.GetUI(name);
				if (e != default && mgr.Exists(e))
				{
					TriggerUIEvent(e, "OnRefresh", data);
					return e;
				}
				else if (!UIRequestMgr.Check(uid))
				{
					e = UIRequest.Create(mgr, uid);
					if (mgr.Exists(e))
					{
						if (data != default)
							mgr.AddComponentObject(e, data);
						return e;
					}
				}
			}
			return default;
		}

		public static Entity GetUIEntity(string name)
		{

			if (!string.IsNullOrEmpty(name))
				return UIModule.Instance.GetUI(name);
			return default;
		}

		public static bool CheckUIIsOpen(string name)
		{
			if (string.IsNullOrEmpty(name)) return false;
			var mgr = UIModule.Instance.GetEntityManager();
			var e = UIModule.Instance.GetUI(name);
			return e.IsExists() && UIModule.Instance.CheckOpened(e);
		}

		public static IEnumerator WaitUI(string name)
		{

			if (!string.IsNullOrEmpty(name))
			{
				var mgr = UIModule.Instance.GetEntityManager();
				var e = OpenUI(name);
				if (mgr.Exists(e))
					return new WaitUIOpen(mgr, e);
			}
			return default;

		}

		public static IEnumerator WaitUIWithAnimation(string name)
		{
			WaitUIOpen.needWaitAnimation = true;
			WaitUIOpen.needDelay = 10;
			return WaitUI(name);

		}

		public static void CloseUI(IEnumerator ui)
		{
			if (ui != null)
			{
				if (ui.Current is Entity e)
					CloseUI(e);
				else if (ui.Current is string name)
				{
					if (name.StartsWith("-"))
						CloseAllUI(name.Substring(1));//UIModule.Instance.CloseAllUI(name.Substring(1));
					else
						CloseUIByName(name);
				}
			}
		}

		/// <summary>
		/// 判断某个UI是否包含某个组标签
		/// </summary>
		/// <param name="uiID"></param>
		/// <param name="groupTag"></param>
		/// <returns></returns>
		public static bool HasUIGroup(int uiID, int groupTag)
		{
			if (ConfigSystem.Instance.TryGet(uiID, out ui_resRowData config))
			{
				if (config.GroupLength > 0)
				{
					for (int i = 0; i < config.GroupLength; i++)
					{
						if (config.Group(i) == groupTag)
							return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// 关闭UI
		/// </summary>
		/// <param name="ignore">排除UI</param>
		public static void CloseAllUI(params string[] ignore)
		{
			var wins = UIModule.Instance.GetVisibleUI();
			if (wins != null)
			{
				foreach (var w in wins)
				{
					if (HasUIGroup(w.Value.configID, 10))
					{
						continue;
					}
					if (ignore.Length > 0 && ignore.Contains(w.name)) continue;
					CloseUI(w.entity);
				}
			}
		}


		public static void SetParam<T>(this Entity entity, T data)
		{
			if (entity != default)
			{
				var mgr = UIModule.Instance.GetEntityManager();
				var param = new UIParam() { Value = data };
				if (mgr.Exists(entity))
					mgr.AddComponentData(entity, param);
			}
		}

		public static UIParam GetParam(this UIContext ui)
		{
			if (ui.gameWorld.GetEntityManager().HasComponent<UIParam>(ui.entity))
			{
				return ui.gameWorld.GetEntityManager().GetComponentObject<UIParam>(ui.entity);
			}
			return default;
		}

		public static bool IsExists(this Entity entity)
		{
			var mgr = UIModule.Instance.GetEntityManager();
			return entity != Entity.Null && mgr.Exists(entity);
		}

		/// <summary>
		/// 显示跟随物体的HUD
		/// </summary>
		/// <param name="uiname"></param>
		/// <param name="follow"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static Entity ShowHUD(string uiname, Transform follow, float3 offset)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = UIRequest.Create(entityManager, SGame.UIUtils.GetUI(uiname));

			if (!entityManager.HasComponent<HUDFlow>(ui))
			{
				entityManager.AddComponentObject(ui, new HUDFlow() { Value = follow, offset = offset });
			}
			else
			{
				entityManager.SetComponentData(ui, new HUDFlow() { Value = follow, offset = offset });
			}

			return ui;
		}

		/// <summary>
		/// 显示HUD, 并跟随entity
		/// </summary>
		/// <param name="uiname"></param>
		/// <param name="follow"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static Entity ShowHUD(string uiname, Entity follow, float3 offset)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = UIRequest.Create(entityManager, SGame.UIUtils.GetUI(uiname));

			if (!entityManager.HasComponent<HUDFlowE>(ui))
			{
				entityManager.AddComponent<HUDFlowE>(ui);
			}

			entityManager.SetComponentData(ui, new HUDFlowE() { Value = follow, offset = offset });
			return ui;
		}

		/// <summary>
		/// 显示等待HUD
		/// </summary>
		/// <param name="progressTime"></param>
		/// <returns></returns>
		public static Entity ShowWaitUI(float progressTime, Transform pos)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("progress", pos, float3.zero);
			entityManager.AddComponent<TweenTime>(ui);
			entityManager.SetComponentData(ui, new TweenTime() { Value = progressTime });
			return ui;
		}

		/// <summary>
		/// 显示飘字
		/// </summary>
		/// <param name="title"></param>
		/// <param name="pos"></param>
		/// <param name="color"></param>
		/// <param name="fontSize"></param>
		/// <param name="duration"></param>
		/// <param name="speed"></param>
		/// <returns></returns>
		public static Entity ShowTipsNew(
			double gold,
			Transform pos,
			Color color,
			int fontSize,
			float duration,
			int speed)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("floattext", pos, float3.zero);
			entityManager.AddComponent<Translation>(ui);
			entityManager.AddComponent<HUDTips>(ui);
			entityManager.AddComponent<LiveTime>(ui);

			entityManager.SetComponentData(ui, new Translation { Value = pos.position });
			entityManager.SetComponentData(ui, new HUDTips { title = Utils.ConvertNumberStr(gold), color = color, fontSize = fontSize, speed = speed });
			entityManager.SetComponentData(ui, new LiveTime { Value = duration });
			PropertyManager.Instance.GetGroup(PropertyGroup.ITEM).AddNum((int)ItemID.GOLD, gold);
			return ui;
		}

		public static Entity ShowOrderTips(
			int foodType,
			int foodNum,
			Transform pos,
			bool isFriend = false)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("ordertip", pos, float3.zero);
			entityManager.AddComponent<Translation>(ui);
			entityManager.AddComponentData<FoodItem>(ui, new FoodItem { itemID = foodType, num = foodNum, isFriend = isFriend });
			entityManager.SetComponentData(ui, new Translation { Value = pos.position });
			return ui;
		}

		/// <summary>
		/// 显示好友提示UI
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="playerID"></param>
		/// <returns></returns>
		public static Entity ShowFriendTip(Transform pos, int playerID)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("friendtip", pos, float3.zero);
			entityManager.AddComponent<Translation>(ui);
			entityManager.AddComponentData(ui, new UIParam() { Value = playerID });
			entityManager.AddComponentData(ui, new Translation { Value = pos.position });
			return ui;
		}

		public static Entity ShowUpdateTip(Transform pos)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("update", pos, float3.zero);
			return ui;
		}

		public static Entity ShowSleepTip(Transform pos)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("effect", pos, float3.zero);
			return ui;
		}

		public static Entity ShowReputationTip(Transform pos, int characterID)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("reputationtip", pos, float3.zero);
			ReputationModule.Instance.AddLikeNum(characterID);
			return ui;
		}

		public static string Tips(this string tips, string pix = null)
		{
			if (HudModule.Instance != null)
				HudModule.Instance.SystemTips(UIListener.AutoLocal(pix + tips));
			return tips;
		}


		public static string ErrorTips(this string tips, bool local = true)
		{
			return Tips(tips, local ? "@error_" : "error_");
		}

		/// <summary>
		/// 触发一个UI事件
		/// </summary>
		/// <param name="ui"></param>
		/// <param name="eventName">UI事件名称</param>
		/// <param name="param"></param>
		public static void TriggerUIEvent(Entity e, string eventName, object param)
		{
			var EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			if (EntityManager.HasComponent<UIWindow>(e))
			{
				var ui = EntityManager.GetComponentObject<UIWindow>(e);
				if (ui != null && ui.Value != null)
				{
					ui.Value.DispatchEvent(eventName, param);
				}
			}
		}

		public static bool IsChild(GObject gObject, GObject target)
		{
			if (gObject == null || target == null) return false;
			var p = target;
			while (gObject != p)
			{
				p = p.parent;
				if (p == null) return false;
			}
			return true;
		}

		static public GObject AddListItem(GComponent list, Action<int, object, GObject> onAdded = null, object data = null, object res = null, int idx = -1)
		{
			if (list != null)
			{
				GObject item = default;
				var index = idx >= 0 ? idx : list.numChildren;
				var r = res == null ? null : res is Func<object, string> ? (res as Func<object, string>)(data) : res.ToString();
				if (list is GList gList)
				{ index = idx >= 0 ? idx : gList.numItems; item = gList.AddItemFromPool(r); }
				else if (!string.IsNullOrEmpty(r))
				{
					item = UIPackage.CreateObjectFromURL(r);
					if (item != null)
						list.AddChild(item);
				}
				if (item != null && onAdded != null)
					onAdded(index, data, item);
				return item;
			}
			return default;
		}

		static public void AddListItems<T>(GComponent list, IList<T> datas, Action<int, object, GObject> onAdded = null, List<GObject> rets = default, object res = null, bool ignoreNull = false, bool useIdx = false)
		{
			if (datas?.Count > 0 && list != null)
			{
				for (int i = 0; i < datas.Count; i++)
				{
					var d = datas[i];
					if (!ignoreNull || d != null)
					{
						var g = AddListItem(list, onAdded, datas[i], res, idx: useIdx ? i : -1);
						if (rets != null) rets.Add(g);
					}
				}
			}
		}

		public static float GetSafeUIOffset(bool needRootScale = true)
		{
			var offset = Screen.safeArea.y > 0 ? Screen.safeArea.y : Math.Max(0, Screen.height - Screen.safeArea.height - Screen.safeArea.y);
			if (offset > 0 && needRootScale)
				offset /= GRoot.inst.scaleY;
			return offset;
		}

		public static void SetUIListTouchEffect(string uiName, string uiPath, bool state, float value = 0)
		{
			Entity e = GetUIEntity(uiName);
			if (e == Entity.Null)
				return;
			var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<UIWindow>(e);
			var item = ui.Value.contentPane.GetChildByPath(uiPath);
			if (item == null)
				Debug.Log("ui path not found={0}, {1}" + uiName + uiPath);

			if (item is GList)
			{
				GList list = item as GList;
				list.scrollPane.touchEffect = state;
				if (value != 0)
					list.scrollPane.SetPercY(value, false);
			}
		}

		public static Vector2Int GetUISize(string uiName, string uiPath, int width, int height)
		{
			if (width != 0 && height != 0)
				return new Vector2Int(width, height);

			Entity e = GetUIEntity(uiName);
			var ui = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentObject<UIWindow>(e);
			var item = ui.Value.contentPane.GetChildByPath(uiPath);
			if (item == null)
			{
				Debug.Log("ui path not found={0}, {1}" + uiName + uiPath);
				return new Vector2Int(0, 0);
			}
			return new Vector2Int((int)item.width, (int)item.height);
		}


		/// <summary>
		/// 简单弹出框
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="text">文本</param>
		/// <param name="call">点击回调，-1关闭，0：点击第一个按钮，1：点击第二个按钮</param>
		/// <param name="btns">按钮名列表</param>
		/// <param name="other">其他参数，成对出现 k1,v1,k2,v2.....</param>
		public static void Confirm(string title, string text, Action<int> call = null, string[] btns = null, object[] other = null)
		{
			var list = new List<object>() { "title", title, "text", text, "call", call, "btns", btns };
			if (other != null && other.Length > 0) list.AddRange(other);
			OpenUI("confirm", list.ToArray());
		}

	}
}