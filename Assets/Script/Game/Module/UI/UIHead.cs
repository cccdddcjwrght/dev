using SGame.UI;
using log4net;
using SGame.UI.Setting;
using Unity.Entities.UniversalDelegates;

namespace SGame
{

	public class UIHead : IUIScript
	{
		private static ILog log = LogManager.GetLogger("xl.gameui");
		private UI_SimpleHeadIcon m_view;

		public void OnInit(UIContext context)
		{
			m_view = context.content as UI_SimpleHeadIcon;
			
			
		}


		

		public static IUIScript Create() { return new UILogin(); }

	}
}
