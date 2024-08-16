
namespace SGame.UI{
	using log4net;
	using SGame;
	using SGame.UI.Technology;
	
	public partial class UITechnology : IUIScript
	{
		private static ILog log = LogManager.GetLogger("ui." + nameof(UITechnology));

		private UI_TechnologyUI m_view;

		private AbilityData m_AbilityData;

		public void OnInit(UIContext context)
		{
			context.onUninit += OnClose;
			m_view = context.content as UI_TechnologyUI;
			m_AbilityData = DataCenter.Instance.abilityData;
			BeforeInit(context);
			InitUI(context);
			InitEvent(context);
			InitLogic(context);
			AfterInit(context);
		}

		private void OnClose(UIContext context)
		{
			context.onUninit -= OnClose;
			UnInitUI(context);
			UnInitEvent(context);
			UnInitLogic(context);
			AfterUnInit(context);
			m_view = default;
		}

		void BeforeInit(UIContext context)
		{
			
		}
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

