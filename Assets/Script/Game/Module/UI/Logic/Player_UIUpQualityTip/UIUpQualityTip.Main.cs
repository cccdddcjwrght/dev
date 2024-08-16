﻿
namespace SGame.UI{
	using log4net;
	using SGame;
	using SGame.UI.Player;
	
	public partial class UIUpQualityTip : IUIScript
	{
		private static ILog log = LogManager.GetLogger("ui." + nameof(UIUpQualityTip));

		private UI_UpQualityTipUI m_view;

		public void OnInit(UIContext context)
		{
			context.onUninit += OnClose;
			context.onShown += OnShow;
			m_view = context.content as UI_UpQualityTipUI;
			BeforeInit(context);
			InitUI(context);
			InitEvent(context);
			InitLogic(context);
			AfterInit(context);
		}

		private void OnClose(UIContext context)
		{
			context.onUninit -= OnClose;
			context.onShown -= OnShow;
			UnInitUI(context);
			UnInitEvent(context);
			UnInitLogic(context);
			AfterUnInit(context);
			m_view = default;
		}

		partial void BeforeInit(UIContext context);
		partial void InitUI(UIContext context);
		partial void InitEvent(UIContext context);
		partial void InitLogic(UIContext context);
		partial void AfterInit(UIContext context);
		partial void UnInitUI(UIContext context);
		partial void UnInitEvent(UIContext context);
		partial void UnInitLogic(UIContext context);
		partial void AfterUnInit(UIContext context);

		private void OnShow(UIContext context) => DoShow(context);
		partial void DoShow(UIContext context);
	}
}

