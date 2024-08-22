
namespace SGame.UI{
	using log4net;
	using SGame;
	using SGame.UI.Explore;
	
	public partial class UIFightInfo : IUIScript
	{
		private static ILog log = LogManager.GetLogger("ui." + nameof(UIFightInfo));

		private UI_FightInfoUI m_view;

		public void OnInit(UIContext context)
		{
			context.onUninit += OnClose;
			context.onShown += OnShow;
			context.onHide += OnHide;
			context.beginShown += OnOpen;

			m_view = context.content as UI_FightInfoUI;
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
			context.onHide -= OnHide;
			context.beginShown -= OnOpen;
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

		private void OnHide(UIContext context) => DoHide(context);
		partial void DoHide(UIContext context);

		private void OnOpen(UIContext context) => DoOpen(context);
		partial void DoOpen(UIContext context);

	}
}

