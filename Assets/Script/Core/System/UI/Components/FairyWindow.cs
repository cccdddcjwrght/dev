using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;
using log4net;
using FairyGUI;

namespace SGame.UI
{
	public class FairyWindow : FairyGUI.Window, IBaseWindow
	{
		private const string SHOW_ANIM_NAME = "doshow";
		private const string HIDE_ANIM_NAME = "dohide";

		public Entity uiEntity;
		
		public PoolID m_poolID = PoolID.NULL;

		public PoolID poolID => m_poolID;

		public UI_TYPE type => UI_TYPE.WINDOW;

		public void SetPoolID(PoolID id)
		{
			m_poolID = id;
		}

		public GComponent content => this.contentPane;

		public string uiname { get; set; }

		// UI 是否已经初始化完毕
		public bool isReadly { get { return _isReadly; } }
		
		private bool _isReadly = false;

		// 是否延迟关闭
		private bool m_isDelayClose = false;

		// 是否已经调用过ONSHOWN
		private bool m_isReadyShowed = false;

		// 是否正在隐藏
		private bool m_isHiding = false;

		private bool m_isClosed = false;

		public bool isReadyShowed { get { return m_isReadyShowed && isShowing; } }

		public bool isHiding { get { return m_isHiding; } }

		public bool isClosed { get { return m_isClosed || isDisposed; } } // 已经销毁的也是DISPOSED

		// 显示版本
		public int screenSizeVer = 0;

		public FairyGUI.FitScreen fitScreen;

		// 显示结束事件
		public Action<FairyWindow> onShownFinish;

		public bool needCache = false;

		private IUIScript m_uiScript;
		private UIContext m_context;

		public int configID { get => m_context.configID; }

		public UIContext context => m_context;

		/// <summary>
		/// 判断是否正在关闭
		/// </summary>
		public bool isClosing => m_isClosed || m_isDelayClose;


		private static ILog log = LogManager.GetLogger("UI");

		public bool isFullScreen { get; set; }

		public void OnFrameUpdate(double deltaTime)
		{
			if (screenSizeVer != StageCamera.screenSizeVer)
				HandleScreenSizeChanged();

			m_context.onUpdate?.Invoke(m_context);
		}

		// 调用函数结束通知
		public void FinishAnimation(bool isShow)
		{
			if (isShow)
			{
				OnShown();
			}
			else
			{
				HideImmediately();
			}
		}

		/// <summary>
		/// 显示UI时调用动画
		/// </summary>
		protected override void DoShowAnimation()
		{
			m_context.beginShown?.Invoke(m_context);
			if (m_context.doShowAnimation != null)
				m_context.doShowAnimation(m_context);
			else
				base.DoShowAnimation();
		}

		/// <summary>
		/// 隐藏UI时调用动画
		/// </summary>
		protected override void DoHideAnimation()
		{
			m_isHiding = true;
			m_context.beginHide?.Invoke(m_context);
			if (m_context.doHideAnimation != null)
				m_context.doHideAnimation(m_context);
			else
				base.DoHideAnimation();
		}


		//protected override void Do

		protected override void OnInit()
		{
			if (isDisposed)
				return;

			base.OnInit();

			m_isReadyShowed = false;
			m_isDelayClose = false;
			_isReadly = true;
			m_isHiding = false;
			container.renderMode = RenderMode.ScreenSpaceOverlay;


			if (isFullScreen)
			{
				fitScreen = FitScreen.FitSize;
				MakeFullScreen();
			}
			else
			{
				fitScreen = FitScreen.None;
				x = (GRoot.inst.width - width) / 2;
				y = (GRoot.inst.height - height) / 2;
			}
			
			if (m_uiScript != null)
				m_uiScript.OnInit(m_context);

		}

		override protected void OnShown()
		{
			name = uiname;
			contentPane.name = "contentPane";
			//log.Debug("UI OnShow=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
			m_isReadyShowed = true;
			m_isDelayClose = false;

			base.OnShown();

			m_context.onShown?.Invoke(m_context);
		}

		protected override void OnHide()
		{
			//log.Debug("UI OnHide=" + this.uiname + " isDelayClose=" + m_isDelayClose.ToString());
			m_isReadyShowed = false;
			m_isHiding = false;
			base.OnHide();
			m_context.onHide?.Invoke(m_context);

			if (m_isDelayClose == true)
			{
				m_isDelayClose = false;
				CloseImmediately();
			}
		}

		public void Reopen()
		{
			// 清空之前的动画
			log.Debug("Reopen=" + uiname);
			CleanTween();
			if (isShowing == true)
				HideImmediately();
			this.visible = true;
			this.contentPane.visible = true;
			m_isClosed = false;
			m_isReadyShowed = false;
			m_isDelayClose = false;
			_isReadly = true;
			m_isHiding = false;
			context.onOpen?.Invoke(context);
			this.Show();
			//OnShown();
		}

		void CleanTween()
		{
			foreach (var trans in contentPane.Transitions)
			{
				trans.Stop(true, false);
			}
			GTween.Kill(contentPane, false);
		}

		public bool Initalize(IUIScript scirpt, UIContext context)
		{
			m_uiScript = scirpt;
			m_context = context;
			return true;
		}

		public void Close(bool nocache = false)
		{
			CleanTween();
			if (nocache || !needCache)
				m_isDelayClose = true;
			if (isShowing)
				Hide();
			else
				CloseImmediately();
		}

		public void CloseImmediately()
		{
			m_isClosed = true;
			m_context.onClose?.Invoke(m_context);
		}

		public void OnClose()
		{

		}

		public void HandleScreenSizeChanged()
		{
			screenSizeVer = StageCamera.screenSizeVer;
			float width = GRoot.inst.width;
			float height = GRoot.inst.height;

			var ui = contentPane;
			if (ui != null)
			{
				switch (fitScreen)
				{
					case FitScreen.FitSize:
						MakeFullScreen();
						SetXY(0, 0, true);
						break;

					case FitScreen.FitWidthAndSetMiddle:
						SetSize(width, ui.sourceHeight);
						SetXY(0, (int)((height - ui.sourceHeight) / 2), true);
						break;

					case FitScreen.FitHeightAndSetCenter:
						SetSize(ui.sourceWidth, height);
						SetXY((int)((width - ui.sourceWidth) / 2), 0, true);
						break;
				}

			}
		}
	}
}