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
		public void Init(UIContext context, EntityCommandBuffer commandBuffer)
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

				if (ui.Type == (int)UIType.UI)
				{
					// 添加UI对动画 显示与隐藏的支持
					UIAnimationBind.Bind(context);
					context.window.isFullScreen = true;

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
					HUDSetup(context, commandBuffer);
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
			log.Error(context.configID);
			9.ToAudioID().PlayAudio();

		}

		void OnPlayUIHideAudio(UIContext context)
		{
			8.ToAudioID().PlayAudio();
		}

		#endregion

		/// <summary>
		/// HUD ui设置
		/// </summary>
		/// <param name="context"></param>
		/// <param name="commandBuffer"></param>
		void HUDSetup(UIContext context, EntityCommandBuffer commandBuffer)
		{
			var entityManager = context.gameWorld.GetEntityManager();
			var ui = context.entity;
			//entityManager.HasComponent<HUDSync>()
			// 添加同步功能
			commandBuffer.AddComponent<HUDSync>(ui);

			// 设置父节点
			//HudModule.Instance.GetHUD().Value.contentPane.AddChild(context.window);
			//HudModule.Instance.GetHUD().Value.container.AddChild(context.window);

			// 添加3D 信息
			if (!entityManager.HasComponent<LocalToWorld>(ui))
				commandBuffer.AddComponent<LocalToWorld>(ui);
			if (!entityManager.HasComponent<Translation>(ui))
				commandBuffer.AddComponent<Translation>(ui);
			if (!entityManager.HasComponent<Rotation>(ui))
				commandBuffer.AddComponent<Rotation>(ui);
		}

		public void AfterShow(UIContext context, EntityCommandBuffer commandBuffer)
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
						Translation t = new Translation() { Value = (float3)flow.Value.position + flow.offset };
						commandBuffer.SetComponent(e, t);

						var uiwindow = context.window;
						Vector2 pos = SGame.UIUtils.GetUIPosition(uiwindow.parent, t.Value, PositionType.POS3D);
						uiwindow.xy = pos;

						log.Info(string.Format("entity pre init = {0}  pos = {1}", e, pos));
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