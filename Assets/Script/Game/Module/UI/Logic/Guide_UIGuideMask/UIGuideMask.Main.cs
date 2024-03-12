﻿
namespace SGame.UI{
	using log4net;
	using SGame;
	using SGame.UI.Guide;
	
	public partial class UIGuideMask : IUIScript
	{
		private static ILog log = LogManager.GetLogger("ui." + nameof(UIGuideMask));

		private UI_GuideMaskUI m_view;

		public void OnInit(UIContext context)
		{
			context.onClose += OnClose;
			m_view = context.content as UI_GuideMaskUI;
			BeforeInit(context);
			InitUI(context);
			InitEvent(context);
			InitLogic(context);
			AfterInit(context);
		}

		private void OnClose(UIContext context)
		{
			context.onClose -= OnClose;
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
	}
}

