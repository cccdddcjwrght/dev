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

		public static IEnumerator WaitUI(string name)
		{

			if (!string.IsNullOrEmpty(name))
			{
				var mgr = UIModule.Instance.GetEntityManager();
				var e = UIRequest.Create(mgr, GetUI(name));
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

		public static UIParam GetParam(this UIContext ui)
		{
			if (ui.gameWorld.GetEntityManager().HasComponent<UIParam>(ui.entity))
			{
				ui.gameWorld.GetEntityManager().GetComponentObject<UIParam>(ui.entity);
			}
			return default;
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
		/// 创建漂字
		/// </summary>
		/// <param name="mgr"></param>
		/// <param name="title">文字</param>
		/// <param name="pos">3d场景中的位置</param>
		/// <param name="color">颜色</param>
		/// <param name="fontSize">字体大小</param>
		/// <param name="duration">持续时间</param>
		/// <returns></returns>
		public static Entity ShowTipsNew(string uiName,
			string title,
			float3 pos,
			Color color,
			int fontSize, float duration)
		{
			EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
			Entity ui = UIRequest.Create(entityManager, SGame.UIUtils.GetUI(uiName));
			entityManager.AddComponent<Translation>(ui);
			entityManager.AddComponent<HUDTips>(ui);
			entityManager.AddComponent<LiveTime>(ui);

			entityManager.SetComponentData(ui, new Translation { Value = pos });
			entityManager.SetComponentData(ui, new HUDTips { title = title, color = color, fontSize = fontSize });
			entityManager.SetComponentData(ui, new LiveTime { Value = duration });
			return ui;
		}

		public static string Tips(this string tips, string pix = null)
		{
			tips = UIListener.AutoLocal(pix + tips);
			Debug.Log("Tips:" + tips);
			return tips;
		}

		//////////////////// 老代码后续清除 ///////////////////////////////////////
		/// <summary>
		/// 创建漂字
		/// </summary>
		/// <param name="mgr"></param>
		/// <param name="title">文字</param>
		/// <param name="pos">3d场景中的位置</param>
		/// <param name="color">颜色</param>
		/// <param name="fontSize">字体大小</param>
		/// <param name="duration">持续时间</param>
		/// <returns></returns>
		public static Entity ShowTips(EntityManager mgr, string title, float3 pos, Color color, int fontSize, float duration, PositionType posType)
		{
			return FloatTextRequest.CreateEntity(mgr, title, pos, color, 50, 2.0f, posType);
		}

	}
}