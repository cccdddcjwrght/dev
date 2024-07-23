using Unity.Entities;
using log4net;
using SGame.UI;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace SGame
{
	/// <summary>
	/// 用于处理UI的预处理
	/// </summary>
	public class UIPreprocess : IPreprocess
	{
		private static ILog log = LogManager.GetLogger("xl.ui");

		// 初始化UI状态, 包括是否全屏等等
		public void Init(UIContext context)
		{
			if (context.configID != 0)
			{
				if (!ConfigSystem.Instance.TryGet(context.configID, out GameConfigs.ui_resRowData ui))
				{
					log.Error("ui config not found=" + context.configID);
					return;
				}

				// 设置UI显示层级
				context.window.sortingOrder = ui.Order;
				context.window.uiname = ui.Name;

				EventManager.Instance.Trigger((int)GameEvent.GUIDE_UI_SHOW, ui.Name);

#if !EVENT_LOG_OFF
				log.Info("show ui name=" + ui.Name + " uitype=" + ui.Type); 
#endif

				if (ui.Type == (int)UIType.UI)
				{
					// 添加UI对动画 显示与隐藏的支持
					UIAnimationBind.Bind(context);
					context.window.isFullScreen = true;

					context.onShown += OnUIShow;
					context.onHide += OnUIHide;


					if (ui.Mask != 0)
					{
						context.beginShown += OnMaskShow;
						context.onHide += OnMaskHide;
					}

					if (0 == ui.DisableAudio)
					{
						context.beginShown += OnPlayUIShowAudio;
						context.beginHide += OnPlayUIHideAudio;
					}

				}
				else if (ui.Type == (int)UIType.HUD)
				{
					context.window.isFullScreen = false;
					HUDSetup(context);
				}


			}
		}

		void OnMaskShow(UIContext otherUI)
		{
			log.Debug("show mask =" + otherUI.window.uiname);
			EventManager.Instance.Trigger((int)GameEvent.ON_UI_MASK_SHOW, otherUI);
		}

		void OnMaskHide(UIContext otherUI)
		{
			log.Debug("hide mask =" + otherUI.window.uiname);
			EventManager.Instance.Trigger((int)GameEvent.ON_UI_MASK_HIDE, otherUI);
		}

		#region 音效

		void OnPlayUIShowAudio(UIContext context)
		{
			9.ToAudioID().PlayAudio();

		}

		void OnPlayUIHideAudio(UIContext context)
		{
			8.ToAudioID().PlayAudio();
		}

		#endregion

		#region 事件

		void OnUIShow(UIContext context) => EventManager.Instance.Trigger(((int)GameEvent.UI_SHOW), context.window.uiname);
		void OnUIHide(UIContext context) => EventManager.Instance.Trigger(((int)GameEvent.UI_HIDE), context.window.uiname);

		#endregion

		/// <summary>
		/// HUD ui设置
		/// </summary>
		/// <param name="context"></param>
		/// <param name="commandBuffer"></param>
		void HUDSetup(UIContext context)
		{
			var entityManager = context.gameWorld.GetEntityManager();
			var ui = context.entity;
			
			// 添加同步功能
			entityManager.AddComponent<HUDSync>(ui);

			// 添加3D 信息
			if (!entityManager.HasComponent<LocalToWorld>(ui))
				entityManager.AddComponent<LocalToWorld>(ui);
			if (!entityManager.HasComponent<LocalTransform>(ui))
				entityManager.AddComponentData(ui, LocalTransform.Identity);
		}

		public void AfterShow(UIContext context)
		{
			if (context.configID != 0)
			{
				if (!ConfigSystem.Instance.TryGet(context.configID, out GameConfigs.ui_resRowData ui))
				{
					log.Error("ui config not found=" + context.configID);
					return;
				}

				if (ui.Type == (int)UIType.HUD)
				{
					// 初始化时更新UI位置
					var entityManager = context.gameWorld.GetEntityManager();
					var e = context.entity;
					if (entityManager.HasComponent<HUDFlow>(e))
					{
						var flow = entityManager.GetComponentObject<HUDFlow>(e);
						LocalTransform trans = entityManager.GetComponentData<LocalTransform>(e);
						trans.Position = (float3)flow.Value.position + flow.offset;
						entityManager.SetComponentData(e, trans);

						var uiwindow = context.window;
						Vector2 pos = SGame.UIUtils.GetUIPosition(uiwindow.parent, trans.Position, PositionType.POS3D);
						uiwindow.xy = pos;
					}
				}
			}
		}

		private void SetupHUD()
		{

		}

		public bool GetUIInfo(int configId, out string comName, out string pkgName)
		{
			comName = null;
			pkgName = null;
			if (ConfigSystem.Instance.TryGet(configId, out GameConfigs.ui_resRowData ui))
			{
				comName = ui.ComName;
				pkgName = ui.PackageName;
				return true;
			}

			return false;
		}
	}
}