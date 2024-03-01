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
			return UIModule.Instance.CloseUI(ui);
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
				var e = UIRequest.Create(mgr, GetUI(name));
				if (mgr.Exists(e))
				{
					if (data != default)
						mgr.AddComponentObject(e, data);
					return e;
				}
			}
			return default;
		}

		public static Entity GetUIEntity(string name) {

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

		public static void CloseUI(IEnumerator ui)
		{
			if (ui != null)
			{
				if (ui.Current is Entity e)
					CloseUI(e);
				else if (ui.Current is string name)
				{
					if (name.StartsWith("-"))
						UIModule.Instance.CloseAllUI(name.Substring(1));
					else
						CloseUIByName(name);
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
			return entity != default && mgr.Exists(entity);
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
			Transform pos)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("ordertip", pos, float3.zero);
			entityManager.AddComponent<Translation>(ui);
			entityManager.AddComponent<FoodType>(ui);
			entityManager.SetComponentData(ui, new Translation { Value = pos.position });
			entityManager.SetComponentData(ui, new FoodType { Value = foodType, num = foodNum });

			return ui;
		}

		public static Entity ShowUpdateTip(Transform pos)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = ShowHUD("update", pos, float3.zero);
			return ui;
		}

		public static string Tips(this string tips, string pix = null)
		{
			tips = UIListener.AutoLocal(pix + tips);
			Debug.Log("Tips:" + tips);
			return tips;
		}

		public static string ErrorTips(this string tips)
		{
			return Tips(tips, "error_");
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

		static public GObject AddListItem(GComponent list, Action<int , object, GObject> onAdded = null, object data = null, string res = null)
		{
			if (list != null)
			{
				GObject item = default;
				var index = list.numChildren;
				if (list is GList gList)
				{ index = gList.numItems; item = gList.AddItemFromPool(res); }
				else if (!string.IsNullOrEmpty(res))
				{
					item = UIPackage.CreateObjectFromURL(res);
					if (item != null)
						list.AddChild(item);
				}
				if (item != null && onAdded != null)
					onAdded(index , data, item);
				return item;
			}
			return default;
		}

		static public void AddListItems<T>(GComponent list, IList<T> datas, Action<int , object, GObject> onAdded = null, List<GObject> rets = default, string res = null)
		{
			if (datas?.Count > 0 && list != null)
			{
				for (int i = 0; i < datas.Count; i++)
				{
					var g = AddListItem(list, onAdded, datas[i], res);
					if (rets != null) rets.Add(g);
				}
			}
		}

	}
}